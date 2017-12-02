using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EmediaRSA
{
    public partial class Form1 : Form
    {
        public string sciezka { get; set; }
        private WAV wav;
        private RSA rsa;

        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            rsa = new RSA();
            wav = new WAV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Wczytaj_sciezke_do_odczytu())
            {
                wav.Wczytaj(sciezka);
                richTextBox1.Text = wav.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // szyfrowanie
            Szyfruj();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // deszyfrowanie
            Deszyfruj();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(Wczytaj_sciezkie_do_zapisu())
            {
                wav.Zapisz(sciezka);
            }
        }

        public bool Wczytaj_sciezkie_do_zapisu()
        {
            SaveFileDialog dlg = new SaveFileDialog { FileName = "" , Filter = "Wav files (*.wav)|*.wav"};
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sciezka = dlg.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Szyfruj()
        {
            wav.R_new = rsa.Szyfruj(wav.R);
            wav.L_new = rsa.Szyfruj(wav.L);

            MessageBox.Show("Zaszyfrowano");
        }

        public void Deszyfruj()
        {
            wav.R_new = rsa.Deszyfruj(wav.R);
            wav.L_new = rsa.Deszyfruj(wav.L);

            MessageBox.Show("Deszyfrowano");
        }

        public bool Wczytaj_sciezke_do_odczytu()
        {
            OpenFileDialog dlg = new OpenFileDialog { FileName = "" , Filter = "Wav files (*.wav)|*.wav" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sciezka = dlg.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
