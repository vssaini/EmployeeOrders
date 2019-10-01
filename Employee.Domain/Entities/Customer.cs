using System;

namespace Employee.Domain.Entities
{
    public class Customer: IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public string CustomerName { get; set; }
        public string Address { get; set; }
    }
}
