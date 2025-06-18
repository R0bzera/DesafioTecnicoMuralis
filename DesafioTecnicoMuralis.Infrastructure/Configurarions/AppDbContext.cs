using DesafioTecnicoMuralis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Infrastructure.Configurarions
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ClienteEntity> Clientes => Set<ClienteEntity>();
        public DbSet<EnderecoEntity> Enderecos => Set<EnderecoEntity>();
        public DbSet<ContatoEntity> Contatos => Set<ContatoEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}