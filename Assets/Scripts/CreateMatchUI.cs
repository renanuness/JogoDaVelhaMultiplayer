using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMatchUI : MonoBehaviour
{
    public InputField MatchName;
    private MyNetworkManager _networkManager;
    private MainMenuManager _mainMenu;
    private void Start()
    {
        _networkManager = MyNetworkManager.Instance;
        _mainMenu = MainMenuManager.Instance;
    }

    public void OnCreateMatchClicked()
    {
        _networkManager.StartMatchMakerGame(MatchName.text, (success, matchInfo) => {
            if (!success)
            {
                Debug.Log("Failed to create game.");
            }
            else
            {
                _mainMenu.ShowLobbyMenu();
            }

        });
    }

    public void OnFindMatchClicked()
    {
        MainMenuManager.Instance.ShowListMatchesPanel();
    }
}
