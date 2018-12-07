using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Data.Repo.Entity.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        private DateTime? _createdDate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => this._createdDate ?? DateTime.UtcNow;
            set => this._createdDate = value;
        }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
