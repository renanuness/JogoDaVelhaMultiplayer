using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
    private MyNetworkManager _networkManager;

    private void Start()
    {
        _networkManager = MyNetworkManager.Instance;
    }

    private void Update()
    {

    }

    [Client]
    public void OnEnterLobbyScene()
    {
        Debug.Log("Client entered on the lobby");
    }

    [Client]
    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);
        Debug.Log("Client Network Player start");


        if (_networkManager == null)
        {
            _networkManager = MyNetworkManager.Instance;
        }

        base.OnStartClient();

        _networkManager.RegisterNetworkPlayer(this);
    }

}
