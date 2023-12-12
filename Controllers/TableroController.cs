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

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
    }

    public IActionResult Index()
    {
        
        if (isAdmin())
        {
            GetAllTablerosViewModel tableros = new GetAllTablerosViewModel(_tableroRepository.GetAll());
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
    }

    public IActionResult GetAllTablerosOperador()
    {
        if (HttpContext.Session.GetString("Rol") == "Operador")
        {
            GetAllTablerosViewModel tableros = new GetAllTablerosViewModel(_tableroRepository.GetTablerosByUsuario(Int32.Parse(HttpContext.Session.GetString("Id")!)));
            return View("GetAllTablerosOperador",tableros);
        } else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult NewTablero()
    {
        if (isAdmin())
        {
            return View(new TableroViewModel());
        } else
        {
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
    }

    [HttpPost]
    public IActionResult NewTablero(TableroViewModel tableroVM)
    {
        if(ModelState.IsValid)
        {
            Tablero tablero = new Tablero(tableroVM.IdUsuarioPropietario, tableroVM.Nombre, tableroVM.Desc);
            _tableroRepository.Create(tablero);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Home", action = "Index"});
    }

    [HttpGet]
    public IActionResult UpdateTablero(int Id)
    {
        if (isAdmin())
        {
            TableroViewModel tablero = new TableroViewModel(_tableroRepository.GetById(Id));
            return View(tablero);
        } else
        {
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateTablero(UpdateTableroViewModel tableroVM)
    {
        if (ModelState.IsValid)
        {
            Tablero tableroModificado = new Tablero(tableroVM.IdUsuarioPropietario, tableroVM.Nombre, tableroVM.Desc);
            _tableroRepository.Update(tableroVM.Id, tableroModificado);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Home", action = "Index"});
    }

    
    public IActionResult DeleteTablero(int Id)
    {
        if (isAdmin())
        {
            Tablero tablero = _tableroRepository.GetById(Id);
            return View(tablero);
        } else
        {
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteTablero(Tablero tablero)
    {
        if (ModelState.IsValid)
        {
            _tableroRepository.Delete(tablero.Id);
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