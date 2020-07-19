using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JFJRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JFJRepresentacoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class RepresentacaoController : ControllerBase
    {
        private readonly RepresentacaoContexto _context;
        public RepresentacaoController(RepresentacaoContexto ctx)
        {
            this._context = ctx;
        }
        [HttpPost("login")]
        public ActionResult login(Usuario user)
        {
            user.Senha = Settings.HashPassword(user.Senha);
            Usuario usuario = _context.usuarios.Where(el => el.Email.Equals(user.Email) && el.Senha.Equals(user.Senha)).FirstOrDefault();
            if(usuario == null)
            {
                return BadRequest("Usuario não encontrado");
            }else
            {
                string token = TokenService.generateToken(usuario);
                return Ok(token);
            }
        }  
        [HttpGet("loginTeste")]
        public Usuario loginTeste()
        {
            Usuario user = new Usuario()
            {
                nome = "Eduardo Valois Franz",
                Email = "dudufranz13@gmail.com",
                Senha = Settings.HashPassword("teste12345"),
                autorizacao = "jfjadmin"                            
            };
            _context.usuarios.Add(user);
            _context.SaveChanges();
            return user;
        }

        // GET: api/Representacao
        [HttpGet]
        public ActionResult Get()
        {
            var representacoes =  from representacao in _context.representacoes.ToList()
                                  select new { representacao.ID, representacao.Nome, representacao.Descricao, representacao.UrlImagem, representacao.UrlSite};
            return Ok(representacoes);
        }

        // GET: api/Representacao/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            Representacao representacao = _context.representacoes.Find(id);
            if(representacao == null)
            {
                return NotFound("Representação não encontrada");
            }
            else
            {
            return Ok(representacao);
            }
        }

        // POST: api/Representacao
        [HttpPost]
        //[Authorize(Roles = "jfjadmin")]
        [Authorize]
        public ActionResult Post([FromBody] Representacao representacao)
        {
            if (ModelState.IsValid)
            {
                _context.representacoes.Add(representacao);
                _context.SaveChanges();
                return Ok(representacao);
            }
            else
            {
                return BadRequest("Erro ao tentar adicionar a representação");
            }
        }

        // PUT: api/Representacao/5
        [HttpPut("{id}")]
        [Authorize(Roles = "jfjadmin")]
        public ActionResult Put(int id, [FromBody] Representacao representacao)
        {
            Representacao oldRep = _context.representacoes.Find(id);
            if (oldRep != null)
            {
                oldRep.Nome = representacao.Nome;
                oldRep.Descricao = representacao.Descricao;
                oldRep.UrlImagem = representacao.UrlImagem;
                oldRep.UrlSite = representacao.UrlSite;
                _context.SaveChanges();
                return Ok(oldRep);
            }
            else
            {
                return NotFound("Representação não encontrada");
            }       
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "jfjadmin")]
        public ActionResult Delete(int id)
        {
            Representacao oldRep = _context.representacoes.Find(id);
            if(oldRep == null)
            {
                return NotFound("Representação não encontrada");
            }
            else
            {
                _context.representacoes.Remove(oldRep);
                _context.SaveChanges();
                return NoContent();
            }
        }
    }
}
