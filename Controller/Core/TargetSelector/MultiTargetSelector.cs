using Avalonia.Styling;
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
        
        List<UnitBase> orderedByBoard = TargetOrderingHelper.OrderByBoardPreservingRepeats(
            selectedTargets,
            Board.GetBoardForPlayer(enemyPlayerId)
        );
        /*
        // DEBUG
        Console.WriteLine("===== DEBUG MultiTarget Order =====");
        Console.WriteLine("SelectedTargets (orden original del algoritmo):");
        foreach (var unit in selectedTargets)
        {
            Console.WriteLine($" - {unit.Name}");
        }

        Console.WriteLine("\nBoard actual (izq -> der):");
        foreach (var kv in Board.GetBoardForPlayer(enemyPlayerId))
        {
            Console.WriteLine($" {kv.Key}: {kv.Value?.Name ?? "(vacío)"}");
        }

        Console.WriteLine("\nOrderedByBoard (orden final para imprimir):");
        foreach (var unit in orderedByBoard)
        {
            Console.WriteLine($" - {unit.Name}");
        }
        Console.WriteLine("=====================================\n");
        */
        SelectorView.ShowSeparator();
        return orderedByBoard;
    }
}