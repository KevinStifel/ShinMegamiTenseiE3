namespace Shin_Megami_Tensei
{
    public sealed class PreparedSummonDataMissingException(int playerId)
        : Exception($"No hay datos preparados para la invocación del jugador {playerId}.");
}