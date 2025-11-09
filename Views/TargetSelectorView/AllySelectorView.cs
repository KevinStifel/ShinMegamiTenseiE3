using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class AllySelectorView : TargetSelectorViewBase
{
    public AllySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase attackerUnit, List<UnitBase> allies)
    {
        string headerText = $"Seleccione un objetivo para {attackerUnit.Name}";
        ShowTargetList(headerText, allies);
    }
}