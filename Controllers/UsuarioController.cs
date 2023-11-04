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

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        usuarioRepository.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        return View(usuarioRepository.GetById(idUsuario));
    }
    
    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario)
    {
        usuarioRepository.Update(usuario.Id, usuario);
        return RedirectToAction("Index");
    }


}