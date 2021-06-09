using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotaTudo
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Valor { get; set; }
        public FormasPagamento FormaPagamento { get; set; }
        public string FormaPagamentoString => FormaPagamento.ToString();
        public int FichaId { get; set; }
        public int PessoaId { get; set; }
        public PessoaDTO Pessoa { get; set; }
    }

    public class CompraCreationDTO
    {
        public string Item { get; set; }
        public decimal Valor { get; set; }
        public FormasPagamento FormaPagamento { get; set; }
        public int FichaId { get; set; }
        public int PessoaId { get; set; }
    }
}
