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
    private IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        try
        {
            if(isAdmin())
            {
                GetAllTareasAdminViewModel tareas = new GetAllTareasAdminViewModel(_tareaRepository.GetAll());
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
            if (HttpContext.Session.GetString("Rol") != null)
            {
                GetAllTareasViewModel tareas = new GetAllTareasViewModel(_tareaRepository.GetTareasByUsuario(Int32.Parse(HttpContext.Session.GetString("Id"))));
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

    public IActionResult GetTareasOperadorByTablero(int idTablero, string esPropietario)
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                var propietario = bool.Parse(esPropietario);
                if(propietario)
                {
                    GetListsTareasViewModel tareas = new GetListsTareasViewModel(_tareaRepository.GetTareasUsuarioByTablero(Int32.Parse(HttpContext.Session.GetString("Id")), idTablero), _tareaRepository.GetTareasByTablero(idTablero), _tareaRepository.GetTareasNoAssignedByTablero(idTablero));
                    return View("GetAllTareasTablero", tareas);
                }else{
                    GetListsTareasViewModel tareas = new GetListsTareasViewModel(_tareaRepository.GetTareasUsuarioByTablero(Int32.Parse(HttpContext.Session.GetString("Id")), idTablero), new List<Tarea>(), _tareaRepository.GetTareasNoAssignedByTablero(idTablero));
                    return View("GetAllTareasTablero", tareas);
                }
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
            NewTareaViewModel nuevaTarea;
            if(isAdmin())
            {
                nuevaTarea = new NewTareaViewModel(new TareaViewModel(), _tableroRepository.GetAll(), _usuarioRepository.GetAll());
            }else{
                nuevaTarea = new NewTareaViewModel(new TareaViewModel(), _tableroRepository.GetTablerosByUsuario(int.Parse(HttpContext.Session.GetString("Id"))), _usuarioRepository.GetAll());
                // return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
            return View("NewTarea", nuevaTarea);
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpPost]
    public IActionResult NewTarea(NewTareaViewModel tareaVM)
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                if(ModelState.IsValid)
                {
                    Tarea tarea = new Tarea(tareaVM.IdTablero, tareaVM.IdUsuarioAsignado, tareaVM.Nombre, tareaVM.Estado, tareaVM.Desc, tareaVM.Color);
                    _tareaRepository.Create(tarea);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }


    [HttpGet]
    public IActionResult UpdateTarea(int Id, string esPropietario)
    {
        try
        {
            if (isAdmin())
            {
                Tarea tarea = _tareaRepository.GetById(Id);
                if (tarea != null)
                {
                    return View(new UpdateTareaViewModel(tarea, _usuarioRepository.GetAll()));
                }
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }else{
                var propietario = bool.Parse(esPropietario);
                if (propietario)
                {
                    Tarea tarea = _tareaRepository.GetById(Id);
                    if (tarea != null)
                    {
                        return View(new UpdateTareaViewModel(tarea, _usuarioRepository.GetAll()));
                    }
                }
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpGet]
    public IActionResult UpdateEstadoTarea(int Id)
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                Tarea tarea = _tareaRepository.GetById(Id);
                if (tarea != null)
                {
                    return View(new UpdateEstadoTareaViewModel());
                }
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
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
            if (HttpContext.Session.GetString("Rol") != null)
            {
                if(ModelState.IsValid)
                {
                    Tarea tarea = new Tarea(tareaVM.IdTablero, tareaVM.IdUsuarioAsignado, tareaVM.Nombre, tareaVM.Estado, tareaVM.Desc, tareaVM.Color);
                    _tareaRepository.Update(tareaVM.Id, tarea);
                    return RedirectToAction("Index");
                }
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }else{
                return RedirectToRoute(new {controller = "Login", action = "Index"});
            }
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
                Tarea tareaAux = _tareaRepository.GetById(Id);
                if (tareaAux != null)
                {
                    TareaViewModel tarea = new TareaViewModel(tareaAux);
                    return View(tarea);
                }
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
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
            if(isAdmin())
            {
                if(ModelState.IsValid)
                {
                    _tareaRepository.Delete(tarea.Id);
                    return RedirectToAction("Index");
                }
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