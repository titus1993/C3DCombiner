using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.BD
{
    class Usuario
    {
        public int Id;
        public String Nombre;
        public String Username;
        public Boolean Estado;

        public Usuario(int id, String username, String nombre)
        {
            this.Nombre = nombre;
            this.Id = id;
            this.Username = username;
            Estado = true;
        }

        public Usuario()
        {
            Estado = false;
        }
    }
}
