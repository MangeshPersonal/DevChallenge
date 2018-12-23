using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.BaseEntity
{
    public interface IEntity
    {
         int ID { get; set; }
         string CreatedBy { get; set; }
         string ModifiedBy { get; set; }
         DateTime CreatedOn { get; set; }
         DateTime ModifiedOn { get; set; }

    }
}
