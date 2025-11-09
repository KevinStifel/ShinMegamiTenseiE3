using Avalonia.Styling;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MultiEnemySelector : TargetSelectorBase
{
    public MultiEnemySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new EnemySelectorView(view))
    {
    }

    protected override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        List<UnitBase> aliveEnemies = Board.GetAliveUnits(enemyPlayerId);

        int playerSkillUseCount = Board.GetSkillUseCount(currentPlayerId);
        int totalHits = HitCalculator.CalculateHits(skillData.Hits, playerSkillUseCount);

        List<UnitBase> selectedTargets = MultiTargetCalculator.CalculateTargets(
            aliveEnemies,
            playerSkillUseCount,
            totalHits
        );
        
        List<UnitBase> orderedByBoard = TargetOrderingHelper.OrderByBoardPreservingRepeats(
            selectedTargets,
            Board.GetBoardForPlayer(enemyPlayerId)
        );
        
        SelectorView.ShowSeparator();
        return orderedByBoard;
    }
}