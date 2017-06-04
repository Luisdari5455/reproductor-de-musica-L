using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reproductor_de_musica_L
{
    class datoscancion
    {
        string titulo;
        string ubicacion;
        string duracion;
       
        string genero;
        string media_types;

        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                titulo = value;
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

 

        public string Genero
        {
            get
            {
                return genero;
            }

            set
            {
                genero = value;
            }
        }

        public string Media_types
        {
            get
            {
                return media_types;
            }

            set
            {
                media_types = value;
            }
        }
    }
    }

