using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum Roles
{
    Administrador = 0,
    Operador = 1
}

namespace tl2_tp10_2023_0ignacio.Models
{
    public class Usuario
    {
        private int id;
        private string nombreDeUsuario;
        private string pass;
        private Roles rol;


        public int Id { get => id; set => id = value; }
        public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public string Pass { get => pass; set => pass = value; }
        public Roles Rol { get => rol; set => rol = value; }

        public Usuario(){}
        public Usuario( string nombreDeUsuario, string pass, Roles rol)
        {
            this.nombreDeUsuario = nombreDeUsuario;
            this.pass = pass;
            this.rol = rol;
        }
    }
}