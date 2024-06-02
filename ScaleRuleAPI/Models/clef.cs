using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ScaleRuleAPI.Models
{
   
    public class Clef
    {
        private int id = 0;
        private string clefName = "";
        private string symbol = "";
        private int line = 0;

        internal Clef()
        { }

        internal Clef(int id, string clefName, string symbol, int line)
        {
            this.id = id;
            this.clefName = clefName;
            this.symbol = symbol;
            this.line = line;
        }


        public int Id  // property
        {
            get { return this.id; }   // get method
            set { id = value; }  // set method
        }

        public string ClefName  // property
        {
            get { return this.clefName; }   // get method
            set { clefName = value; }  // set method
        }

        public string Symbol  // property
        {
            get { return this.symbol; }   // get method
            set { symbol = value; }  // set method
        }

        public int Line  // property
        {
            get { return this.line; }   // get method
            set { line = value; }  // set method
        }
    }
}
