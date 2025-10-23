﻿namespace Shin_Megami_Tensei;

public class PlayerInfo
{
    public int Id { get; }
    public int SkillCount { get; private set; }

    public PlayerInfo(int id)
    {
        Id = id;
        SkillCount = 0;
    }

    public void IncrementSkillCount() => SkillCount++;

    public void ResetSkillCount() => SkillCount = 0;
}