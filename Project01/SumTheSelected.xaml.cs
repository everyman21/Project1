/*
Sum	the	Selected	
Show	the	user	a	4	x	4	grid,	each	grid	cell	should	contain	a	randomly	generated	number
Color	5	of	the	tiles	at	random	(use	bolded	fonts	as	well	+	borders	etc.)	
Track	how	much	time	it	takes	for	the	user	to	enter	the	sum	of	the	numbers	in	the	colored	tiles	
Set	three	difficulty	levels	
Numbers	are	from	1	to	9	
Numbers	are	from	100	to	999	
Numbers	are	from	1000	to	9999	
*/
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
using System.Diagnostics;



namespace Project01
{
    /// <summary>
    /// Interaction logic for SumTheSelected.xaml
    /// </summary>
    public partial class SumTheSelected : Window
    {
        private int upperLimit; // upper limit of content numbers - to be randomly generated
        private int lowerLimit; // lower limit of content numbers - to be randomly generated
        private int sum; // sum of the 5 selected numbers
        StringBuilder mrstring = new StringBuilder(); // string builder for game play
        int[] selected = new int[5]; // array to store 5 randomly generated numbers
        StringBuilder stringthing = new StringBuilder();  // string builder for SelectFive method
        Stopwatch timer = new Stopwatch(); // stop watch for timing users input
        // variables to interact with GameScore
        private int timesPlayed; // number of times game has been played
        private float gameScore; // overall score of the user this session
        private float currentScore; // current score of a single instance


        /// <summary>
        /// returns GameScore after calculating it
        /// </summary>
        public float GameScore { get { return gameScore * 100; } }
        /// <summary>
        /// Public method to allow access to how many times the game has been played.
        /// </summary>
        /// <returns>number of times the game has been played this session</returns>
        public int GetPlayTimes() { return timesPlayed; }


        /// <summary>
        /// Level propery which is set by user 
        /// </summary>

        public int Level
        {
            get
            {
                if (level1Radio.IsChecked == true)
                    return 1;
                else if (level2Radio.IsChecked == true)
                    return 2;
                else if (level3Radio.IsChecked == true)
                    return 3;
                else
                {
                    // If no level is selected - user LevelSelect to get one
                    LevelSelect level = new LevelSelect();

                    level.ShowDialog();
                    // read in the variable stored by LevelSelect
                    return (int)Application.Current.Properties["Level"];
                }
            }
        }




        /// <summary>
        /// Sets up the game for its first play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // replace with stopwatch
            playAgainButton.IsEnabled = false;
            DispatcherTimer update = new DispatcherTimer();
            update.Tick += new EventHandler(timer_Tick);
            update.Interval = new TimeSpan(0, 0, 1);
            update.Start();



        }

        /// <summary>
        /// Triggers an event to update the timerLabel with the stopwatches' elapsed time in seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {

