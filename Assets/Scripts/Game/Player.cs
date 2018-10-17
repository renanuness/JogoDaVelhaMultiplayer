using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Symbol
{
    CROSS,
    CIRCLE
}

public interface IPlayer
{
    void Play();
}

public class Player : IPlayer {

    Symbol _symbol;
    NetworkPlayer _owner;

    public Player(NetworkPlayer owner, Symbol symbol)
    {
        _owner = owner;
        _symbol = symbol;
    }

    public Symbol GetSymbol()
    {
        return _symbol;
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
            Players.PlayerOne = new Player(owner, Symbol.CIRCLE);
            return Players.PlayerOne;
        }
        else
        {
            Players.PlayerTwo = new Player(owner, Symbol.CROSS);
            return Players.PlayerTwo;
        }
    }
}