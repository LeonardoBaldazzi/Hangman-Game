using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            pn1.Visibility = Visibility.Hidden;
            pnLingua.Visibility = Visibility.Hidden;
            pnNGiocatori.Visibility = Visibility.Hidden;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Impiccato impiccato = new Impiccato();

            impiccato.Show(); //Apriu una nuova schermata
            this.Close(); //Chiudi questa schermata
        }

        private void CbModalita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pn1.Visibility = Visibility.Visible;

            Debug.WriteLine(cbModalita.Text);

            if (cbModalita.SelectedIndex == 2)
                pnNGiocatori.Visibility = Visibility.Visible;
            else
                pnNGiocatori.Visibility = Visibility.Hidden;
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
