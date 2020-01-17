using System.Windows;
﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using WpfAnimatedGif;

namespace impiccato_v1
{
    /// <summary>
    /// Logica di interazione per Caricamento.xaml
    /// </summary>
    public partial class Caricamento : Window
    {
        public static string lingua; //Lingua scelta
        public static string difficolta; //Difficoltà scelta
        public static string modalita; //Modalità scelta
        public static string nGiocatori; //Modalità scelta

        public static string parolaGenerata;
        public static string aiuto;

        public Caricamento(string l, string d, string mod, string nGioc)
        {
            lingua = l;
            difficolta = d;
            modalita = mod;
            nGiocatori = nGioc;

            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try //Tratta gli errori!
            {
                //Starta il processo di wdb
                lblCaricamento.Content = "Impostando WDB";

                //Cancella il contenuto del file di wdb in output (visto che ancora quest'ultimo non lo supporta nativamente)
                string[] toWrite = { };
                File.WriteAllLines("./lib/Output.cfg", toWrite);

                string[] inpFile = new string[3];

                inpFile[2] = "wd"; //Richiedi tutte le parole

                inpFile[0] = lingua + " " + difficolta + ".wdb";

                File.WriteAllLines("./lib/Request.cfg", inpFile); //Scrivi nel file di richiesta l'array

                lblCaricamento.Content = "Caricamento WDB";

                Directory.SetCurrentDirectory("./lib");

                System.Diagnostics.Process.Start(@".\wdb.exe"); //Esegui il sottoprogramma "wdb.exe" per la prima volta

                Thread.Sleep(3000);

                string[] readedOutput = File.ReadAllLines(@".\Output.cfg");

                lblCaricamento.Content = "Generazione parola";

                string[] noNullArray;
                int lunghezzaArr = 0;

                while (true) //Quanto deve essere lungo l'array senza righe superflue?
                {
                    if (string.IsNullOrEmpty(readedOutput[lunghezzaArr]) || string.IsNullOrWhiteSpace(readedOutput[lunghezzaArr]))
                        break;

                    lunghezzaArr++;
                }

                noNullArray = new string[lunghezzaArr];

                for (int i = 0; i < noNullArray.Length; i++) //Copia l'array senza righe superflue
                {
                    noNullArray[i] = readedOutput[i];
                }

                Random rnd = new Random();
                int pos = rnd.Next(0, noNullArray.Length - 1); //Parola random

                parolaGenerata = noNullArray[pos]; //Generazione parola

                if (difficolta == "Facile" || difficolta == "Medio")
                {
                    //Prendi l'aiuto
                    lblCaricamento.Content = "Generazione aiuto";

                    inpFile[2] = "desc " + pos; //Chiedi l'aiuto per l'ip iesimo

                    File.WriteAllLines("./Request.cfg", inpFile); //Scrivi nel file di richiesta l'array

                    System.Diagnostics.Process.Start(@".\wdb.exe"); //Esegui il sottoprogramma "wdb.exe" per la seconda volta

                    Thread.Sleep(3000);

                    string[] contenutisecondi = File.ReadAllLines("./Output.cfg"); //Leggi il file di output
                    aiuto = contenutisecondi[0];
                }
                else { aiuto = ""; } //Se viene scelta la difficoltà difficile allora non ci deve essere nessun aiuto

                Impiccato imp = new Impiccato(parolaGenerata, aiuto); //Passa la parola generata e l'aiuto alla schermata "impiccato"

                imp.Show(); //Visualizza l'altra finestra

                this.Close(); //Chiudi questa finestra
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); //Errore!

                this.Close(); //Chiudi tutto!
            }
        }
    }
}
