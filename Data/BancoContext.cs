using ContactControl.Data.Map;
using ContactControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactControl.Data
{
	public class BancoContext : DbContext
	{
		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
		{
		}

		public DbSet<ContatoModel> Contatos { get; set; }
		public DbSet<UsuarioModel> Usuarios { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ContatoMap());
			base.OnModelCreating(modelBuilder);
		}
	}
}
