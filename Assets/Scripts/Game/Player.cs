using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    GameController _gameController;
    Symbol _symbol;

    NetworkPlayer _owner;

    private void Start()
    {
        _gameController = GameController.Instance;
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

    public void Init(Symbol s)
    {
        _symbol = s;
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
                        _gameController.SwitchCurrentPlayer();
                    }
                }
            }
        }
    }
}

