using System.Text.Json;
using LLServer.Gacha.Database;
using LLServer.Models.Requests.Travel;
using LLServer.Models.UserData;

namespace LLServer.Gacha;

public class GachaDataProvider
{
    private readonly ILogger<GachaDataProvider> logger;
    private readonly GachaDbContext gachaDbContext;
    
    public GachaDataProvider(ILogger<GachaDataProvider> logger, GachaDbContext gachaDbContext)
    {
        this.logger = logger;
        this.gachaDbContext = gachaDbContext;
    }

    public GachaResult[] GetGachaResult(string gachaGachaId, int gachaCardCount)
    {
        //try to get gacha table from database
        GachaTable? gachaTable = gachaDbContext.GachaTables.Find(gachaGachaId);

        if (gachaTable == null)
        {
            gachaDbContext.Add(new GachaTable
            {
                id = gachaGachaId,
                cardGroupIds = Array.Empty<int>()
            });
            
            gachaDbContext.SaveChanges();

            return Array.Empty<GachaResult>();
        }

        if (!gachaTable.isValid)
        {
            return Array.Empty<GachaResult>();
        }
        
        return GetGachaResult(gachaTable, gachaCardCount);
    }
    
    public GachaResult[] GetGachaResult(GachaTable gachaTable, int gachaCardCount)
    {
        //load cards and card groups from database
        List<GachaCard> cards = new();

        void AddCard(GachaCard card)
        {
            //add the card if at least one of the gacha table character ids is in the card character ids
            if (card.characterIds.Any(c => gachaTable.characterIds.Contains(c)))
            {
                cards.Add(card);
            }
        }
        
        //add unique cards
        foreach (string cardId in gachaTable.cardIds)
        {
            GachaCard? card = gachaDbContext.GachaCards.Find(cardId);

            if (card == null)
            {
                logger.LogWarning("Failed to find card with id {CardId}", cardId);
                continue;
            }

            AddCard(card);
        }
        
        //add cards from card groups
        foreach (int cardGroupId in gachaTable.cardGroupIds)
        {
            GachaCardGroup? cardGroup = gachaDbContext.GachaCardGroups.Find(cardGroupId);

            if (cardGroup == null)
            {
                logger.LogWarning("Failed to find card group with id {CardGroupId}", cardGroupId);
                continue;
            }

            foreach (string cardId in cardGroup.cardIds)
            {
                GachaCard? card = gachaDbContext.GachaCards.Find(cardId);

                if (card == null)
                {
                    logger.LogWarning("Failed to find card with id {CardId}", cardId);
                    continue;
                }

                AddCard(card);
            }
        }
        
        List<GachaResult> gachaResults = new();

        for (int index = 0; index < gachaCardCount; index++)
        {
            GachaResult result = new();
            
            //get random card
            GachaCard card = cards[new Random().Next(cards.Count)];

            //get random character id that is in the gacha table and the card
            List<int> matchedIDs = new();
            foreach (int characterId in gachaTable.characterIds)
            {
                if (card.characterIds.Contains(characterId))
                {
                    matchedIDs.Add(characterId);
                }
            }

            //get a random character id from the matched ids
            int finalCharacterID = matchedIDs[new Random().Next(matchedIDs.Count)];

            if (card.cardType == CardType.Member)
            {
                int rarity = 0;
                if (gachaTable.id.Contains("rare")) rarity = 1;
                
                //get random rarity
                //int rarity = card.rarityIds[new Random().Next(card.rarityIds.Length)];
                result.itemId = card.GetGameCardID(finalCharacterID, rarity);
            }
            else
            {
                result.itemId = card.GetGameCardID(finalCharacterID);
            }

            result.category = (MailboxItemCategory)(int)card.cardType;
            
            gachaResults.Add(result);
        }

        return gachaResults.ToArray();
    }

    public void ScanCards()
    {
        string[] memberFiles = Directory.GetFiles("wwwroot/card/member", "*.png");
        ScanFolder(memberFiles, CardType.Member);
        
        string[] skillFiles = Directory.GetFiles("wwwroot/card/skill", "*.png");
        ScanFolder(skillFiles, CardType.Skill);
        
        string[] memorialFiles = Directory.GetFiles("wwwroot/card/memorial", "*.png");
        ScanFolder(memorialFiles, CardType.Memorial);

        gachaDbContext.SaveChanges();
    }

