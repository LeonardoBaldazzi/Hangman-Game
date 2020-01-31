using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
//using System.Timers; //Serve per capire quando passare il turno al processore
using System.Threading;  //Serve per capire quando passare il turno al processore

namespace impiccato_v1.Multiplyer
{
    public partial class Impiccaato_Multiplayer : Window
    {
        //Variabili globali
        private string parolaGenerata, aiuto, difficolta, nGiocatori, modalita; //Variabili passate dall'altro form
        private char[] parolaCodificata;

        private int lettereIndovinate = 1; //Questo perchè la prima lettera la sappiamo già

        private bool isVsCPU = false;

        string[] giocatoriNomi; //nomi effettivi dei giocatori

        private char[] parolaModificata; //Array che serve per il metodo della generazione delle parole
        private char[] letteraGiaUscita = new char[26]; //Array che contiene le lettere già uscite
        private int posLettere = 0;

        private int[] punteggio; //Array contentente il punteggio di ogni giocatore, verrà usato per la classifica
        private int nErrori = 0;

        private int posArrayPunteggio = 0; //Posizione dell'array per il punteggio

        //Array per le vocali e le consonanti
        private char[] alfaConsonanti = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        private char[] alfaVocali = { 'a', 'e', 'i', 'o', 'u' };

        private string[] nomi = new string[10];

        private static int quanteVocali = 0;

        //Oggetti globali
        private WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer(); //Oggetto per il suono
        private Random rnd = new Random(); //Oggetto per il random
        private Timer tm; //Oggetto per il timer
        private BitmapImage image; //Oggetto per l'immagine

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) //Se cambia il testo per la textbox adibita alla lettera/parola
        {
            if (txtInp.Text.Length > 0)
                btnProva.IsEnabled = true;
            else
                btnProva.IsEnabled = false;
        }

