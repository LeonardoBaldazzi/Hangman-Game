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

namespace impiccato_v1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Impiccato impiccato = new Impiccato();

            impiccato.Show();
        }

        private void CbModalita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pn1.Visibility = Visibility.Visible;
        }

        private void CbDifficolta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pnLingua.Visibility = Visibility.Visible;
        }

        private void CbLingua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnGioca.IsEnabled = true;
        }
    }
}
