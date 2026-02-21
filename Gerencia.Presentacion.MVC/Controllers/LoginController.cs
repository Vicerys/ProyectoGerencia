using Gerencia.Core.Dtos;
using Gerencia.Presentacion.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly Empleado_Api _api;
        public LoginController(Empleado_Api api) => _api = api;


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(InicioSesionDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var empleado = await _api.LoginAsync(login);
            if (empleado is null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return View(login);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
