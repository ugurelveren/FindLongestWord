using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindLongestWords
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please insert path of wordlist file");
            Console.WriteLine("Or just click enter key and use wordlist.txt");
            List<string> wordList = new List<string>();
            var path = Console.ReadLine();
            if (!string.IsNullOrEmpty(path))
            {
                wordList = File.ReadAllLines(path).ToList();
            }
            else
            {
                wordList = File.ReadAllLines("wordlist.txt").ToList();
            }
            
            wordList = wordList.Where(m => !string.IsNullOrEmpty(m)).Distinct().OrderBy(m => m.Length).ToList();
            
            Trie trie = new Trie();
            var concatenatingWords = trie.AddWords(wordList).Distinct().OrderByDescending(m => m.Length).ToList();
            var word1 = concatenatingWords[0];
            var word2 = concatenatingWords[1];

            Console.WriteLine("Longest Word");
            Console.WriteLine(word1);
            

            foreach (var item in trie.GetSubWords(word1))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("Second Longest Word");
            Console.WriteLine(word2);

            foreach (var item in trie.GetSubWords(word2))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("Total Words Count");

            Console.WriteLine(concatenatingWords.Count);

            Console.ReadLine();

        }
    }
    

    public class Trie
    {
        public bool IsWord { get; set; }
        public char Value { get; set; }
        public Trie[] Child = new Trie[26];
        
        public List<string> GetSubWords(string word)
        {
            List<string> subwords = new List<string>();
            GetSubWords(word, 0, subwords);

            return subwords.Distinct().ToList();
        }
        
        public List<string> AddWords(List<string> wordList)
        {
            var concatenatedWordList = new List<string>();
            var mainTrie = this;

            foreach (var item in wordList)
            {
                var trie = mainTrie;

                for (int i = 0; i < item.Length; i++)
                {
                    if (trie.Child[item[i] - 'a'] == null)
                    {
                        trie.Child[item[i] - 'a'] = new Trie();
                        trie.Child[item[i] - 'a'].Value = item[i];
                    }
                    else
                    {
                        if (trie.Child[item[i] - 'a'].IsWord)
                        {
                            var index = i + 1;
                            if (CheckWord(item, index))
                            {
                                
                                    concatenatedWordList.Add(item);
                                

                            }
                        }
                    }
                    trie = trie.Child[item[i] - 'a'];
                }
                trie.IsWord = true;
            }
            return concatenatedWordList;
        }

        private bool GetSubWords(string word , int index , List<string> subwords)
        {
            
            string subWord = string.Empty;
            
            Trie node = this;
            while (index < word.Length && node.Child[word[index] - 'a'] != null)
            {
                
                node = node.Child[word[index++] - 'a'];
                subWord = subWord + node.Value;
                

                if (index == word.Length)
                {
                    subwords.Add(subWord);
                    return true;
                }

                if (node.IsWord)
                {
                    if (GetSubWords(word, index , subwords))
                    {
                        subwords.Add(subWord);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckWord( string word, int i)
        {
            var list = new List<string>();
            Trie node = this;
            while (i < word.Length && node.Child[word[i] - 'a'] != null)
            {
                node = node.Child[word[i++] - 'a'];

                if (i == word.Length) return node.IsWord;

                if (node.IsWord)
                {
                    if (CheckWord( word, i))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
