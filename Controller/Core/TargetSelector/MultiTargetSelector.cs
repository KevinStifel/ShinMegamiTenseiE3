using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MultiEnemySelector : TargetSelectorBase
{
    public MultiEnemySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new EnemySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
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

        SelectorView.ShowSeparator();
        return selectedTargets;
    }
}