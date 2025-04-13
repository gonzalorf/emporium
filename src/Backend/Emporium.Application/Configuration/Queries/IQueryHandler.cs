﻿using MediatR;

namespace Emporium.Application.Configuration.Queries;
public interface IQueryHandler<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{

}