        private void btnAiuto_Click(object sender, RoutedEventArgs e)
        {
            //Bottone per l'aiuto
            MessageBox.Show(aiuto, "Un'aiuto pert te!", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void btnProva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Bottone per l'inserimento della lettera/parola
                string inp = txtInp.Text;

                txtInp.Text = "";

                if (!isVsCPU) //Se non è contro il processore
                {
                    lblTurno.Content = giocatoriNomi[posArrayPunteggio];
                }

                if (inp.Length == 1) //E' una lettera
                {
                    ControllaLettera(Convert.ToChar(inp)); //Controlla la lettera
                }
                else
                {
                    if (inp == parolaGenerata)
                    {
                        punteggio[posArrayPunteggio] += 2;

                        tm.Dispose(); //Ferma il timer

                        if (!(posArrayPunteggio == 1 && isVsCPU))
                            RiproduciAudio("Win.wav");

                        MessageBox.Show("L'utente " + lblTurno.Content + " Ha vinto!");

                        //Classifica
                        Classifica cl = new Classifica(punteggio);
                        cl.Show();

                        //Torna alla schermata iniziale
                        MainWindow mn = new MainWindow();
                        mn.Show();

                        this.Close(); //Chiudi la finestra
                    }
                    else //Parola sbagliata
                    {
                        nErrori++;

                        Aggiorna();
                    }
                }

                posArrayPunteggio++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        public Impiccaato_Multiplayer(string p, string aiut, string diff, string nGioc, string mod)
        {
            try
            {
                Directory.SetCurrentDirectory("../Sounds/"); //Directory per i suoni

                /*
                Caricamento car = new Caricamento("", "", "", ""); //Mi serve per dire che alla prossima giocata dovrò impostare la cartella di lavoro un passo indietro
                car.c = 1; */

                parolaGenerata = p;
                aiuto = aiut;
                difficolta = diff;
                nGiocatori = nGioc;
                modalita = mod;

                InitializeComponent();

                parolaModificata = new char[p.Length];

                parolaCodificata = GeneraParola().ToCharArray();
                lblParola.Content = new string(parolaCodificata); //Genera la parola

                punteggio = new int[int.Parse(nGiocatori)]; //Inizializza l'Array contenente il punteggio di ogni giocatore

                if (difficolta == "Difficile")
                    btnAiuto.Visibility = Visibility.Hidden; //Non mostrare il bottone degli aiuti

                if (modalita == "Multiplayer (Bot)")
                {
                    btnNomi.Visibility = Visibility.Hidden; //Nascondi il pulsante per vedere i nomi

                    lblTurno.Content = "Guest"; //Il primo turno, se scelto di giocare contro al computer, sarà quello dell'utente
                    isVsCPU = true; //Servirà più tardi

                    tm = new Timer(ControllaMomentoCPU, null, 0, 100); //L'intervallo del timer sarà di 100ms, essendo in multithreading il metodo verrà sempre eseguito ogni tot millisecondi
                }
                else
                {
                    nomi[0] = "Pluto"; //nomi giocatori
                    nomi[1] = "Paperino";
                    nomi[2] = "Lorenzo Camion";
                    nomi[3] = "Francesco Tonini";
                    nomi[4] = "Marco Poli";
                    nomi[5] = "Cristofer Napule'";
                    nomi[6] = "Teo";
                    nomi[7] = "Pippo Omicini";
                    nomi[8] = "Fattori Comune";
                    nomi[9] = "Filippi(ne)"; //nomi giocatori

                    giocatoriNomi = new string[int.Parse(nGiocatori)];
                    int min = rnd.Next(1, 11 - int.Parse(nGiocatori)); //crea un numero random per la gestione casuale di un nome
                    for (int i = 0; i < int.Parse(nGiocatori); i++) //Riempio l'aqrray dei nomi effettivi
                    {
                        giocatoriNomi[i] = nomi[min + i];
                    }

                    lblTurno.Content = giocatoriNomi[0]; //Nome del primo utente

                    tm = new Timer(ControllaTurno, null, 0, 100); //Metodo eseguito ogni 100ms per controllare di chi sarà il turno e se si ha vinto
                }

                Aggiorna();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void ControllaTurno(object a) //Controllo se il numero dei giocatori è quello massimo, allora 0
        {
            try //Gestione errori
            {
                Dispatcher.Invoke(() => //Serve per poter accedere ai componenti della ui
                {
                    if (posArrayPunteggio == int.Parse(nGiocatori))
                    {
                        posArrayPunteggio = 0;
                    }


                    if (lblParola.Content.ToString().Length == lettereIndovinate) //Controlla se il numero di lettere inserite è uguale a quelle presenti nella parola da trovare
                    {
                        //Vittoria
                        lblParola.Content = parolaGenerata; //Qual'era la parola?

                        punteggio[posArrayPunteggio] += 2;

                        tm.Dispose(); //Ferma il timer

                        if (!(posArrayPunteggio == 1 && isVsCPU))
                            RiproduciAudio("Win.wav");

                        MessageBox.Show("L'utente " + lblTurno.Content + " Ha vinto!");

                        //Classifica
                        Classifica cl = new Classifica(punteggio);
                        cl.Show();

                        //Torna alla schermata iniziale
                        MainWindow mn = new MainWindow();
                        mn.Show();

                        this.Close(); //Chiudi la finestra
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void ControllaMomentoCPU(object a) //Controlla se è il momento di passare al processore (l'ggetto anche se non viene usato è necessario)
        {
            try
            {
                Dispatcher.Invoke(() => //Serve per poter accedere ai componenti della ui
                {
                    if (posArrayPunteggio == 1)
                    {
                        //E' il turno del processore
                        btnProva.IsEnabled = false; //L'utente non può più fare niente

                        lblTurno.Content = "CPU";

                        char lettera = AILettera(); //Genero la lettera random
                        ControllaLettera(lettera); //Controlla se la lettera inserita è valida

                        posArrayPunteggio = 0; //Passa la palla all'altro utente
                    }
                    else
                    {
                        lblTurno.Content = "Guest"; //Turno dell'utente
                    }

                    btnProva.IsEnabled = true;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void ControllaLettera(char lettera) //Controlla che la lettera inserita sia valida, se valida allora scrivi dentro la label
        {
            try
            {
                if (Array.IndexOf(letteraGiaUscita, lettera) < 0)
                {
                    //La lettera non è ancora uscita
                    letteraGiaUscita[posLettere] = lettera;
                    posLettere++;

                    for (int i = 1; i < parolaGenerata.Length; i++) //Non prende in cosiderazione la prima lettera visto che la sappiamo già
                    {
                        if (parolaGenerata[i] == lettera)
                        {
                            //La lettera è buona, serve un metodo che riscrive la parola con la lettera scelta
                            parolaModificata[i] = lettera;

                            if (!(posArrayPunteggio == 1 && isVsCPU))
                                RiproduciAudio("Indovinata.wav");

                            lettereIndovinate++;

                            punteggio[posArrayPunteggio]++;
                        }
                    }

                    if(lettereIndovinate == parolaGenerata.Length)
                    {
                        //Vittoria!
                        tm.Dispose(); //Ferma il timer

                        lblParola.Content = parolaGenerata; //Qual'era la parola?

                        punteggio[posArrayPunteggio] += 2;

                        if (!(posArrayPunteggio == 1 && isVsCPU))
                            RiproduciAudio("Win.wav");

                        MessageBox.Show("L'utente " + lblTurno.Content + " Ha vinto!");

                        //Classifica
                        Classifica cl = new Classifica(punteggio);
                        cl.Show();

                        //Torna alla schermata iniziale
                        MainWindow mn = new MainWindow();
                        mn.Show();

                        this.Close(); //Chiudi la finestra
                    }

                    if (Array.IndexOf(parolaModificata, lettera) < 0)
                    {
                        if (!(posArrayPunteggio == 1 && isVsCPU))
                            RiproduciAudio("Err.wav");

                        nErrori++;

                        Aggiorna();
                    }
                }
                else
                {
                    //Sbagliato! (la lettera è già uscita)
                    if (!(posArrayPunteggio == 1 && isVsCPU))
                        RiproduciAudio("Err.wav");

                    punteggio[posArrayPunteggio]--;

                    nErrori++;

                    Aggiorna();
                }

                lblParola.Content = new string(parolaModificata); //Aggiorna la label
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void BtnNomi_Click(object sender, RoutedEventArgs e) //Mostra come si chiamano i giocatori
        {
            NomiGiocatori nomiN = new NomiGiocatori(giocatoriNomi);
            nomiN.Show();
        }

        private char AILettera() //Metodo usato dall'"intelligenza artificiale" per poter generare la lettera
        {
            try
            {
                int consOVoc = rnd.Next(0, 2); //Prima decido se generare una vocale o una consonante
                int posDentroArray;
                bool rifai = false;

                char toReturn;

                if (consOVoc == 0 && quanteVocali != alfaVocali.Length) //Altrimenti sarebbe rimasto dentro all'infinito
                {
                    //Genero una vocale
                    do
                    {
                        posDentroArray = rnd.Next(0, alfaVocali.Length - 1); //Posizione random dentro l'array dell'alfabeto delle vocali

                        toReturn = alfaVocali[posDentroArray]; //Assegna toReturn alla posizione random

                        Debug.WriteLine(toReturn);

                        if (Array.IndexOf(letteraGiaUscita, toReturn) > 0) //Controlla se la lettera è già stata scelta
                        {
                            rifai = true;
                        }
                        else
                        {
                            rifai = false;
                            letteraGiaUscita[posLettere] = toReturn;

                            posLettere++;
                        }

                    } while (rifai);

                    quanteVocali++; //Incrementa il contatore delle vocali
                }
                else
                {
                    //Genero una consonante
                    do
                    {
                        posDentroArray = rnd.Next(0, alfaConsonanti.Length - 1); //Posizione random dentro l'array dell'alfabeto delle consonanti

                        toReturn = alfaConsonanti[posDentroArray]; //Assegna toReturn alla posizione random

                        Debug.WriteLine(toReturn);

                        if (Array.IndexOf(alfaConsonanti, toReturn) < 0) //Controlla se la lettera è già stata scelta
                        {
                            rifai = true;
                        }
                        else
                        {
                            rifai = false;
                            letteraGiaUscita[posLettere] = toReturn;

                            posLettere++;
                        }

                    } while (rifai);
                }

                return toReturn; //Ritorna il valore
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!

                return 'E'; //Ritona giusto per
            }
        }

        private string GeneraParola()
        {
            try
            {
                parolaModificata = new char[parolaGenerata.Length];

                char primoCarattere = Convert.ToChar(parolaGenerata[0].ToString().ToUpper()); //Prima lettera è visibile (maiscola)

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!

                return " ERRORE ";
            }
        }

        private void Aggiorna() //Aggiorna le opzioni della schermata
        {
            try //Gestione errori
            {
                //Aggiorna il link delle immagini
                if (nErrori <= 6)
                {
                    image = new BitmapImage(new Uri(@"..\Immagini\Impicato err" + nErrori + ".png", UriKind.Relative));
                }

                imgImpiccato.Source = image;

                if (nErrori > 0)
                    btnAiuto.IsEnabled = true; //Dopo il primo errore il bottone degli aiuti sarà cliccabile

                if(nErrori == 6)
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

                    tm.Dispose(); //Chiudi il timer

                    n = rnd.Next(1, numFile); //File random

                    Debug.WriteLine("Riproduco il file numero: " + n.ToString());

                    RiproduciAudio("Lose " + n.ToString() + ".wav");

                    lblParola.Content = parolaGenerata; //Qual'era la parola?

                    if (modalita == "Multiplayer (Bot)")
                    {
                        MessageBox.Show("Hai perso :(");
                    }

                    //classifica
                    Classifica cl = new Classifica(punteggio);
                    cl.Show();

                    if (Impostazioni.meme) //Controlla se l'utente ha scelto di essere preso in giro
                    {
                        meme m = new meme(); //Oggeto per i meme
                        m.Show();
                    }

                    MainWindow mn = new MainWindow(); //Torna alla schermata iniziale
                    mn.Show();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }

        private void RiproduciAudio(string v) //Riproduci un file audio
        {
            try
            {
                //Controlla se l'utente ha scelto di avere gli effetti
                if (Impostazioni.effetti)
                {
                    player.URL = v; //Posizione del file da riprodurre
                    player.controls.play(); //Play del file
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }
    }
}
