using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayer : MonoBehaviour {


    public Text PlayerNameTxt;
    private string playerName;

    public string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            playerName = value;
        }
    }

	public void Init()
    {
        PlayerNameTxt.text = playerName;	
	}
	
	private void Update ()
    {
		
	}
}
