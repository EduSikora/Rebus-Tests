using System;
using System.Threading.Tasks;
using Rebus.Pipeline;
using Rebus.Transport;

namespace MessageContextWithinRebusTransaction.Pipeline
{
    public class RebusTransactionStep : IIncomingStep
    {
        public async Task Process(IncomingStepContext context, Func<Task> next)
        {
            var contextHeaders = MessageContext.Current.Headers;
            using(var scope = new RebusTransactionScope())
            {
                contextHeaders = MessageContext.Current.Headers;
                await next();
                
                await scope.CompleteAsync();
            }
        }
    }
}