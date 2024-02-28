using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.ViewModels;

namespace tl2_tp10_2023_0ignacio.Repositories
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion;

        public TableroRepository(string CadenaDeConexion)
        {
            cadenaConexion = CadenaDeConexion;
        }
        
        public void Create(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre_tablero, descripcion_tablero) VALUES (@idUsuarioProp, @nombreTablero, @descTablero)";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuarioProp", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descTablero", tablero.Desc));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(int id, Tablero tablero)
        {
            var query = $"UPDATE Tablero SET id_usuario_propietario = @idUsuarioProp, nombre_tablero = @nombreTablero, descripcion_tablero = @descTablero WHERE id_tablero = @idTablero;";
            
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioProp", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descTablero", tablero.Desc));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Tablero> GetAll()
        {
            var query = @"SELECT * FROM Tablero";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                List<Tablero> tableros = new List<Tablero>();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre_tablero"].ToString();
                        tablero.Desc = reader["descripcion_tablero"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
                return tableros;
            }
        }

        public List<TableroUsuarioViewModel> GetAllTablerosUsurios()
        {
            var query = @"SELECT * FROM Tablero INNER JOIN Usuario ON(Tablero.id_usuario_propietario = Usuario.id_usuario)";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                List<TableroUsuarioViewModel> tableros = new List<TableroUsuarioViewModel>();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new TableroUsuarioViewModel();
                        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.NombreTablero = reader["nombre_tablero"].ToString();
                        tablero.NombreDeUsuarioPropietario = reader["nombre_de_usuario"].ToString();
                        tablero.DescTablero = reader["descripcion_tablero"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
                return tableros;
            }
        }

        public List<Tablero> GetTablerosByAssignedTasks(int Id_usuario)
        {
            var query = @"SELECT DISTINCT(tablero.id_tablero), id_usuario_propietario, tablero.nombre_tablero, tablero.descripcion_tablero FROM usuario INNER JOIN tablero ON(id_usuario = id_usuario_propietario) INNER JOIN tarea ON(tablero.id_tablero = tarea.id_tablero)WHERE id_usuario_asignado = @idUsuarioAsig AND id_usuario_propietario != @idUsuarioAsig;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                List<Tablero> tableros = new List<Tablero>();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsig", Id_usuario));

                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        Tablero tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario= Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre_tablero"].ToString();
                        tablero.Desc = reader["descripcion_tablero"].ToString();
                        
                        tableros.Add(tablero); 
                    }
                }
                connection.Close();
                return tableros;
            }
        }

        public List<Tablero> GetTablerosByUsuario(int Id)
        {
            
            var query = @"SELECT * FROM Tablero where id_usuario_propietario = @idUsuarioProp";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@idUsuarioProp", Id));

                List<Tablero> tableros = new List<Tablero>();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre_tablero"].ToString();
                        tablero.Desc = reader["descripcion_tablero"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
                return tableros;
            }
        }

        public Tablero GetById(int id)
        {
            var query = @"SELECT * FROM Tablero WHERE id_tablero = @idTablero";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                Tablero tablero = null;
                
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre_tablero"].ToString();
                        tablero.Desc = reader["descripcion_tablero"].ToString();
                    }
                }
                connection.Close();
                if (tablero == null)
                {
                    throw new Exception("El tablero que se quiere encontrar no existe");
                }
                return tablero;
            }
        }

        public void DeleteByUsuario(int IdUsuario)
        {
            var query = @"DELETE FROM Tablero WHERE id_usuario_propietario = @idUsuario";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", IdUsuario));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            var query = @"DELETE FROM Tablero WHERE id_tablero = @idTablero";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}