using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(View view) : base(view) { }

    public UnitBase? ApplySamuraiSummon(
        UnitBase monsterToSummon, 
        PlayerBoardFormation playerFormation, 
        SummonPlacement summonPlacement)
    {
        playerFormation.ActiveBoard[summonPlacement.BoardPosition] = monsterToSummon;
        playerFormation.ReserveUnits.Remove(monsterToSummon);

        bool hasReplacedUnit = summonPlacement.ReplacedUnit != null;
        if (hasReplacedUnit)
            playerFormation.ReserveUnits.Insert(0, summonPlacement.ReplacedUnit!);

        EffectView.ShowSummonResult(monsterToSummon);
        return summonPlacement.ReplacedUnit;
    }

    public UnitBase ApplyMonsterSummon(
        SummonData summonData, 
        PlayerBoardFormation playerBoardFormation)
    {
        string summonerPosition = GetSummonerBoardPosition(
            playerBoardFormation, summonData.Summoner);

        playerBoardFormation.ActiveBoard[summonerPosition] = summonData.MonsterToSummon;
        playerBoardFormation.ReserveUnits.Remove(summonData.MonsterToSummon);
        playerBoardFormation.ReserveUnits.Insert(0, summonData.Summoner);

        EffectView.ShowSummonResult(summonData.MonsterToSummon);
        return summonData.Summoner;
    }

    private static string GetSummonerBoardPosition(
        PlayerBoardFormation playerBoardFormation, 
        UnitBase summoner)
    {
        var boardSlot = playerBoardFormation.ActiveBoard.First(slot => 
            ReferenceEquals(slot.Value, summoner));
        
        string positionKey = boardSlot.Key;
        return positionKey;
    }

    public override void ApplyEffect(
        UnitBase casterUnit, 
        List<UnitBase> targets, 
        SkillExecutionContext skillExecutionContext)
    {
    }
}