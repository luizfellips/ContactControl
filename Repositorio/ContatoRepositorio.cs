using ContactControl.Data;
using ContactControl.Models;
using ContactControl.Repositorio;

namespace ContactControl.Repository
{
	public class ContatoRepositorio : IContatoRepositorio
	{
		private readonly BancoContext _bancoContext;
		public ContatoRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public ContatoModel Adicionar(ContatoModel contato)
		{
			_bancoContext.Contatos.Add(contato);
			_bancoContext.SaveChanges();
			return contato;
		}

        public bool ApagarContato(ContatoModel contato)
        {
			try
			{
                _bancoContext.Contatos.Remove(contato);
                _bancoContext.SaveChanges();
                return true;
            }
			catch
			{
				return false;
			}
			
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
			ContatoModel contatoDB = ListarPorID(contato.Id);
			if (contatoDB == null) throw new Exception("Houve um erro na atualização do contato");
			contatoDB.Nome = contato.Nome;
			contatoDB.Celular = contato.Celular;
			contatoDB.Email = contato.Email;

			_bancoContext.Contatos.Update(contatoDB);
			_bancoContext.SaveChanges();
			return contatoDB;
        }

        public List<ContatoModel> BuscarTodos()
        {
			return _bancoContext.Contatos.ToList();
        }

        public ContatoModel ListarPorID(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }
    }
}
