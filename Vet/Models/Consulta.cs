using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Models
{
    public class Consulta
    {
        [Key]
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }

        //FK
        [ForeignKey(nameof(Vet))]
        public int VetFK { get; set; }
        public Vet Vet { get; set; }

        [ForeignKey(nameof(Animal))]
        public int AnimaisFK { get; set; }
        public Animais Animal { get; set; }
    }
}
