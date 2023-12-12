using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_0ignacio.Models;


namespace tl2_tp10_2023_0ignacio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario, pass_usuario, rol_usuario) VALUES (@nombreDeUsuario, @passUsuario, @rolUsuario)";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@passUsuario", usuario.Pass));
                command.Parameters.Add(new SQLiteParameter("@rolUsuario", Convert.ToInt32(usuario.Rol)));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(int id, Usuario usuario)
        {
            var query = $"UPDATE Usuario SET nombre_de_usuario = @nombreDeUsuario, pass_usuario = @passUsuario, rol_usuario = @rolUsuario WHERE id_usuario = @idUsuario";
            
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                command.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@passUsuario", usuario.Pass));
                command.Parameters.Add(new SQLiteParameter("@rolUsuario", Convert.ToInt32(usuario.Rol)));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Usuario> GetAll()
        {
            var query = @"SELECT * FROM Usuario";
            List<Usuario> usuarios = new List<Usuario>();

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Pass = reader["pass_usuario"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol_usuario"]);
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }   
        
        public Usuario GetById(int idUsuario)
        {
            var query = @"SELECT * FROM Usuario WHERE id_usuario = @idUsuario";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                var usuario = new Usuario();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Pass = reader["pass_usuario"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol_usuario"]);
                    }
                }
                connection.Close();
                return usuario;
            }
        }

        public void Delete(int idUsuario)
        {
            var query = @"DELETE FROM Usuario WHERE id_usuario = @idUsuario";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}