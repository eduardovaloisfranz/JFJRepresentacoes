using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using JFJRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public ActionResult changePassword([FromBody] Usuario user){
            Usuario selectedUser = _context.usuarios.Where(el => el.Email.Equals(user.Email)).FirstOrDefault();
            if(selectedUser == null)
            {
                return BadRequest("Problema ao encontrar o usuario");
            }
            try
            {
                string token = TokenService.recoveryPasswordToken(user.Email);
                string corpoEmail = "Informe esse código para recuperar sua senha: <strong>" + token + "</strong> Entretantoo esse token possui validade de apenas 30 minutos";
                Settings.SendEmail(user.Email, corpoEmail, "Recuperação de Conta");                
                return NoContent();

            }catch(Exception ex)
            {
                return BadRequest("Erro de Programação");
            }
        }

        [HttpPost("token")]
        public ActionResult changePassword([FromBody] TokenPassword token)
        {
            var stream = token.token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            var exp = tokenS.Claims.First(claim => claim.Type == "exp").Value;
            DateTime expirationTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            expirationTime = expirationTime.AddSeconds(Convert.ToDouble(exp)).ToUniversalTime();
            if(DateTime.UtcNow > expirationTime)
            {
                return BadRequest("Token expirado");
            }
            else
            {
                Usuario user = _context.usuarios.Where(user => user.Email.Equals(email)).FirstOrDefault();
                var guid = Guid.NewGuid().ToString();
                user.Senha = Settings.HashPassword(guid);
                _context.SaveChanges();
                return Ok(guid);
            }

        }
    }
}
