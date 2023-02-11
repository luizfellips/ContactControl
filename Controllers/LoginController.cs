using ContactControl.Helper;
using ContactControl.Models;
using ContactControl.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactControl.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            // se o usuário estiver logado, redirecionar para home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
            
        }
        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email,redefinirSenhaModel.Login);
                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string message = $"Sua nova senha é {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", message);
                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = "Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviar seu e-mail. Por favor tente novamente.";
                        }
                        return RedirectToAction("Index","Login");
                    }

                    TempData["MensagemErro"] = "Não conseguimos redefinir sua senha, verifique os dados informados e tente novamente.";
                    return RedirectToAction("RedefinirSenha","Login");
                }
                return View("Index");
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos redefinir sua senha. Mais detalhes do erro: {error.Message}";
                return RedirectToAction("Index","Login");
            }
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
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
                            _sessao.CriarSessaoDoUsuario(usuario);
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
