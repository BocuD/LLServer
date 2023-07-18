using System.Text.Json;
using LLServer.Database.Models;
using LLServer.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Models.UserData;

public class PersistentUserDataContainer
{
    private DbContext Context { get; }
    private User User { get; }
    
    public PersistentUserDataContainer(DbContext context, User user)
    {
        Context = context;
        User = user;
    }
    
    public UserData UserData
    {
        get => User.UserData;
        set
        {
            User.UserData = value;
        }
    }
    
    public UserDataAqours UserDataAqours
    {
        get => User.UserDataAqours;
        set
        {
            User.UserDataAqours = value;
        }
    }
    
    public UserDataSaintSnow UserDataSaintSnow
    {
        get => User.UserDataSaintSnow;
        set
        {
            User.UserDataSaintSnow = value;
        }
    }
    
    public List<MemberData> Members
    {
        get => User.Members;
        set
        {
            User.Members = value;
        }
    }

    public void Initialize(InitializeUserData initializeCommand)
    {
        //copy all properties
        if (initializeCommand.UserData != null) UserData = new ReflectionMapper<UserData, UserData>().Map(initializeCommand.UserData, UserData);
        if (initializeCommand.UserDataAqours != null)
            UserDataAqours =
                new ReflectionMapper<UserDataAqours, UserDataAqours>().Map(initializeCommand.UserDataAqours, UserDataAqours);
        
        if (initializeCommand.UserDataSaintSnow != null)
            UserDataSaintSnow =
                new ReflectionMapper<UserDataSaintSnow, UserDataSaintSnow>().Map(initializeCommand.UserDataSaintSnow,
                    UserDataSaintSnow);

        //initialize this manually for now
        UserData.PlayLs4 = 1;
        
        Console.WriteLine($"Updated user data {JsonSerializer.Serialize(this)}");
    }

    public void SetUserData(SetUserData input)
    {
        if (input.UserData != null) UserData = new ReflectionMapper<UserData, UserData>().Map(input.UserData, UserData);
        if (input.UserDataAqours != null)
            UserDataAqours =
                new ReflectionMapper<UserDataAqours, UserDataAqours>().Map(input.UserDataAqours, UserDataAqours);
        
        if (input.UserDataSaintSnow != null)
            UserDataSaintSnow =
                new ReflectionMapper<UserDataSaintSnow, UserDataSaintSnow>().Map(input.UserDataSaintSnow,
                    UserDataSaintSnow);

        //initialize this manually for now
        UserData.PlayLs4 = 1;

        if (input.EquipSkills != null)
        {
            foreach (EquipSkill equipSkill in input.EquipSkills)
            {
                //find a matching character id in members, if it doesn't exist add a new member entry
                MemberData? member = Members.FirstOrDefault(m => m.CharacterId == equipSkill.CharacterId);
                if (member == null)
                {
                    member = new MemberData();
                    Members = Members.Append(member).ToList();
                }

                member.Camera = equipSkill.Camera;
                member.CharacterId = equipSkill.CharacterId;
                member.CardMemberId = equipSkill.CardMemberId;
                member.CardMemorialId = equipSkill.CardMemorialId;
                member.Main = equipSkill.Main;
                member.Stage = equipSkill.Stage;
            }
        }

        if (input.MemberYells != null)
        {
            foreach (MemberYell memberYell in input.MemberYells)
            {
                //find a matching character id in members, if it doesn't exist add a new member entry
                MemberData? member = Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
                if (member == null)
                {
                    member = new MemberData();
                    Members = Members.Append(member).ToList();
                }

                member.YellPoint = memberYell.YellPoint;
                member.AchieveRank = memberYell.YellRank;
            }
        }
        
        Console.WriteLine($"Updated user data {JsonSerializer.Serialize(this)}");
    }
}