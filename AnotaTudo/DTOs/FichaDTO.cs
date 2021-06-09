using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotaTudo
{
    public class FichaDTO
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }

        public string DataString => Data.ToString("dd/MMM");
        public IEnumerable<CompraDTO> Compras { get; set; }
    }

    public class FichaCreationDTO
    {
        public DateTime Data { get; set; }
    }
}
