using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class PartySelector : TargetSelectorBase
{
    public PartySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new AllySelectorView(view)) { }
    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        var aliveAllies = Board.GetAliveUnits(currentPlayerId).ToList();

        var orderedAllies = aliveAllies
            .Where(u => u != activeUnit)
            .Append(activeUnit)
            .ToList();
        return orderedAllies;
    }
}
