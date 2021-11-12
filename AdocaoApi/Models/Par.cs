using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdocaoApi.Models
{
    public class Par
    {
        public RetornoPet RetornoPet { get; set; }
        public float Distancia { get; set; }
        public int Matches { get; set; }
        public string MensagemErro { get; set; }
        
        public Par(RetornoPet retornoPet, float distancia, int matches)
        {
            RetornoPet = retornoPet;
            Distancia = distancia;
            Matches = matches;
        }

        public Par(string mensagemErro)
        {
            MensagemErro = mensagemErro;
        }
    }
}
