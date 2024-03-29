﻿namespace LLServer.Models.UserData;

public class ProfileCard
{
    private Int128 id;

    public ProfileCard(ProfileCard profileCard)
    {
        id = profileCard.id;
    }
    
    public ProfileCard(Int128 id)
    {
        this.id = id;
    }

    public override string ToString()
    {
        //return as 32 character hex string
        return id.ToString("X32");
    }
}