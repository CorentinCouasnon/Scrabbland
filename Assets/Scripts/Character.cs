﻿using System;

public struct Character
{
    int _intelligence;
    int _speed;
    int _wisdom;
    int _gold;
    
    public CharacterSO CharacterSO { get; private set; }

    public int Intelligence
    {
        readonly get => _intelligence;
        private set
        {
            if (value == _intelligence)
                return;

            var prev = _intelligence;
            _intelligence = value;
            IntelligenceChanged?.Invoke(prev, value);
        }
    }

    public int Speed
    {
        readonly get => _speed;
        private set
        {
            if (value == _speed)
                return;

            var prev = _speed;
            _speed = value;
            SpeedChanged?.Invoke(prev, value);
        }
    }

    public int Wisdom
    {
        readonly get => _wisdom;
        private set
        {
            if (value == _wisdom)
                return;

            var prev = _wisdom;
            _wisdom = value;
            WisdomChanged?.Invoke(prev, value);
        }
    }

    public int Gold
    {
        readonly get => _gold;
        private set
        {
            if (value < 0)
                value = 0;
            
            if (value == _gold)
                return;

            var prev = _gold;
            _gold = value;
            GoldChanged?.Invoke(prev, value);
        }
    }

    public Action<int, int> IntelligenceChanged { get; set; }
    public Action<int, int> SpeedChanged { get; set; }
    public Action<int, int> WisdomChanged { get; set; }
    public Action<int, int> GoldChanged { get; set; }

    public Character(CharacterSO characterSo) : this()
    {
        CharacterSO = characterSo;
        _intelligence = CharacterSO.BaseIntelligence;
        _speed = CharacterSO.BaseSpeed;
        _wisdom = CharacterSO.BaseWisdom;
    }
}