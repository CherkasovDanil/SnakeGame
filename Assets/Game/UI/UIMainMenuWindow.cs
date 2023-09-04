using System;
using Game.UI.UIService.Interfaces;
using Game.UI.UIService.Realization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIMainMenuWindow : UICanvasWindow
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private IUIService _uiService;
    [Inject]
    private void Construct(IUIService uiService)
    {
        _uiService = uiService;
    }
    
    public override void Show()
    {
        gameObject.SetActive(true);
        
        playButton.onClick.AddListener(OnPlayButtonClickEventHandler);
        quitButton.onClick.AddListener(OnExitButtonClickEventHandler);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        
        playButton.onClick.RemoveListener(OnPlayButtonClickEventHandler);
        quitButton.onClick.RemoveListener(OnExitButtonClickEventHandler);
    }
    
    private void OnPlayButtonClickEventHandler()
    {
        OnPlayButtonClickEvent?.Invoke(this,EventArgs.Empty);
    }
    
    private void OnExitButtonClickEventHandler()
    {
        OnExitButtonClickEvent?.Invoke(this,EventArgs.Empty);
    }
    
    public EventHandler OnPlayButtonClickEvent
    {
        get;
        set;
    }

    public EventHandler OnExitButtonClickEvent
    {
        get;
        set;
    }
}
