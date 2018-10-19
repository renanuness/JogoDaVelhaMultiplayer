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
            if( _instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }

    public GameObject PlayerPrefab;

    private List<Player> _players = new List<Player>();
    private Symbol _currentPlayer;

    private void Start()
    {
        CreatePlayers();
        _currentPlayer = _players[0].GetSymbol();
    }

    private void Update()
    {
        GetPlayerBySymbol(_currentPlayer).Play();
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
        //Criar callback pra quando o player se desconectar, deletar o player dele dessa lista
        if (_players.Contains(p))
        {
            Predicate<Player> playerFinder = (Player pl) => { return pl.netId == p.netId; };  
            _players.Find(playerFinder).SetOwner(null);
        }
    }

    public void CreatePlayers()
    {
        Symbol symbol = UnityEngine.Random.Range(1, 3) == (int)Symbol.CIRCLE ? Symbol.CIRCLE : Symbol.CROSS;
        if (_players.Count == 0)
        {
            for(int i = 0; i < 2; i++)
            {
                var go = Instantiate(PlayerPrefab);
                go.GetComponent<Player>().Init(symbol);
                symbol = symbol == Symbol.CIRCLE ? Symbol.CROSS : Symbol.CIRCLE;
            }
        }
    }
    
    public bool AttributePlayer2Player(NetworkPlayer player)
    {
        if(_players[0].GetOwner() == null)
        {
            _players[0].SetOwner(player);
            player.SetPlayer(_players[0]);
            return true;
        }
        else if(_players[1].GetOwner() == null)
        {
            _players[1].SetOwner(player);
            player.SetPlayer(_players[1]);
            return true;
        }
        return false;
    }
}