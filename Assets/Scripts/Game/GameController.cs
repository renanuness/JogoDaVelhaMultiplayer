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

public interface IPlayer
{
    void Play();
}

[Serializable]
public class Player : IPlayer {

    Symbol _symbol;
    NetworkPlayer _owner;

    public Player()
    {

    }

    public Player(NetworkPlayer owner, Symbol symbol)
    {
        _owner = owner;
        _symbol = symbol;
    }

    public void SetSymbol(Symbol s)
    {
        _symbol = s;
    }

    public Symbol GetSymbol()
    {
        return _symbol;
    }

    public NetworkPlayer GetOwner()
    {
        return _owner;
    }

    public void SetOwner(NetworkPlayer player)
    {
        _owner = player;
    }

    public void Play()
    {
        if (BoardController.Instance.GetEmptys() == 0)
        {
            BoardController.Instance.EndGame();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Linecast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition);
                if (hit.collider.gameObject != null)
                {
                    if (BoardController.Instance.MakeMove(BoardController.Instance.SquareToPosition(hit.collider.gameObject), _symbol))
                    {
                        Symbol winner;
                        if (BoardController.Instance.CheckWin(BoardController.Instance.board, out winner))
                        {
                            //win;
                            BoardController.Instance.EndGame(this);
                        }
                        //_owner.ChangePlayer();
                    }
                }
            }
        }
    }
}

public static class Players
{
    public static Player PlayerOne;
    public static Player PlayerTwo;
}

public static class PlayerFactory
{
    public static Player CreatePlayer(NetworkPlayer owner)
    {
        
        if(Players.PlayerOne == null)
        {
            Symbol symbol = UnityEngine.Random.Range(1, 3) == (int)Symbol.CIRCLE ? Symbol.CIRCLE : Symbol.CROSS;
            Players.PlayerOne = new Player(owner, symbol);
            Debug.Log("Player one created");
            return Players.PlayerOne;
        }
        else
        {
            Symbol symbol = Players.PlayerOne.GetSymbol() == Symbol.CIRCLE ? Symbol.CROSS : Symbol.CIRCLE;
            Players.PlayerTwo = new Player(owner, symbol);
            Debug.Log("Player two created");
            return Players.PlayerTwo;
        }
    }
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

    Player _currentPlayer;
    private void Start()
    {
        _currentPlayer = Players.PlayerOne;
    }
    //Command
    private void Update()
    {
        _currentPlayer.Play();
    }
}