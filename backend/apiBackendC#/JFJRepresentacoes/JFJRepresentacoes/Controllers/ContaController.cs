using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JFJRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JFJRepresentacoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly RepresentacaoContexto _context;
        public ContaController(RepresentacaoContexto ctx)
        {
            this._context = ctx;
        }
        [HttpPost("login")]
        public ActionResult login(Usuario user)
        {
            user.Senha = Settings.HashPassword(user.Senha);
            Usuario usuario = _context.usuarios.Where(el => el.Email.Equals(user.Email) && el.Senha.Equals(user.Senha)).FirstOrDefault();
            if (usuario == null)
            {
                return BadRequest("Usuario não encontrado");
            }
            else
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
           
        // PUT: api/Conta/5
        [HttpPut("{id}")]
        [Authorize(Roles = "jfjadmin")]
        public ActionResult Put (int id, [FromBody] Usuario user)
        {
            Usuario oldUser = _context.usuarios.Find(id);
            if(oldUser == null)
            {
                return NotFound("Usuário não encontrado");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    oldUser.nome = user.nome;
                    oldUser.Email = user.Email;
                    oldUser.Senha = Settings.HashPassword(user.Senha);
                    _context.SaveChanges();
                    oldUser.Senha = null;
                    return Ok(oldUser);
                }
                else
                {
                    return BadRequest("Problema ao modificar o usuario");
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="jfjadmin")]
        public ActionResult Delete(int id)
        {
            Usuario user = _context.usuarios.Find(id);
            if(user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            else
            {
                _context.usuarios.Remove(user);
                _context.SaveChanges();
                return NoContent();
            }
        }
        [HttpPost("recuperarSenha")]
        public ActionResult changePassword(string email)
        {
            Settings.SendEmail("dudufranz13@gmail.com", "Este é o corpo do email!", "Titulo do Emails");
            return NoContent();
        }
    }
}
