using UnityEngine;

namespace Game.UI.UIService.Interfaces
{
    public interface IUIRoot
    {
        Canvas Canvas { get; set; }
        Transform Container { get; }
        Transform PoolContainer { get; }
    }
}