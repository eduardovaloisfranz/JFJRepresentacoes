using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFJRepresentacoes.Models
{
    public class RepresentacaoContexto : DbContext
    {
        public RepresentacaoContexto(DbContextOptions<RepresentacaoContexto> options) : base(options)
        {

        }

        public DbSet<Representacao> representacoes { get; set; }

        public DbSet<Usuario> usuarios { get; set; }
    }

}
