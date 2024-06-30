using Proji.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public decimal BasePrice { get; set; }
        public CarType CarType { get; set; }
    }
}
