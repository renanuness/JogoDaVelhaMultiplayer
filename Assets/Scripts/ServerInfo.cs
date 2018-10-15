using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class ServerInfo : MonoBehaviour
{
    public Text GameName;
    public string _gameName;
    
    private MyNetworkManager _networkManager;
    private MatchInfoSnapshot _matchInfo;

    public void SetMatchInfo(MatchInfoSnapshot info)
    {
        _matchInfo = info;
    }

    public void Init()
    {
        GameName.text = _gameName;
        _networkManager = MyNetworkManager.Instance;
    }
    
    public void JoinMatch()
    {
        MainMenuManager mainMenuManager = MainMenuManager.Instance;
        if(_networkManager.matchMaker == null)
        {
            Debug.Log("matchmaker equals null");
            _networkManager.StartMatchMaker();
        }
        _networkManager.JoinMatchMakerGame(_matchInfo.networkId, (sucess, matchInfo) =>
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
