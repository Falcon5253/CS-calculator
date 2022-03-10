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

namespace CS_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow a = new TaskWindow();
            a.Show();
        }
        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string expression = text.Text;
            calculate(expression);
        }
        public void calculate(string expression)
        {
            Console.WriteLine("Пока робит");
        }
    }
}
