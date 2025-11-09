namespace Shin_Megami_Tensei
{
    public sealed class ActionCanceledException : Exception { }
    
    public sealed class BattleEndedException : Exception { }
    
    public sealed class SkillNotFoundException(string message) : Exception(message);
    
    public sealed class InvalidActionOptionException(string message) : Exception(message);
    
    public sealed class UnknownAffinityTypeException(string message) : Exception(message);


    
}