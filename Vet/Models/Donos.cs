using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Models
{

    public class Donos {
        public Donos() {
            ListAnimais = new HashSet<Animais>();
        }
        //id nome nif(int)
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        [StringLength(40, ErrorMessage = "O {0} nao deve ter mais de {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O {0} e de preenchimento obrigatorio")]
        [StringLength(9,MinimumLength =9, ErrorMessage = "O {0} deve ter exatament {1} caracteres")]
        [RegularExpression("[1356][0-9]{8}",ErrorMessage ="So sao aceites 9 algarismos comecando pelo 1, 3,5 ou 6")]
        public string NIF { get; set; }

        public  ICollection<Animais> ListAnimais { get; set; }

    }
}
