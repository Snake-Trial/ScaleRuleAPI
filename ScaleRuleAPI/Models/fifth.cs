namespace ScaleRuleAPI.Models
{
    internal class Fifth
    {
        private int id = 0;
        private int pitchOrder = 0;
        private string noteName = "";
        private bool isKeynote = false;
        private int pitchClassId = 0;
        private int midi = 0;

        internal Fifth()
        { }

        internal Fifth(int id, int pitchOrder, string noteName, bool isKeynote, int pitchClassId, int midi)
        {
            this.id = id;
            this.pitchOrder = pitchOrder;
            this.noteName = noteName;
            this.isKeynote = isKeynote;
            this.pitchClassId = pitchClassId;
            this.midi = midi;
        }

        internal int Id  // property
        {
            get { return id; }   // get method
            set { id = value; }  // set method
        }

        internal int PitchOrder  // property
        {
            get { return pitchOrder; }   // get method
            set { pitchOrder = value; }  // set method
        }

        internal string NoteName  // property
        {
            get { return noteName; }   // get method
            set { noteName = value; }  // set method
        }

        internal bool IsKeynote  // property
        {
            get { return isKeynote; }   // get method
            set { isKeynote = value; }  // set method
        }

        internal int PitchClassID  // property
        {
            get { return pitchClassId; }   // get method
            set { pitchClassId = value; }  // set method
        }

        internal int Midi  // property
        {
            get { return midi; }   // get method
            set { midi = value; }  // set method
        }
    }
}