namespace MakeShiftScouting.Models
{
    public class ScoutingPageSectionField
    {
        public string title { get; set; }
        public string code { get; set; }
        public bool required { get; set; }
        public string defaultValue { get; set; }
        public int min { get; set; }
        public string type { get; set; }
        public Dictionary<string, string> choices { get; set; } = new Dictionary<string, string>();
    }
}
