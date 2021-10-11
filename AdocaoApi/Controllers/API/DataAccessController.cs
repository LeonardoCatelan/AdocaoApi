using AdocaoApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdocaoApi.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class DataAccessController : ControllerBase
    {

        private readonly Contexto _context;

        public DataAccessController(Contexto context)
        {
            _context = context;
        }

        [HttpGet] //Tipo de requisição
        [ActionName("TestingApi")] //Nome da funçao, onde vai ser chamado na API
        public string RetornaString()
        {
            return "teste";
        }

        [HttpPost]
        [ActionName("CadastroAdotante")]
        public async Task<IActionResult> CadastroAdotante([Bind("Id, Usuario, Nome, Sobrenome, Idade, Email, Celular, EnderecoCep, EnderecoNumero, Moradia, AnimalPreferido, PortePreferido, GeneroPreferido")] Adotante adotante)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(adotante);
                    await _context.SaveChangesAsync();
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    errorMessage = $"Erro ao salvar cadastro: {e.InnerException.Message}";
                }
            }
            return StatusCode(400, errorMessage);
        }

        [HttpPost]
        [ActionName("CadastroPet")]
        public async Task<IActionResult> CadastroPet([Bind("Id, Usuario, Senha, Nome, Sobrenome, Email, Celular, EnderecoCep, EnderecoNumero, Animal, Porte, Genero, Vacinas, Raca, Cor")] Pet pet)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(pet);
                    var result = await _context.SaveChangesAsync();
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    errorMessage = $"Erro ao salvar cadastro: {e.InnerException.Message}";
                }
            }
            return StatusCode(400, errorMessage);
        }

        [HttpPost]
        [ActionName("LoginAdotante")]
        public IActionResult LoginAdotante(Adotante adotante)
        {
            try
            {
                var usuario = _context.Adotante
                  .Where(s => s.Usuario == adotante.Usuario)
                  .ToList();
                if (usuario.Count == 0)
                {
                    return StatusCode(401, "Erro ao autorizar, usuário inválido");
                }

                usuario = _context.Adotante
                   .Where(s => s.Senha == adotante.Senha)
                   .ToList();
                if (usuario.Count == 1)
                {
                    return StatusCode(200, usuario[0].Id.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return StatusCode(401, "Erro ao Autorizar, senha incorreta");
        }
    }
}
