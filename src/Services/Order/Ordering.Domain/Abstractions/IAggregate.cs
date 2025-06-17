using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public interface IAggregate : IEntity
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }

        IDomainEvent[] ClearDomainEvents();
    }


    public interface IAggregate<T> : IAggregate, IEntity<T> where T : IEquatable<T>
    {
        
    }
}
