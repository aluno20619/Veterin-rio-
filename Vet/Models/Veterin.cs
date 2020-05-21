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
        
        [Required]
        public string Nome { get; set; }
        
        [Display(Name = "Cedula Profissional")]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [StringLength(9)]
        [RegularExpression("vet-[0-9]{5}", ErrorMessage = "O {0} deve ser escrito em minusculas começar por vet- seguido de 5 characters")]
        public string NumCedulaProf { get; set; }
        
        public string Fotografia { get; set; }
        
        public virtual ICollection<Consulta> Listconsultas { get; set; }
    }
}
