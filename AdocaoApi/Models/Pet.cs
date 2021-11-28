using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdocaoApi.Models
{
    [Table("Pet")]
    public class Pet
    {
        //Dados do tutor que está cadastrando o pet
        [Display(Name = "Id")]
        [Column("Id")]
        public double Id { get; set; }

        [Display(Name = "Usuario")]
        [Column("Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [Column("Senha")]
        public string Senha { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Column("Sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Email")]
        [Column("Email")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [Column("Celular")]
        public string Celular { get; set; }

        [Display(Name = "EnderecoCep")]
        [Column("EnderecoCep")]
        public string EnderecoCep { get; set; }

        [Display(Name = "Cidade")]
        [Column("Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Column("Estado")]
        public string Estado { get; set; }

        //Dados do pet
        [Display(Name = "Animal")]
        [Column("Animal")]
        public string Animal { get; set; }

        [Display(Name = "Porte")]
        [Column("Porte")]
        public string Porte { get; set; }

        [Display(Name = "Genero")]
        [Column("Genero")]
        public string Genero { get; set; }

        [Display(Name = "Vacinas")]
        [Column("Vacinas")]
        public string Vacinas { get; set; }

        [Display(Name = "Raca")]
        [Column("Raca")]
        public string Raca { get; set; }

        [Display(Name = "Cor")]
        [Column("Cor")]
        public string Cor { get; set; }

        [Display(Name = "Img")]
        [Column("Img")]
        public string Img { get; set; }

        public Pet(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }
    }
}
