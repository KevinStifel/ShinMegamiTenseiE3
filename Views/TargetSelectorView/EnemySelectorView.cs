using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class EnemySelectorView : TargetSelectorViewBase
{
    public EnemySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase attackerUnit, List<UnitBase> enemies)
    {
        string headerText = $"Seleccione un objetivo para {attackerUnit.Name}";
        ShowTargetList(headerText, enemies);
    }
}