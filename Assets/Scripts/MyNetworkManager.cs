using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class MyNetworkManager : NetworkManager
{
    private short _minimumPlayers = 2;

    [SerializeField]
    protected NetworkPlayer _networkPlayerPrefab;

    public List<NetworkPlayer> connectedPlayers;

    public event Action<LobbyPlayer> playerJoined; 

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

    public void StartMatchMakerGame(string matchName, Action<bool, MatchInfo> onCreate)
    {
        StartMatchMaker();

        matchMaker.CreateMatch(matchName, (uint)4, true, "", "", "", 0, 0, OnMatchCreate);
        
    }

    public void JoinMatchMakerGame(NetworkID netId, Action<bool, MatchInfo> onJoin)
    {
        Debug.Log("NetID:" + netId);
        matchMaker.JoinMatch(netId, "", "", "", 0, 0, OnMatchJoined);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchCreate(success, extendedInfo, matchInfo);
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
        base.OnMatchJoined(success, extendedInfo, matchInfo);

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

    //public override void OnStartClient(NetworkClient client)
    //{
    //    base.OnStartClient(client);
    //    Debug.Log("Connected:" + client.isConnected);
    //}

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Conectei");
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
        
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //base.OnServerConnect(conn);
        Debug.Log("Conectou alguém");
        //talvez eu crie o código aqui pq precio chamr no server o momento que um cliente se conecta e passar pra ele os players 
        //q  estão conectados pra ele poder instaciar os players obj no lobby dele

    }

    public override void OnStartHost()
    {
        Debug.Log("OnStartHost");
        base.OnStartHost();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log(conn);
        _mainMenuManager.ShowStartMenu();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        // Intentionally not calling base here - we want to control the spawning of prefabs
        Debug.Log("OnServerAddPlayer");
        NetworkPlayer newPlayer = Instantiate(_networkPlayerPrefab);
        DontDestroyOnLoad(newPlayer);
        NetworkServer.AddPlayerForConnection(conn, newPlayer.gameObject, playerControllerId);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        Debug.Log("Trocou a cena");
    }

    public void RegisterNetworkPlayer(NetworkPlayer player)
    {
        connectedPlayers.Add(player);
        player.onPlayerReady += IsPlayersReady;
        Debug.Log("Player adicionado à lista");

        LobbyPlayer lobbyPlayer = player.OnEnterLobbyScene();

        if (playerJoined != null)
        {
            playerJoined(lobbyPlayer);
        }
        
    }
    
    private void IsPlayersReady()
    {
        if(connectedPlayers.Count >= _minimumPlayers)
        {
            foreach (var player in connectedPlayers)
            {
                if (!player.IsReady)
                {
                    break;
                }
                ServerChangeScene("GameScene");
            }
        }
    }
}
