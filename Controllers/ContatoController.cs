using ContactControl.Filters;
using ContactControl.Models;
using ContactControl.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactControl.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
		private readonly IContatoRepositorio _contatoRepositorio;
		public ContatoController(IContatoRepositorio contatoRepositorio)
		{
			_contatoRepositorio = contatoRepositorio;
		}

        public IActionResult Index()
        {
			List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }
		public IActionResult Criar()
		{
			return View();
		}

		public IActionResult Editar(int id)
		{
			ContatoModel model = _contatoRepositorio.ListarPorID(id);
			return View(model);
		}
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View("Editar",contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos atualizar seu contato, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
		{
            ContatoModel model = _contatoRepositorio.ListarPorID(id);
            return View(model);
		}
        public IActionResult Apagar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorID(id);
            try
            {
            bool apagado = _contatoRepositorio.ApagarContato(contato);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Oops! não conseguimos apagar seu contato.";
                }
            return View("Editar", contato);
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops! Não conseguimos apagar seu contato, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
		public IActionResult Criar(ContatoModel contato)
		{
			try
			{
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
			catch (Exception erro)
			{
                TempData["MensagemErro"] = $"Oops! Não conseguimos cadastrar seu contato, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
			}
			
		}
	}
}
