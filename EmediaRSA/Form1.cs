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

            richTextBox2.Text = rsa.klucze.ToString();

            wav = new WAV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Wczytaj();
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
            Zapisz();
        }

        public void Wczytaj()
        {
            if (Wczytaj_sciezke_do_odczytu())
            {
                wav.Wczytaj(sciezka);
                richTextBox1.Text = wav.ToString();
                MessageBox.Show("Wczytano");
            }
        }

        public void Zapisz()
        {
            if (Wczytaj_sciezkie_do_zapisu())
            {
                wav.Zapisz(sciezka);
                MessageBox.Show("Zapisano");
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
            wav.bytes = rsa.Szyfruj(wav.bytes);

            MessageBox.Show("Zaszyfrowano");
        }

        public void Deszyfruj()
        {
            wav.bytes = rsa.Deszyfruj(wav.bytes);

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
