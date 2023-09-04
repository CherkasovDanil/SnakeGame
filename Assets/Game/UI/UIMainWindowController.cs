using System;
using Game.UI.UIService.Interfaces;
using UnityEngine;

namespace Game.UI
{
    public class UIMainWindowController
    {
        private readonly IUIService _uiService;
        private UIMainMenuWindow _uiMainMenuWindow;

        public UIMainWindowController(IUIService uiService)
        {
            _uiService = uiService;
            
            _uiMainMenuWindow = _uiService.Get<UIMainMenuWindow>();

            _uiMainMenuWindow.OnPlayButtonClickEvent += OnPlayButtonClickEventHandler;
            _uiMainMenuWindow.OnExitButtonClickEvent += OnExitButtonClickEventHandler;
        }
        
        private void OnPlayButtonClickEventHandler(object sender, EventArgs e)
        {
           _uiService.Hide<UIMainMenuWindow>();
            
            /*_uiGameWindowController.StartNewGame();*/
        }
        private void OnExitButtonClickEventHandler(object sender, EventArgs e)
        {
            _uiService.Hide<UIMainMenuWindow>();
            Application.Quit();
        }
    }
}