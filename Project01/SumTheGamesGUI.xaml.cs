/* 
 * Author: Qi Zhang
 * Date: 2013
 * Description: project 01
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
using System.Drawing;

namespace Project01
{
    /// <summary>
    /// Interaction logic for SumTheNumbersGUI.xaml
    /// </summary>
    public partial class SumTheGamesGUI : Window
    {
        /// <summary>
        /// arrary to instance  the new sumtheNumbers games
        /// </summary>
        private SumTheNumbers[] newGame ;

        /// <summary>
        /// Field variable to instance of class DispatcherTimer
        /// </summary>
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Field variable to define question timer(2 second/number)
        /// </summary>
        private int questionTimerAct=2;


        /// <summary>
        /// Field variable to define answer timer(5 second/number)
        /// </summary>
        private int AnswerTimerAct = 5;


        /// <summary>
        /// Field variable to define correct account 
        /// </summary>
        private int crtAccount = 0;

        /// <summary>
        /// Field variable to define which timer is using, initial the timer is "question" 
        /// </summary>
        private String TimerStyle = "question";

        /// <summary>
        /// Field variable to define how many times has game been played.
        /// </summary>
        private int playingTimesAct = 0;

        /// <summary>
        /// Field variable to instance stringbuilder 
        /// </summary>
        private StringBuilder builder;

        /// <summary>
        /// Field variable to record the score
        /// </summary>
        private float score;

        /// <summary>
        /// field variables to instance menuItem, label ,grid, textbox
        /// </summary>
        private MenuItem FileMenuItem ;
        private MenuItem ToolsMenuItem ;
        private MenuItem HelpMenuItem ;
        private MenuItem NewGameMenuItem ;
        private MenuItem ExitMenuItem ;
        private MenuItem SeeReportMenuItem ;
        private MenuItem AboutMenuItem ;
        private Label TimerLabel;
        private Grid ShowNumbersGrid;
        private TextBox[] ShowNumbersTextBoxs;

        /// <summary>
        /// read-only propertiey of score*100
        /// </summary>
        public float Score { get { return score*100; } }
        /// <summary>
        /// read-only propertiey of playingTimesAct
        /// </summary>
        public int PlayingTimesAct { get { return playingTimesAct; } }
        /// <summary>
        /// default constructor
        /// </summary>
        public SumTheGamesGUI()
        {
            newGame = new SumTheNumbers[10];

            for (int i = 0; i < newGame.Length; i++)
            {
                newGame[i] = new SumTheNumbers();
            }
            builder = new StringBuilder();    
            InitializeComponent();
            onload();
            
        }

        /// <summary>
        /// onload method, initial the game and GUI
        /// </summary>
        private void onload()
        {
            questionTimerAct = 2;
            AnswerTimerAct = 5;
            AnswerTextBox.Text = null;
            FeedbackHereLabel.Content = "press the Start Game button to start the Game.";
            FeedbackHereLabel.Foreground = Brushes.Black;
            StartGameButton.Content = "Start Game";
            StartGameButton.IsEnabled = true;
            AnswerTextBox.IsEnabled = false;
            AnswerTextBox.BorderBrush = Brushes.Black;
            AnswerTextBox.BorderThickness = new Thickness(0.5);
            CheckAnswerButton.IsEnabled = false;
            GameLevelStackPlane.IsEnabled = true;
            //GameLevel1RadioButton.IsChecked = true;
            
            AddMenuItems();
            AddShowNumbersStackPlane();
            SetUpTimer();
        }

        /// <summary>
        /// adding all MenuItems to MainMenu
        /// </summary>
        private void AddMenuItems()
        {
            MainMenu.Items.Clear();
            
            FileMenuItem = new MenuItem();
            ToolsMenuItem = new MenuItem();
            HelpMenuItem = new MenuItem();

            FileMenuItem.Name = "FileMenuItem";
            FileMenuItem.Header = "_File";
            ToolsMenuItem.Name = "ToolsMenuItem";
            ToolsMenuItem.Header = "_Tools";
            HelpMenuItem.Header = "_Help";

            NewGameMenuItem = new MenuItem();
            ExitMenuItem = new MenuItem();
            SeeReportMenuItem = new MenuItem();
            AboutMenuItem = new MenuItem();

            NewGameMenuItem.Header = "_New Game";
            NewGameMenuItem.Name = "NewGameMenuItem";
            NewGameMenuItem.Click += new RoutedEventHandler(NewGameMeuItem_Click);

            ExitMenuItem.Header = "_Exit";
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Click += new RoutedEventHandler(ExitMenuItem_Click);

            SeeReportMenuItem.Header = "_See Report";
            SeeReportMenuItem.Name = "SeeReportMenuItem";
            SeeReportMenuItem.Click += new RoutedEventHandler(SeeReportMenuItem_Click);

            AboutMenuItem.Name = "AboutMenuItem";
            AboutMenuItem.Header = "_About";
            AboutMenuItem.Click += new RoutedEventHandler(AboutMenuItem_Click);

            FileMenuItem.Items.Add(NewGameMenuItem);
            FileMenuItem.Items.Add(new Separator());
            FileMenuItem.Items.Add(ExitMenuItem);

            ToolsMenuItem.Items.Add(SeeReportMenuItem);

            HelpMenuItem.Items.Add(AboutMenuItem);

            MainMenu.Items.Add(FileMenuItem);
            MainMenu.Items.Add(ToolsMenuItem);
            MainMenu.Items.Add(HelpMenuItem);
        }

        /// <summary>
        /// add all items to ShowNumbersStackPlane
        /// </summary>
        public void AddShowNumbersStackPlane()
        {
            ShowNumberStackPlane.Children.Clear();
            TimerLabel = new Label();
            TimerLabel.Name = "TimerLabel";
            TimerLabel.Content = "Timer will start with New Game";
            TimerLabel.HorizontalAlignment = HorizontalAlignment.Center;

            ShowNumbersGrid = new Grid();

           
            ShowNumbersGrid.HorizontalAlignment = HorizontalAlignment.Center;
            ShowNumbersGrid.VerticalAlignment = VerticalAlignment.Center;
            ShowNumbersGrid.ShowGridLines = true;
            ShowNumbersGrid.Margin = new Thickness(20);
            ShowNumbersGrid.Height = 40;
            ShowNumbersGrid.Width = 300*0.8;
            
            
            
            ColumnDefinition[] ShowNumbersColumnDefinition = new ColumnDefinition[5];
            ShowNumbersTextBoxs = new TextBox[5];

            for (int i = 0; i < ShowNumbersColumnDefinition.Length; i++)
            {
                ShowNumbersColumnDefinition[i]=new ColumnDefinition();
                ShowNumbersGrid.ColumnDefinitions.Add(ShowNumbersColumnDefinition[i]);             

            }

            for (int i = 0; i < ShowNumbersColumnDefinition.Length; i++)
            {

                ShowNumbersTextBoxs[i] = new TextBox();
                ShowNumbersTextBoxs[i].Text = "*";
                ShowNumbersTextBoxs[i].IsReadOnly = true;
                ShowNumbersTextBoxs[i].HorizontalAlignment = HorizontalAlignment.Stretch;
                ShowNumbersTextBoxs[i].VerticalAlignment = VerticalAlignment.Stretch;
                ShowNumbersTextBoxs[i].TextAlignment = TextAlignment.Center;
                
               
                
                ShowNumbersTextBoxs[i].Height = ShowNumbersGrid.Height;
                ShowNumbersTextBoxs[i].Width = ShowNumbersGrid.Width /5* 0.8;
                Grid.SetColumn(ShowNumbersTextBoxs[i], i);
                
                

            }
            for (int i = 0; i < ShowNumbersColumnDefinition.Length; i++)
            {               
                ShowNumbersGrid.Children.Add(ShowNumbersTextBoxs[i]);
            }

                ShowNumberStackPlane.Children.Add(TimerLabel);
                ShowNumberStackPlane.Children.Add(ShowNumbersGrid);
        }

        /// <summary>
        /// set up the game timer
        /// </summary>
        private void SetUpTimer()
        {
            
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        /// <summary>
        /// new Game MenuItem click Handlor
        /// starting the new game
        /// if the array NewGame 's size is not big enough, it will resize the array.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void NewGameMeuItem_Click(object sender, RoutedEventArgs e)
        {
            if (newGame[playingTimesAct].IsPlayed==true)
            {
                playingTimesAct++;
            }
            
            if (newGame.Length < playingTimesAct - 2)
            {
                Array.Resize(ref newGame, (playingTimesAct * 2));
            }
            newGame[playingTimesAct] = new SumTheNumbers();          
            onload();
        }
        /// <summary>
        /// exit menuItem click handlor
        /// close the GUI
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            
        }

        /// <summary>
        /// seeReportMenuItem click handlor
        /// if game is not played, show No report Yet.
        /// in other cases, it will show the game score details
        /// </summary>
        /// <param name="sender">sender inforamtion</param>
        /// <param name="e">routed event arguements</param>
        private void SeeReportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string inf = "";
            if (newGame[0].IsPlayed==false)
            {
                inf = "No report Yet.";

            }
            else
            {
                int act=0;

                if (newGame[playingTimesAct].IsPlayed==true)
                {
                    act=playingTimesAct+1;
                }else
                {
                    act=playingTimesAct;
                }

                inf = Report() + Environment.NewLine + Environment.NewLine
                    + "You win " + crtAccount + " " + TimeOrTimes(crtAccount) + " Vs. Lose " + (act - crtAccount) + " " + TimeOrTimes(act - crtAccount);
                inf+=Environment.NewLine + Environment.NewLine+"The Grade is " + crtAccount + " out of "+ act;
                
               
            
            }
            Application.Current.Properties["reportText"] = inf;
            
            ReportWindow about = new ReportWindow();
            about.Owner = this;
            about.ShowDialog();
        }

        /// <summary>
        /// check which word will show depond on the number
        /// </summary>
        /// <param name="number">times</param>
        /// <returns>time or times(number!=0 or 1)</returns>
        private string TimeOrTimes(int number)
        {
            string inf = "";
            if (number == 0 || number==1)
            {
                inf = "time";
            }
            else
            {
                inf = "times";
            }

            return inf;
        }

        /// <summary>
        /// AboutMenuItem click handlor
        /// show the message box about authors name
        /// </summary>
        /// <param name="sender">sender information</param>
        /// <param name="e">routed event arguments</param>
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Authors name : Qi Zhang" + Environment.NewLine +
                            "   Game Name : Sum the Numbers " + Environment.NewLine +
                            "             Version : 1.0.5 ", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// checkanswer button click handlor
        /// if the user click the button in 5 second, it will show ""You only spend XX second to got the answer "
        /// and check the answer is correct or incorrect
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            String getCustom = AnswerTextBox.Text;

            if (dispatcherTimer.IsEnabled)
            {
                
                TimerLabel.Content = "You only spend " + (5-AnswerTimerAct) + " second to got the answer ";
                ResetTimer();
            }

            string inf = "";
            
            try
            {

                if (newGame[playingTimesAct].CheckAnswer(int.Parse(getCustom)))
                {
                    crtAccount++;
                    inf = "Winner!You are correct!";

                }
                else
                {
                    inf = "Loss!  " + newGame[playingTimesAct].QuestionWithAnswerText();
                }
                

                


            }
            catch (System.OverflowException)
            {
                if (getCustom.Contains("-"))
                {
                    inf = "Loss!  " + "the number entered was too small.";

                }
                else
                {
                    inf = "Loss!  " + "the number entered was too large.";
                }
                
                
            }
            catch (System.FormatException)
            {
                inf = "Loss!  " + "your answer is not Int";       
               
            }
            AnswerTextBox.Text = "";
            AnswerTextBox.IsEnabled = false;
            inf += Environment.NewLine + "Please use Menu to start New Game.";
            FeedbackHereLabel.Content = inf;
            CheckAnswerButton.IsEnabled = false;
            GameLevelStackPlane.IsEnabled = false;
            MainMenu.IsEnabled = true;

            int total = 1; ;
           

                if (newGame[playingTimesAct].IsPlayed == true)
                {
                    total = playingTimesAct + 1;
                }
                else
                {
                    total = playingTimesAct;
                }
                score = float.Parse(crtAccount.ToString()) /  float.Parse(total.ToString());
               
            
           
        }
        /// <summary>
        /// start game button click handler
        /// start the timer and show the question.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            newGame[playingTimesAct].IsPlayed = true;
            dispatcherTimer.Start();
            AnswerTextBox.Text = null;
            AnswerTextBox.Focus();
            StartGameButton.IsEnabled = false;
            GameLevelStackPlane.IsEnabled = false;
            MainMenu.IsEnabled = false;

        }

        /// <summary>
        /// timer Tick
        /// set the information on the feedback label
        /// there are two kinds of timers, one is question timer(2 second/question), another is answer timer(5s/answer)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            string inf = "";
            TimerLabel.Foreground = Brushes.Red;
            FeedbackHereLabel.Content = "Game is Playing";
            if (FeedbackHereLabel.Foreground == Brushes.Black)
            {
                FeedbackHereLabel.Foreground = Brushes.Red;
            }
            else
            {
                FeedbackHereLabel.Foreground = Brushes.Black;
            }
           
            switch(TimerStyle)
            {
                case "question":
                    if (questionTimerAct != 0 && AnswerTimerAct != 0)
                    {
                        foreach (TextBox number in ShowNumbersTextBoxs)
                        {
                            number.Text = "*";

                        }
                        TimerLabel.FontWeight = FontWeights.ExtraBold;
                        ShowNumbersTextBoxs[5 - AnswerTimerAct].Text = newGame[playingTimesAct].Numbers.ElementAt(5 - AnswerTimerAct).ToString();
                        inf = questionTimerAct + " second";
                        
                        questionTimerAct--;

                    }
                    else if (questionTimerAct == 0 && AnswerTimerAct != 0)
                    {

                        
                        inf = questionTimerAct + " second";
                        questionTimerAct = 2;                        
                        AnswerTimerAct--;
                    }
                    else if (AnswerTimerAct == 0)
                    {
                        questionTimerAct = 2;
                        AnswerTimerAct = 5;
                        inf = "Please answer the question in "  + AnswerTimerAct + " second";
                        AnswerTimerAct--;

                        foreach (TextBox number in ShowNumbersTextBoxs)
                        {
                            number.Text = "*";

                        }
                        TimerStyle = "answer";
                        AnswerTextBox.IsEnabled = true;
                        AnswerTextBox.BorderThickness = new Thickness(2);
                        AnswerTextBox.BorderBrush=Brushes.Red;
                        CheckAnswerButton.IsEnabled = true;


                    }

                    break;

                case "answer":
                    //MessageBox.Show(AnswerTextBox.BorderBrush.ToString());
                    if (AnswerTextBox.BorderBrush == Brushes.Red)
                    {
                        AnswerTextBox.BorderBrush = Brushes.Black;
                    }
                    else
                    {
                        AnswerTextBox.BorderBrush = Brushes.Red;
                    }
                    

                    if (AnswerTimerAct != 0)
                    {
                        inf = "Please answer the question in " + AnswerTimerAct + " second";
                        if (AnswerTextBox.IsFocused == false)
                        {
                            AnswerTextBox.Focus();
                        }
                        AnswerTimerAct--;

                    }
                    else 
                    {
                       
                        TimerLabel.Foreground = Brushes.Blue;
                        inf = "Time is Over";
                        CheckAnswerButton_Click(this, null);                       
                        CheckAnswerButton.IsEnabled = false;
                        ResetTimer();

                    }
                    
                    break;
            }          
 
            TimerLabel.Content = inf;
        }

        /// <summary>
        /// game level radio Button click handlor
        /// set NewGame's select game level
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void GameLevelRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectGameLevel = (sender as RadioButton);
            newGame[playingTimesAct].SelectGameLevel = int.Parse(selectGameLevel.Content.ToString().ToCharArray().ElementAt(6).ToString()) - 1;
           
        }

        /// <summary>
        /// return the answer information
        /// </summary>
        /// <returns>answer information</returns>
        private string showAnswerToString()
        {
            string inf = "";
            for (int i = 0; i < newGame[playingTimesAct].Answer.Length; i++)
            {
                inf += newGame[playingTimesAct].Answer.ElementAt(i) + " ";
            }
            return inf;
        }

        /// <summary>
        /// reset the timer
        /// </summary>
        private void ResetTimer()
        {
            questionTimerAct = 2;
            AnswerTimerAct = 5;
            dispatcherTimer.Stop();
            TimerStyle = "question";
        }

        /// <summary>
        /// create the game report
        /// </summary>
        /// <returns>return game report</returns>
        public string Report()
        {
            builder.Clear();
            foreach (SumTheNumbers game in newGame)
            {
                if (game.IsPlayed == true)
                {
                    builder.Append(game.QuestionWithAnswerText()).Append(" ").Append(" Your answer was ");
                    builder.Append(game.AnswerAttempt).Append(System.Environment.NewLine); // or use AppendLine()

                }
            }

            
            
            return builder.ToString();
        }

        /// <summary>
        /// get the times which has played
        /// </summary>
        /// <returns>play times</returns>
        public int GetPlayTimes()
        {
            int account = 0;
            foreach (SumTheNumbers game in newGame)
            {
                if (game.IsPlayed == true)
                {
                    account++;

                }
            }

            return account;

        }

        


    }
}
