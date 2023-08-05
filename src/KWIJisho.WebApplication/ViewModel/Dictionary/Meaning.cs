using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWIJisho.WebApplication.ViewModel.Dictionary
{
    public class Meaning
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("Name")]
        [Key]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}