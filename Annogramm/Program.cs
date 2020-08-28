using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Annogramm.DataStructs;
using Annogramm.Properties;

namespace Annogramm
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Console.WriteLine("Загрузка словаря");

            //var root = new CharTreeNodeDict(' ', null);

            LoadDictionary();

            sw.Stop();
            Console.WriteLine($"Загрузка словаря завершена. {(float)sw.ElapsedMilliseconds / 1000} second");

            Console.ReadKey();

            while (true)
            {
                Console.Clear();

                Console.Write("Input: ");
                var word = Console.ReadLine().ToUpper();

                GetAnnograms(word);

                foreach (var r in Results)
                    Console.WriteLine(r);


                Results.Clear();
                //if (CheckPartOfWord(root, word))
                //    Console.WriteLine($"Part of Word '{word}' Exist");
                //else
                //    Console.WriteLine($"Part of Word '{word}' NOT Exist");

                Console.ReadKey();
            }
        }

        static void GetAnnograms(string str)
        {
            LinkedList<char> chars = new LinkedList<char>(str.ToUpper().ToArray());

            foreach (var _ch in chars.Distinct())
            {
                var newChars = new LinkedList<char>(chars.ToArray());
                newChars.Remove(_ch);

                Process(newChars, _ch, curDictNodeDictionary, string.Empty, new List<string>());
            }
        }

        static HashSet<string> Results = new HashSet<string>();

        static ICharTreeNode curDictNodeDictionary = new CharTreeNodeDict(' ', null);

        static void Process(LinkedList<char> chars, char ch, ICharTreeNode curDictNode, string curWord, List<string> words)
        {
            if (!curDictNode.TryGetChild(ch, out curDictNode))
                return;

            curWord += ch;

            //Часть слова
            foreach (var _ch in chars.Distinct())
            {
                var newChars = new LinkedList<char>(chars.ToArray());
                newChars.Remove(_ch);

                Process(newChars, _ch, curDictNode, curWord, words.ToList());
            }

            //Слово
            if (curDictNode.IsEndOfWord)
            {
                words.Add(curWord);
                curWord = string.Empty;

                //Если символы закончились
                if (chars.Count == 0)
                {
                    AddResult(string.Join(" ", words));
                    return;
                }

                foreach (var _ch in chars.Distinct())
                {
                    var newChars = new LinkedList<char>(chars.ToArray());
                    newChars.Remove(_ch);

                    Process(newChars, _ch, curDictNodeDictionary, string.Empty, words.ToList());
                }
            }
        }

        static void LoadDictionary()
        {
            var words = Resources.dict.Replace("\r",string.Empty).Split('\n');

            foreach(var word in words)
            {
                string s = word.ToUpper();

                if (s.Length > 0)
                {
                    foreach (var ch in s)
                        curDictNodeDictionary = curDictNodeDictionary.AddAndGet(ch);

                    curDictNodeDictionary.IsEndOfWord = true;
                    curDictNodeDictionary = curDictNodeDictionary.GetRoot();
                }
            }
        }

        static void AddResult(string str)
        {
            var arr = str.Split(' ');

            Array.Sort(arr);

            Results.Add(string.Join(" ", arr));
        }





        //static bool CheckWord(string word)
        //{
        //    if (word == null || word.Length == 0)
        //        return false;

        //    //storage = storage.GetRoot();

        //    for (int i = 0; i < word.Length; i++)
        //    {
        //        if (!storage.TryGetChild(word[i], out storage))
        //        {
        //            return false;
        //        }

        //        if (i == word.Length - 1)
        //            return storage.IsEndOfWord;
        //    }

        //    return true;
        //}

        //static bool CheckPartOfWord(string word)
        //{
        //    if (word == null || word.Length == 0)
        //        return false;

        //    //storage = storage.GetRoot();

        //    for (int i = 0; i < word.Length; i++)
        //        if (!storage.TryGetChild(word[i], out storage))
        //            return false;

        //    return true;
        //}
    }
}
