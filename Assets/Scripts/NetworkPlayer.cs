using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{

    private void Start()
    {

    }

    private void Update()
    {

    }

    [Client]
    public void OnEnterLobbyScene()
    {
        Debug.Log("Client entered on the lobby");
    }
}
