using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{

    [SerializeField]
    protected GameObject _lobbyPrefab;
    private MyNetworkManager _networkManager;

    [SyncVar(hook = "OnReadyChanged")]
    private bool _isReady = false;
    private string _playerName;
    private MainMenuManager _mainMenuManager;

    public event Action onPlayerReady;
    public event Action<NetworkPlayer> syncVarsChanged;

    public string Name
    {
        get
        {
            return _playerName;
        }
        set
        {
            _playerName = value;
        }
    }

    public bool IsReady
    {
        get
        {
            return _isReady;
        }
    }

    //<summary>
    //Gets the lobby object associated with this player
    //</summary>
    public LobbyPlayer lobbyObject
    {
        get;
        private set;
    }




    private void Start()
    {
        _networkManager = MyNetworkManager.Instance;
        _mainMenuManager = MainMenuManager.Instance;
        _playerName = _mainMenuManager.PlayerNametInput.text;
        //_networkManager.playerJoined += InstantiatePlayerList;
    }

    public void  Ready(bool ready)
    {
        _isReady = ready;
        onPlayerReady();
    }

    private LobbyPlayer CreateLobbyObject()
    {
        lobbyObject = Instantiate(_lobbyPrefab).GetComponent<LobbyPlayer>();
        lobbyObject.PlayerName = _playerName;
        Debug.Log(_playerName + "tem autoridade:" + hasAuthority);
        lobbyObject.Init(this);
        return lobbyObject;
    }

    [Client]
    public LobbyPlayer OnEnterLobbyScene()
    {
        Debug.Log("Client entered on the lobby");
        return CreateLobbyObject();
        
    }

    //Quando começar o modo cliente, instanciar um objeto para o jogador.
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

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("Start local player");

    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        Debug.Log("Start authority");
    }
    //
    private void OnReadyChanged(bool value)
    {
        _isReady = value;

        if (syncVarsChanged != null)
        {
            syncVarsChanged(this);
        }
    }
}
