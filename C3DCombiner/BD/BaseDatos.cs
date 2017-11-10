using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.BD
{
    class BaseDatos
    {
        SqlConnection conexion = new SqlConnection("Data Source=localhost;Initial Catalog=C3DCombiner;Integrated Security=True");

        private void AbrirConexion()
        {
            try
            {
                conexion.Open();
            }
            catch
            {

            }
        }

        private void CerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch
            {

            }
        }

        public Boolean IniciarSesion(String usuario, String clave)
        {
            try
            {
                AbrirConexion();
                SqlCommand comando = new SqlCommand("SELECT * FROM USUARIO WHERE username = '" + usuario + "' AND clave = '" + clave + "';", conexion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TitusTools.Usuario = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(3));
                    return true;
                }

                CerrarConexion();
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public Boolean ExisteRepositorio(String nombre)
        {
            Boolean estado = false;
            try
            {
                AbrirConexion();

                SqlCommand comando = new SqlCommand("select * from archivo where usuario = " + TitusTools.Usuario.Id.ToString() + " and nombre = '" + nombre + "';", conexion);
                SqlDataReader reader = comando.ExecuteReader();                

                if (reader.HasRows)
                {
                    estado = true;
                }
                reader.Close();
                CerrarConexion();
            }
            catch(Exception ex) {
            }

            return estado;
        }

        public Boolean ExisteRutaRepositorio(String ruta)
        {
            Boolean estado = false;
            try
            {
                AbrirConexion();

                SqlCommand comando = new SqlCommand("select * from archivo where  CAST(url AS nvarchar(max)) = '" + ruta + "';", conexion);
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    estado = true;
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception ex)
            {
            }

            return estado;
        }

        public String GetCodigoRepositorio(String ruta)
        {
            String codigo = "";
            try
            {
                AbrirConexion();

                SqlCommand comando = new SqlCommand("select codigo from archivo where  CAST(url AS nvarchar(max)) = '" + ruta + "';", conexion);
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    codigo = reader.GetString(0);
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception ex)
            {
            }

            return codigo;
        }

        public void InsertarRepositorio(String nombre, String descripcion, String codigo)
        {
            try
            {
                String url = "http://localhost:1993/Archivo?id=";
                AbrirConexion();

                SqlCommand comando = new SqlCommand("insert into archivo (nombre, descripcion, codigo, url, usuario) values('" + nombre.Replace(".tree","") + "','" + descripcion + "','" + codigo + "','" + url + "'," + TitusTools.Usuario.Id.ToString() + ");", conexion);


                comando.ExecuteNonQuery();
                CerrarConexion();

                AbrirConexion();
                SqlCommand comando2 = new SqlCommand("select id from archivo where nombre = '" + nombre.Replace(".tree","") + "' and usuario =  " + TitusTools.Usuario.Id + ";",conexion);


                SqlDataReader reader = comando2.ExecuteReader();

                int id = 0;
                if (reader.HasRows)
                {
                    reader.Read();

                    id = reader.GetInt32(0);
                }

                reader.Close();
                CerrarConexion();
                AbrirConexion();
                SqlCommand comando3 = new SqlCommand("update archivo set url = '" + url + id.ToString() + "' where nombre = '" + nombre.Replace(".tree", "") + "' and usuario =  " + TitusTools.Usuario.Id + ";", conexion);
                comando3.ExecuteNonQuery();
                CerrarConexion();
            }
            catch(Exception ex) { }
        }

        public void ModificarRepositorio(String nombre, String descripcion, String codigo)
        {
            try
            {
                AbrirConexion();
                SqlCommand comando = new SqlCommand("update archivo set descripcion = '" + descripcion + "', codigo = '" + codigo + "', modificacion = getdate() where nombre = '" + nombre.Replace(".tree","") + "' and usuario =  " + TitusTools.Usuario.Id + ";", conexion);
                comando.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex) {

            }
        }
    }
}
