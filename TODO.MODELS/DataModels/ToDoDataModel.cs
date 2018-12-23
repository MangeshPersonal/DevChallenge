using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace TODO.MODELS.DataModels
{
    [Table("ToDo")]
    public class ToDoDataModel: BaseEntity.IEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
