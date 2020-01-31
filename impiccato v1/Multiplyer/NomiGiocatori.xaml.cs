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

namespace impiccato_v1.Multiplyer
{
    /// <summary>
    /// Logica di interazione per NomiGiocatori.xaml
    /// </summary>
    public partial class NomiGiocatori : Window
    {
        //Variabili globali
        private string[] nomi;

        public NomiGiocatori(string[] n)
        {
            int i = 1;
            nomi = n;

            InitializeComponent();

            //Scrivi dentro la txtbox
            foreach(string a in nomi)
            {
                txtNomiGioc.Text += "Giocatore " + i + " : " + a + Environment.NewLine;

                i++;
            }
        }

        private void Btnchiudi_Click(object sender, RoutedEventArgs e) //Chiudi tutto
        {
            this.Close();
        }
    }
}
