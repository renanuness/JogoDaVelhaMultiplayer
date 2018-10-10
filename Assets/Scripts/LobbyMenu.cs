using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    public GameObject LobbyPlayer;
    public Transform PlayersListUI;
    private MyNetworkManager _networkManager;

	void Start ()
    {
        _networkManager = MyNetworkManager.Instance;
        _networkManager.playerJoined += InstantiatePlayerList;
        Debug.Log("Ativou: Lobby Menu");
	}
	
	void Update ()
    {
	    	
	}

    private void InstantiatePlayerList(NetworkPlayer player)
    {
        var go = Instantiate(LobbyPlayer, PlayersListUI);
        go.GetComponent<LobbyPlayer>().PlayerName = player.Name;
        go.GetComponent<LobbyPlayer>().Init();
    } 
}
