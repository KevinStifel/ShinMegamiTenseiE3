using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class PartySelector : TargetSelectorBase
{
    public PartySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new AllySelectorView(view)) { }

    protected override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        var aliveAllies = Board.GetAliveUnits(currentPlayerId);

        var alliesExcludingSelf = aliveAllies.Where(unit => unit != activeUnit);
        var alliesWithSelfLast = alliesExcludingSelf.Append(activeUnit);
        var orderedAllies = alliesWithSelfLast.ToList();

        return orderedAllies;
    }
}
