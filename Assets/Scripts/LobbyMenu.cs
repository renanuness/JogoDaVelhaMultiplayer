using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    public static LobbyMenu _instance = null;
    public GameObject LobbyPlayer;
    public Transform PlayersListUI;
    private MyNetworkManager _networkManager;

    public static LobbyMenu Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<LobbyMenu>();
            }
            return _instance;
        }
    }

	void Awake ()
    {
        _networkManager = MyNetworkManager.Instance;

        Debug.Log("Ativou: Lobby Menu");
    }

    private void Start()
    {
        _networkManager.playerJoined += PlayerJoined;
        RefreshConnectedPlayers();
    }

    public void AddPlayer(LobbyPlayer playerLobby)
    {
        playerLobby.transform.SetParent(PlayersListUI, false);
    }

    private void PlayerJoined()
    {
        //player.transform.SetParent(PlayersListUI);
        RefreshConnectedPlayers();
    }

    private void RefreshConnectedPlayers()
    {
        foreach(var player in _networkManager.connectedPlayers)
        {
            Debug.Log(player.hasAuthority);
            player.lobbyObject.transform.SetParent(PlayersListUI, false);
        }
    }

}
