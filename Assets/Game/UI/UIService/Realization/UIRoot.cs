using Game.UI.UIService.Interfaces;
using UnityEngine;

namespace Game.UI.UIService.Realization
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform container;
        [SerializeField] private Transform poolContainer;
        
        public Canvas Canvas
        {
            get => canvas;
            set => canvas = value;
        }

        public Transform Container => container;

        public Transform PoolContainer => poolContainer;
    }
}