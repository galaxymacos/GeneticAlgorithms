namespace GeneticAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Message targetMessage = new Message("To be or not to be");
            MessageFinder messageFinder = new MessageFinder(targetMessage, 0.01,100);

        }
    }
}