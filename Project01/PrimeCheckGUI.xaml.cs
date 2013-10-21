using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project01
{
    /// <summary>
    /// Interaction logic for PrimeCheckGUI.xaml
    /// </summary>
    public partial class PrimeCheckGUI : Window
    {
        public PrimeCheckGUI()
        {
            InitializeComponent();
            PrepareNewGame();

        }

        enum State { Starting, CheckAnswer, NextQuestionUsed, EndOfTest };
        State ProgramState;

        private int rightAnswerCount = 0;
        private int currentQuestion = 1;
        private const int numberOfQuestions = 10;
        private int randomNumber;

        private int playingTimesAct = 0;
        private float gameScore;
        private float currentScore;

        public float GameScore { get { return gameScore * 100; } }
        public int GetPlayTimes() { return playingTimesAct;  }

        private void PrepareNewGame()
        {
            StartButton.IsEnabled = true;
            EasyRadioBtn.IsEnabled = true;
            MediumRadioBtn.IsEnabled = true;
            HardRadioBtn.IsEnabled = true;
            QuestionNumber.IsEnabled = false;
            CountDown.IsEnabled = false;
            RandomNumberLabel.IsEnabled = false;
            Status.IsEnabled = false;
            Score.IsEnabled = false;
            NotPrimeBtn.IsEnabled = false;
            PrimeBtn.IsEnabled = false;

            EasyRadioBtn.IsChecked = true;

           playingTimesAct=0;

        }

        // Displays the current question number
        // Place to enter the Answer
        private void StartQuestion()
        {
            generateNumber();
            StartTimer();
            QuestionNumber.Content = "Question " + currentQuestion + "/10";
            Score.Content = rightAnswerCount + "/10";

            
        }

 
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            EasyRadioBtn.IsEnabled = true;
            MediumRadioBtn.IsEnabled = true;
            HardRadioBtn.IsEnabled = true;
            QuestionNumber.IsEnabled = true;
            CountDown.IsEnabled = true;
            RandomNumberLabel.IsEnabled = true;
            Status.IsEnabled = true;
            Score.IsEnabled = true;
            NotPrimeBtn.IsEnabled = true;
            PrimeBtn.IsEnabled = true;
            currentQuestion = 1;
            rightAnswerCount = 0;
            StartQuestion();

            playingTimesAct++;

        }

        private void generateNumber()
        {
            if (EasyRadioBtn.IsChecked == true)
            {
                randomNumber = RandomUtil.IntWithRange(1, 10);
            }
            else if (MediumRadioBtn.IsChecked == true)
            {
                randomNumber = RandomUtil.IntWithRange(10, 100);
            }
            else if (HardRadioBtn.IsChecked == true)
            {
                randomNumber = RandomUtil.IntWithRange(100, 1000);
            }
            RandomNumberLabel.Content = randomNumber;

        }




        // Checks if we are going to the next question
        public bool IsNextQuestion()
        {
            if (currentQuestion >= numberOfQuestions)
                return false;
            else
                return true;
        }

        // Advances to the next question
        public void advanceNextQuestion()
        {
            if (currentQuestion < numberOfQuestions)
                currentQuestion++;
        }




        public bool IsAnswerPrime()
        {
            if ((randomNumber & 1) == 0)
            {
                if (randomNumber == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 3; (i * i) <= randomNumber; i += 2)
            {
                if ((randomNumber % i) == 0)
                {
                    return false;
                }
            }
            return randomNumber != 1;
        }




        private void NextQuestionUsed()
        {
            if (IsNextQuestion())
            {
                advanceNextQuestion();
                ProgramState = State.Starting;
                StartQuestion();

            }
            else
            {
                EndOfTest();

            }
        }

        private void EndOfTest()
        {
            StartButton.IsEnabled = true;
            EasyRadioBtn.IsEnabled = true;
            MediumRadioBtn.IsEnabled = true;
            HardRadioBtn.IsEnabled = true;
            QuestionNumber.IsEnabled = false;
            CountDown.IsEnabled = false;
            RandomNumberLabel.IsEnabled = false;
            Status.IsEnabled = false;
            Score.IsEnabled = false;
            NotPrimeBtn.IsEnabled = false;
            PrimeBtn.IsEnabled = false;
            MessageBox.Show("You have completed the Test! You got: " + rightAnswerCount + "/10");

            currentScore += float.Parse(rightAnswerCount.ToString()) *float.Parse("0.1") ;
            gameScore = currentScore / playingTimesAct;
        }

        private void PrimeBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("PrimeBtn ClickMode");
            timer.Stop();
            if (IsAnswerPrime())
            {
                Status.Content = "You are Correct!";
                rightAnswerCount++;
            }
            else
            {
                Status.Content = "You are Wrong!";
            }
            NextQuestionUsed();
        }
        private void NotPrimeBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Console.WriteLine("NOT PrimeBtn ClickMode");
            if (!IsAnswerPrime())
            {
                Status.Content = "You are Correct!";
                rightAnswerCount++;

            }
            else
            {
                Status.Content = "You are Wrong!";
            }
            NextQuestionUsed();
        }


        int secondsLeft;
        const int secondsToAnswer = 5;
        DispatcherTimer timer;

        private void StartTimer()
        {
            /*
            aTimer = new System.Timers.Timer(secondsToAnswer * 1000);
            secondsLeft = secondsToAnswer;
            CountDown.Content = secondsLeft;
            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 1 seconds (1000 milliseconds).
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
            aTimer.Start();
            */
            secondsLeft = secondsToAnswer;
            CountDown.Content = secondsLeft;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Start();

        }

        private void OnTimedEvent(object source, EventArgs e)
        {
            secondsLeft--;
            Console.WriteLine(secondsLeft);
            CountDown.Content = secondsLeft.ToString();
            if (secondsLeft < 0)
            {
                Status.Content = "Sorry... no more time!";
                timer.Stop();
                NextQuestionUsed();

            }


        }
    }
}
