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
using System.Reflection;
using System.Xml;

namespace reproductor_de_musica_L
{
    public partial class Form1 : Form
    {
       List<datoscancion> listadatos = new List<datoscancion>();
        bool Play = false;
        string[] archivomp3;
        string[] rutasArchivosmp3;



        public Form1()
        {
            InitializeComponent();
        }


        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Abrir.FileName = "Archivo Nuevo";
            Abrir.Filter = "Archivo mp3| *.mp3|archivo mp4|*.mp4|archivo avi|*.avi";
            Abrir.FilterIndex = 1;
            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = (Abrir.FileName);
               
               

            }
            string doc = Abrir.FileName;
            TagLib.File file = TagLib.File.Create(doc);
            datoscancion datmp = new datoscancion();

            datmp.Titulo = file.Tag.Title;
            datmp.Genero = label5.Text = file.Tag.FirstGenre;
            datmp.Ubicacion = axWindowsMediaPlayer1.currentMedia.sourceURL;
            datmp.Duracion = label4.Text = file.Properties.Duration.ToString();
            datmp.Media_types= label8.Text = file.Properties.MediaTypes.ToString();
            listadatos.Add(datmp);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listadatos;
            dataGridView1.Refresh();
            label2.Text = axWindowsMediaPlayer1.currentMedia.getItemInfo("Title");
            label3.Text = axWindowsMediaPlayer1.currentMedia.sourceURL;
            
            richTextBox1.Text = file.Tag.Lyrics;


        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Luis David : Ingeniería en Sistemas... ");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            axWindowsMediaPlayer1.Ctlcontrols.play();





        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            axWindowsMediaPlayer1.URL = rutasArchivosmp3[listBox1.SelectedIndex];
            label2.Text = axWindowsMediaPlayer1.currentMedia.getItemInfo("Title");
            label3.Text = axWindowsMediaPlayer1.currentMedia.sourceURL;
            label4.Text = axWindowsMediaPlayer1.currentMedia.durationString;

            datoscancion datmp = new datoscancion();
            datmp.Titulo = axWindowsMediaPlayer1.currentMedia.getItemInfo("Title");
            datmp.Ubicacion = axWindowsMediaPlayer1.currentMedia.sourceURL;
       
            listadatos.Add(datmp);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listadatos;
            dataGridView1.Refresh();


        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void limpiarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog BusquedaArchivos = new OpenFileDialog();
            BusquedaArchivos.Multiselect = true;
            if (BusquedaArchivos.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                archivomp3 = BusquedaArchivos.SafeFileNames;
                rutasArchivosmp3 = BusquedaArchivos.FileNames;
                foreach (var archivomp3 in archivomp3)
                {
                    listBox1.Items.Add(archivomp3);

                }
                axWindowsMediaPlayer1.URL = rutasArchivosmp3[0];



                listBox1.SelectedIndex = 0;
            }
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void ActualirDatosTrack()
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                macTrackBar2.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();

            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }

            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                macTrackBar2.Value = -1;

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActualirDatosTrack();
            macTrackBar2.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            macTrackBar1.Value = axWindowsMediaPlayer1.settings.volume;

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            ActualirDatosTrack();
        }

        private void macTrackBar1_ValueChanged(object sender, decimal value)
        {
            axWindowsMediaPlayer1.settings.volume = macTrackBar1.Value;

        }

        private void macTrackBar2_ValueChanged(object sender, decimal value)
        {

           // axWindowsMediaPlayer1.Ctlcontrols.currentPosition = macTrackBar2.Value;

        }

        private void albumToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
          
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void limpiarListaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          

        }

        private void button4_Click_2(object sender, EventArgs e)
        {
       
          
        }

        private void button4_Click_3(object sender, EventArgs e)
        {
            Close();
        }
    }

    }
   