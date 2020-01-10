using System;
using System.IO;
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
    /// Logica di interazione per Caricamento.xaml
    /// </summary>
    public partial class Caricamento : Window
    {
        static MainWindow mn = new MainWindow();

          int nErrori = 0; //Numero di errori che si incrementerà
          string lingua = mn.cbLingua.Text; //Lingua scelta
          string difficolta = mn.cbDifficolta.Text; //Difficoltà scelta
          string modalita = mn.cbModalita.Text; //Malità scelta
          string nGiocatori = mn.txtNGiocatori.Text; //Numero giocatori
        public Caricamento()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Starta
            lblCaricamento.Content = "Caricamento WDB";

            string[] inpFile = new string[3];

            lingua[0] = lingua[0].ToString().ToUpper();
            inpFile[0] = lingua + ".wdb";
            inpFile[0][0] = inpFile[0][0].ToString().ToUpper();
        }
    }
}