            timerLabel.Content = (timer.Elapsed.Seconds);
        }

        /// <summary>
        /// Default constructor which initializes the window and sets values for first play
        /// </summary>
        public SumTheSelected()
        {
            InitializeComponent();

            checkAnswerButton.IsEnabled = false;
            sum = 0;

            SelectFive();
            DrawGameBoard(this.Level);
            timesPlayed++;
            timer.Start();

        }



        /// <summary>
        /// Constructor which will be called if the user chooses to play again
        /// </summary>
        /// <param name="timesplayed">current number of times played</param>
        /// <param name="level">level the user has selected for next play</param>
        public SumTheSelected(int timesplayed, int level)
        {
            InitializeComponent();
            timesPlayed += timesplayed;
            checkAnswerButton.IsEnabled = false;
            sum = 0;

            SelectFive();
            DrawGameBoard(level);

            timer.Start();

        }

        /// <summary>
        /// Creates the game board by randomly drawing a 16 number grid. The range of the
        /// numbers generated depends on the level which has been selected.
        /// </summary>
        /// <param name="level">Selected by the user</param>
        private void DrawGameBoard(int level)
        {
            int labelNumber = 1;
            int column = 0;
            int row = 0;
            int tempContent;

            for (int i = 0; i <= 15; i++)
            {
                if (level == 1)
                {
                    lowerLimit = 1;
                    upperLimit = 10;
                }
                else if (level == 2)
                {
                    lowerLimit = 100;
                    upperLimit = 1000;
                }

                else if (level == 3)
                {
                    lowerLimit = 1000;
                    upperLimit = 10000;
                }

                else
                {   // should never be triggered
                    MessageBox.Show("Invalid Level Entered, now exiting");
                    this.Close();
                }

                // create a new label to be added to the grid
                Label newLabel = new Label();
                newLabel.Name = "label" + labelNumber; // set labels name to "label" + a sequence number
                tempContent = RandomUtil.IntWithRange(lowerLimit, upperLimit); // set the content to a random number of difficulty set by user
                newLabel.Content = tempContent;
                newLabel.VerticalContentAlignment = VerticalAlignment.Center;
                newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                newLabel.Margin = new Thickness(2);
                Grid.SetColumn(newLabel, column);
                Grid.SetRow(newLabel, row);

                // If the sequence number of the label is one of the pre selected five, set its background to red and its font to ultra bold
                if (selected.Contains(labelNumber))
                {
                    newLabel.Background = Brushes.Red;
                    newLabel.FontWeight = FontWeights.UltraBold;
                    sum += tempContent; // add it to the running sum for later verification
                    mrstring.Append(tempContent + "+"); // used for error checking
                }

                innerGrid.Children.Add(newLabel);  // add label to the grid
                labelNumber++;
                if (column >= 3)
                {
                    column = 0; // start at first column
                    row++;     // move to the next row
                }
                else
                {
                    column++;  // move to the next column
                }


            }
            mrstring.Append(" = " + sum);
        }

        /// <summary>
        /// Select 5 numbers randomly to be used for the red colored squares
        /// </summary>
        private void SelectFive()
        {

            for (int i = 0; i <= 4; i++)
            {
                int randomNumber = RandomUtil.IntWithRange(1, 16);

                // if the number generated already is in the array - get another
                if (selected.Contains(randomNumber))
                {
                    randomNumber = RandomUtil.IntWithRange(randomNumber + 1, 16);

                }
                selected[i] = randomNumber;

                // used for error checking 
                stringthing.AppendLine(selected[i].ToString());

            }


        }

        /// <summary>
        /// Check to see if the user's input is correct and provide feedback for them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            playAgainButton.IsEnabled = true;
            int time = Convert.ToInt32(timer.Elapsed.Seconds);
            

            // attempt to read in user input - toss it if it is invalid and show the error
            try
            {

                if (Convert.ToInt32(answerTextBox.Text) == sum)
                {
                    feedbackLabel.Text = "Correct! Your Time was " + time + " seconds!";
                    checkAnswerButton.IsEnabled = false;
                }

                else
                {
                    feedbackLabel.Text = "Sorry, thats incorrect. The Correct answer is: \n " + mrstring.ToString();
                    checkAnswerButton.IsEnabled = false;

                }

            }
            catch (Exception typeError)
            {
                MessageBox.Show( typeError.Message, "Invalid input!");
                checkAnswerButton.IsEnabled = false;
            }

            // calculate the user's score and reset the timer
            currentScore += time * float.Parse("0.1");
            gameScore = currentScore / timesPlayed;
            timer.Reset();
        }

        /// <summary>
        /// When an answer has been entered - allow the user to check if its correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void answerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkAnswerButton.IsEnabled = true;
        }

        /// <summary>
        /// Button which allows the User to play again - at the same or a new 
        /// difficulty level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playAgainButton_Click(object sender, RoutedEventArgs e)
        {

            SumTheSelected newGame = new SumTheSelected(timesPlayed, this.Level);

            newGame.Show(); 
            this.Close();
        }

        /// <summary>
        /// Show info about the Author, Date of creation and Version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Author: Andy Groenenberg \n Date:2013-10-20 \n Version:1.0.7");
        }

        /// <summary>
        /// Exit current window - but does not shut down outer application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}
