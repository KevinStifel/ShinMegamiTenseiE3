using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class AllEnemySelector : TargetSelectorBase
{
    public AllEnemySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new EnemySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        List<UnitBase> aliveEnemies = Board.GetAliveUnits(enemyPlayerId);

        SelectorView.ShowSeparator();
        return aliveEnemies;
    }
}