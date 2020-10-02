using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoBiblioteca.Model
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

        public int UsuarioCriacao { get; set; }=0;

        public int UsuarioAlteracao { get; set; }=0;

        public DateTime DataCriacao { get; set; }= DateTime.Now;

        public DateTime DataAlteracao { get; set; }= DateTime.Now;
    }
}
