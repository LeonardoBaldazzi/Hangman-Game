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
using System.Windows.Shapes;

namespace impiccato_v1
{
    /// <summary>
    /// Logica di interazione per Impiccato.xaml
    /// </summary>
    /// 

    public partial class Impiccato : Window
    {
        private int nErrori = 0;
        bool presenzaLettera = false;
        private char[] parolaModificata;
        private string parolaGenerata, aiuto, parolaCodificata;
        private int k = 0;
        private char[] lettereGia = new char[26]; //Lettere già inserite

        public Impiccato(string parolaGen, string aiut)
        {
            try //Gestione Errori!
            {
                InitializeComponent();

                //Scrivi la parola codificata nella label
                parolaCodificata = GeneraParolaPiuEMeno(Caricamento.parolaGenerata);
                lblParola.Content = parolaCodificata;

                lblLunghezza.Content = "Lunhezza parola: " + Caricamento.parolaGenerata.Length;

                if (Caricamento.difficolta == "Difficile") //Se la difficoltà è difficile allora non mostrare gli aiuti
                {
                    btnAiuti.Visibility = Visibility.Hidden;
                }

                if (Caricamento.modalita == "Singleplayer") //Se la modalità è singleplayer allora non mostrare il turno
                {
                    lblTurno.Visibility = Visibility.Hidden;
                }

                parolaGenerata = parolaGen;
                aiuto = aiut;

                //Scrivi in debug!
                Debug.WriteLine("");
                Debug.WriteLine("Parola codificata: " + lblParola.Content);
                Debug.WriteLine("Parola non codificata: " + parolaGenerata);
                Debug.WriteLine("Aiuto: " + aiuto);
                Debug.WriteLine("");

                Aggiorna();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void Aggiorna()
        {
            //Aggiorna il link dell'immagines
            BitmapImage image = new BitmapImage(new Uri(@"\Immagini\Impicato err" + nErrori + ".png", UriKind.Relative));

            ImmagineImpiccato.Source = image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try //Gestione Errori
            {
                //Conferma quello scritto dentro la txtBoc "txtInputLettera", il metodo controlla che quello scritto è una lettera o una parola, se lettera vede se è una lettera che si trova dentro la prola, altrimenti confronta la parola inserita con quella generata
                if (txtInputLettera.Text.Length == 1) //E' una lettera
                {
                    presenzaLettera = false;
                    for (int i = 0; i < lettereGia.Length; i++)
                    {
                        if (Convert.ToChar(txtInputLettera.Text) == lettereGia[i])
                        {
                            i = lettereGia.Length;
                            presenzaLettera = true;
                        }
                    }

                    if (presenzaLettera == false) // entra se la lettera NON è gia uscita
                    {
                        lettereGia[k] = Convert.ToChar(txtInputLettera.Text);

                        k++;

                        for (int i = 0; i < parolaGenerata.Length; i++)
                        {
                            if (parolaGenerata[i] == Convert.ToChar(txtInputLettera.Text))
                            {
                                //La lettera è buona, serve un metodo che riscrive la parola con la lettera scelta
                                parolaModificata[i] = Convert.ToChar(txtInputLettera.Text);

                            }
                            else
                            {
                                nErrori++;

                                Aggiorna(); //Aggiorna l'immagine dell'impiccato
                            }
                        }
                    }
                    else
                    {
                        nErrori++;

                        Aggiorna();
                    }
                }
                else //inserisce una parola
                {
                    if (txtInputLettera.Text == parolaGenerata)
                    {
                        //Hai vinto!!! (serve un metodo)

                    }
                    else
                    {
                        //parola sbagliata
                        nErrori++;

                        Aggiorna();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private string GeneraParolaPiuEMeno(string parolaGenerata) //Genera la parola con i più e i meno (prima lettera visibile)
        {
            try
            {
                parolaModificata = new char[parolaGenerata.Length];

                char primoCarattere = Convert.ToChar(parolaGenerata[0].ToString().ToUpper()); //Errore!

                char[] alfaConsonanti = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
                char[] alfaVocali = { 'a', 'e', 'i', 'o', 'u' };

                for (int i = 0; i < parolaGenerata.Length; i++)
                {
                    if (i == 0)
                    {
                        parolaModificata[0] = primoCarattere; //Metti il primo carattere maiuscolo
                    }
                    else
                    {
                        if (Array.IndexOf(alfaVocali, parolaGenerata[i]) >= 0)
                        {
                            parolaModificata[i] = '+'; //+ = vocale
                        }
                        else
                        {
                            parolaModificata[i] = '-'; //- = consonante
                        }
                    }
                }

                string toReturn = new string(parolaModificata); //Da array a stringa


                return toReturn;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!

                return " ERRORE ";
            }
        }

        private void BtnAiuti_Click(object sender, RoutedEventArgs e)
        {
	        //Fai visualizzare il form con gli aiuti
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { }
    }
}
