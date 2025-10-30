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
        var aliveAllies = Board.GetAliveUnits(currentPlayerId)
            .Where(u => u != activeUnit)
            .ToList();

        var deadAllies = Board.GetAllDeadUnits(currentPlayerId);

        var allAllies = new List<UnitBase>();
        allAllies.AddRange(aliveAllies);
        allAllies.AddRange(deadAllies);

        SelectorView.ShowSeparator();

        return allAllies;
    }
}