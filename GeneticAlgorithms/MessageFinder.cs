using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithms
{
    public class MessageFinder
    {
        private Message[] MessagePool;
        private readonly int[] scores;

        private readonly double mutationRate;
        private readonly Message _answerMessage;
        private string lettersSupport = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ .,?;:";

        private readonly int time;
        

        public MessageFinder(Message answer,double mutationRate,  int population)
        {
            MessagePool = new Message[population];
            scores = new int[population];
            this.mutationRate = mutationRate;
            _answerMessage = answer;
            GeneratePool();
//
            while (!SpawnNextGeneration())
            {
                Console.WriteLine($"Iterate for the {++time} times");
                int mostScoreIndex = -1;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] > mostScoreIndex)
                    {
                        mostScoreIndex = i;
                    }
                }

                Console.WriteLine("The closest is "+MessagePool[mostScoreIndex].message);
            }

        }

        private bool SpawnNextGeneration()
        {
            CalculateScore();
            Message[] newMessagePool = new Message[MessagePool.Length];
            for (int i = 0; i < MessagePool.Length; i++)
            {
                Message[] parents = PickParent();
                Message child = CrossOver(parents[0],parents[1]);
                child = Mutation(child);
                if (MessageComparator.Equals(child,_answerMessage))
                {
                    Console.WriteLine("Has generate a child: "+child.message);
                    return true;
                }
                newMessagePool[i] = child;
            }

            MessagePool = newMessagePool;
            return false;

        }

        private Message Mutation(Message originalMessage)
        {
            double mutationNum = mutationRate * 100;
            StringBuilder sb = new StringBuilder();
            if (new Random().Next(0, 100) <= mutationNum)
            {
                for (int i = 0; i < originalMessage.message.Length; i++)
                {
                
                    if (new Random().Next(0, 100) <= mutationNum)
                    {
                        char randomAlphabet = lettersSupport[new Random().Next(0, lettersSupport.Length)];
                        sb.Append(randomAlphabet);
                    }
                    else
                    {
                        sb.Append(originalMessage.message[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < originalMessage.message.Length; i++)
                {
                
                        sb.Append(originalMessage.message[i]);
                }
            }

            
            originalMessage.message = sb.ToString();
            return originalMessage;
        }

        private Message CrossOver(Message father, Message mother)
        {
            string fatherString = father.message;
            string motherString = father.message;
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fatherString.Length; i++)
            {
                if (random.NextDouble() > 0.5)
                {
                    sb.Append(fatherString[i]);
                }
                else
                {
                    sb.Append(motherString[i]);
                }
            }
//            string childString = fatherString.Substring(0, fatherString.Length / 2) +
//                                 motherString.Substring(fatherString.Length / 2);
            return new Message(sb.ToString());
        }

        private void GeneratePool()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < MessagePool.Length; i++)
            {
                for (int j = 0; j < _answerMessage.message.Length; j++)
                {
                    char randomAlphabet = lettersSupport[new Random().Next(0, lettersSupport.Length)];
                    sb.Append(randomAlphabet);
                }

                MessagePool[i] = new Message(sb.ToString());
                sb.Clear();
            }

        }

        private void CalculateScore()
        {
            for (int i = 0; i < MessagePool.Length; i++)
            {
                int score = MessageComparator.CountMatchScore(_answerMessage, MessagePool[i]);
                score *= 4;
                scores[i] = score;
            }
        }

        private Message[] PickParent()
        {
            var parentPool = new List<Message>();
            for (int i = 0; i < scores.Length; i++)
            {
                
                for (int j = 0; j < scores[i]; j++)
                {
                    parentPool.Add(MessagePool[i]);
                }
            }

            return new[]{parentPool[new Random().Next(0, parentPool.Count)], parentPool[new Random().Next(0, parentPool.Count)]};
            
        }
        
        
        
        
    }
}