using System;

namespace DojoMyBusiness.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString("D");
        }

        public string Id { get; set; }
    }
}
