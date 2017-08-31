using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatePracticeHelper
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime startTime;
        private int secondNumber = -1;
        private int firstNumber = -1;
        private double answer = Double.NaN;
        private RandomGenerator _generator;
        private ICommand _checkAnswer;
        private ICommand _startGame;
  
        private bool numbersVisible = true;

        public bool NumbersVisible
        {
            get { return numbersVisible; }
            set
            {
                numbersVisible = value;
                Notify("NumbersVisible");
            }
        }


        private int _answerCorrect;

        private double _elapsedSeconds;

        public double ElapsedSeconds
        {
            get { return _elapsedSeconds; }
            set
            {
                _elapsedSeconds = value;
                Notify("ElapsedSeconds");
            }
        }


        public int AnswerCorrect
        {
            get { return _answerCorrect; }
            set
            {
                _answerCorrect = value;
                Notify("AnswerCorrect");
            }
        }





        public MainViewModel()
        {
            this._generator = new RandomGenerator(10, 100, 10, 100);
            this.AnswerCorrect = -1;
            this.Answer = -1;
            this.ElapsedSeconds = -1;
            initCommands();
        }

        private void initCommands()
        {
            this.StartGameCommand = new CustomCommand(startGame);
            this.CheckAnswer = new CustomCommand(canCheckAnswer, checkAnswer);
        }

        public int FirstNumber
        {
            get { return firstNumber; }
            set
            {
                firstNumber = value;
                Notify("FirstNumber");
            }
        }


        public int SecondNumber
        {
            get { return secondNumber; }
            set
            {
                secondNumber = value;
                Notify("SecondNumber");
            }
        }

        public double Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                Notify("Answer");
            }
        }



        private void Notify(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }


        public ICommand CheckAnswer
        {
            get { return _checkAnswer; }
            set
            {
                _checkAnswer = value;
                Notify("CheckAnswer");
            }
        }

        public ICommand StartGameCommand
        {
            get { return _startGame; }
            set
            {
                _startGame = value;
                Notify("StartGameCommand");
            }
        }

        private async void startGame(object par)
        {
            NumbersVisible = true;
            this.FirstNumber = _generator.GenerateFirst();
            this.SecondNumber = _generator.GenerateSecond();
            this.Answer = -1;
            this.AnswerCorrect = -1;
            this.ElapsedSeconds = -1;
            
            startTime = DateTime.Now;
            await Task.Delay(3000);
            NumbersVisible = false;
            
        }

        private void checkAnswer(object par)
        {
            bool correct = Checker.CheckMultiply(FirstNumber, SecondNumber, Answer);
            if (correct) AnswerCorrect = 1; else AnswerCorrect = 0;
            ElapsedSeconds = Math.Round(MyTimer.GetElapsedSeconds(startTime), 2);
            
        }

        private bool canCheckAnswer(object par)
        {
            return this.Answer != -1;
        }

    }

  
}
