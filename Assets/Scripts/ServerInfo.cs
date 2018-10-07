using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class ServerInfo : MonoBehaviour
{

    public Text GameName;
    public string _gameName;
    
    private MyNetworkManager _networkManager;
    private NetworkID _networkId;

    public void SetGameName(string gameName)
    {
        _gameName = gameName;
    }

    public void SetNetworkId(NetworkID networkId)
    {
        _networkId = networkId;
    }

    public void Init()
    {
        GameName.text = _gameName;
        _networkManager = MyNetworkManager.Instance;
                
    }

    
    public void JoinMatch()
    {

        MainMenuManager mainMenuManager = MainMenuManager.Instance;
        _networkManager.JoinMatchMakerGame(_networkId, (sucess, matchInfo) =>
        {
            if (sucess)
            {
                mainMenuManager.ShowLobbyMenu();
            }
            else
            {

            }
        });
    }
}
