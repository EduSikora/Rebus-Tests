namespace MessageContextWithinRebusTransaction.Messages
{
    public class TesteMessage
    {
        public TesteMessage(string test)
        {
            Teste = test;
        }

        public string Teste { get; set; }
    }
}