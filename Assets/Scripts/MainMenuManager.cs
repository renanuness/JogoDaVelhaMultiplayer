using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {


    public enum Menus
    {
        StartMenu,
        Lobby
    }

    public CanvasGroup StartMenuPanel;
    public CanvasGroup LobbyPanel;

    private CanvasGroup _currentPanel;

	private void Start ()
    {
		
	}
	
	private void Update ()
    {
		
	}

    public void ShowStartMenu()
    {
        ChangePanel(StartMenuPanel);
    }

    public void ShowLobbyMenu()
    {
        ChangePanel(LobbyPanel);
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
