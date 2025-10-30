namespace Shin_Megami_Tensei
{
    public sealed class ActionOptions
    {
        public IReadOnlyDictionary<string, string> Map { get; }

        public ActionOptions(IReadOnlyDictionary<string, string> actionMap)
        {
            Map = actionMap;
        }

        public string GetSelectedOption(string menuSelection) => Map[menuSelection.Trim()];
    }
}