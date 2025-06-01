using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
       where TCommand : IQuery<TResponse>
       where TResponse : notnull
    {
    }

    public interface IQueryHandler<in TCommand> : IRequestHandler<TCommand, Unit>
      where TCommand : IQuery<Unit>
    {
    }
}
