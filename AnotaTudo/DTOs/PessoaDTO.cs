using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotaTudo
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class PessoaCreationDTO
    {
        public string Nome { get; set; }
    }
}
