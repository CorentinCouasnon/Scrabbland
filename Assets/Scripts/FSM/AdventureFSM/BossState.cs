using System.Linq;
using MatchFSM;
using UnityEngine;

namespace AdventureFSM
{
    public class BossState : AdventureState {
        
        GameUI _gameUI;
        MatchRewardUI _matchRewardUI;
        Participant _player;

        int _goldGained;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            var match = MatchController.Instance.CreateBossMatch(AdventureController.Instance.Adventure.Character.CharacterSO);
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
            
            var finalPosition = MatchController.Instance.Match.Participants.Count(p => p.Score >= p.Handicap);
            
            AdventureController.Instance.Adventure.IsBossDefeated = finalPosition == 1;
            AdventureController.Instance.Adventure.TotalActCompleted += finalPosition == 1 ? 1 : 0;

            _goldGained = GetGoldAmount(finalPosition);
            
            _matchRewardUI = Object.FindAnyObjectByType<MatchRewardUI>(FindObjectsInactive.Include);
            _matchRewardUI.Continued += OnContinued;
            _matchRewardUI.Show(finalPosition == 1 ? "Boss defeated!" : "You lost..", _goldGained);
        }

        void OnContinued()
        {
            AdventureController.Instance.Adventure.Character.Gold += _goldGained;
            
            _matchRewardUI.Continued -= OnContinued;
            _matchRewardUI.Hide();
            AdventureController.Instance.State = new AdventureEndState();
            
            var board = Object.FindAnyObjectByType<BoardController>(FindObjectsInactive.Include);
            board.ResetBoard();
        }
        
        int GetGoldAmount(int finalPosition)
        {
            return finalPosition == 1 ? 500 : 0;
        }
    }
}