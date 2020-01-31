using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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

    public partial class ImpiccatoSingleplayer : Window
    {
        //Variabili globali
        private int nErrori = 0;
        bool presenzaLettera = false;
        private char[] parolaModificata;
        private string parolaGenerata, aiuto, parolaCodificata;
        private int k = 0;
        private char[] lettereGia = new char[26]; //Lettere già inserite

        private int lettereIndovinate = 1; //Conatore per le lettere indovinate

        //Oggetti globali
        private WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer(); //Oggetto per il suono
        private Random rnd = new Random(); //Oggetto per il random

        public ImpiccatoSingleplayer(string parolaGen, string aiut)
        {
            try //Gestione Errori!
            {
                Directory.SetCurrentDirectory("../Sounds"); //Directory per i suoni

                InitializeComponent();

                //Scrivi la parola codificata nella label
                parolaCodificata = GeneraParolaPiuEMeno(Caricamento.parolaGenerata);
                lblParola.Content = parolaCodificata;

                if (Caricamento.difficolta == "Difficile") //Se la difficoltà è difficile allora non mostrare gli aiuti
                {
                    btnAiuti.Visibility = Visibility.Hidden;
                }

                parolaGenerata = parolaGen;
                aiuto = aiut;

                //Scrivi in debug!
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
            try //Gestione errori
            {
                //Aggiorna il link dell'immagini
                BitmapImage image = new BitmapImage(new Uri(@"\Immagini\Impicato err" + nErrori + ".png", UriKind.Relative));

                ImmagineImpiccato.Source = image;

                if (nErrori > 0)
                    btnAiuti.IsEnabled = true; //Abilita il bottone soltanto quando ho fatto il primo errore

                if (nErrori == 6)
                {
                    //Hai perso!
                    int n = 1, numFile = 1;

                    //C'è un suono random che viene riprodotto (L'utente può integrarli inserendo un file che inizi con "Lose ")
                    DirectoryInfo d = new DirectoryInfo(@"../Sounds/");
                    FileInfo[] Files = d.GetFiles("*.wav");

                    //Prendi il numero di file che inizia con "Lose"
                    if (Files.Length > 0)
                    {
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.StartsWith("Lose "))
                                numFile++;
                        }
                    }
                    else
                    {
                        //Errore
                        throw new Exception("Non è presente nessun file audio nella cartella './Sounds/'");
                    }

                    n = rnd.Next(1, numFile); //File random

                    Debug.WriteLine("Riproduco il file numero: " + n.ToString());

                    RiproduciAudio("Lose " + n.ToString() + ".wav"); //Riproduci l'audio

                    lblParola.Content = parolaGenerata; //Qual'era la parola?

                    MessageBox.Show("Hai perso :(");

                    if (Impostazioni.meme) //Controlla se l'utente ha scelto di essere preso in giro
                    {
                        meme m = new meme();
                        m.Show();
                    }
                    else //Non è stato preso in giro
                    {
                        MainWindow mn = new MainWindow(); //Torna alla schermata iniziale
                        mn.Show();
                    }

                    this.Close();

                    Directory.SetCurrentDirectory("../"); //Ritorna alla directory principale
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void btnConferma_Click(object sender, RoutedEventArgs e)
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

                        for (int i = 1; i < parolaGenerata.Length; i++) //Non prende in cosiderazione la prima lettera visto che la sappiamo già
                        {
                            if (parolaGenerata[i] == Convert.ToChar(txtInputLettera.Text))
                            {
                                //La lettera è buona, serve un metodo che riscrive la parola con la lettera scelta
                                parolaModificata[i] = Convert.ToChar(txtInputLettera.Text);

                                RiproduciAudio("Indovinata.wav");

                                lettereIndovinate++;
                            }
                        }

                        if(Array.IndexOf(parolaModificata, Convert.ToChar(txtInputLettera.Text)) < 0) //Se non è presente
                        {
                            RiproduciAudio("Err.wav");

                            nErrori++;

                            Aggiorna();
                        }
                    }
                    else //La lettera è già stata inserita
                    {
                        RiproduciAudio("Err.wav");

                        nErrori++;

                        Aggiorna();
                    }
                }
                else //E' stata inserita una parola
                {
                    if (txtInputLettera.Text.ToLower() == parolaGenerata.ToLower() || lettereIndovinate == parolaGenerata.Length) //Così se l'utente scrive con lettere maiuscole e minuscole la parola viene accettata lo stesso
                    {
                        //Hai vinto!!!
                        RiproduciAudio("Win.wav");

                        MessageBox.Show("Hai Vinto!");

                        MainWindow mn = new MainWindow(); //Torna alla schermata iniziale
                        mn.Show();

                        this.Close();
                    }
                    else
                    {
                        //parola sbagliata
                        RiproduciAudio("Err.wav");

                        nErrori++;

                        Aggiorna();
                    }
                }

                lblParola.Content = new string(parolaModificata); //Aggiorna la label della parola
                txtInputLettera.Text = ""; //Cancella il contenuto della lettera
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void RiproduciAudio(string p) //Metodo che riproduce un file
        {
            try
            {
                //Controlla se l'utente ha scelto di avere gli effetti
                if (Impostazioni.effetti)
                {
                    player.URL = p; //Posizione del file da riprodurre
                    player.controls.play(); //Play del file
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

                char primoCarattere = Convert.ToChar(parolaGenerata[0].ToString().ToUpper()); //Prima lettera è visibile (maiscola)

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
                        if (parolaGenerata[i] != ' ')
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
                        else //Se il carattere è uno spazio
                        {
                            parolaModificata[i] = ' ';
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

        private void txtInputLettera_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtInputLettera.Text.Length > 0)
            {
                btnConferma.IsEnabled = true;
            }
            else
            {
                btnConferma.IsEnabled = false;
            }
        }

        private void BtnAiuti_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(aiuto, "Un'aiuto per te!", MessageBoxButton.OK, MessageBoxImage.Question); //Fai visualizzare il form con gli aiuti
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) //Metodo inutile
        { }
    }
}
