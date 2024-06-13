using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordsValidator : MonoBehaviour
{
    [SerializeField] TextAsset _wordsTxt;
    [SerializeField] string _wordToFind;
    [SerializeField] string _rack;

    Trie _trie;

    void Awake()
    {
        _trie = new Trie(_wordsTxt.text.Split('\n'));
    }

    [ContextMenu("IsWord")]
    public void Debug_IsWord()
    {
        Debug.Log($"{_wordToFind} is in dictionary : {_trie.IsWord(_wordToFind)}");
    }

    public bool IsWord(string word)
    {
        return _trie.IsWord(word);
    }
    
    [ContextMenu("Search rack")]
    public void Debug_Search()
    {
        var words = Search(_rack);

        Debug.Log($"Words found : {words.Count}");
        
        foreach (var word in words)
        {
            Debug.Log(word);
        }
    }

    public List<string> Search(string rack)
    {
        return _trie.Search(rack.ToUpper().ToCharArray().ToList());
    }
}