using DG.Tweening;
using UnityEngine;

namespace AdventureFSM
{
    public class ShopState : AdventureState { 
        
        GameUI _gameUI;
        ShopUI _shopUI;
        
        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenShop();
            
            _shopUI = Object.FindAnyObjectByType<ShopUI>(FindObjectsInactive.Include);
            _shopUI.Quitted += OnQuitted;
        }
        
        public override void Leave()
        {
            base.Leave();
            
            _shopUI.Quitted -= OnQuitted;
            _gameUI.HideShop();
        }

        void OnQuitted()
        {
            AdventureController.Instance.State = new LocationSelectionState();
        }
    }
}