using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class DeadAllySelectorView : TargetSelectorViewBase
{
    public DeadAllySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase attackerUnit, List<UnitBase> deadAllies)
    {
        string title = $"Seleccione un objetivo para {attackerUnit.Name}";
        ShowTargetList(title, deadAllies);
    }
    protected override string FormatUnitDetails(UnitBase unit)
    {
        return $"{unit.Name} HP:0/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}";
    }
}