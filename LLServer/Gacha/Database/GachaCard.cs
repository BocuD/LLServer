using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LLServer.Gacha.Database;

public enum CardType
{
    Member = 1,
    Skill = 2,
    Memorial = 6
}

public class GachaCard
{
    [Key] public string id { get; set; } //Id without the leading character identifier or rarity identifier, so 10011 for Honoka HR would become 001
    
    public string name { get; set; }
    public int[] characterIds { get; set; }
    
    [NotMapped] public bool[] characterIdBools
    {
        get
        {
            if (characterIds == null)
            {
                return new bool[23];
            }
            
            bool[] temp = new bool[23];
            
            //set the bools for each character id
            foreach (int characterId in characterIds)
            {
                temp[characterId] = true;
            }

            return temp;
        }
        set
        {
            //extract int[] for each value that is set
            List<int> temp = new();
            
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i])
                {
                    temp.Add(i);
                }
            }
            
            characterIds = temp.ToArray();
        }
    }
    
    public int[] rarityIds { get; set; }
    public CardType cardType { get; set; }
    
    //input: 10011 or 121281
    //output: trim the trailing 1 or 0, trim the leading 1 or 2 characters depending on length so that the result is 001 or 128
    public static string GetTrimmedCardID(string cardId, CardType cardType)
    {
        string output = cardId;
        
        if (cardType is CardType.Member)
        {
            //first get rid of last character
            output = cardId.Substring(0, cardId.Length - 1);
        }

        //u's are 0, aqours are 1, saint snow are 2
        int idolKind = 0;

        int characterId = GetCharacterID(cardId, cardType);
        switch (characterId)
        {
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
                idolKind = 1;
                break;
            
            case 21:
            case 22:
                idolKind = 2;
                break;
        }

        //remove character id from the beginning
        output = output.Substring(characterId.ToString().Length);

        return idolKind + output;
    }

    public int GetGameCardID(int characterId, int rarityId = 0)
    {
        string output;
        
        switch (cardType)
        {
            case CardType.Member:
                output = $"{characterId}{id.Substring(1)}{rarityId}";
                break;
            
            case CardType.Skill:
            case CardType.Memorial:
            default:
                output = $"{characterId}{id.Substring(2)}";
                break;
        }
        
        return int.Parse(output);
    }
    
    public static int GetCharacterID(string cardId, CardType cardType)
    {
        switch (cardType)
        {
            case CardType.Skill when cardId.Length == 4:
            case CardType.Memorial when cardId.Length == 4:
                return int.Parse(cardId.Substring(0, 1));
            
            case CardType.Skill:
            case CardType.Memorial:
                return int.Parse(cardId.Substring(0, 2));
            
            //if length is 5, then the character id is the first character
            case CardType.Member when cardId.Length == 5:
                return int.Parse(cardId.Substring(0, 1));
            
            //if length is 6, then the character id is the first 2 characters
            case CardType.Member:
            default:
                return int.Parse(cardId.Substring(0, 2));
        }
    }

    public string GetCardImages()
    {
        string cardImages = "";

        bool singleLine = false;
        if (characterIds.Length < 4) singleLine = true;

        void DrawCardImages(int rarityId = 0)
        {
            if (!singleLine) cardImages += "<div class=\"image-row\">";

            foreach (int characterId in characterIds)
            {
                switch (cardType)
                {
                    case CardType.Member:
                        cardImages +=
                            $"<img src=\"/card/member/{characterId}{id.TrimStart('s', 'm').Substring(1)}{rarityId}.png\" alt=\"{name}\">";
                        break;

                    case CardType.Skill:
                        cardImages +=
                            $"<img src=\"/card/skill/{characterId}{id.TrimStart('s', 'm').Substring(1)}.png\" alt=\"{name}\">";
                        break;

                    case CardType.Memorial:
                        cardImages +=
                            $"<img src=\"/card/memorial/{characterId}{id.TrimStart('s', 'm').Substring(1)}.png\" alt=\"{name}\">";
                        break;
                }
            }
            
            if (!singleLine)
            {
                cardImages += "</div>";
                cardImages += "<br>";
            }
        }

        if (cardType == CardType.Member)
        {
            if (singleLine) cardImages += "<div class=\"image-row\">";
            
            foreach (int rarityId in rarityIds)
            {
                DrawCardImages(rarityId);
            }

            if (singleLine)
            {
                cardImages += "</div>";
                cardImages += "<br>";
            }
        }
        else DrawCardImages();

        return cardImages;
    }

    //get an image of the card using the first available character id
    public string GetCardImage()
    {
        int firstCharacterId = characterIds[0];
        
        switch (cardType)
        {
            default:
            case CardType.Member:
                return $"/card/member/{firstCharacterId}{id.TrimStart('s', 'm').Substring(1)}{1}.png";
            
            case CardType.Skill:
                return $"/card/skill/{firstCharacterId}{id.TrimStart('s', 'm').Substring(1)}.png";
            
            case CardType.Memorial:
                return $"/card/memorial/{firstCharacterId}{id.TrimStart('s', 'm').Substring(1)}.png";
        }
    }
}