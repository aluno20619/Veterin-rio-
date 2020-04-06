using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Models
{
    public class Animais
    {
        public Animais() {
            Listconsultas = new HashSet<Consulta>();
        }
        //id nome especie raca peso foto
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Especie { get; set; }
        public string Raca { get; set; }
        public int Peso { get; set; }
        public string Foto { get; set; }

        public ICollection<Consulta> Listconsultas { get; set; }
        //****************************
        // add da foreign key
        //****************************
        [ForeignKey("Dono")]//anotacao vai assossiar o atri s«donofk ao atr dono
        public int DonoFK { get; set; }
        public Donos Dono { get; set; }
    }
}
