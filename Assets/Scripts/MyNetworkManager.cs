using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class MyNetworkManager : NetworkManager
{
    [SerializeField]
    protected NetworkPlayer _networkPlayerPrefab;

    public List<NetworkPlayer> connectedPlayers;

    public static MyNetworkManager Instance
    {
        get;
        private set;
    }

    private MainMenuManager _mainMenuManager;

	private void Awake ()
    {
		if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            connectedPlayers = new List<NetworkPlayer>();
        }
	}

    private void Start()
    {
        _mainMenuManager = MainMenuManager.Instance;
    }

    public void StartMatchMakerGame(string matchName)
    {
        StartHost();
        StartMatchMaker();

        matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public void JoinMatchMakerGame(NetworkID netId, Action<bool, MatchInfo> onJoin)
    {
        matchMaker.JoinMatch(netId, "", "", "", 0, 0, OnMatchJoined);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            _mainMenuManager.ShowLobbyMenu();
        }
        else
        {
            Debug.Log(matchInfo);
            Debug.Log(extendedInfo);
        }
    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {

        if (success)
        {
            Debug.Log("Sucesso:" + extendedInfo);
            Debug.Log("Sucesso:" + matchInfo);
            _mainMenuManager.ShowLobbyMenu();
        }
        else
        {
            Debug.Log(extendedInfo);
            Debug.Log(matchInfo);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Conectei");
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
        
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Conectou alguém");

    }

    public override void OnStartHost()
    {
        Debug.Log("OnStartHost");
        base.OnStartHost();
    }
     
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        // Intentionally not calling base here - we want to control the spawning of prefabs
        Debug.Log("OnServerAddPlayer");

        NetworkPlayer newPlayer = Instantiate<NetworkPlayer>(_networkPlayerPrefab);
        DontDestroyOnLoad(newPlayer);
        NetworkServer.AddPlayerForConnection(conn, newPlayer.gameObject, playerControllerId);
    }

    public void RegisterNetworkPlayer(NetworkPlayer player)
    {
        connectedPlayers.Add(player);
        Debug.Log("Player adicionado à lista");
    }
}
