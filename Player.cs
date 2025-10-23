using Shin_Megami_Tensei_GUI;

namespace Shin_Megami_Tensei;

public class Player(IUnit?[] unitsInBoard, IEnumerable<IUnit> unitsInReserve) : IPlayer
{
    public IUnit?[] UnitsInBoard { get; } = unitsInBoard;
    public IEnumerable<IUnit> UnitsInReserve { get; set; } = unitsInReserve;
}