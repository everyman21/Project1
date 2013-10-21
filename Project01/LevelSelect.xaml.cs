/* LevelSelect - allows the user to select which level they'd like to play
 * Level is then stored in an application variable and this windows is closed
 * @Author: Andy Groenenberg
 * @Date:2013/10/20
 * @Version:1.0
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

namespace Project01
{
    /// <summary>
    /// Interaction logic for LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {
       

       private int level;
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
                    // should never pop as level 1 is selected by default forcing user to change it
                    MessageBox.Show("No level selcted - Level 1 will be initiated by default","Error: No Level Selected");
                    return 1;
                }
            }
        }


 public LevelSelect()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Level"] = Level;
            this.Close();
        }
    }
}
