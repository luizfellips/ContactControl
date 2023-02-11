using ContactControl.Helper;
using ContactControl.Models;
using ContactControl.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactControl.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    alterarSenhaModel.Id = usuarioLogado.Id;
                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso.";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Oops não conseguimos alterar sua senha, tente novamente. Mais detalhes do erro: {error.Message} ";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
