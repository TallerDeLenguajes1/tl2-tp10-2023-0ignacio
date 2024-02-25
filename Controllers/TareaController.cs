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
    private ITableroRepository _tableroRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
    }

    public IActionResult Index()
    {
        try
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
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
        
    }

    public IActionResult GetAllTareasOperador()
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") == "Operador")
            {
                GetAllTareasViewModel tareas = new GetAllTareasViewModel(_tareaRepository.GetTareasByUsuario(Int32.Parse(HttpContext.Session.GetString("Id")!)));
                return View("GetAllTareasOperador",tareas);
            } else
            {
                return NotFound();
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpGet]
    public IActionResult NewTarea()
    {
        try
        {
            if(isAdmin())
            {
                return View("NewTarea", new TareaViewModel());
            }else{
                return View("NewTareaOperador", new TareaOperadorViewModel(new Tarea(),_tableroRepository.GetTablerosByUsuario(int.Parse(HttpContext.Session.GetString("Id")))));
                // return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpPost]
    public IActionResult NewTarea(TareaViewModel tareaVM)
    {
        try
        {
            if(ModelState.IsValid)
            {
                Tarea tarea = new Tarea(tareaVM.IdTablero, tareaVM.IdUsuarioAsignado, tareaVM.Nombre, tareaVM.Estado, tareaVM.Desc, tareaVM.Color);
                _tareaRepository.Create(tarea);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpPost]
    public IActionResult NewTareaOperador(NewTareaOperadorViewModel tareaOVM)
    {
        try
        {
            if(ModelState.IsValid)
            {
                Tarea tarea = new Tarea(tareaOVM.Tarea.IdTablero, tareaOVM.Tarea.IdUsuarioAsignado, tareaOVM.Tarea.Nombre, tareaOVM.Tarea.Estado, tareaOVM.Tarea.Desc, tareaOVM.Tarea.Color);
                _tareaRepository.Create(tarea);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpGet]
    public IActionResult UpdateTarea(int Id)
    {
        try
        {
            if (isAdmin())
            {
                TareaViewModel tarea = new TareaViewModel(_tareaRepository.GetById(Id));
                return View(tarea);
            }else{
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateTarea(TareaViewModel tareaVM)
    {
        try
        {
            if(ModelState.IsValid)
            {
                Tarea tarea = new Tarea(tareaVM.IdTablero, tareaVM.IdUsuarioAsignado, tareaVM.Nombre, tareaVM.Estado, tareaVM.Desc, tareaVM.Color);
                _tareaRepository.Update(tareaVM.Id, tarea);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    
    public IActionResult DeleteTarea(int Id)
    {
        try
        {
            if (isAdmin())
            {
                TareaViewModel tarea = new TareaViewModel(_tareaRepository.GetById(Id));
                return View(tarea);
            }else{
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteTarea(Tarea tarea)
    {
        try
        {
            if(ModelState.IsValid)
            {
                _tareaRepository.Delete(tarea.Id);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
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