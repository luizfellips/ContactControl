using ContactControl.Models;
using ContactControl.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactControl.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if(usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = "A senha do usuário é inválida, tente novamente.";
                    }
                    TempData["MensagemErro"] = "Login ou senha inválidos, tente novamente.";
                }
                return View("Index");
            }
            catch(Exception error)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos efetuar seu login. Mais detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
