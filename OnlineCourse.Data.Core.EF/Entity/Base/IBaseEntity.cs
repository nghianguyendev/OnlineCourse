using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Data.Repo.Entity.Base
{
    public interface IBaseEntity
    {
        DateTime CreatedDate { get; set; }
        
        DateTime? ModifiedDate { get; set; }

        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }

        byte[] Version { get; set; }
    }
}
