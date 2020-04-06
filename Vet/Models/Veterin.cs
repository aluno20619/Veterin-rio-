using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Models
{
    public class Veterin
    {
        public Veterin() {
            Listconsultas = new HashSet<Consulta>();
        }
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string NumCedulaProf { get; set; }
        public string Fotografia { get; set; }
        public ICollection<Consulta> Listconsultas { get; set; }
    }
}
