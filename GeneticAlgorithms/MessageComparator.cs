using System;

namespace GeneticAlgorithms
{
    public static class MessageComparator
    {
        public static bool Equals(Message a, Message b)
        {
            return CountMatchScore(a, b) == a.message.Length;
        }
        
        
        public static int CountMatchScore(Message a, Message b)
        {
            if (a.message.Length != b.message.Length)
            {
                return -1;
            }
            if (a.message.Length != b.message.Length)
            {
                Console.WriteLine("A and B length don't match");
                throw new InvalidOperationException();
            }
            int score = 0;
            for (int i = 0; i < a.message.Length; i++)
            {
                if (a.message[i] == b.message[i])
                {
                    score++;
                }
            }

            return score;
        }
    }
}