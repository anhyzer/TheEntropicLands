using System.Collections;
using System.Collections.Generic;
using System;

namespace TheEntropicLands
{
    public abstract class Command
    {
        private string _name, _secondaryName;
        public string Name { get { return _name; } set { _name = value; } }
        public string SecondaryName { get { return _secondaryName; } set { _secondaryName = value; } }

        private string _secondWord;
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }

        private string _thirdWord;
        public string ThirdWord { get { return _thirdWord; } set { _thirdWord = value;} }

        private string _fourthWord;
        public string FourthWord { get { return _fourthWord; } set { _fourthWord = value; } }

        private string _phrase;

        public string Phrase { get { return _phrase; } set { _phrase = value; } }  

        public Command()
        {
            this.Name = "";
            this.SecondaryName = "";
            this.SecondWord = null;
            this.ThirdWord = null;
            this.FourthWord = null;
            this.Phrase = null;
        }
        public bool HasSecondWord()
        {
            return this.SecondWord != null;
        }
        public bool HasThirdWord()
        {
            return this.ThirdWord != null;
        }
        public bool HasFourthWord()
        {
            return this.FourthWord != null;
        }
        public abstract bool Execute(Player player);
    }
}
