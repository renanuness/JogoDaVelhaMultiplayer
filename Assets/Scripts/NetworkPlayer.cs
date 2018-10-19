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
    private GameController _gameController;

    [SyncVar(hook = "OnReadyChanged")]
    private bool _isReady = false;
    private string _playerName;
    private MainMenuManager _mainMenuManager;
    private Player _player;

    public event Action onPlayerReady;
    public event Action<NetworkPlayer> syncVarsChanged;
    public event Action<NetworkPlayer> onAuthorityStart;

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
        _gameController = GameController.Instance;

        _playerName = _mainMenuManager.PlayerNametInput.text;
        //_networkManager.playerJoined += InstantiatePlayerList;
    }

    public void  Ready(bool ready)
    {
        _isReady = ready;
        onPlayerReady();
    }

    public override void OnStartServer()
    {
        Debug.Log("OnStartServer:" + hasAuthority);
    }

    private void CreateLobbyObject()
    {

        lobbyObject = Instantiate(_lobbyPrefab).GetComponent<LobbyPlayer>();
        lobbyObject.PlayerName = _playerName;
    }

    public Symbol GetPlayerSymbol()
    {
        return _player.GetSymbol() ;
    }

    [Client]
    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);
        Debug.Log("Client Network Player start");

        base.OnStartClient();
        if (_networkManager == null)
        {
            _networkManager = MyNetworkManager.Instance;
        }

        _networkManager.RegisterNetworkPlayer(this);
    }



    [Client]
    public void OnEnterLobbyScene()
    {
        Debug.Log("Client entered on the lobby");
         CreateLobbyObject();
    }
    #region Overrides
    //Quando começar o modo cliente, instanciar um objeto para o jogador.

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("Start local player");

    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        onAuthorityStart(this);
        lobbyObject.Init(this);
    }

    #endregion
    

    private void OnReadyChanged(bool value)
    {
        _isReady = value;
        lobbyObject.UpdateButton(this);
    }

    #region Commands

    [Command]
    public void CmdSetReady(bool ready)
    {
        _isReady = ready;
        if (ready)
        {
            if (onPlayerReady != null)
            {
                onPlayerReady();
            }
        }
    }

    #endregion

    #region RPC

    #endregion

    //


    //TODO: no momento em que entrar na cena de menu antes da partida
    // será instanciado um player OBJ pra cada playerNetwork, com síbolo aleatório, o player um sempre irá começar a partida
    //

    #region 
    public void SetPlayer(Player p)
    {
        _player = p;
    }
    #endregion
}
