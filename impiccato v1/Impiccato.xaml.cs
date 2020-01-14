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

namespace impiccato_v1
{
    /// <summary>
    /// Logica di interazione per Impiccato.xaml
    /// </summary>
    /// 

    public partial class Impiccato : Window
    {
        static MainWindow mn = new MainWindow();

        internal static int nErrori = 0; //Numero di errori che si incrementerà
        internal static string lingua = mn.cbLingua.Text; //Lingua scelta
        internal static string difficolta = mn.cbDifficolta.Text; //Difficoltà scelta
        internal static string modalita = mn.cbModalita.Text; //Malità scelta
        internal static string nGiocatori = mn.txtNGiocatori.Text; //Numero giocatori

        
        public Impiccato()
        {
            InitializeComponent();

            Aggiorna();
        }

        private void Aggiorna()
        {
            //Aggiorna il link dell'immagines
            BitmapImage image = new BitmapImage(new Uri(@"\Immagini\Impicato err" + nErrori + ".png", UriKind.Relative));

            ImmagineImpiccato.Source = image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAiuti_Click(object sender, RoutedEventArgs e)
        {
	    //Fai visualizzare il form con gli aiuti
        }
    }
}
