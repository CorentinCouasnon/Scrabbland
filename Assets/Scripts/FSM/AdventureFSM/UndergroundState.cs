using System.Linq;
using MatchFSM;
using UnityEngine;

namespace AdventureFSM
{
    public class UndergroundState : AdventureState 
    {
        GameUI _gameUI;
        MatchRewardUI _matchRewardUI;
        Participant _player;

        int _goldGained;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);

            var match = MatchController.Instance.CreateMatch(AdventureController.Instance.Adventure.Character.CharacterSO, 3, 1, 5);
            MatchController.Instance.Match = match;
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenMatch();

            MatchController.Instance.State = new InitializingState();

            _player = match.Participants.Single(p => p.IsPlayer);
            _player.ScoreChanged += OnScoreChanged;
        }
        
        public override void Leave()
        {
            base.Leave();

            _player.ScoreChanged -= OnScoreChanged;
            _gameUI.HideMatch();
        }

        void OnScoreChanged(int newScore)
        {
            if (_player.Score < _player.Handicap)
                return;

            MatchController.Instance.State = null;

            var finalPosition = MatchController.Instance.Match.Participants.Count(p => p.Score >= p.Handicap);

            AdventureController.Instance.Adventure.TotalOpponentDefeated += 4 - finalPosition;
            
            _goldGained = GetGoldAmount(finalPosition);
            
            _matchRewardUI = Object.FindAnyObjectByType<MatchRewardUI>(FindObjectsInactive.Include);
            _matchRewardUI.Continued += OnContinued;
            _matchRewardUI.Show(finalPosition == 1 ? "Match win!" : "Match lost..", _goldGained);
        }

        void OnContinued()
        {
            AdventureController.Instance.Adventure.Character.Gold += _goldGained;
            
            _matchRewardUI.Continued -= OnContinued;
            _matchRewardUI.Hide();
            AdventureController.Instance.State = new LocationSelectionState();
            
            var board = Object.FindAnyObjectByType<BoardController>(FindObjectsInactive.Include);
            board.ResetBoard();
        }
        
        int GetGoldAmount(int finalPosition)
        {
            var baseAmount = 0;
            var variation = 0;
        
            switch (finalPosition)
            {
                case 1:
                    baseAmount = 180;
                    variation = 20;
                    break;
                case 2:
                    baseAmount = 120;
                    variation = 20;
                    break;
                case 3:
                    baseAmount = 60;
                    variation = 20;
                    break;
                case 4:
                    baseAmount = 20;
                    variation = 10;
                    break;
            }
        
            return (int) (baseAmount + Random.value * variation);
        }
    }
}