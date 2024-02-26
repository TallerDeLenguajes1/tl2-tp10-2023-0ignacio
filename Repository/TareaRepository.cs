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
            try
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
            }catch(Exception){
                throw new Exception("Hubo un problema al crear la tarea.");
            }
        }

        public void Update(int id, Tarea tarea)
        {
            try
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
            }catch(Exception){
                throw new Exception("Hubo un problema al modificar la tarea.");
            }
        }

        public List<TareaUsuarioTableroViewModel> GetAll()
        {
            try
            {
                var query = @"SELECT Tarea.id_tarea, Tarea.id_tablero, Tarea.nombre_tarea, Tarea.estado_tarea, Tarea.descripcion_tarea, Tarea.color_tarea, Tarea.id_usuario_asignado, Tablero.nombre_tablero, Usuario.nombre_de_usuario FROM Tarea INNER JOIN Tablero ON(Tarea.id_tablero = Tablero.id_tablero) LEFT JOIN Usuario ON(Tarea.id_usuario_asignado = Usuario.id_usuario)";
                List<TareaUsuarioTableroViewModel> tareas = new List<TareaUsuarioTableroViewModel>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();

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
                }
                return tareas;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tareas.");
            }
        }

        public List<Tarea> GetTareasByUsuario(int Id)
        {
            try
            {
                var query = @"SELECT * FROM Tarea where id_usuario_asignado = @idUsuarioAsignado";
                List<Tarea> tareas = new List<Tarea>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", Id));

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
                }
                return tareas;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tareas del usuario.");
            }
        }

        public List<Tarea> GetTareasByTablero(int Id)
        {
            try
            {
                var query = @"SELECT * FROM Tarea where id_tablero = @idTablero";
                List<Tarea> tareas = new List<Tarea>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idTablero", Id));

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
                }
                return tareas;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tareas del usuario.");
            }
        }

        public List<Tarea> GetTareasUsuarioByTablero(int IdUsuario, int IdTablero)
        {
            try
            {
                var query = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuarioAsignado AND id_tablero = @idTablero";
                List<Tarea> tareas = new List<Tarea>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", IdUsuario));
                    command.Parameters.Add(new SQLiteParameter("@idTablero", IdTablero));

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
                }
                return tareas;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tareas del usuario pertenecientes al tablero.");
            }
        }

        public List<Tarea> GetTareasNoAssignedByTablero(int IdTablero)
        {
            try
            {
                var query = @"SELECT * FROM Tarea WHERE id_usuario_asignado IS NULL AND id_tablero = @idTablero";
                List<Tarea> tareas = new List<Tarea>();

                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter("@idTablero", IdTablero));

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
                }
                return tareas;
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la lista de tareas del usuario pertenecientes al tablero.");
            }
        }

        public Tarea GetById(int id)
        {
            try
            {
                var query = @"SELECT * FROM Tarea WHERE id_tarea = @idTarea";
                using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                    var tarea = new Tarea();
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
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
                    return tarea;
                }
            }catch(Exception){
                throw new Exception("Hubo un problema al devolver la tarea.");
            }
        }

        public void Delete(int id)
        {
            try
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
            }catch(Exception){
                throw new Exception("Hubo un problema al eliminar la tarea.");
            }
        }
    }
}