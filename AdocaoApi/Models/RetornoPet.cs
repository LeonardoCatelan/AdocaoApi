using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdocaoApi.Models
{
    public class RetornoPet
    {
        //Dados do tutor que está cadastrando o pet

        public double Id { get; set; }
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        //Dados do pet
        public string Animal { get; set; }
        public string Porte { get; set; }
        public string Genero { get; set; }
        public string Vacinas { get; set; }
        public string Raca { get; set; }
        public string Cor { get; set; }

        public RetornoPet(double id, string usuario, string nome, string sobrenome, 
            string email, string celular, string animal, string porte, string genero,
            string vacinas, string raca, string cor)
        {
            Id = id;
            Usuario = usuario;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Celular = celular;
            Animal = animal;
            Porte = porte;
            Genero = genero;
            Vacinas = vacinas;
            Raca = raca;
            Cor = cor;
        }
    }
}
