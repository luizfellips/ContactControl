using ContactControl.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContactControl.Controllers
{
    public class RestritoController : Controller
    {
        [PaginaParaUsuarioLogado]
        public IActionResult Index()
        {
            return View();
        }
    }
}
