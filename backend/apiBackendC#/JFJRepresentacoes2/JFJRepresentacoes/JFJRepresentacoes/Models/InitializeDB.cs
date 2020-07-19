using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFJRepresentacoes.Models
{
    public class InitializeDB
    {
        public static void Initialize(RepresentacaoContexto ctx)
        {
            ctx.Database.EnsureCreated();
            if (ctx.representacoes.Any())
            {
                return;
            }

            var representacoes = new Representacao[]
            {
                new Representacao{Nome="BluFitas", Descricao="A Blufitas é uma empresa especializada em produtos para fechamento de embalagens.", UrlImagem="https://www.blufitas.com.br/wp-content/uploads/sites/462/2019/08/logo_blufitas.png", UrlSite="https://www.blufitas.com.br/"},
                new Representacao{Nome="Não é mais blufitas", Descricao="Outra empresakk so msm img", UrlImagem="https://www.blufitas.com.br/wp-content/uploads/sites/462/2019/08/logo_blufitas.png", UrlSite="https://www.blufitas.com.br/"},
                new Representacao{Nome="Nova empresa a ser representada", Descricao="Empresa que faz alguma coisa", UrlImagem="https://i.pinimg.com/originals/8d/aa/2d/8daa2d22c2a57235c343dafdeb7add08.jpg", UrlSite="https://www.google.com"}

            };
            foreach(Representacao rep in representacoes)
            {
                ctx.representacoes.Add(rep);                
            }
            ctx.SaveChanges();
        }
    }
}
