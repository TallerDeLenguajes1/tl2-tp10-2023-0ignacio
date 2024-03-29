using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;
using tl2_tp10_2023_0ignacio.ViewModels;

namespace tl2_tp10_2023_0ignacio.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;
    private ITareaRepository _tareaRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        try
        {
            if (isAdmin())
            {
                GetAllTablerosAdminViewModel tableros = new GetAllTablerosAdminViewModel(_tableroRepository.GetAllTablerosUsurios());
                if(tableros != null)
                {
                    return View(tableros);
                } else
                {
                    return NotFound();
                }
            }else{
                return RedirectToAction("GetAllTablerosOperador");
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    public IActionResult GetAllTablerosOperador()
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                GetAllTablerosViewModel tableros = new GetAllTablerosViewModel(_tableroRepository.GetTablerosByUsuario(Int32.Parse(HttpContext.Session.GetString("Id"))), _tableroRepository.GetTablerosByAssignedTasks(int.Parse(HttpContext.Session.GetString("Id"))));
                return View("GetAllTablerosOperador",tableros);
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
    public IActionResult NewTablero()
    {
        try
        {
            if (isAdmin())
            {
                return View("NewTablero", new NewTableroViewModel(new TableroViewModel(), _usuarioRepository.GetAll()));
            } else
            {
                return View("NewTableroOperador", new TableroViewModel());
                // return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpPost]
    public IActionResult NewTablero(NewTableroViewModel tableroVM)
    {
        try
        {
            if(isAdmin())
            {
                if(ModelState.IsValid)
                {
                    Tablero tablero = new Tablero(tableroVM.IdUsuarioPropietario, tableroVM.Nombre, tableroVM.Desc);
                    _tableroRepository.Create(tablero);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpPost]
    public IActionResult NewTableroOperador(TableroViewModel tableroVM)
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                if(ModelState.IsValid)
                {
                    Tablero tablero = new Tablero(int.Parse(HttpContext.Session.GetString("Id")), tableroVM.Nombre, tableroVM.Desc);
                    _tableroRepository.Create(tablero);
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
    

    [HttpGet]
    public IActionResult UpdateTablero(int Id)
    {
        try
        {
            if (isAdmin())
            {
                Tablero tablero = _tableroRepository.GetById(Id);
                if(tablero == null)
                {
                    return RedirectToRoute(new {controller = "Home", action = "Index"});
                }else{
                    return View(new UpdateTableroViewModel(tablero, _usuarioRepository.GetAll()));
                }
            } else
            {
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateTablero(UpdateTableroViewModel tableroVM)
    {
        try
        {
            if(isAdmin())
            {   
                if (ModelState.IsValid)
                {
                    Tablero tableroModificado = new Tablero(tableroVM.IdUsuarioPropietario, tableroVM.Nombre, tableroVM.Desc);
                    _tableroRepository.Update(tableroVM.Id, tableroModificado);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }


    public IActionResult DeleteTablero(int Id)
    {
        try
        {
            if (isAdmin())
            {
                Tablero tablero = _tableroRepository.GetById(Id);
                if (tablero == null)
                {
                    return RedirectToRoute(new {controller = "Home", action = "Index"});
                }else{
                    return View(tablero);
                }
            } else
            {
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteTablero(Tablero tablero)
    {
        try
        {
            if(isAdmin())
            {
                if (ModelState.IsValid)
                {
                    _tareaRepository.DeleteByTablero(tablero.Id);
                    _tableroRepository.Delete(tablero.Id);
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