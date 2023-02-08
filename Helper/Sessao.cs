using ContactControl.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ContactControl.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");
            if(string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado",valor);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}
