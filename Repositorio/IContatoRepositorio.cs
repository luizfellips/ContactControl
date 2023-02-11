using ContactControl.Models;

namespace ContactControl.Repositorio
{
	public interface IContatoRepositorio
	{
		bool ApagarContato(ContatoModel contato);
		ContatoModel Atualizar(ContatoModel contato);
        ContatoModel ListarPorID(int id);
        List<ContatoModel> BuscarTodos(int usuarioId);
		ContatoModel Adicionar(ContatoModel contato);

	}
}
