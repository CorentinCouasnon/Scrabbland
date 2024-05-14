using DG.Tweening;
using UnityEngine;

namespace AdventureFSM
{
    public class UndergroundState : AdventureState { 
        
        GameUI _gameUI;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenMatch();
            
            var seq = DOTween.Sequence();
            seq.AppendInterval(2f);
            seq.OnComplete(() => { AdventureController.Instance.State = new LocationSelectionState(); });
        }
        
        public override void Leave()
        {
            base.Leave();
            
            _gameUI.HideMatch();
        }
    }
}