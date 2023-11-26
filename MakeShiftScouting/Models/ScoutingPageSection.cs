namespace MakeShiftScouting.Models
{
    public class ScoutingPageSection
    {
        public string name { get; set; }
        public bool preserveDataOnReset { get; set; }
        public List<ScoutingPageSectionField> fields { get; set; } = new List<ScoutingPageSectionField>();
    }
}
