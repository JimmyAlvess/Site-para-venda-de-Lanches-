using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage ="O nome do lanche deve ser informado")]
        [Display(Name = "Nome do lanche")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição do lanche")]
        [MinLength(20, ErrorMessage ="Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage ="Descrição pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage ="A descrição detalhada deve ser informada")]
        [Display(Name = "Descrição detalhada")]
        [MinLength(20, ErrorMessage ="Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage ="Descrição detalhada pode excer {1} caracteres ")]
        public string DescricaoDetalhada  { get; set; }

        [Required(ErrorMessage ="O preço deve ser informado" )]
        [Display(Name ="Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,99.99,ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage ="0 {0} deve ter no máximo {1} caracteres")]
        public string  ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "0 {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Prefido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name ="Estoque")]
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; } 
    }
}

