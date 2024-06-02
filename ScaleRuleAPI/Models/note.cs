namespace ScaleRuleAPI.Models
{
    public class Note
    {
        private string _name = string.Empty;
        private string _vex = string.Empty;

        internal Note()
        { }

        internal Note(string name, string vex)
        {
            Name = name;
            Vex = vex;
        }

        public string Name  // property
        {
            get { return _name; }   // get method
            set { _name = value; }  // set method
        }

        public string Vex  // property
        {
            get { return _vex; }   // get method
            set { _vex = value; }  // set method
        }
    }
}
