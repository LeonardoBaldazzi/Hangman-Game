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
    /// Logica di interazione per Impostazioni.xaml
    /// </summary>
    public partial class Impostazioni : Window
    {
        //Variabili globali
        public static bool effetti = true;
        public static bool meme = false;
		
        public Impostazioni()
        {
            InitializeComponent();

            //Aggiorna i checkbox
            chEffetti.IsChecked = effetti;
            chMeme.IsChecked = meme;
        }

        private void btnChiudi_Click(object sender, RoutedEventArgs e)
        {
            //Aggiorna le variabili globali
            effetti = (bool)chEffetti.IsChecked;
            meme = (bool)chMeme.IsChecked;

            this.Close(); //Chiudi la finestras
        }
    }
}
