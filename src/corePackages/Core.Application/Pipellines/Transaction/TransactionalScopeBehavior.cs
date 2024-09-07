﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Pipellines.Transaction
{
    public class TransactionalScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest :TRequest<TResponse>, ITransactionalRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using TransactionScope transactionScope = new (TransactionScopeAsyncFlowOption.Enabled);
            TResponse response;
            try
            {
                response = await next();
                 transactionScope.Complete();
            }
            catch (Exception)
            {
                transactionScope.Dispose();
                throw;
            }
            return response;
        }
    }
}