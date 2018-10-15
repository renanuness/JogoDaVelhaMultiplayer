using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager> {


    public enum Menus
    {
        StartMenu,
        Lobby
    }

    public CanvasGroup StartMenuPanel;
    public CanvasGroup LobbyPanel;
    public CanvasGroup ListMatchesPanel;
    private CanvasGroup _currentPanel;
    public InputField PlayerNametInput;

	private void Start ()
    {
        _currentPanel = StartMenuPanel;
        ChangePanel(_currentPanel);
	}
	
	private void Update ()
    {
		
	}
    
    //StartMenu
    public void ShowStartMenu()
    {
        ChangePanel(StartMenuPanel);
    }
    
    //LobbyMenu
    public void ShowLobbyMenu()
    {
        ChangePanel(LobbyPanel);
    }



    //ListMatches
    public void ShowListMatchesPanel()
    {
        ChangePanel(ListMatchesPanel);
    }
    

    public void ChangePanel(CanvasGroup newPanel)
    {
        if(_currentPanel != null)
        {
            _currentPanel.gameObject.SetActive(false);
        }

        _currentPanel = newPanel;

        if (_currentPanel != null)
        {
            _currentPanel.gameObject.SetActive(true);
        }
    }
}
