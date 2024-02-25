using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.ViewModels;
using tl2_tp10_2023_0ignacio.Repositories;

namespace tl2_tp10_2023_0ignacio.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        if(isAdmin())
        {
            return View();
        }else{
            if (HttpContext.Session.GetString("Rol") == "Operador")
            {
                return RedirectToAction("GetAllUsuariosOperador");
            } else
            {
                return RedirectToRoute(new {controller = "Login", action = "Index"});
            }
        }
    }

    public IActionResult GetAllUsuarios()
    {
        try
        {
            GetAllUsuariosViewModel usuarios = new GetAllUsuariosViewModel(_usuarioRepository.GetAll());
            
            if(isAdmin())
            {
                return View(usuarios);
            }else{
                if (HttpContext.Session.GetString("Rol") == "Operador")
                {
                    return RedirectToAction("GetAllUsuariosOperador");
                } else
                {
                    return RedirectToRoute(new {controller = "Login", action = "Index"});
                }
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpGet] 
    public IActionResult GetAllUsuariosOperador()
    {
        try
        {
            UsuarioViewModel usuario = new UsuarioViewModel(_usuarioRepository.GetById(Int32.Parse(HttpContext.Session.GetString("Id")!)));

            if(HttpContext.Session.GetString("Rol") == "Operador")
            {
                return View(usuario);
            } else
            {
                return RedirectToRoute(new {controller = "Login", action = "Index"});
            }
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpGet]
    public IActionResult NewUsuario()
    {
        try
        {
            if (isAdmin())
            { 
                return View(new NewUsuarioViewModel());
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
    public IActionResult NewUsuario(NewUsuarioViewModel usuarioVM)
    {
        try
        {
            if(ModelState.IsValid)
            {
                Usuario usuario = new Usuario(usuarioVM.NombreDeUsuario, usuarioVM.Pass, usuarioVM.Rol);
                _usuarioRepository.Create(usuario);
                return RedirectToAction("GetAllUsuarios");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    [HttpGet]
    public IActionResult UpdateUsuario(int Id)
    {
        try
        {
            if(isAdmin())
            {
                UpdateUsuarioViewModel usuario = new UpdateUsuarioViewModel(_usuarioRepository.GetById(Id));
                return View(usuario);
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
    public IActionResult ConfirmUpdateUsuario(Usuario usuarioVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario(usuarioVM.NombreDeUsuario, usuarioVM.Pass, usuarioVM.Rol);
                _usuarioRepository.Update(usuarioVM.Id, usuario);
                return RedirectToAction("GetAllUsuarios");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    
    public IActionResult DeleteUsuario(int Id)
    {
        try
        {
            if(isAdmin())
            {
                Usuario usuario = _usuarioRepository.GetById(Id);
                return View(usuario);
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
    public IActionResult ConfirmDeleteUsuario(Usuario usuario)
    {
        try
        {
            if(ModelState.IsValid)
            {
                _usuarioRepository.Delete(usuario.Id);
                return RedirectToAction("GetAllUsuarios");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToRoute("Error"); 
        }
    }

    private bool isAdmin()
    {
        if(HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
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