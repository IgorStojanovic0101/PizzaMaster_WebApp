using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.Domain.ViewModels
{
    public class RestoranViewModel
    {
        public int Id { get; set; }
        public string RestoranIme { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
