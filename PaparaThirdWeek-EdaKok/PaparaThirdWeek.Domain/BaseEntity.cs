using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaparaThirdWeek.Domain
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
