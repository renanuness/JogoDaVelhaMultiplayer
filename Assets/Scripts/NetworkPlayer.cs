using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
    private MyNetworkManager _networkManager;
    private string name;

    public string Name
    {
        get
        {
            return name;
        }
    }
    private void Start()
    {
        _networkManager = MyNetworkManager.Instance;
        name = "Renan" + Random.Range(0, 10);
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
