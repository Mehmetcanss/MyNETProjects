using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatePracticeHelper
{
    public class RandomGenerator
    {
        private Random rnd;
        private int firstDown;
        private int firstUp;
        private int secondDown;
        private int secondUp;

        public RandomGenerator(int firstDown, int firstUp, int secondDown, int secondUp)
        {
            this.rnd = new Random();
            this.firstDown = firstDown;
            this.firstUp = firstUp;
            this.secondDown = secondDown;
            this.secondUp = secondUp;
        }

        public int GenerateFirst()
        {
            return rnd.Next(firstDown, firstUp);

        }


        public int GenerateSecond()
        {
            return rnd.Next(secondDown, secondUp);
        }
        
    }
}
