using Game.UI.UIService.Interfaces;
using UnityEngine;

namespace Game.UI.UIService.Realization
{
    public abstract class UICanvasWindow : MonoBehaviour, IUICanvasWindow
    {
        public abstract void Show();

        public abstract void Hide();
    }
}