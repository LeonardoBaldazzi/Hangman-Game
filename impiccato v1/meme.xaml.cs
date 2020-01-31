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
    /// Logica di interazione per meme.xaml
    /// </summary>
    public partial class meme : Window
    {
        public meme()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mn = new MainWindow(); //Torna alla schermata iniziale
                mn.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }
    }
}
