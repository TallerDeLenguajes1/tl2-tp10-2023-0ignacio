using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;
using tl2_tp10_2023_0ignacio.ViewModels;

namespace tl2_tp10_2023_0ignacio.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository _tareaRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        if(isAdmin())
        {
            GetAllTareasViewModel tareas = new GetAllTareasViewModel(_tareaRepository.GetAll());
            if(tareas != null)
            {
                return View(tareas);
            }else{
                return NotFound();
            }
        }else{
            return RedirectToAction("GetAllTareasOperador");
        }
        
    }

    public IActionResult GetAllTareasOperador()
    {
        if (HttpContext.Session.GetString("Rol") == "Operador")
        {
            GetAllTareasViewModel tareas = new GetAllTareasViewModel(_tareaRepository.GetTareasByUsuario(Int32.Parse(HttpContext.Session.GetString("Id")!)));
            return View("GetAllTareasOperador",tareas);
        } else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult NewTarea()
    {
        if(isAdmin())
        {
            return View(new TareaViewModel());
        }else{
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
    }

    [HttpPost]
    public IActionResult NewTarea(TareaViewModel tareaVM)
    {
        if(ModelState.IsValid)
        {
            Tarea tarea = new Tarea(tareaVM.IdTablero, tareaVM.IdUsuarioAsignado, tareaVM.Nombre, tareaVM.Estado, tareaVM.Desc, tareaVM.Color);
            _tareaRepository.Create(tarea);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Home", action = "Index"});
    }

    [HttpGet]
    public IActionResult UpdateTarea(int Id)
    {
        if (isAdmin())
        {
            TareaViewModel tarea = new TareaViewModel(_tareaRepository.GetById(Id));
            return View(tarea);
        }else{
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateTarea(TareaViewModel tareaVM)
    {
        if(ModelState.IsValid)
        {
            Tarea tarea = new Tarea(tareaVM.IdTablero, tareaVM.IdUsuarioAsignado, tareaVM.Nombre, tareaVM.Estado, tareaVM.Desc, tareaVM.Color);
            _tareaRepository.Update(tareaVM.Id, tarea);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Home", action = "Index"});
    }

    
    public IActionResult DeleteTarea(int Id)
    {
        if (isAdmin())
        {
            TareaViewModel tarea = new TareaViewModel(_tareaRepository.GetById(Id));
            return View(tarea);
        }else{
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteTarea(Tarea tarea)
    {
        if(ModelState.IsValid)
        {
            _tareaRepository.Delete(tarea.Id);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Home", action = "Index"});
    }

    private bool isAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return true;
        } else
        {
            return false;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}