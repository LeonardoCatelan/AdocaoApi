using AdocaoApi.Models;
using AdocaoApi.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
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
        public async Task<IActionResult> CadastroAdotante([Bind("Usuario, Nome, Sobrenome, Idade, Email, Celular, EnderecoCep, Cidade, Estado, AnimalPreferido, PortePreferido, GeneroPreferido")] Adotante adotante)
        {
            //criptografando a senha do usuário para armazenar no banco
            string senhaCriptografada = CriptografarSenha.HashPassword(adotante.Senha); //criptografa a senha
            adotante.Senha = senhaCriptografada;
            //criando um id aleatorio para guardar no banco
            double idAleatorio = double.Parse(DateTime.Now.ToString("ddMMyyHHmmssff"));
            adotante.Id = idAleatorio;
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
        public async Task<IActionResult> CadastroPet([Bind("Usuario, Senha, Nome, Sobrenome, Email, Celular, EnderecoCep, Cidade, Estado, Animal, Porte, Genero, Vacinas, Raca, Cor, Img")] Pet pet)
        {
            //criptografando a senha do usuário para armazenar no banco
            string senhaCriptografada = CriptografarSenha.HashPassword(pet.Senha); //criptografa a senha
            pet.Senha = senhaCriptografada;
            double idAleatorio = double.Parse(DateTime.Now.ToString("ddMMyyHHmmssff"));
            pet.Id = idAleatorio;
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
            //criptografando a senha do usuário para comparar com a armazenada no banco
            string senhaCriptografada = CriptografarSenha.HashPassword(adotante.Senha); //criptografa a senha
            adotante.Senha = senhaCriptografada;
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
                   .Where(s => s.Usuario == adotante.Usuario)
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

        [HttpPost]
        [ActionName("LoginTutor")]
        public IActionResult LoginTutor(Pet pet)
        {
            string senhaCriptografada = CriptografarSenha.HashPassword(pet.Senha); //criptografa a senha
            pet.Senha = senhaCriptografada;
            try
            {
                var usuario = _context.Pet
                  .Where(s => s.Usuario == pet.Usuario)
                  .ToList();
                if (usuario.Count == 0)
                {
                    return StatusCode(401, "Erro ao autorizar, usuário inválido");
                }

                usuario = _context.Pet
                   .Where(s => s.Usuario == pet.Usuario)
                   .Where(s => s.Senha == pet.Senha)
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

        [HttpPost]
        [ActionName("BuscarPet")]
        public List<Par> BuscarPet(double id, float distanciaMaxima) 
        {
            //Busca os dados do usuário com base no ID enviado no request
            var usuario = _context.Adotante
                  .Where(s => s.Id == id)
                  .ToList();

            //Busca os pets que são do mesmo estado que o usuário (google = max de 50000 solicitações de projeto por dia)
            var pets = _context.Pet
                .Where(s => s.Estado == usuario[0].Estado.ToString())
                .ToList();

            var jose = pets.Count();

            string origem = usuario[0].EnderecoCep.ToString();
            string destino = "";
            string apiKey = "AIzaSyAlXtXA8rnDXrWSbrwplRBSgmMa-1lK3Yw";
            string url = "";
            List<int> listaDistancia = new List<int>();

            for(int i = 0; i < pets.Count; i++)
            {
                try {
                //Thread.Sleep(50); Se estourar o número de requisições por segundo da API do google, ligar esse thread sleep e ajustar o tempo de acordo
                destino = pets[i].EnderecoCep.ToString();
                url = $"https://maps.googleapis.com/maps/api/distancematrix/json?destinations={destino}&origins={origem}&key={apiKey}";

                HttpResponseMessage response = WebRequests.Post(url);
                string result = response.Content.ReadAsStringAsync().Result;
                dynamic parsedBody = JObject.Parse(result);

                string distanciaString = parsedBody.rows[0].elements[0].distance.value;
                int resultDistancia = int.Parse(distanciaString);
                
                listaDistancia.Add(resultDistancia);
                }
                catch
                {
                    listaDistancia.Add(0);
                }
            }
            float distanciaMetros = distanciaMaxima * 1000;

            //lista de pares, feita dois dados, o objeto do pet encontrado, e a distancia dele pro adotante
            List<Par> listaPares = new List<Par>();

            int[] matches = new int[listaDistancia.Count];

            for(int  i = 0; i < listaDistancia.Count; i++)
            {
                int aux = 0; //variavel de auxilio que vai ser utilizada para contabilizar as similaridades
                if(listaDistancia[i] < distanciaMetros && listaDistancia[i] != 0)
                {
                    if(usuario[0].AnimalPreferido == pets[i].Animal)
                    {
                        aux = 3;
                    }
                    if (usuario[0].PortePreferido == pets[i].Porte)
                    {
                        aux++;
                    }
                    if (usuario[0].GeneroPreferido == pets[i].Genero)
                    {
                        aux++;
                    }
                    int distanciaKM = listaDistancia[i] / 1000;
                    listaPares.Add
                        (new Par 
                            (new RetornoPet
                                (pets[i].Id, pets[i].Usuario, pets[i].Nome, pets[i].Sobrenome, pets[i].Email, pets[i].Celular, pets[i].Animal, pets[i].Porte, pets[i].Genero, pets[i].Vacinas, pets[i].Raca, pets[i].Cor, pets[i].Img), distanciaKM, aux
                            )   
                        );
                }
            }
            
            if(listaPares.Count == 0)
            {
                listaPares.Add(new Par("Não foi possível encontrar nenhum pet dentro da distância desejada, por favor aumente a distância e tente novamente"));
            }

            //Ordena a lista por matches.
            var listaOrdenada = listaPares.OrderByDescending(p => p.Matches).ToList();
            return listaOrdenada;
        }
    }
}
