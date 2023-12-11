using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;

namespace tl2_tp10_2023_0ignacio.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private TableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    public IActionResult Index()
    {
        var tableros = tableroRepository.GetAll();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult NewTablero()
    {
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult NewTablero(Tablero tablero)
    {
        if(ModelState.IsValid)
        {
            tableroRepository.Create(tablero);
            return RedirectToAction("Index");
        }
        return View(tablero);
    }

    [HttpGet]
    public IActionResult UpdateTablero(int Id)
    {
        var tablero = tableroRepository.GetById(Id);
        if (tablero == null)
        {
            return NotFound();
        }

        return View(tablero);
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateTablero(Tablero tablero)
    {
        if (ModelState.IsValid)
        {
            tableroRepository.Update(tablero.Id, tablero);
            return RedirectToAction("Index");
        }
        return View(tablero);
    }

    
    public IActionResult DeleteTablero(int Id)
    {
        var tablero = tableroRepository.GetById(Id);
        if (tablero == null)
        {
            return NotFound();
        }
        return View(tablero);
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteTablero(Tablero tablero)
    {
        tableroRepository.Delete(tablero.Id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}