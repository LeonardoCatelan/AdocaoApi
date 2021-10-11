using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdocaoApi.Models
{
    [Table("Adotante")]
    public class Adotante
    {
        [Display(Name = "Id")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Column("Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Senha")]
        [Column("Senha")]
        public string Senha { get; set; }

        [Display(Name = "Sobrenome")]
        [Column("Sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Idade")]
        [Column("Idade")]
        public int Idade { get; set; }

        [Display(Name = "Email")]
        [Column("Email")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [Column("Celular")]
        public string Celular { get; set; }

        [Display(Name = "EnderecoCep")]
        [Column("EnderecoCep")]
        public string EnderecoCep { get; set; }

        [Display(Name = "EnderecoNumero")]
        [Column("EnderecoNumero")]
        public string EnderecoNumero { get; set; }

        [Display(Name = "Moradia")]
        [Column("Moradia")]
        public string Moradia { get; set; }

        [Display(Name = "AnimalPreferido")]
        [Column("AnimalPreferido")]
        public string AnimalPreferido { get; set; }

        [Display(Name = "PortePreferido")]
        [Column("PortePreferido")]
        public string PortePreferido { get; set; }

        [Display(Name = "GeneroPreferido")]
        [Column("GeneroPreferido")]
        public string GeneroPreferido { get; set; }

        public Adotante(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }
    }
}
