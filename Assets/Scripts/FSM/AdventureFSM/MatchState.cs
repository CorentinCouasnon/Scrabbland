using DG.Tweening;
using MatchFSM;
using UnityEngine;

namespace AdventureFSM
{
    public class MatchState : AdventureState { 
        
        GameUI _gameUI;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            MatchController.Instance.Match = MatchController.Instance.CreateMatch(AdventureController.Instance.Adventure.Character.CharacterSO, 3);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenMatch();

            MatchController.Instance.State = new InitializingState();
            
            var seq = DOTween.Sequence();
            seq.AppendInterval(100f);
            seq.OnComplete(() => { AdventureController.Instance.State = new LocationSelectionState(); });
        }
        
        public override void Leave()
        {
            base.Leave();
            
            _gameUI.HideMatch();
        }
    }
}