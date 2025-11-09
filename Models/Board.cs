namespace Shin_Megami_Tensei
{
    public class Board
    {
        public Dictionary<string, UnitBase?> PlayerOneBoard { get; internal set; }
        public Dictionary<string, UnitBase?> PlayerTwoBoard { get; internal set; }

        public List<UnitBase> PlayerOneRoster { get; }
        public List<UnitBase> PlayerTwoRoster { get; }
        
        internal readonly Dictionary<int, int> SkillUseCounters = new();
        
        public Board(List<UnitBase> playerOneUnits, List<UnitBase> playerTwoUnits)
        {
            PlayerOneRoster = new List<UnitBase>(playerOneUnits);
            PlayerTwoRoster = new List<UnitBase>(playerTwoUnits);

            PlayerOneBoard = new Dictionary<string, UnitBase?>();
            PlayerTwoBoard = new Dictionary<string, UnitBase?>();
        }
    }
}