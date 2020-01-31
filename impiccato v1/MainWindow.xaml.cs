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

            System.IO.Directory.SetCurrentDirectory(@".\"); //Serve per fare un reset della cartella corrente di lavoro
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try //Gestione Errori
            {
                int lav;
                if (cbModalita.Text == "Multiplayer (Locale)" && (!int.TryParse(txtNGiocatori.Text, out lav) || lav <= 1 || lav > 10))
                {
                    MessageBox.Show("Numero degli utenti sbagliato", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    Caricamento carica = new Caricamento(cbLingua.Text, cbDifficolta.Text, cbModalita.Text, txtNGiocatori.Text); //Passa al nuovo form i contenuti delle combobox e dei txtbox

                    carica.Show(); //Apri la schermata di caricamento
                    this.Close(); //Chiudi questa schermata
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void CbModalita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try //Gestione errori
            {
                pn1.Visibility = Visibility.Visible;

                Debug.WriteLine(cbModalita.Text);

                if (cbModalita.SelectedIndex == 2)
                    pnNGiocatori.Visibility = Visibility.Visible;
                else
                    pnNGiocatori.Visibility = Visibility.Hidden;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void CbDifficolta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pnLingua.Visibility = Visibility.Visible;
        }

        private void CbLingua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnGioca.IsEnabled = true;
        }

        private void btnImpostazione_Click(object sender, RoutedEventArgs e)
        {
            Impostazioni imp = new Impostazioni();
            imp.Show();
        }
    }
}
