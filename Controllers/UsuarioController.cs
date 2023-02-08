using ContactControl.Filters;
using ContactControl.Models;
using ContactControl.Repositorio;
using ContactControl.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ContactControl.Controllers
{
	[PaginaRestritaAdmin]
	public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos cadastrar seu usuário, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel model = _usuarioRepositorio.ListarPorID(id);
            return View(model);
        }

        public IActionResult Apagar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorID(id);
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(usuario);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Oops! não conseguimos apagar seu Usuário.";
                }
                return View("Editar", usuario);
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos apagar seu usuário, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel model = _usuarioRepositorio.ListarPorID(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };
                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View("Editar", usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos atualizar seu usuário, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
