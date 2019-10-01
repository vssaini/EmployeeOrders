using System;

namespace Employee.Domain
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? DeletedOn { get; set; }

    }
}
