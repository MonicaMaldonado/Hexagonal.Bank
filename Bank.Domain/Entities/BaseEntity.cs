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
        public DateTime CreateAt { get; protected set; }
        public DateTime? UpdateAt { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreateAt = DateTime.UtcNow;
        }
        protected void Delete()
        {
            IsActive = false;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
