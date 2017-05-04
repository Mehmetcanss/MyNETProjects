using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticDataModels
{
    class ButtonDefinition : BaseDefinition
    {
        public ButtonDefinition() : base()
        {

        }

        private string _text = "new Button";
        private string _textColor = "Black";
        private double _borderRadius = 2;
        private double _borderWidth = 1;
        private double _fontSize = 12;

        public double FontSize
        {
            get { return _fontSize = 12; }
            set
            {
                _fontSize = value;
                Notify("FontSize");
            }
        }



        public double BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                _borderWidth = value;
                Notify("BorderWidth");
            }
        }


        public double BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                Notify("BorderRadius");
            }
        }


        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Notify("Text");
            }
        }


        public string TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                Notify("TextColor");
            }
        }


    }
}
