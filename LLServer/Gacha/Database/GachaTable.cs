using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LLServer.Gacha.Database;

public class GachaTable
{
    [Key] public string id { get; set; }
    
    [NotMapped] public string newName { get; set; }
    
    public bool isValid { get; set; }
    
    public int[] characterIds { get; set; }
    
    public int[] cardGroupIds { get; set; }

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

    public int maxRarity { get; set; }
    
    public string[] cardIds { get; set; }

    public GachaResult[] GetGachaResult(int gachaCardCount)
    {
        GachaResult[] gachaResults = new GachaResult[gachaCardCount];
        
        foreach (GachaResult gachaResult in gachaResults)
        {
            //TODO: implement gacha logic
        }

        return gachaResults;
    }

    public string GachaTableMetaData
    {
        get => JsonSerializer.Serialize(metaData);
        set
        {
            if (value == null || value == "")
            {
                metaData = new GachaTableMetaData();
                return;
            }

            metaData = JsonSerializer.Deserialize<GachaTableMetaData>(value);
        }
    }

    [NotMapped] public GachaTableMetaData metaData { get; set; } = new();
}

public class GachaTableMetaData
{
    public List<string> pamphletIds { get; set; } = new(); //pamhlet ids that were used to access this gacha
    public List<int> count { get; set; } = new(); //how many times it was accessed per pamphlet
    public List<int> characterIds { get; set; } = new(); //which characters were used to access this gacha
}