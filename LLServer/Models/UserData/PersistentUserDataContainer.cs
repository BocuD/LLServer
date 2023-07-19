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
    
    public UserData UserData => User.UserData;
    public UserDataAqours UserDataAqours => User.UserDataAqours;
    public UserDataSaintSnow UserDataSaintSnow => User.UserDataSaintSnow;
    public List<MemberData> Members => User.Members;
    public List<MemberCardData> MemberCards => User.MemberCards;
    public List<MusicData> Musics => MusicData.GetBaseMusicData();
    public List<LiveData> Lives => LiveData.GetBaseLiveData();
    public List<StageData> Stages => StageData.GetBaseStageData();

    public void Initialize(InitializeUserData initializeCommand)
    {
        //copy all properties
        if (initializeCommand.UserData != null) 
            ReflectionMapper.Map(initializeCommand.UserData, UserData);
        
        if (initializeCommand.UserDataAqours != null)
            ReflectionMapper.Map(initializeCommand.UserDataAqours, UserDataAqours);
        
        if (initializeCommand.UserDataSaintSnow != null)
            ReflectionMapper.Map(initializeCommand.UserDataSaintSnow, UserDataSaintSnow);

        int newMemberCharacterId = 1;
        
        //assign first member cards
        switch (initializeCommand.UserData.IdolKind)
        {
            case 0:
                newMemberCharacterId = initializeCommand.UserData.CharacterId;
                break;
            
            case 1:
                newMemberCharacterId = initializeCommand.UserDataAqours.CharacterId;
                break;
            
            case 2:
                newMemberCharacterId = initializeCommand.UserDataSaintSnow.CharacterId;
                break;
        }

        //add first member card
        MemberCards.Add(new MemberCardData
        {
            CardMemberId = MemberCardData.InitialMemberCards[newMemberCharacterId],
            Count = 1,
            New = true,
        });
        
        //initialize other data
        UserData.Level = 1;

        Console.WriteLine($"Updated user data {JsonSerializer.Serialize(this)}");
    }

    public void SetUserData(SetUserData input)
    {
        if (input.UserData != null) ReflectionMapper.Map(input.UserData, UserData);
        if (input.UserDataAqours != null) ReflectionMapper.Map(input.UserDataAqours, UserDataAqours);
        if (input.UserDataSaintSnow != null) ReflectionMapper.Map(input.UserDataSaintSnow, UserDataSaintSnow);

        if (input.EquipSkills != null)
        {
            foreach (EquipSkill equipSkill in input.EquipSkills)
            {
                //find a matching character id in members, if it doesn't exist add a new member entry
                MemberData? member = Members.FirstOrDefault(m => m.CharacterId == equipSkill.CharacterId);
                if (member == null)
                {
                    Members.Add(new MemberData());
                    member = Members.Last();
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
                    Members.Add(new MemberData());
                    member = Members.Last();
                }

                member.YellPoint = memberYell.YellPoint;
                member.AchieveRank = memberYell.YellRank;
            }
        }
        
        Console.WriteLine($"Updated user data {JsonSerializer.Serialize(this)}");
    }
}