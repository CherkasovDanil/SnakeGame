using Game.UI.UIService.Realization;
using UnityEngine;

namespace Game.UI.UIService.Interfaces
{
    public interface IUIService
    {
        T Show<T>() where T : UICanvasWindow;
        void Hide<T>() where T : UICanvasWindow;
        T Get<T>() where T : UICanvasWindow;

        void InitWindows(Transform poolDeactiveContiner = null);
        void LoadWindows();
    }
}