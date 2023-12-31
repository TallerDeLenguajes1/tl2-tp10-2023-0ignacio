using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.Repositories
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public List<Tablero> GetAll();
        public List<Tablero> GetTablerosByUsuario(int Id);
        public Tablero GetById(int id);
        public void Delete(int id);
    }
}