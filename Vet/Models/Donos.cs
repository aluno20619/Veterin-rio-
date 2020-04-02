using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Models
{
   
    public class Donos{
        public  Donos() {
        ListAnimais = new HashSet<Animais>();
    }
    //id nome nif(int)
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string NIF { get; set; }
        public  ICollection<Animais> ListAnimais { get; set; }

    }
}
