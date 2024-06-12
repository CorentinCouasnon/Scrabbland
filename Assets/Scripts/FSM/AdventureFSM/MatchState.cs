using System.Linq;
using DG.Tweening;
using MatchFSM;
using UnityEngine;

namespace AdventureFSM
{
    public class MatchState : AdventureState { 
        
        GameUI _gameUI;
        Participant _player;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);

            var match = MatchController.Instance.CreateMatch(AdventureController.Instance.Adventure.Character.CharacterSO, 3);
            MatchController.Instance.Match = match;
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenMatch();
            
            // TODO: Move into MatchWinState on Leave
            var board = Object.FindAnyObjectByType<BoardController>(FindObjectsInactive.Include);
            board.ResetBoard();

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
            if (_player.Score >= _player.Handicap)
                AdventureController.Instance.State = new LocationSelectionState();
        }
    }
}