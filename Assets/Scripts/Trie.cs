using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trie
{
    public struct Letter
    {
        public const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static implicit operator Letter(char c)
        {
            return new Letter { Index = Chars.IndexOf(char.ToUpper(c)) };
        }

        public int Index;

        public char ToChar()
        {
            return Chars[Index];
        }

        public override string ToString()
        {
            return Chars[Index].ToString();
        }
    }

    public class Node
    {
        public string Word;

        public bool IsTerminal => Word != null;

        public Dictionary<Letter, Node> Edges = new Dictionary<Letter, Node>();
    }

    public Node Root = new Node();

    public Trie(string[] words)
    {
        for (int w = 0; w < words.Length; w++)
        {
            var word = words[w];
            var node = Root;
            for (int len = 1; len <= word.Length; len++)
            {
                var letter = word[len - 1];
                Node next;
                if (!node.Edges.TryGetValue(letter, out next))
                {
                    next = new Node();
                    if (len == word.Length)
                    {
                        next.Word = word;
                    }

                    node.Edges.Add(letter, next);
                }

                node = next;
            }
        }
    }
    
    public bool IsWord(string word)
    {
        var currentNode = Root;

        foreach (var c in word.ToCharArray()) 
        {
            if (!currentNode.Edges.ContainsKey(c)) 
                return false;

            currentNode = currentNode.Edges[c];
        }

        return currentNode.IsTerminal;
    }

    public List<string> Search(List<char> letters)
    {
        var words = new List<string>();
        var result = new List<Node>();
        GetAllNodes(Root, result);
        
        foreach (var node in result)
        {
            if (!node.IsTerminal)
                continue;
            
            if (CanMakeWord(node.Word, letters))
                words.Add(node.Word);
        }

        return words;
    }

    static void GetAllNodes(Node node, List<Node> result)
    {
        if (node == null)
            return;

        result.Add(node);

        foreach (var child in node.Edges.Values)
        {
            GetAllNodes(child, result);
        }
    }

    static bool CanMakeWord(string word, List<char> letters)
    {
        var letterCount = new Dictionary<char, int>();
        
        foreach (var letter in letters)
        {
            if (letterCount.ContainsKey(letter))
                letterCount[letter]++;
            else
                letterCount[letter] = 1;
        }

        foreach (var letter in word)
        {
            if (!letterCount.ContainsKey(letter) || letterCount[letter] <= 0)
                return false;
            
            letterCount[letter]--;
        }

        return true;
    }
}