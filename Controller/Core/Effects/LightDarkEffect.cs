using System;
using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class LightDarkEffect : EffectBase
{
    private TurnManager _turnManager = null!;
    private BoardManager _boardManager = null!;
    private SkillData _skillData = null!;
    private int _currentPlayerId;
    private int _enemyPlayerId;
    private AffinityElement _elementType;

    public LightDarkEffect(View view) : base(view) { }

    public override void ApplyEffect(UnitBase casterUnit, List<UnitBase> targets, SkillExecutionContext context)
    {
        _turnManager = context.TurnManager;
        _boardManager = context.BoardManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);
        _elementType = AffinityMapper.Parse(_skillData.Type);

        Console.WriteLine($"[DEBUG] Iniciando LightDarkEffect con skill: {_skillData.Name}, elemento: {_elementType}");
        Console.WriteLine($"[DEBUG] Caster: {casterUnit.Name}, Targets: {targets.Count}");

        foreach (var target in targets)
            ApplyLightDarkToTarget(casterUnit, target);

        var topAffinity = AffinityPriorityHelper.GetTopPriorityReaction(targets, _elementType);
        Console.WriteLine($"[DEBUG] Top Affinity: {topAffinity}");

        var topBehavior = AffinityBehaviorFactory.Create(topAffinity);
        ApplyTurnChange(topBehavior);
    }

    private void ApplyLightDarkToTarget(UnitBase caster, UnitBase target)
    {
        Console.WriteLine($"[DEBUG] -> Procesando ataque a {target.Name}");

        var behavior = GetAffinityBehavior(target, _elementType);
        Console.WriteLine($"[DEBUG] AffinityBehavior detectado: {behavior.Type}");

        var affinityView = AffinityViewFactory.Create(behavior.Type, View, _elementType);

        Console.WriteLine($"[DEBUG] Ejecutando ApplyLightDarkEffect...");
        behavior.ApplyLightDarkEffect(caster, target, _skillData);

        Console.WriteLine($"[DEBUG] Mostrando LightDarkReaction...");
        affinityView.ShowLightDarkReaction(caster, target, _skillData);

        Console.WriteLine($"[DEBUG] HP {target.Name}: {target.Stats.HP}/{target.Stats.MaxHP}");
        EffectView.ShowHpStatus(target);

        if (target.Stats.HP == 0)
        {
            Console.WriteLine($"[DEBUG] {target.Name} ha muerto, eliminando del tablero...");
            _boardManager.HandleUnitDeath(_enemyPlayerId, target);
        }

        Console.WriteLine($"[DEBUG] Fin de procesamiento para {target.Name}");
    }

    private void ApplyTurnChange(AffinityBehavior behavior)
    {
        Console.WriteLine($"[DEBUG] Aplicando cambio de turno ({behavior.Type})...");
        var turnChange = _turnManager.ApplyAffinityTurnEffect(behavior);
        ActionView.ShowTurnConsumption(turnChange);
        Console.WriteLine($"[DEBUG] TurnChange => FullConsumed: {turnChange.ConsumedFull}, BlinkConsumed: {turnChange.ConsumedBlinking}, BlinkGained: {turnChange.GainedBlinking}");
    }
}
