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
            BitmapImage image = new BitmapImage(new Uri(@"\Immagini\Impicato err" + nErrori + ".png", UriKind.Relative));

            ImmagineImpiccato.Source = image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAiuti_Click(object sender, RoutedEventArgs e)
        {
            if(lingua == "Italiano")
            {
                if(difficolta == "Facile")
                {
                    string[,] italianoF;
                    italianoF = new string[2, 10];
                }
                else if (difficolta == "Media")
                {
                    string[,] italianoM;
                    italianoM = new string[2, 10];
                }
                else
                {
                    string[,] italianoD;
                    italianoD = new string[2, 10];
                }
            } else if(lingua == "Inglese")
            {
                if (difficolta == "Facile")
                {
                    string[,] ingleseF;
                    ingleseF = new string[2, 10];
                }
                else if (difficolta == "Media")
                {
                    string[,] ingleseM;
                    ingleseM = new string[2, 10];
                }
                else
                {
                    string[,] ingleseD;
                    ingleseD = new string[2, 10];
                }
            }
            else
            {
                if (difficolta == "Facile")
                {
                    string[,] spagnoloF;
                    spagnoloF = new string[2, 10];

                }
                else if (difficolta == "Media")
                {
                    string[,] spagnoloM;
                    spagnoloM = new string[2, 10];
                }
                else
                {
                    string[,] spagnoloD;
                    spagnoloD = new string[2, 10];
                }
            }

        }
    }
}
