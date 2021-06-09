using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotaTudo
{
    public class Ficha
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }

        public List<Compra> Compras { get; set; }
    }
}
