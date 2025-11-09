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

    public abstract void ShowAvailableTargets(UnitBase casterUnit, List<UnitBase> candidates);

    public int ReadTargetIndex(int totalOptions)
    {
        return InputValidator.ReadValidatedIndex(View, totalOptions);
    }

    public void ShowSeparator()
    {
        View.WriteLine("----------------------------------------");
    }
    protected void ShowTargetList(string title, List<UnitBase> candidates)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine(title);

        for (int index = 0; index < candidates.Count; index++)
        {
            var unit = candidates[index];
            string unitDetails = FormatUnitDetails(unit);
            View.WriteLine($"{index + 1}-{unitDetails}");
        }

        View.WriteLine($"{candidates.Count + 1}-Cancelar");
    }

    protected virtual string FormatUnitDetails(UnitBase unit)
    {
        return $"{unit.Name} HP:{unit.Stats.HP}/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}";
    }
}