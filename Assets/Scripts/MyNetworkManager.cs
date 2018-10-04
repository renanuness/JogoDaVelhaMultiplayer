using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class MyNetworkManager : NetworkManager
{

    public List<NetworkLobbyPlayer> connectedPlayers;

    public static MyNetworkManager Instance
    {
        get;
        private set;
    }

	private void Awake () {
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
	
    public void StartMatchMakerGame(string matchName)
    {
        StartMatchMaker();
        matchMaker.CreateMatch(matchName, 0, false, "", "", "", 0, 0, OnMatchCreate);
    }

    public void JoinMatchMakerGame(NetworkID netId)
    {
        matchMaker.JoinMatch(netId, "", "", "", 0, 0, OnMatchJoined);
    }

    public void ListMatchMakerGames()
    {
        matchMaker.ListMatches(1, 10, "", false, 0, 0, OnMatchList);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchCreate(success, extendedInfo, matchInfo);
    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchJoined(success, extendedInfo, matchInfo);
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        base.OnMatchList(success, extendedInfo, matchList);
    }

}
