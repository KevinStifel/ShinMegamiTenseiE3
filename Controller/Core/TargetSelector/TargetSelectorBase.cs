using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei;

public abstract class TargetSelectorBase
{
    protected readonly TargetSelectorViewBase SelectorView;
    protected readonly BoardManager Board;
    protected readonly View View;

    protected TargetSelectorBase(View view, BoardManager boardManager, TargetSelectorViewBase selectorView)
    {
        View = view;
        Board = boardManager;
        SelectorView = selectorView;
    }
    
    public IReadOnlyList<UnitBase> SelectTargetsReadOnly(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        var selectedTargets  = SelectTargets(activeUnit, currentPlayerId, skillData);
        return selectedTargets.ToArray();
    }
    protected abstract List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData);

    protected int ReadTargetIndex(List<UnitBase> targetList)
        => SelectorView.ReadTargetIndex(targetList.Count);

    protected static bool IsSelectionCanceled(int index)
        => index < 0;
}