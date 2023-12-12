using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public enum EstadoTarea
{
    Ideas = 0,
    ToDo = 1,
    Doing = 2,
    Review = 3,
    Done = 4
}

namespace tl2_tp10_2023_0ignacio.Models
{
    public class Tarea
    {
        private int id;
        private int idTablero;
        private int? idUsuarioAsignado;
        private string nombre;
        private EstadoTarea estado;
        private string desc;
        private string color;

        public int Id { get => id; set => id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Color { get => color; set => color = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }

        public Tarea(){}
        public Tarea(int idTablero, int? idUsuarioAsignado, string nombre, EstadoTarea estado, string desc, string color)
        {
            this.idTablero = idTablero;
            this.idUsuarioAsignado = idUsuarioAsignado;
            this.nombre = nombre;
            this.Estado = estado;
            this.desc = desc;
            this.color = color;
        }
    }
}