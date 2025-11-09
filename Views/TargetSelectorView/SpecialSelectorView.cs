using Shin_Megami_Tensei;
using System;
using System.Collections.Generic;

namespace Shin_Megami_Tensei_View;

public sealed class SpecialSelectorView : TargetSelectorViewBase
{
    public SpecialSelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase summonerUnit, List<UnitBase> summonableMonsters)
    {
        string title = "Seleccione un monstruo para invocar";
        ShowTargetList(title, summonableMonsters);
    }

    public void ShowSummonPositions(List<(string Position, UnitBase? currentUnitAtPosition)> summonOptions)
    {
        View.WriteLine("Seleccione una posición para invocar");

        for (int index = 0; index < summonOptions.Count; index++)
        {
            var (position, currentUnitAtPosition) = summonOptions[index];
            int slotNumber = Array.IndexOf(GameConstants.BoardPositions, position) + 1;

            string positionInfo = currentUnitAtPosition == null
                ? $"{index + 1}-Vacío (Puesto {slotNumber})"
                : $"{index + 1}-{currentUnitAtPosition.Name} HP:{currentUnitAtPosition.Stats.HP}/{currentUnitAtPosition.Stats.MaxHP} MP:{currentUnitAtPosition.Stats.MP}/{currentUnitAtPosition.Stats.MaxMP} (Puesto {slotNumber})";

            View.WriteLine(positionInfo);
        }

        View.WriteLine($"{summonOptions.Count + 1}-Cancelar");
    }

    public int ReadPositionIndex(int totalPositions)
    {
        string input = View.ReadLine();
        if (!int.TryParse(input, out int index))
            return -1;

        index -= 1;
        bool isValidIndex = index >= 0 && index < totalPositions;

        return isValidIndex ? index : -1;
    }
}