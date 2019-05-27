using System.Reflection;
using MessageContextWithinRebusTransaction.Handlers;
using MessageContextWithinRebusTransaction.Messages;
using MessageContextWithinRebusTransaction.Pipeline;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Pipeline;
using Rebus.Pipeline.Receive;
using Rebus.Routing.TypeBased;

namespace MessageContextWithinRebusTransaction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var activator = new BuiltinHandlerActivator()
                .Register(() => new Handler1());
                
            Configure.With(activator)
                .Transport(t => t.UseRabbitMq("amqp://192.168.99.100", Assembly.GetEntryAssembly().GetName().Name))
                .Routing(r => r.TypeBased()
                    .Map<TesteMessage>("appMessage")) 
                .Options(options =>
                    options.Decorate<IPipeline>(c =>
                    {
                        var pipeline = c.Get<IPipeline>();
                        var step = new RebusTransactionStep();
                        return new PipelineStepInjector(pipeline)
                            .OnReceive(step, PipelineRelativePosition.After, typeof(ActivateHandlersStep));
                    }))
                .Start();                             
            
            activator.Bus.Subscribe<TesteMessage>().Wait();
            
            activator.Bus.Publish(new TesteMessage("Mensagem Teste.")).Wait();
        }
    }
}