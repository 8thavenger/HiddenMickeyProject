using System;

namespace HiddenMickeyProject.Data
{
    public class Entry
    {
        private string clue;
        private string hint = string.Empty;
        private int locationId;

        public string Clue
        {
            get { return clue; }
            set { clue = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); ; }
        }

        public string Hint
        {
            get { return hint; }
            set { hint = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); ; }
        }

        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }
    }
}