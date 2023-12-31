﻿using MakeShiftScouting.Models;

namespace MakeShiftScouting
{
    public class ScoutingPage
    {
        public string title { get; set; }
        public string page_title { get; set; }
        public List<ScoutingPageSection> sections { get; set; } = new List<ScoutingPageSection>();
    }
}
