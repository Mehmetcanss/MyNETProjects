using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace GPARechner_Lokal
{

    public class Lecture
    {


        private string _name;


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _weight;

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private double _note;

        public double Note
        {
            get { return _note; }
            set { _note = value; }
        }


    }
}