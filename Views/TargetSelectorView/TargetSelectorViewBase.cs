using Shin_Megami_Tensei;
using System.Collections.Generic;

namespace Shin_Megami_Tensei_View;

public abstract class TargetSelectorViewBase
{
    protected readonly View View;

    protected TargetSelectorViewBase(View view)
    {
        View = view;
    }

    public abstract void ShowAvailableTargets(UnitBase casterUnit, List<UnitBase> targetList);

    public int ReadTargetIndex(int totalOptions)
    {
        return InputValidator.ReadValidatedIndex(View, totalOptions);
    }

    public void ShowSeparator()
    {
        View.WriteLine("----------------------------------------");
    }
    protected void ShowTargetList(string headerText, List<UnitBase> targetList)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine(headerText);

        for (int index = 0; index < targetList.Count; index++)
        {
            var unit = targetList[index];
            string unitDetails = FormatUnitDetails(unit);
            View.WriteLine($"{index + 1}-{unitDetails}");
        }

        View.WriteLine($"{targetList.Count + 1}-Cancelar");
    }

    protected virtual string FormatUnitDetails(UnitBase unit)
    {
        return $"{unit.Name} HP:{unit.Stats.HP}/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}";
    }
}