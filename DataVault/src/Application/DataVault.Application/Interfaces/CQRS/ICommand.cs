﻿namespace DataVault.Application.Interfaces.CQRS;

using MediatR;

public interface ICommand<out TResult> : IRequest<IOperationResult<TResult>>
{
}