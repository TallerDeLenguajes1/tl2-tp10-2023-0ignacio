using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.Repositories
{
    public interface ITareaRepository
    {
        public void Create(Tarea tarea);
        public void Update(int id, Tarea tarea);
        public List<Tarea> GetAll();
        public List<Tarea> GetTareasByUsuario(int Id);
        public Tarea GetById(int id);
        public void Delete(int id);
    }
}