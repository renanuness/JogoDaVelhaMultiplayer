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

    public List<NetworkLobbyPlayer> connectedPlayers;

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
            connectedPlayers = new List<NetworkLobbyPlayer>();
        }
	}

    private void Start()
    {
        _mainMenuManager = MainMenuManager.Instance;
    }

    public void StartMatchMakerGame(string matchName)
    {

        StartMatchMaker();
        StartHost();
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
            //NetworkServer.Listen(7777);
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
        base.OnClientConnect(conn);
        Debug.Log("Conectei");
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Conectou alguém");

    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        Debug.Log("OnStartHost");
    }

}
