using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MassReviveAllySelector : TargetSelectorBase
{
    public MassReviveAllySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new AllySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        var aliveOnBoard = Board.GetAliveUnits(currentPlayerId)
            .Where(u => u != activeUnit)
            .ToList();

        var aliveInReserve = Board.GetReserveUnitsForPlayer(currentPlayerId)
            .Where(u => u.Stats.HP > 0)
            .ToList();

        var deadAllies = Board.GetAllDeadUnits(currentPlayerId);

        var allAllies = new List<UnitBase>();
        allAllies.AddRange(aliveOnBoard);
        allAllies.AddRange(deadAllies);
        allAllies.AddRange(aliveInReserve);

        SelectorView.ShowSeparator();
        return allAllies;
    }
}