using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

using System.Linq;

namespace Ordgaater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*string path = "ordliste.txt";
            var allLines = File.ReadAllLines(path);
            string currentWord = "";

            for (int index = 0; index < 10; index++)
            {
                string line = allLines[index];
                string[] splitLine = line.Split('\t');

                if(currentWord != splitLine[1]) Console.WriteLine(splitLine[1]);
                
                currentWord = splitLine[1];

            }*/

            /*Velg et tilfeldig ord fra lista. 
                Se om det finnes et annet ord som begynner på de 3 samme bokstavene som det tilfeldige ordet slutter på.
                Hvis ikke, prøv samme med 4 og 5.Hvis du finner en match, skriv ut begge ordene.*/

            

            string[] allWords = getWordsArray("ordliste.txt");

            findCorrespondingWords(allWords, 200);

            


        }

        static string[] getWordsArray(string path)
        {
            var allLines = File.ReadAllLines(path);
            string currentWord = "";                //currentWord fungerer som lastWord. Bruk string.Empty i stedet for ""
            List<string> list = new List<string>();

            for (int index = 0; index < allLines.Length; index++)
            {
                string line = allLines[index];
                string[] splitLine = line.Split('\t');

                if (currentWord != splitLine[1] && (splitLine[1].Length < 11 && splitLine[1].Length > 6) && !splitLine[1].Contains("-")) list.Add(splitLine[1]);

                currentWord = splitLine[1];

            }
            return list.ToArray();

        }

        static string findWord(string randomWord, string[] allLines, int numberOfLetters) //all lines bør hete all words
        {
            string lastLetters = randomWord.Substring(randomWord.Length - numberOfLetters);

            for (int i = 0; i < allLines.Length; i++)
            {
                if ((allLines[i].Substring(0, numberOfLetters) == lastLetters))//&& wordIsInWordList(lastLetters, allLines)
                {
                    Console.WriteLine("felles bokstaver: " + lastLetters);
                    return allLines[i];

                }
                
            }
            return "";//returnere String.Empty i stedet??
        }

        static void findCorrespondingWords(string[] allLines, int numberOfPairs)  //all lines bør hete all words
        {
            Random rnd = new Random();
            int pairCount = 0;

            while(pairCount < numberOfPairs)        //for (int j = 0; j < numberOfPairs; j++)
            {

                int randomIndex = rnd.Next(0, allLines.Length);

                string randomWord = allLines[randomIndex];
                string lastThreeLetters = randomWord.Substring(randomWord.Length - 3);
                //Console.WriteLine("Tilfeldig ord: " + randomWord);
                //Console.WriteLine(lastThreeLetters);

                for (int i = 4; i <= 5; i++)
                {
                    string foundWord = findWord(randomWord, allLines, i);
                    if (foundWord != "")
                    {
                        Console.WriteLine("ordpar nr: " + pairCount);
                        Console.WriteLine(randomWord);
                        Console.WriteLine(foundWord);
                        Console.WriteLine("\n");
                        pairCount++;
                        break;
                    }
                    
                }
            }

        }

        static bool wordIsInWordList(string word, string[] allLines) //all lines bør hete all words
        {
            for (int i = 0; i < allLines.Length; i++)
            {
                //Console.WriteLine("wordIsInWordList kjorer, " +word);
                if (allLines.Contains(word))
                {
                    //Console.WriteLine("ord finnes: " + word);
                    return true;

                }

                /*
                if (allLines[i].Trim() == word.Trim())
                {
                    Console.WriteLine("ord finnes: " + word);
                    return true;

                }*/

            }
            return false;

        }
    }
}