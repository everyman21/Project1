/* 
 * Author: Qi Zhang
 * upate by : Andrew Goenenberg, Matt Murphey
 * Date: 2013 - 10 - 20
 * Description: Project 01 - Brain Games
 * Verion `1.0.5
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// field variable for instance game names
        /// </summary>
        private String[] GameNames = new String[3] { "SumTheNumbers", "SumTheSelected", "PrimeCheck" };///add by Qi,Andrew,Matt

        /// <summary>
        /// field variable for intance game describes for each game
        /// </summary>
        private StringBuilder[] GameDes=new  StringBuilder[3];

        /// <summary>
        /// field variables for MenuItems
        /// </summary>
        private MenuItem FileMenuItem ;
        private MenuItem GameRecordMenuItem ;
        private MenuItem HelpMenuItem ;

        private MenuItem[] NewGamesMenuItem ;
        private MenuItem ExitMenuItem ;
        private MenuItem[] GameRecord ;
        private MenuItem AboutMenuItem ;

        /// <summary>
        /// field variables for instance game record reader
        /// </summary>
        private GameRecordReader[] gameRecordReaders;


        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            OnLoad();
           
                     
        }

        /// <summary>
        /// onload method
        /// 
        /// initial GUI and create the txt file of each game
        /// </summary>
        private void OnLoad()
        {
            AddMainMenuItem();
            AddMaxScoreToGamesInfTextBox();
            CreateGameDes();
        }

        /// <summary>
        /// add the menuItems to MainMenu
        /// </summary>
        private void AddMainMenuItem()
        {
            MainMenu.Items.Clear();
            FileMenuItem = new MenuItem();
            GameRecordMenuItem = new MenuItem();
            HelpMenuItem = new MenuItem();


            FileMenuItem.Header = "_File";
            GameRecordMenuItem.Header = "_Game Record";
            HelpMenuItem.Header = "_Help";

            NewGamesMenuItem = new MenuItem[GameNames.Length];
            ExitMenuItem = new MenuItem();
            GameRecord = new MenuItem[GameNames.Length];
            gameRecordReaders = new GameRecordReader[GameNames.Length];
            AboutMenuItem = new MenuItem();


            for (int i = 0; i < GameNames.Length; i++)
            {
                NewGamesMenuItem[i] = new MenuItem();


                NewGamesMenuItem[i].Name = "NewGame" + GameNames[i] + "MenuItem";
                NewGamesMenuItem[i].Header = "New _" + GameNames[i] + " Game";
                NewGamesMenuItem[i].Click += new RoutedEventHandler(NewGameMenuItem_Click);
                FileMenuItem.Items.Add(NewGamesMenuItem[i]);

                GameRecord[i] = new MenuItem();
                GameRecord[i].Header = "Record of _" + GameNames[i];
                GameRecord[i].Name = "Game" + GameNames[i] + "RecordMenuItem";
                GameRecord[i].Click += new RoutedEventHandler(GameRecordMenuItem_Click);
                GameRecordMenuItem.Items.Add(GameRecord[i]);

                gameRecordReaders[i] = new GameRecordReader(GameNames[i]);
                
               
                
            }

            FileMenuItem.Items.Add(new Separator());
            FileMenuItem.Items.Add(ExitMenuItem);

            ExitMenuItem.Header = "_Exit";
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Click += new RoutedEventHandler(ExitMenuItem_Click);

            AboutMenuItem.Name = "AboutMenuItem";
            AboutMenuItem.Header = "_About";
            AboutMenuItem.Click += new RoutedEventHandler(AboutMenuItem_Click);

            HelpMenuItem.Items.Add(AboutMenuItem);


            MainMenu.Items.Add(FileMenuItem);
            MainMenu.Items.Add(GameRecordMenuItem);
            MainMenu.Items.Add(HelpMenuItem);

        }

        /// <summary>
        /// set max score of each game to their textbox
        /// </summary>
        private void AddMaxScoreToGamesInfTextBox()
        {
            for (int i = 1; i < 4; i++)
            {
                String inf = " Game " + i + " Max Score " + Environment.NewLine;
                if (gameRecordReaders[i - 1].GetOldList().Count != 0)
                {
                    inf += gameRecordReaders[i - 1].GetOldList().First().ToString();
                }
                else
                {
                    inf += "Max Score is not available";
                }
                
                SetGameInfTextBox(i, inf,Brushes.Black);
            }
            
        }

        /// <summary>
        /// create game descirbe for each game
        /// </summary>
        private void CreateGameDes()
        {
            for (int i = 0; i < GameDes.Length;i++ )
            {
                GameDes[i] = new StringBuilder();
            }

            GameDes[0].AppendLine("Sum the Numbers:");// add by Qi
            GameDes[0].AppendLine(" > 5 random numbers from 100 to 999");
            GameDes[0].AppendLine(" > show one number / 2 second");
            GameDes[0].AppendLine(" > enter the answer in 5 second");
            GameDes[0].AppendLine(" > 3 Levels: ");
            GameDes[0].AppendLine("             1) sum of last two numbers");
            GameDes[0].AppendLine("             2) sum of last three numbers");
            GameDes[0].AppendLine("             3) sum of last four numbers");

            GameDes[1].AppendLine("Sum the Selected: \n > 5 random numbers will be colored red \n > the user must enter the sum of \n > those numbers as quickly as possible! \n"+
                " 3 Levels of difficulty:\n    1) 1-9 \n    2) 100-999 \n    3) 1000-9999");//add by Andrew

            GameDes[2].AppendLine("PrimeTime: \n > A random numbers will be displayed\n >the user must identify if the number is a  \n >prime number or not quickly as possible! \n" +
                " 3 Levels of difficulty:\n      1) 1-9 \n      2) 10-99 \n      3) 100-999");//add by Matt
            
        }
        /// <summary>
        /// new game menuItem click handlor
        /// check which game is selected and play that game
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void NewGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem newgame = sender as MenuItem;
            string selectGameName = "";
            if (sender is MenuItem)
            {
                foreach (string name in GameNames)
                {
                    if (newgame.Name.Contains(name))
                    {
                        selectGameName = name;
                    }
                }

                GamePlaying(selectGameName);

            }
            
        }

        /// <summary>
        /// Play the select game
        /// instance the select game and get the sorce form the window
        /// </summary>
        /// <param name="selectGameName">the select game name</param>
        private void GamePlaying(String selectGameName)
        {
            int index = 0;
            for(int i=0;i<GameNames.Length;i++)
            {
                if (selectGameName==GameNames[i])
                {
                    index = i;
                }
            }
            
            switch (index)
            {
               //add by Qi
                case 0:
                    SumTheGamesGUI game = new SumTheGamesGUI();

                    game.Owner = this;

                    if (!game.ShowDialog().Value)
                    {

                        if (game.GetPlayTimes() != 0)
                        {

                            foreach (GameRecordReader reader in gameRecordReaders)
                            {
                                if (reader.Path.Contains(selectGameName))
                                {
                                    reader.UpdateNewScoreToFile(game.Score, DateTime.Now);
                                    // MessageBox.Show(game.Score.ToString());
                                }
                            }
                        }
                    }

                    break;
                //add by Andrew
                case 1: 
                 SumTheSelected game2 = new SumTheSelected();

                    game2.Owner = this;

                    if (!game2.ShowDialog().Value)
                    {

                        if (game2.GetPlayTimes() != 0)
                        {

                            foreach (GameRecordReader reader in gameRecordReaders)
                            {
                                if (reader.Path.Contains(selectGameName))
                                {
                                    reader.UpdateNewScoreToFile(game2.GameScore, DateTime.Now);
                                    // MessageBox.Show(game.Score.ToString());
                                }
                            }
                        }
                    }break;


                //add by Matt
                case 2:
                    PrimeCheckGUI game1 = new PrimeCheckGUI();

                    game1.Owner = this;

                    if (!game1.ShowDialog().Value)
                    {

                        if (game1.GetPlayTimes() != 0)
                        {

                            foreach (GameRecordReader reader in gameRecordReaders)
                            {
                                if (reader.Path.Contains(selectGameName))
                                {
                                    reader.UpdateNewScoreToFile(game1.GameScore, DateTime.Now);
                                    // MessageBox.Show(game.Score.ToString());
                                }
                            }
                        }
                    }break;


            }
        }

        /// <summary>
        /// exit menu item click handler
        /// close the GUI
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// game recodr menu item click Handlor
        /// read the select game txt file and show the report
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void GameRecordMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string selectGameName = "";
            MenuItem senderItem = (sender as MenuItem);
            GameRecordWindow about ;
            
            foreach (string name in GameNames)
            {
                if (senderItem.Name.Contains(name))
                {
                    selectGameName = name;
                }
            }

            foreach (GameRecordReader reader in gameRecordReaders)
            {
                if (reader.Path.Contains(selectGameName))
                {
                    about = new GameRecordWindow(reader.Path);
                    about.Title = "Game Record: " + selectGameName;
                    about.Owner = this;
                    about.ShowDialog();
                }
            }
           
           
        }


        /// <summary>
        /// AboutMenuItem click handlor
        /// show the message box about authors name
        /// </summary>
        /// <param name="sender">sender information</param>
        /// <param name="e">routed event arguments</param>
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            
            builder.AppendLine("Team Members : Andrew Groenenberg, Matt Murphey and Qi Zhang  ").AppendLine();
            builder.AppendLine(" Main Window Design By : Qi Zhang").AppendLine();
            builder.AppendLine(" Graphics by :Matt Murphey").AppendLine();
            builder.AppendLine(" Updated by : Andrew Groenenberg and Matt Murphey").AppendLine();
            builder.AppendLine(" Version : 1.0.5 ");
            
            MessageBox.Show( builder.ToString(), "About MainWindow", MessageBoxButton.OK, MessageBoxImage.Information);
        }

       
        /// <summary>
        /// game Image Mouser enter handlor
        /// enlarge the image ,change the mouse cursor and show the game describe in gameinfTextbox when the mouse entre 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private void GameImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Image newsender=(sender as Image);
            newsender.Width =140;
            Cursor = Cursors.Hand;
            newsender.Height = newsender.Width;
            int number = int.Parse(newsender.Name.ToArray().ElementAt(4).ToString());
            SetGameInfTextBox(number, GameDes[number-1].ToString(), Brushes.Red);
            

        }

        /// <summary>
        /// game Image Mouser leave handlor
        /// reset the image ,change the mouse cursor and show the max game score in gameinfTextbox when the mouse leave 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private void GameImage_MouseLeave(object sender, MouseEventArgs e)
        {
            Image newsender = (sender as Image);
            newsender.Width = 100;
            newsender.Height = newsender.Width;
            AddMaxScoreToGamesInfTextBox();
            Cursor = Cursors.Arrow;

        }

        /// <summary>
        /// game Image Mouse left button click handlor
        /// play the select game
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private void GameImage_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Image newsender = (sender as Image);            
            GamePlaying(GameNames[int.Parse(newsender.Name.ToArray().ElementAt(4).ToString())-1]);

        }

        /// <summary>
        /// set game infromation text box infomation
        /// </summary>
        /// <param name="number">game's number</param>
        /// <param name="inf">the inforamtion will be shown</param>
        /// <param name="color">textbox text's color</param>
        private void SetGameInfTextBox(int number,string inf,Brush color)
        {
            switch (number)
            {
                case 1: Game1InfTextBox.Text = inf;Game1InfTextBox.Foreground=color ; break;
                case 2: Game2InfTextBox.Text = inf; Game2InfTextBox.Foreground = color; break;
                case 3: Game3InfTextBox.Text = inf; Game3InfTextBox.Foreground = color; break;

            }
        }
    }
}
