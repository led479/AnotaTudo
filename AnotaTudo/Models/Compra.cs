using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotaTudo
{
    public class Compra
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Valor { get; set; }
        public FormasPagamento FormaPagamento { get; set; }
        public int FichaId { get; set; }
        public Ficha Ficha { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
