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
using System.Xml.Linq;

namespace reproductor_de_musica_L
{
    public partial class Form1 : Form
    {

        static XmlDocument documento = new XmlDocument();
        static string ruta = @"biblio.xml";
        List<Datosbiblioteca> listabiblioteca = new List<Datosbiblioteca>();
        List<listcanciones> listareproduci = new List<listcanciones>();
        List<datoscancion> listadatos = new List<datoscancion>();
        bool Play = false;





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


                tag_info(Abrir.FileName);

          




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



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void limpiarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

            //axWindowsMediaPlayer1.Ctlcontrols.currentPosition = macTrackBar2.Value;

        }

        private void albumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listabiblioteca.RemoveRange(0, listabiblioteca.Count);
            listareproduci.RemoveRange(0, listareproduci.Count);
           
            leerbiblioteca();
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = listabiblioteca;
            dataGridView3.Refresh();

        }

        private void button4_Click(object sender, EventArgs e)
        {

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

        }





        public void tag_info(string dato)
        {
            string dat = dato;
            TagLib.File file = TagLib.File.Create(dat);
            System.Drawing.Image currentImage = null;



            if (file.Tag.Pictures.Length > 0)
            {
                TagLib.IPicture pic = file.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                if (ms != null && ms.Length > 4096)
                {
                    currentImage = System.Drawing.Image.FromStream(ms);

                    pictureBox2.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }
                ms.Close();
            }

            datoscancion datmp = new datoscancion();
            datmp.Titulo = file.Tag.Title;
            datmp.Genero = label5.Text = file.Tag.FirstGenre;
            datmp.Ubicacion = axWindowsMediaPlayer1.currentMedia.sourceURL;
            datmp.Duracion = label4.Text = file.Properties.Duration.ToString();
            datmp.Media_types = label8.Text = file.Properties.MediaTypes.ToString();
            datmp.Artista1 = label9.Text = file.Tag.FirstArtist;
            listadatos.Add(datmp);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listadatos;
            dataGridView1.Refresh();
            label2.Text = axWindowsMediaPlayer1.currentMedia.getItemInfo("Title");
            label3.Text = axWindowsMediaPlayer1.currentMedia.sourceURL;
            richTextBox1.Text = file.Tag.Lyrics;

        }

        private void minimizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public void xml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Lista_Favoritos");
            doc.AppendChild(raiz);

            XmlElement cancion = doc.CreateElement("Cancion");


            XmlElement titulo = doc.CreateElement("Titulo");


            XmlElement url = doc.CreateElement("Url");


            for (int i = 0; i < listareproduci.Count(); i++)
            {
                //nuevo documento
                cancion = doc.CreateElement("Cancion");
                raiz.AppendChild(cancion);

                titulo = doc.CreateElement("Titulo");
                titulo.AppendChild(doc.CreateTextNode(listareproduci[i].Titulo));
                cancion.AppendChild(titulo);

                url = doc.CreateElement("Url");
                url.AppendChild(doc.CreateTextNode(listareproduci[i].Ubicacion));
                cancion.AppendChild(url);



                doc.Save(@"miXML.xml");
            }


        }
        public void EscribirXml()
        {
            for (int i = 0; i < listabiblioteca.Count(); i++)
            {
                if (label11.Text == listabiblioteca[i].Titulo1)
                {
                    //Creamos el escritor.
                    using (XmlTextWriter Writer = new XmlTextWriter(@"biblio.xml", Encoding.UTF8))
                    {
                        //Declaración inicial del Xml.
                        Writer.WriteStartDocument();

                        //Configuración.
                        Writer.Formatting = Formatting.Indented;
                        Writer.Indentation = 5;

                        //Escribimos el nodo principal.
                        Writer.WriteStartElement("Blibioteca");

                        //Escribimos un nodo empleado.
                        Writer.WriteStartElement("Cancion");

                        //Escribimos cada uno de los elementos del nodo empleado.
                        Writer.WriteElementString("nombre", listabiblioteca[i].Titulo1);
                        Writer.WriteElementString("url", listabiblioteca[i].Ubicacion);
                        Writer.WriteElementString("num", listabiblioteca[i].Numero);
                        Writer.WriteElementString("album", listabiblioteca[i].Album);
                        //Escribimos el subnodo teléfono.
                        Writer.WriteElementString("duracion", listabiblioteca[i].Duracion);
                        Writer.WriteElementString("calidad", listabiblioteca[i].Calidad);

                        //Cerramos el nodo y el documento.
                        Writer.WriteEndElement();
                        Writer.WriteEndDocument();
                        Writer.Flush();
                    }
                }
            }
        }
        public void cargar()
        {
            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = listareproduci;

            dataGridView2.Refresh();
        }

        private void button4_Click_4(object sender, EventArgs e)
        {
            listareproduci.RemoveRange(0, listareproduci.Count);

            var myPlayList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("MyPlayList");

            for (int i = 0; i < listareproduci.Count; i++)
            {
                var mediaItem = axWindowsMediaPlayer1.newMedia(listareproduci[i].Ubicacion);
                myPlayList.appendItem(mediaItem);
            }
            axWindowsMediaPlayer1.currentPlaylist = myPlayList;
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            axWindowsMediaPlayer1.URL = dataGridView2.CurrentRow.Cells["Ubicacion"].Value.ToString();
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            media = axWindowsMediaPlayer1.newMedia(dataGridView2.CurrentRow.Cells["Ubicacion"].Value.ToString());
            playlist.appendItem(media);

            axWindowsMediaPlayer1.currentPlaylist = playlist;
            listadatos.RemoveRange(0, listadatos.Count);
            string dat = dataGridView2.CurrentRow.Cells["Ubicacion"].Value.ToString();

            tag_info(dat);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listadatos;
            dataGridView1.Refresh();
        }

        private void crearListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            openFileDialog1.Multiselect = true;
            listcanciones reprotemp = new listcanciones();
            reprotemp.Ubicacion = openFileDialog1.FileName;
            reprotemp.Titulo = openFileDialog1.SafeFileName.ToString();
            label1.Visible = true;
            dataGridView2.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            label11.Visible = true;
            listareproduci.Add(reprotemp);
            cargar();
            xml();

        }

        private void eliminarCancionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nomb = label11.Text;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (nomb == listareproduci[i].Titulo)
                {
                    listareproduci.RemoveAt(i);
                }
            }
            cargar();

        }

        private void limpiarListaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int contar = listareproduci.Count();
            int inicio = 0;
            listareproduci.RemoveRange(inicio, contar);
            cargar();
        }
        public void actualizar2()
        {
            if (listabiblioteca.Count == 0)
            {
                leerbiblioteca();
            }

        }
        private void button4_Click_5(object sender, EventArgs e)
        {
            if (listareproduci.Count == 0)
            {
                listabiblioteca.RemoveRange(0, listabiblioteca.Count);
                actualizar2();
                int max2 = listabiblioteca.Count;
                for (int i = 0; i < listabiblioteca.Count; i++)
                {
                    if (label12.Text == listabiblioteca[i].Titulo1)
                    {
                        if (i == max2 - 1)
                        {
                            axWindowsMediaPlayer1.URL = listabiblioteca[0].Ubicacion;
                            label12.Text = listabiblioteca[0].Titulo1;
                            break;
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = listabiblioteca[i + 1].Ubicacion;
                            label12.Text = listabiblioteca[i + 1].Titulo1;
                            break;
                        }

                    }
                }
            }
            else
            {
                listareproduci.RemoveRange(0, listareproduci.Count);
                actualizar();
                int max = listareproduci.Count;
                for (int i = 0; i < listareproduci.Count; i++)
                {
                    if (label11.Text == listareproduci[i].Titulo)
                    {
                        if (i == max - 1)
                        {
                            axWindowsMediaPlayer1.URL = listareproduci[0].Ubicacion;
                            label11.Text = listareproduci[0].Titulo;
                            break;
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = listareproduci[i + 1].Ubicacion;
                            label11.Text = listareproduci[i + 1].Titulo;
                            break;
                        }

                    }
                }

            }
            axWindowsMediaPlayer1.Ctlcontrols.play();
            listadatos.RemoveRange(0, listadatos.Count);
            tag_info(axWindowsMediaPlayer1.URL);

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string nom = dataGridView2.CurrentRow.Cells["titulo"].Value.ToString();
            label11.Text = nom;


        }
        public void actualizar()
        {
            if (listareproduci.Count == 0)
            {
                leerxml();
            }

        }
        public void leerxml()
        {
            XDocument documento = XDocument.Load(@"miXML.xml");
            var listar = from lis in documento.Descendants("Lista_Favoritos") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                listcanciones tmp = new listcanciones();
                tmp.Titulo = u.Element("Titulo").Value;
                tmp.Ubicacion = u.Element("Url").Value;
                listareproduci.Add(tmp);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        public void leerbiblioteca()
        {
            XDocument documento = XDocument.Load(@"biblio.xml");
            var listar = from lis in documento.Descendants("Blibioteca") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                Datosbiblioteca tmp = new Datosbiblioteca();
                tmp.Titulo1 = u.Element("Titulo").Value;
                tmp.Ubicacion = u.Element("Url").Value;
                tmp.Numero = u.Element("No").Value;
                tmp.Calidad = u.Element("Calidad").Value;
                tmp.Album = u.Element("Album").Value;
                tmp.Duracion = u.Element("Duracion").Value;
                listabiblioteca.Add(tmp);

            }
        }
     
        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            axWindowsMediaPlayer1.URL = dataGridView3.CurrentRow.Cells["Ubicacion"].Value.ToString();
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            media = axWindowsMediaPlayer1.newMedia(dataGridView3.CurrentRow.Cells["Ubicacion"].Value.ToString());
            playlist.appendItem(media);

            axWindowsMediaPlayer1.currentPlaylist = playlist;
            listadatos.RemoveRange(0, listadatos.Count);
            string dat = dataGridView3.CurrentRow.Cells["Ubicacion"].Value.ToString();

            tag_info(dat);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listadatos;
            dataGridView1.Refresh();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nom = dataGridView3.CurrentRow.Cells["titulo1"].Value.ToString();
            label12.Text = nom;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nomb = label11.Text;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (nomb == listareproduci[i].Titulo)
                {
                    listareproduci.RemoveAt(i);
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    
                  
                }
            }
            cargar();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            openFileDialog1.Multiselect = true;
            listcanciones reprotemp = new listcanciones();
            reprotemp.Ubicacion = openFileDialog1.FileName;
            reprotemp.Titulo = openFileDialog1.SafeFileName.ToString();
            label1.Visible = true;
            dataGridView2.Visible = true;
            listareproduci.Add(reprotemp);
            cargar();
            xml();

        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            int contar = listareproduci.Count();
            int inicio = 0;
            listareproduci.RemoveRange(inicio, contar);
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            cargar();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listabiblioteca.RemoveRange(0, listabiblioteca.Count);
            string nom = label12.Text;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            ModificarDatosXml(nom);
            leerbiblioteca();
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = listabiblioteca;
            dataGridView3.Refresh();
        }
        public void ModificarDatosXml(string url)
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Blibioteca/Cancion");

            foreach (XmlNode item in listacancion)
            {
                //Determinamos el nodo a modificar por medio del id de empleado.
                if (item.FirstChild.InnerText == url)
                {
                    //Nodo sustituido.
                    XmlNode nodoOld = item;
                    bibliot.RemoveChild(nodoOld);
                }
            }

            //Salvamos el documento.
            documento.Save(ruta);
        }
        private XmlNode CrearNodoXml(string nom1, string url1, string num1, string album1, string dura1, string cali1)
        {
            //Creamos el nodo que deseamos insertar.
            XmlElement Cancion = documento.CreateElement("Cancion");

            //Creamos el elemento idEmpleado.
            XmlElement nombre = documento.CreateElement("Titulo");
            nombre.InnerText = nom1;
            Cancion.AppendChild(nombre);

            //Creamos el elemento nombre.
            XmlElement Url = documento.CreateElement("Url");
            Url.InnerText = url1;
            Cancion.AppendChild(Url);

            //Creamos el elemento apellidos.
            XmlElement num = documento.CreateElement("No");
            num.InnerText = num1;
            Cancion.AppendChild(num);

            //Creamos el elemento numeroSS.
            XmlElement album = documento.CreateElement("Album");
            album.InnerText = album1;
            Cancion.AppendChild(album);

            //Creamos el elemento fijo.
            XmlElement duracion = documento.CreateElement("Duracion");
            duracion.InnerText = dura1;
            Cancion.AppendChild(duracion);
            //Creamos el elemento movil.
            XmlElement calidad = documento.CreateElement("Calidad");
            calidad.InnerText = cali1;
            Cancion.AppendChild(calidad);

            return Cancion;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                url = openFileDialog1.FileName;
            }
            listadatos.RemoveRange(0, listadatos.Count);



            Datosbiblioteca blitmp = new Datosbiblioteca();
            TagLib.File file = TagLib.File.Create(url);
            blitmp.Ubicacion = url;
         
            blitmp.Titulo1 = file.Tag.Title;
            label12.Text = file.Tag.Title;
         
            blitmp.Duracion = file.Properties.Duration.ToString();
            blitmp.Numero = Convert.ToString(file.Tag.Track);
            blitmp.Album = file.Tag.Album;
            blitmp.Calidad = Convert.ToString(file.Properties.AudioBitrate);

            listabiblioteca.Add(blitmp);

            string archivo = @"biblio.xml";
            if (File.Exists(archivo) == true)
            {
                InsertarXml();
            }
            else { EscribirXml(); }
            listabiblioteca.RemoveRange(0, listabiblioteca.Count);
            leerbiblioteca();
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = listabiblioteca;
            dataGridView3.Columns["Ubicacion"].Visible = false;
            dataGridView3.Refresh();
        }
        string nombre1, url, num, album, dura, cali;

        private void button11_Click(object sender, EventArgs e)
        {
            listareproduci.RemoveRange(0, listareproduci.Count);
            actualizar();
            var myPlayList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("MyPlayList");

            for (int i = 0; i < listareproduci.Count; i++)
            {
                var mediaItem = axWindowsMediaPlayer1.newMedia(listareproduci[i].Ubicacion);
                myPlayList.appendItem(mediaItem);
            }
            axWindowsMediaPlayer1.currentPlaylist = myPlayList;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            listareproduci.RemoveRange(0, listareproduci.Count);
            actualizar();
            int max = listareproduci.Count;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (label11.Text == listareproduci[i].Titulo)
                {
                    if (i == 0)
                    {
                        axWindowsMediaPlayer1.URL = listareproduci[max - 1].Ubicacion;
                        label11.Text = listareproduci[max - 1].Titulo;
                        break;
                    }
                    else
                    {
                        axWindowsMediaPlayer1.URL = listareproduci[i - 1].Ubicacion;
                        label11.Text = listareproduci[i - 1].Titulo;
                        break;
                    }

                }
            }
            listadatos.RemoveRange(0, listadatos.Count);
            tag_info(axWindowsMediaPlayer1.URL);
        
    }

        private void InsertarXml()
        {
            
            documento = new XmlDocument();
            documento.Load(ruta);

            for (int i = 0; i < listabiblioteca.Count(); i++)
            {

                if (label12.Text == listabiblioteca[i].Titulo1)
                {

                    nombre1 = listabiblioteca[i].Titulo1;
                    url = listabiblioteca[i].Ubicacion;
                    num = listabiblioteca[i].Numero;
                    album = listabiblioteca[i].Album;
                    dura = listabiblioteca[i].Duracion;
                    cali = listabiblioteca[i].Calidad;

                }
            }
           
            XmlNode empleado = this.CrearNodoXml(nombre1, url, num, album, dura, cali);
           
            XmlNode nodoRaiz = documento.DocumentElement;

           
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);   

            documento.Save(ruta);
        }
      
    
    }
}
 
        
    
    