using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Models
{
    public class Animais
    {//id nome especie raca peso foto
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Especie { get; set; }
        public string Raca { get; set; }
        public int Peso { get; set; }
        public string Foto { get; set; }
    }
}
