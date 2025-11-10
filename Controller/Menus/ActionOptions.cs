namespace Shin_Megami_Tensei
{
    public sealed class ActionOptions
    {

        public ActionOptions(IReadOnlyDictionary<string, string> actionMap)
        {
            Map = actionMap;
        }

        public string GetSelectedOption(string menuSelection) => Map[menuSelection.Trim()];
        
        private IReadOnlyDictionary<string, string> Map { get; }
    }
}