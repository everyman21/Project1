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

namespace Project01
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class GameRecordWindow : Window
    {
        /// <summary>
        /// store the report
        /// </summary>
        private string path;

        /// <summary>
        /// Constructor, initializes component
        /// </summary>
        public GameRecordWindow()
        {
            InitializeComponent();
        }

        

        /// <summary>
        /// overload the constructor,pass report to this.report
        /// </summary>
        /// <param name="report">the report string</param>
        public GameRecordWindow(String path) : this()
        {
            this.path = path;
           
        }

        /// <summary>
        /// the handlor of report window loaded
        /// if the reportText is not null, the infomration in reportText will pass to Report string
        /// and Text of TestReportTextBox will be report infromation
        /// </summary>
        /// <param name="sender">sender information</param>
        /// <param name="e">routed Event arguments </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TestReportTextBox.Text = System.IO.File.ReadAllText(path);
        }
     }
}
