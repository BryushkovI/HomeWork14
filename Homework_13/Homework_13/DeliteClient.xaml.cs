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

namespace Homework_13
{
    /// <summary>
    /// Логика взаимодействия для DeliteClient.xaml
    /// </summary>
    public partial class DeliteClient : Window
    {
        MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
        public DeliteClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Function function = new Function();
            function.DeliteClient(mainWindow);
            Close();
            mainWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            mainWindow.Show();
        }
    }
}
