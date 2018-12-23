using System.ComponentModel.DataAnnotations;

namespace TODO.MODELS.APIModel
{
    public class ToDoApiModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
