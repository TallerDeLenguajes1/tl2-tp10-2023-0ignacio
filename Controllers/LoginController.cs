using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;
using tl2_tp10_2023_0ignacio.ViewModels;
namespace tl2_tp10_2023_0ignacio.Controllers;

public class LoginController: Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index(){
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(Usuario usuario){
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");
            Usuario usuarioLoggeado = _usuarioRepository.ValidateUsuario(usuario);
            
            if (usuarioLoggeado.NombreDeUsuario == null)
            {
                _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NombreDeUsuario + " - Clave ingresada: " + usuario.Pass);
                return RedirectToAction("Index");
            }else{
                loggearUsuario(usuarioLoggeado);
                _logger.LogInformation("El usuario " + usuario.NombreDeUsuario + " ingreso correctamente!");
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
    }

    private void loggearUsuario(Usuario usuario)
    {
        try
        {
            HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
            HttpContext.Session.SetString("Rol", usuario.Rol.ToString());
            HttpContext.Session.SetString("Id", usuario.Id.ToString());
            _logger.LogInformation($"El usuario {usuario.NombreDeUsuario} se ha logueado en la sesion con ID: {usuario.Id} y rol: {usuario.Rol}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al iniciar sesion");
            throw;
        }
    }

    [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                TempData["Mensaje"] = "Sesion finalizada";
                _logger.LogInformation("La sesión se cerró exitosamente para el usuario.");
                return RedirectToRoute(new { controller = "Login", action = "Index"});
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ocurrió un error al cerrar sesión.";
                _logger.LogError(ex, "Error al cerrar sesión del usuario");
                return RedirectToRoute(new { controller = "Login", action = "Index"});
            }
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}