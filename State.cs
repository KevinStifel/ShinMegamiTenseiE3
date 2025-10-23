using Shin_Megami_Tensei_GUI;

namespace Shin_Megami_Tensei;

public class State : IState
{
    public IPlayer Player1 { get; set; }
    public IPlayer Player2 { get; set; }

    public IEnumerable<string> Options { get; set; } =
    [
        "Atacar",
        "Disparar",
        "Usar Habilidad",
        "Invocar",
        "Rendirse"
    ];

    public int Turns { get; set; } = 2;
    public int BlinkingTurns { get; set; } = 7;

    public IEnumerable<string> Order { get; set; } =
    [
        "Flynn",
        "Joker",
        "Alice",
    ];
}