using System;
using System.Threading.Tasks;
using MessageContextWithinRebusTransaction.Messages;
using Rebus.Handlers;

namespace MessageContextWithinRebusTransaction.Handlers
{
    public class Handler1: IHandleMessages<TesteMessage>
    {
        public Task Handle(TesteMessage message)
        {
            Console.WriteLine("MessageContextWithinRebusTransaction TesteMessage: "+message.Teste);
            return Task.CompletedTask;
        }
    }
}