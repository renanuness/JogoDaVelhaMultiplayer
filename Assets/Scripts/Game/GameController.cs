using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Symbol
{
    CROSS,
    CIRCLE
}

public enum GameState
{
    MENU,
    GAME,
    GAME_OVER
}

public static class Players
{
    public static Player PlayerOne;
    public static Player PlayerTwo;
}


public class GameController : NetworkBehaviour
{
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Player PlayerPrefab;

    private GameState _gameState;
    private List<Player> _players = new List<Player>();
    private Symbol _currentPlayer;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        CreatePlayers();
        _gameState = GameState.MENU;
        if (isServer)
        {
            ServerInitPlayers();
        }
        if (isClient)
        {
            ClientSyncPlayers();
        }
    }

    private void Update()
    {
        if(_gameState == GameState.GAME)
        {
            GetPlayerBySymbol(_currentPlayer).Play();
        }
    }

    public void AddPlayer(Player p)
    {
        _players.Add(p);
    }

    private Player GetPlayerBySymbol(Symbol s)
    {
        if(_players.Count != 2)
        {
            return null;
        }

        if(_players[0].GetSymbol() == s)
        {
            return _players[0];
        }
        else
        {
            return _players[1];
        }
    }

    public void SwitchCurrentPlayer()
    {
        if(_currentPlayer == _players[0].GetSymbol())
        {
            _currentPlayer = _players[1].GetSymbol();
        }
        else
        {
            _currentPlayer = _players[0].GetSymbol();
        }
    }

    private void DeletePlayer(Player p)
    {
        if (isServer)
        {
            //Criar callback pra quando o player se desconectar, deletar o player dele dessa lista
            if (_players.Contains(p))
            {
                Predicate<Player> playerFinder = (Player pl) => { return pl.netId == p.netId; };
                _players.Find(playerFinder).SetOwner(null);
            }
        }
    }


    public void CreatePlayers()
    {
        if (_players.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Player go = Instantiate(PlayerPrefab);
                _players.Add(go);
            }
        }
        _currentPlayer = _players[0].GetSymbol();
    }

    public void ServerInitPlayers()
    {
        Symbol symbol = UnityEngine.Random.Range(1, 3) == (int)Symbol.CIRCLE ? Symbol.CIRCLE : Symbol.CROSS;
        if (_players.Count == 0)
        {
            foreach(var player in _players)
            {
                player.GetComponent<Player>().Init(symbol);
                symbol = symbol == Symbol.CIRCLE ? Symbol.CROSS : Symbol.CIRCLE;
            }
        }
        _currentPlayer = _players[0].GetSymbol();

    }

    public void ClientSyncPlayers()
    {
        int i = 0;
        foreach(var player in _players)
        {
            RpcClientInitPlayers(i, (int)player.GetSymbol());
            i++;
        }
    }

    [Command]
    public void CmdSyncPlayers()
    {
        ClientSyncPlayers();
    }
    [ClientRpc]
    public void RpcClientInitPlayers(int index, int symbol)
    {
        _players[index].SetSymbol((Symbol)symbol);
    }

    public void AttributePlayer2Player(NetworkPlayer player)
    {
        if(_players[0].GetOwner() == null)
        {
            _players[0].SetOwner(player);
            player.SetPlayer(_players[0]);
        }
        else if(_players[1].GetOwner() == null)
        {
            _players[1].SetOwner(player);
            player.SetPlayer(_players[1]);
        }
    }
}