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
    /// Logica di interazione per Classifica.xaml
    /// </summary>
    public partial class Classifica : Window
    {
        private int[] punteggi;

        public Classifica(int[] p)
        {
            punteggi = p;

            InitializeComponent();

            string[] cosaScrivere = sort();

            foreach(string a in cosaScrivere)
            {
                txtClassifica.Text += a + Environment.NewLine;
            }
        }

        private string[] sort() //Ordina la classifica
        {
            string[] ret = new string[punteggi.Length];

            int lav = 0;

            //Ordino
            string[,] ordinata = new string[ret.Length, 2];

            for(int i = 0; i < ordinata.GetLength(0); i++) //Copia l'array in una matrice
            {
                ordinata[i, 0] = punteggi[i].ToString();
                ordinata[i, 1] = i.ToString();
            }

            for (int i = 0; i < ordinata.GetLength(0); i++)
            {
                for (int x = 0; x < ordinata.GetLength(0); x++)
                {
                    if (x + 1 < ordinata.GetLength(0))
                    {
                        if (int.Parse(ordinata[x, 0]) < int.Parse(ordinata[x + 1, 0]))
                        {
                            //Inverti il punteggio 
                            lav = int.Parse(ordinata[x, 0]);
                            ordinata[x, 0] = ordinata[x + 1, 0];
                            ordinata[x + 1, 0] = lav.ToString();

                            //Inverti il numero del giocatore
                            lav = int.Parse(ordinata[x, 1]);
                            ordinata[x, 1] = ordinata[x + 1, 1];
                            ordinata[x + 1, 1] = lav.ToString();
                        }
                    }
                }
            }

            //Crea cosa deve scrivere nel txtbox
            for(int i = 0; i < ret.Length; i++)
            {
                ret[i] = "Giocatore " + (int.Parse(ordinata[i, 1]) + 1) + " con punteggio: " + ordinata[i, 0];
            }

            return ret;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Chiudi
        {
            this.Close();
        }
    }
}
