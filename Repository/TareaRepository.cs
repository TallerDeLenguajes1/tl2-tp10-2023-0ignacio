using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.ViewModels;

namespace tl2_tp10_2023_0ignacio.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion;

        public TareaRepository(string CadenaDeConexion)
        {
            cadenaConexion = CadenaDeConexion;
        }

        public void Create(Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre_tarea, estado_tarea, descripcion_tarea, color_tarea, id_usuario_asignado) VALUES (@idTablero, @nombreTarea, @estadoTarea, @descTarea, @colorTarea, @idUsuarioAsignado)";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombreTarea", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estadoTarea", Convert.ToInt32(tarea.Estado)));
                command.Parameters.Add(new SQLiteParameter("@descTarea", tarea.Desc));
                command.Parameters.Add(new SQLiteParameter("@colorTarea", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", tarea.IdUsuarioAsignado));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET id_tablero = @idTablero, nombre_tarea = @nombreTarea, estado_tarea = @estadoTarea, descripcion_tarea = @descTarea, color_tarea = @colorTarea, id_usuario_asignado = @idUsuarioAsignado WHERE id_tarea = @idTarea;";
            
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombreTarea", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estadoTarea", Convert.ToInt32(tarea.Estado)));
                command.Parameters.Add(new SQLiteParameter("@descTarea", tarea.Desc));
                command.Parameters.Add(new SQLiteParameter("@colorTarea", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", tarea.IdUsuarioAsignado));
                command.ExecuteNonQuery();
                connection.Close();
            }
            
        }

        public List<TareaUsuarioTableroViewModel> GetAll()
        {
            var query = @"SELECT Tarea.id_tarea, Tarea.id_tablero, Tarea.nombre_tarea, Tarea.estado_tarea, Tarea.descripcion_tarea, Tarea.color_tarea, Tarea.id_usuario_asignado, Tablero.nombre_tablero, Usuario.nombre_de_usuario FROM Tarea INNER JOIN Tablero ON(Tarea.id_tablero = Tablero.id_tablero) LEFT JOIN Usuario ON(Tarea.id_usuario_asignado = Usuario.id_usuario)";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                List<TareaUsuarioTableroViewModel> tareas = new List<TareaUsuarioTableroViewModel>();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new TareaUsuarioTableroViewModel();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.NombreTablero = reader["nombre_tablero"].ToString();
                        tarea.NombreUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : reader["nombre_de_usuario"].ToString();
                        tarea.Nombre = reader["nombre_tarea"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado_tarea"]);
                        tarea.Desc = reader["descripcion_tarea"].ToString();
                        tarea.Color = reader["color_tarea"].ToString();
                        tarea.IdUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
                return tareas;
            }
        }

        public List<Tarea> GetTareasByUsuario(int Id)
        {
            var query = @"SELECT * FROM Tarea where id_usuario_asignado = @idUsuarioAsignado";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", Id));

                List<Tarea> tareas = new List<Tarea>();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre_tarea"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado_tarea"]);
                        tarea.Desc = reader["descripcion_tarea"].ToString();
                        tarea.Color = reader["color_tarea"].ToString();
                        tarea.IdUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
                return tareas;
            }
        }

        public List<Tarea> GetTareasByTablero(int Id)
        {
            var query = @"SELECT * FROM Tarea where id_tablero = @idTablero";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@idTablero", Id));

                List<Tarea> tareas = new List<Tarea>();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre_tarea"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado_tarea"]);
                        tarea.Desc = reader["descripcion_tarea"].ToString();
                        tarea.Color = reader["color_tarea"].ToString();
                        tarea.IdUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
                return tareas;
            }
        }

        public List<Tarea> GetTareasUsuarioByTablero(int IdUsuario, int IdTablero)
        {
                var query = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuarioAsignado AND id_tablero = @idTablero";

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", IdUsuario));
                    command.Parameters.Add(new SQLiteParameter("@idTablero", IdTablero));

                    List<Tarea> tareas = new List<Tarea>();
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tarea = new Tarea();
                            tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                            tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                            tarea.Nombre = reader["nombre_tarea"].ToString();
                            tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado_tarea"]);
                            tarea.Desc = reader["descripcion_tarea"].ToString();
                            tarea.Color = reader["color_tarea"].ToString();
                            tarea.IdUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : Convert.ToInt32(reader["id_usuario_asignado"]);
                            tareas.Add(tarea);
                        }
                    }
                    connection.Close();
                    return tareas;
                }
        }

        public List<Tarea> GetTareasNoAssignedByTablero(int IdTablero)
        {
                var query = @"SELECT * FROM Tarea WHERE id_usuario_asignado IS NULL AND id_tablero = @idTablero";

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idTablero", IdTablero));

                    List<Tarea> tareas = new List<Tarea>();
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tarea = new Tarea();
                            tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                            tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                            tarea.Nombre = reader["nombre_tarea"].ToString();
                            tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado_tarea"]);
                            tarea.Desc = reader["descripcion_tarea"].ToString();
                            tarea.Color = reader["color_tarea"].ToString();
                            tarea.IdUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : Convert.ToInt32(reader["id_usuario_asignado"]);
                            tareas.Add(tarea);
                        }
                    }
                    connection.Close();
                    return tareas;
                }
        }

        public Tarea GetById(int id)
        {
            var query = @"SELECT * FROM Tarea WHERE id_tarea = @idTarea";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));

                Tarea tarea = null;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre_tarea"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado_tarea"]);
                        tarea.Desc = reader["descripcion_tarea"].ToString();
                        tarea.Color = reader["color_tarea"].ToString();
                        tarea.IdUsuarioAsignado = (reader.IsDBNull(reader.GetOrdinal("id_usuario_asignado"))) ? null : Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
                connection.Close();
                if (tarea == null)
                {
                    throw new Exception($"No se encontro ningun usuario con la id: {id}");
                }
                return tarea;
            }
        }

        public void SetNullUsuarioAsignado(int Id)
        {
            var query = $"UPDATE Tarea SET id_usuario_asignado = NULL WHERE id_usuario_asignado = @idTarea;";
            
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", Id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteByTablero(int IdTablero)
        {
            var query = @"DELETE FROM Tarea WHERE id_tablero = @idTablero";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", IdTablero));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            var query = @"DELETE FROM Tarea WHERE id_tarea = @idTarea";
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}