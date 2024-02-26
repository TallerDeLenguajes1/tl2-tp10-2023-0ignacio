using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_0ignacio.Models;

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
            try
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
            }catch(Exception){
                throw new Exception("Hubo un problema al crear el tablero.");
            }
        }

        public void Update(int id, Tablero tablero)
        {
            try
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
            }catch(Exception){
                throw new Exception("Hubo un problema al modificar el tablero.");
            }
        }

        public List<Tablero> GetAll()
        {
            try
            {
                var query = @"SELECT * FROM Tablero";
                List<Tablero> tableros = new List<Tablero>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();

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
                }
                return tableros;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tableros.");
            }
        }

        public List<Tablero> GetTablerosByUsuario(int Id)
        {
            try
            {
                var query = @"SELECT * FROM Tablero where id_usuario_propietario = @idUsuarioProp";
                List<Tablero> tableros = new List<Tablero>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioProp", Id));

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
                }
                return tableros;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tableros del usuario.");
            }
        }

        public Tablero GetById(int id)
        {
            try
            {

                var query = @"SELECT * FROM Tablero WHERE id_tablero = @idTablero";
                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                    var tablero = new Tablero();
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                            tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                            tablero.Nombre = reader["nombre_tablero"].ToString();
                            tablero.Desc = reader["descripcion_tablero"].ToString();
                        }
                    }
                    connection.Close();
                    return tablero;
                }
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver el tablero.");
            }
        }

        public void Delete(int id)
        {
            try
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
            }catch(Exception){
                throw new Exception("Hubo un problema al eliminar el tablero.");
            }
        }
    }
}