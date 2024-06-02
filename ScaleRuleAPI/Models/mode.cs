namespace ScaleRuleAPI.Models
{
    internal class Mode
    {
        private int id = 0;
        private string modeName = "";
        private int familyId = 0;
        private string familyName = "";
        private int[] pattern = [];
        private int enharmonic = 0;

        internal Mode()
        { }

        internal Mode(int id, string modeName, int familyId, string familyName, int[] pattern, int enharmonic)
        {
            this.id = id;
            this.modeName = modeName;
            this.familyId = familyId;
            this.familyName = familyName;
            this.pattern = pattern;
            this.enharmonic = enharmonic;
        }

        internal int Id  // property
        {
            get { return id; }   // get method
            set { id = value; }  // set method
        }

        internal string Name   // property
        {
            get { return modeName; }   // get method
            set { modeName = value; }  // set method
        }

        internal int FamilyId
        {
            get { return familyId; }
            set { familyId = value; }
        }

        internal string FamilyName    //property
        {
            get { return familyName; }   // get method
            set { familyName = value; }  // set method
        }

        internal int[] Pattern
        {
            get { return pattern; }   // get method
            set { pattern = value; }  // set method
        }

        internal int Enharmonic  // property
        {
            get { return enharmonic; }   // get method
            set { enharmonic = value; }  // set method
        }
    }
}