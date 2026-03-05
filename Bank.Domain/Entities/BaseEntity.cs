using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Entities
{
    public class BaseEntity //Patron entidad base, crear propiedades en comun que puedan tener las entidades (ids, estados,fechas, usuarios,etc)
    {
        public Guid Id { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        protected void Delete()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
