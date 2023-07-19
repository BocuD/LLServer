﻿using System.Text.Json;
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
    public List<MusicData> Musics => new();

    //wrapper to get livedata from database
    public List<LiveData> Lives
    {
        get
        {
            //get persistent live data from user
            List<LiveData> liveDatas = new();
            LiveDataMapper mapper = new();
            
            liveDatas.AddRange(User.LiveDatas.Select(x => mapper.FromPersistentLiveData(x)));
            return liveDatas;
        }
    }
    
    public List<PersistentLiveData> PersistentLives => User.LiveDatas;

    public List<StageData> Stages => new();
    public string Flags
    {
        get => User.Flags;
        set => User.Flags = value;
    }

    public void Initialize(InitializeUserData initializeCommand)
    {
        if (initializeCommand.UserData == null)
        {
            return;
        }
        
        //copy all properties
        ReflectionMapper.Map(initializeCommand.UserData, UserData);
        
        if (initializeCommand.UserDataAqours != null)
            ReflectionMapper.Map(initializeCommand.UserDataAqours, UserDataAqours);
        
        if (initializeCommand.UserDataSaintSnow != null)
            ReflectionMapper.Map(initializeCommand.UserDataSaintSnow, UserDataSaintSnow);

        //assign first member cards
        int newMemberCharacterId = initializeCommand.UserData.IdolKind switch
        {
            0 => initializeCommand.UserData.CharacterId!.Value,
            1 => initializeCommand.UserDataAqours!.CharacterId!.Value,
            2 => initializeCommand.UserDataSaintSnow!.CharacterId!.Value,
            _ => 1
        };

        //add first member card
        MemberCards.Add(new MemberCardData
        {
            CardMemberId = MemberCardData.InitialMemberCards[newMemberCharacterId],
            Count = 1,
            New = true,
        });
        
        //initialize other data
        UserData.Level = 1;
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
    }
}