using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reproductor_de_musica_L
{
    class Datosbiblioteca
    {
        string Titulo;
      
        string ubicacion;
        string numero;
        string calidad;
        string album;
        string duracion;
        public string Titulo1
        {
            get
            {
                return Titulo;
            }

            set
            {
                Titulo = value;
            }
        }

     

        public string Calidad
        {
            get
            {
                return calidad;
            }

            set
            {
                calidad = value;
            }
        }

        public string Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public string Album
        {
            get
            {
                return album;
            }

            set
            {
                album = value;
            }
        }

        public string Duracion
        {
            get
            {
                return duracion;
            }

            set
            {
                duracion = value;
            }
        }

        public string Ubicacion
        {
            get
            {
                return ubicacion;
            }

            set
            {
                ubicacion = value;
            }
        }
    }
}