    private void ScanFolder(string[] cardFiles, CardType cardType)
    {
        string prefix = cardType switch
        {
            CardType.Skill => "s",
            CardType.Memorial => "m",
            _ => ""
        };

        foreach (string cardFile in cardFiles)
        {
            //get card id from filename
            string cardId = Path.GetFileNameWithoutExtension(cardFile);

            //try to get card from database using search pattern on card id
            string cardIdSearchPattern = prefix + GachaCard.GetTrimmedCardID(cardId, cardType);

            GachaCard? gachaCard = gachaDbContext.GachaCards.Find(cardIdSearchPattern);

            if (gachaCard == null)
            {
                logger.LogInformation("Found new card: {CardId}", cardIdSearchPattern);

                int[] rarity = Array.Empty<int>();
                if (cardType == CardType.Member)
                {
                    rarity = new[]
                    {
                        int.Parse(cardId.Substring(cardId.Length - 1))
                    };
                }

                GachaCard newCard = new()
                {
                    characterIds = new[]
                    {
                        GachaCard.GetCharacterID(cardId, cardType)
                    },
                    id = cardIdSearchPattern,
                    rarityIds = rarity,
                    name = cardIdSearchPattern,
                    cardType = cardType
                };
                gachaDbContext.Add(newCard);
            }
            else
            {
                //add character id to character ids
                int characterId = GachaCard.GetCharacterID(cardId, cardType);

                if (!gachaCard.characterIds.Contains(characterId))
                {
                    int[] newCharacterIds = gachaCard.characterIds.Append(characterId).ToArray();
                    gachaCard.characterIds = newCharacterIds;
                }

                if (cardType != CardType.Member) continue;
                
                //add rarity id to rarity ids
                int rarity = int.Parse(cardId.Substring(cardId.Length - 1));
                if (!gachaCard.rarityIds.Contains(rarity))
                {
                    int[] newRarityIds = gachaCard.rarityIds.Append(rarity).ToArray();
                    gachaCard.rarityIds = newRarityIds;
                }
            }
        }
    }
    
    public async Task ParseLogFile(string log)
    {
        //parse log
        string[] lines = log.Split('\n');

        //find any instances of "lot_gachas":[{"card_count":1,"gacha_id":"gta_travel_ll_normal","location":900,"order":1}]
        var gachaLines = lines.Where(l => l.Contains("lot_gachas"));

        var travelResults = gachaLines.Select(l =>
        {
            string start = l.Substring(l.IndexOf("\"param\":"));

            //trim everything before the first [
            start = start.Substring(start.IndexOf('{'));

            //trim everything after "protocol":"TravelResult"
            string end = start.Substring(0, start.IndexOf("\"protocol\":\"TravelResult\"") - 1);
            
            return end;
        }).ToArray();

        List<TravelResultParam> travelResultObjects = new();

        //deserialize the json arrays
        foreach (string travelResult in travelResults)
        {
            TravelResultParam? t = JsonSerializer.Deserialize<TravelResultParam>(travelResult);

            if (t == null)
            {
                logger.LogWarning("Failed to deserialize travel result");
                continue;
            }

            travelResultObjects.Add(t);
        }

        logger.LogInformation("Found {GachaCount} travel result entries in uploaded log file", travelResultObjects.Count);

        foreach (TravelResultParam entry in travelResultObjects)
        {
            foreach (LotGacha lotGacha in entry.LotGachas)
            {
                //try to get gacha table from database
                GachaTable? gachaTable = gachaDbContext.GachaTables.Find(lotGacha.GachaId);

                if (gachaTable == null)
                {
                    gachaDbContext.Add(new GachaTable
                    {
                        id = lotGacha.GachaId
                    });
                    
                    gachaTable = gachaDbContext.GachaTables.Find(lotGacha.GachaId);
                    
                    await gachaDbContext.SaveChangesAsync();
                }

                GachaTableMetaData data = gachaTable.metaData;

                //try to find the index of the pamphlet id
                int index = data.pamphletIds.IndexOf(entry.UserTravel.TravelPamphletId.ToString());
                if (index == -1)
                {
                    //add new entry
                    data.count.Add(1);
                    data.pamphletIds.Add(entry.UserTravel.TravelPamphletId.ToString());
                }
                else
                {
                    //increment existing entry
                    data.count[index]++;
                }

                if (!data.characterIds.Contains(entry.UserTravel.CharacterId))
                {
                    data.characterIds.Add(entry.UserTravel.CharacterId);
                }

                gachaTable.metaData = data;
                
                await gachaDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task RescanLogs()
    {
        //clear existing table metadata
        GachaTable[] gachaTables = gachaDbContext.GachaTables.ToArray();
        foreach (GachaTable table in gachaTables)
        {
            table.metaData.count = new List<int>();
            table.metaData.pamphletIds = new List<string>();
        }
        
        await gachaDbContext.SaveChangesAsync();

        string[] logFiles = Directory.GetFiles("uploadedlogs", "*.txt");
        
        foreach (string logFile in logFiles)
        {
            string log = File.ReadAllText(logFile);
            await ParseLogFile(log);
        }
    }
}
