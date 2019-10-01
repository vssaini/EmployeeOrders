using System;
using System.ComponentModel.DataAnnotations;

namespace Employee.Domain.Entities
{
    public class SalesOrder : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }
        public string PONumber { get; set; }

    }
}
