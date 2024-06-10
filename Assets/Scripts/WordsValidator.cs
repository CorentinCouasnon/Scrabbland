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
    public void Search()
    {
        var words = _trie.Search(_rack.ToCharArray().ToList());

        Debug.Log($"Words found : {words.Count}");
        
        foreach (var word in words)
        {
            Debug.Log(word);
        }
    }
}