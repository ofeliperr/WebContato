using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebContato.Models
{
    [Table("Contatos")]
    public class Contato
    {
        [Display(Name = "Código")]
        [Key]
        public int ContatoId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Telefone Residencial")]
        [Required]
        public string TelefoneRes { get; set; }
        [Display(Name = "Telefone Celular")]
        public string TelefoneCel { get; set; }

    }
}
