using System.Collections.ObjectModel;

namespace Shin_Megami_Tensei
{
    public class BoardManager
    {
        private readonly Board _board;
        
        private readonly Dictionary<int, PreparedSummonData> _preparedSummons = new();
        private record PreparedSummonData(UnitBase Monster, string Position, UnitBase? Replaced);

        public BoardManager(Board board)
        {
            _board = board;
            InitializeBoards();
        }
        
        private void InitializeBoards()
        {
            _board.PlayerOneBoard = InitializeBoard(_board.PlayerOneRoster);
            _board.PlayerTwoBoard = InitializeBoard(_board.PlayerTwoRoster);
        }

        private Dictionary<string, UnitBase?> InitializeBoard(IReadOnlyList<UnitBase> teamUnits)
        {
            var board = new Dictionary<string, UnitBase?>(GameConstants.BoardPositions.Length);
            for (var index = 0; index < GameConstants.BoardPositions.Length; index++)
            {
                var position = GameConstants.BoardPositions[index];
                board[position] = index < teamUnits.Count ? teamUnits[index] : null;
            }
            return board;
        }
        
        public void PrepareSummonData(int playerId, UnitBase monster, (string Position, UnitBase? Replaced) summonSlot)
        {
            _preparedSummons[playerId] = new PreparedSummonData(monster, summonSlot.Position, summonSlot.Replaced);
        }

        public (string Position, UnitBase? Replaced) GetPreparedSummonData(int playerId)
        {
            if (_preparedSummons.TryGetValue(playerId, out var data))
                return (data.Position, data.Replaced);

            throw new PreparedSummonDataMissingException(playerId);
        }
        
        public Dictionary<string, UnitBase?> SelectMutableBoard(int playerId)
            => playerId == 1 ? _board.PlayerOneBoard : _board.PlayerTwoBoard;

        public IReadOnlyDictionary<string, UnitBase?> GetBoardForPlayer(int playerId)
        {
            var board = SelectMutableBoard(playerId);
            return new ReadOnlyDictionary<string, UnitBase?>(board);
        }


        public UnitBase GetTeamLeaderUnit(int playerId)
            => GetBoardForPlayer(playerId)[GameConstants.BoardPositions[0]]!;

        public List<UnitBase> GetReserveUnitsForPlayer(int playerId)
        {
            var roster = GetRoster(playerId);

            var playerBoard = GetBoardForPlayer(playerId);
            var allBoardSlots = playerBoard.Values;
            var unitsOnBoard = allBoardSlots.OfType<UnitBase>();
            var boardUnits = unitsOnBoard.ToHashSet();

            var unitsInReserve = roster.Where(unit => !boardUnits.Contains(unit));
    
            return unitsInReserve.ToList();
        }
        public List<UnitBase> GetAliveReserveUnitsForPlayer(int playerId)
        {
            return GetReserveUnitsForPlayer(playerId)
                .Where(unit => unit.Stats.HP > 0)
                .ToList();
        }
        
        public List<UnitBase> GetAliveUnits(int playerId)
        {
            var allBoardSlots = GetBoardForPlayer(playerId).Values;
            var unitsOnBoard = allBoardSlots.OfType<UnitBase>();
            var aliveUnits = unitsOnBoard.Where(unit => unit.Stats.HP > 0);

            return aliveUnits.ToList();
        }
        public List<UnitBase> GetAllDeadUnits(int playerId)
        {
            var deadUnits = new List<UnitBase>();
            var leader = GetTeamLeaderUnit(playerId);
            
            if (leader.Stats.HP == 0)
                deadUnits.Add(leader);

            var reserveUnits = GetReserveUnitsForPlayer(playerId);
            foreach (var unit in reserveUnits)
            {
                if (unit.Stats.HP == 0)
                    deadUnits.Add(unit);
            }
            return deadUnits;
        }
        private IReadOnlyList<UnitBase> GetRoster(int playerId)
            => playerId == 1 ? _board.PlayerOneRoster : _board.PlayerTwoRoster;

        public void HandleUnitDeath(int currentPlayerId, UnitBase unit)
        {
            if (unit is Samurai) return;
            RemoveMonsterFromBoard(currentPlayerId, unit);
        }


        private void RemoveMonsterFromBoard(int playerId, UnitBase monster)
        {
            var board = SelectMutableBoard(playerId);

            foreach (var position in GameConstants.BoardPositions)
            {
                if (ReferenceEquals(board[position], monster))
                {
                    board[position] = null;
                    break;
                }
            }
        }
        public bool HasWinner()
        {
            return GetWinner() != BattleOutcome.Ongoing;
        }

        public BattleOutcome GetWinner()
        {
            if (IsDraw()) return BattleOutcome.Draw;
            if (HasPlayerTwoLost()) return BattleOutcome.PlayerOneWins;
            return HasPlayerOneLost() ? BattleOutcome.PlayerTwoWins : BattleOutcome.Ongoing;
        }

        private bool IsDraw()
        {
            return !IsPlayerAlive(1) && !IsPlayerAlive(2);
        }

        private bool HasPlayerOneLost()
        {
            return !IsPlayerAlive(1) && IsPlayerAlive(2);
        }

        private bool HasPlayerTwoLost()
        {
            return IsPlayerAlive(1) && !IsPlayerAlive(2);
        }

        private bool IsPlayerAlive(int playerId)
        {
            return GetAliveUnits(playerId).Count > 0;
        }
        
        public void RegisterPlayerSkillCounter(int playerId)
        {
            _board.SkillUseCounters.TryAdd(playerId, 0);
        }
        
        public int GetSkillUseCount(int playerId)
        {
            _board.SkillUseCounters.TryAdd(playerId, 0);
            return _board.SkillUseCounters[playerId];
        }

        public void IncrementSkillUseCount(int playerId)
        {
            _board.SkillUseCounters.TryAdd(playerId, 0);
            _board.SkillUseCounters[playerId]++;
        }
        public void ResetPlayerSkillCounter(int playerId)
        {
            _board.SkillUseCounters[playerId] = 0;
        }
    }
}