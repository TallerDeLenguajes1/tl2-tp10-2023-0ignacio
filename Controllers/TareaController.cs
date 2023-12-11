using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;

namespace tl2_tp10_2023_0ignacio.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private TareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    public IActionResult Index()
    {
        var tareas = tareaRepository.GetAll();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult NewTarea()
    {
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult NewTarea(Tarea tarea)
    {
        if(ModelState.IsValid)
        {
            tareaRepository.Create(tarea);
            return RedirectToAction("Index");
        }
        return View(tarea);
    }

    [HttpGet]
    public IActionResult UpdateTarea(int Id)
    {
        var tarea = tareaRepository.GetById(Id);
        if (tarea == null)
        {
            return NotFound();
        }

        return View(tarea);
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateTarea(Tarea tarea)
    {
        if (ModelState.IsValid)
        {
            tareaRepository.Update(tarea.Id, tarea);
            return RedirectToAction("Index");
        }
        return View(tarea);
    }

    
    public IActionResult DeleteTarea(int Id)
    {
        var tarea = tareaRepository.GetById(Id);
        if (tarea == null)
        {
            return NotFound();
        }
        return View(tarea);
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteTarea(Tarea tarea)
    {
        tareaRepository.Delete(tarea.Id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}