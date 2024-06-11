using System.Collections.Generic;

public static class LetterUtils
{
    static readonly List<char> Consonants = new List<char> 
        { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z', };
    
    static readonly List<char> Vowels = new List<char> { 'a', 'e', 'i', 'o', 'u', 'y', };
    
    static readonly List<char> Letters = new List<char>
    {
        'a',
        'b',
        'c',
        'd',
        'e',
        'f',
        'g',
        'h',
        'i',
        'j',
        'k',
        'l',
        'm',
        'n',
        'o',
        'p',
        'q',
        'r',
        's',
        't',
        'u',
        'v',
        'w',
        'x',
        'y',
        'z',
    };

    static readonly Dictionary<char, int> LetterOdds = new Dictionary<char, int>
    {
        { 'e', 1473 },
        { 's', 1001 },
        { 'a', 972 },
        { 'i', 941 },
        { 'r', 852 },
        { 'n', 725 },
        { 't', 683 },
        { 'o', 596 },
        { 'l', 403 },
        { 'u', 367 },
        { 'c', 347 },
        { 'm', 255 },
        { 'p', 234 },
        { 'd', 233 },
        { 'g', 164 },
        { 'b', 144 },
        { 'f', 130 },
        { 'h', 123 },
        { 'z', 105 },
        { 'v', 93 },
        { 'q', 52 },
        { 'y', 40 },
        { 'x', 27 },
        { 'j', 17 },
        { 'k', 9 },
        { 'w', 2 },
    };

    public static Letter CreateRandomLetter()
    {
        return new Letter { Value = Letters.GetRandomWeighted(letter => LetterOdds[letter])};
    }
    
    public static Letter CreateRandomConsonant()
    {
        return new Letter { Value = Consonants.GetRandom()};
    }
    
    public static Letter CreateRandomVowel()
    {
        return new Letter { Value = Vowels.GetRandom()};
    }
}