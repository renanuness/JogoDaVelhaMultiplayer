using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    //private BoardUI _boardUI;

    public int[] board = new int[9];

    public Animation endGameAnimation;
    //public UIManager uiManager;

    private Animator animator;
    private GameObject[] squares = new GameObject[9];
    private static BoardController instance;
    private Symbol _currentPlayer;
    private static BoardController _instance;

    public static BoardController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BoardController>();
            }
            return _instance;
        }
    }


    private void Awake()
    {

        _instance = this;
    }

    public bool MakeMove(int position, Symbol player)
    {

        if (board[position] == 0)
        {
            board[position] = (int)player;
            squares[position].GetComponent<SpriteRenderer>().sprite = (player == Symbol.CIRCLE ? _circleSprite : _crossSprite);
            Debug.Log("Client play");
            //
            return true;
        }
        return false;
    }

    public int SquareToPosition(GameObject square)
    {
        int position = 0;
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i] == square)
            {
                position = i;
                break;
            }
        }
        return position;
    }

    public bool CheckWin(int[] board, out Symbol symbol)
    {
        symbol = 0;
        if (
           ((board[0] == board[1] && board[1] == board[2]) ||
           (board[0] == board[3] && board[3] == board[6]) ||
           (board[0] == board[4] && board[4] == board[8])) &&
           (board[0] != 0)
           )
        {
            symbol = (Symbol)board[0];
            return true;
        }

        if ((board[3] == board[4] && board[4] == board[5]) &&
           (board[3] != 0))
        {
            symbol = (Symbol)board[3];
            return true;
        }

        if (((board[6] == board[7] && board[7] == board[8]) ||
           (board[2] == board[4] && board[4] == board[6])) &&
           (board[6] != 0)
           )
        {
            symbol = (Symbol)board[6];
            return true;
        }

        if ((board[1] == board[4] && board[4] == board[7]) &&
           (board[1] != 0)
           )
        {
            symbol = (Symbol)board[1];
            return true;
        }

        if ((board[2] == board[5] && board[5] == board[8]) &&
           (board[2] != 0))
        {
            symbol = (Symbol)board[2];
            return true;
        }

        return false;
    }

    public int GetEmptys()
    {
        int emptys = 0;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                emptys++;
            }
        }

        return emptys;
    }

    public int GetEmptys(int[] _board)
    {
        int emptys = 0;
        for (int i = 0; i < _board.Length; i++)
        {
            if (_board[i] == 0)
            {
                emptys++;
            }
        }

        return emptys;
    }

    public void EndGame(Player winner)
    {
        animator.SetBool("IsGameOver", true);
        //uiManager.EnableFinalPanel(winner.Name);


    }

    public void EndGame()
    {
        animator.SetBool("IsGameOver", true);
        //uiManager.EnableFinalPanel();
    }
}
