using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;

namespace tl2_tp10_2023_0ignacio.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private UsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        var usuarios = usuarioRepository.GetAll();
        return View(usuarios);
    }

    public IActionResult GetAllUsuarios()
    {
        var usuarios = usuarioRepository.GetAll();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult NewUsuario()
    {
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult NewUsuario(Usuario usuario)
    {
        if(ModelState.IsValid)
        {
            usuarioRepository.Create(usuario);
            return RedirectToAction("GetAllUsuarios");
        }
        return View(usuario);
    }

    [HttpGet]
    public IActionResult UpdateUsuario(int Id)
    {
        var usuario = usuarioRepository.GetById(Id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }
    
    [HttpPost]
    public IActionResult ConfirmUpdateUsuario(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            usuarioRepository.Update(usuario.Id, usuario);
            return RedirectToAction("GetAllUsuarios");
        }
        return View(usuario);
    }

    
    public IActionResult DeleteUsuario(int Id)
    {
        var usuario = usuarioRepository.GetById(Id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    } 

    [HttpPost]
    public IActionResult ConfirmDeleteUsuario(Usuario usuario)
    {
        usuarioRepository.Delete(usuario.Id);
        return RedirectToAction("GetAllUsuarios");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}