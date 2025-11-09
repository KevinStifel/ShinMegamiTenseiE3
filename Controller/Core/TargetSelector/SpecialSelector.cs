using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialSelector : SummonSelectorBase
{
    public SpecialSelector(View view, BoardManager boardManager)
        : base(view, boardManager) { }

    protected override List<UnitBase> GetCandidateUnits(int currentPlayerId)
    {
        return Board.GetAliveReserveUnitsForPlayer(currentPlayerId);
    }
}