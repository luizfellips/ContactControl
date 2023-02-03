using ContactControl.Models;

namespace ContactControl.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        List<UsuarioModel> BuscarTodos();
        bool Apagar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel ListarPorID(int id);
        UsuarioModel Adicionar(UsuarioModel usuario);
    }
}
