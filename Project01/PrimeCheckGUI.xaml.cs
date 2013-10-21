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
    /// A simple Game where the user determines if the number presented is prime or not. 
    /// There are ten questions with a difficulty setting of easy (Numbers: 1 to 9). medium (10 - 99) and hard (100 to 999)
    /// There is a countdown of 5 seconds before the next question is presented
    /// There is a question number count telling the user what question they are on
    /// There is a right question count telling the user how many they have that are correct.
    /// There is a summary box giving the user feedback on there answers and if they ran out of time.
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

        // Sets up before the Start Game button is clicked everything but the start button and difficulty buttons are disabled
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

        // Event Handler Enabling all interative elements, rettinng the current question count & right answer count.
        // It also starts the first question.
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

        // Generates a Random number based on the checked button difficulty, Easy, Medium and Hard. 
        // Easy generates a random number between 1 and 9.
        // Medium generates a random number between 10 to 99.
        // Hard generates a random number between 100 and 999.
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



        // Determines wether the question number is prime or not. 
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



        // if there is a next question. It advances to the next question or it ends the tests
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

        // Ends test. Disables all functionality except to start a new test and select difficulty.  
        // also generates a message box telling how many answers they got correct
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

        // Clicked if it is prime. Determines wether your answer is correct if it is prime. 
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

        // Clicked if its not prime. Determines wether your answer is correct if not prime
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

        // generates a countdown timer of 5 seconds.
        private void StartTimer()
        {
            
            secondsLeft = secondsToAnswer;
            CountDown.Content = secondsLeft;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Start();

        }

        // makes the timer go down in seconds. Logic that if the Timer reaches 0 than
        // A status of "Sorry... no more time!" shows up and the next question is presented
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
