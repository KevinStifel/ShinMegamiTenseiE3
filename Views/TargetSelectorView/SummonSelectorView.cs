using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class SummonSelectorView : TargetSelectorViewBase
{
    public SummonSelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase summonerUnit, List<UnitBase> reserveMonsters)
    {
        string headerText = "Seleccione un monstruo para invocar";
        ShowTargetList(headerText, reserveMonsters);
    }
}