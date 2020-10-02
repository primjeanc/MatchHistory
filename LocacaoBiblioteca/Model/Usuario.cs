using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoBiblioteca.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }
        [Required]
        [MaxLength(30)]
        public string Senha { get; set; }
        public bool Ativo { get; set; } = true;
        //public int UsuarioCriacao { get; set; } = 0;
        //public int UsuarioAlteracao { get; set; } = 0;
        //public DateTime DataCriacao { get; set; } = DateTime.Now;
        //public DateTime DataAlteracao { get; set; } = DateTime.Now;

    }
}
