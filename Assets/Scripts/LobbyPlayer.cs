using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class LobbyPlayer : MonoBehaviour
{
    public InputField PlayerNameTxt;
    public Button ReadyButton;
    public Image SymbolImage;
    public Sprite CrossSprite;
    public Sprite CircleSprite;

    private string playerName;
    private LobbyMenu _lobbyMenu;
    private NetworkPlayer _networkPlayer;

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
    private void Start()
    {

        _lobbyMenu = LobbyMenu.Instance;
        ReadyButton.GetComponent<Image>().color = Color.red;

    }

    public void Init(NetworkPlayer networkPlayer)
    {
        if(_lobbyMenu == null)
        {
            _lobbyMenu = LobbyMenu.Instance;
        }
        _networkPlayer = networkPlayer;
        PlayerNameTxt.text = playerName;
        _networkPlayer.syncVarsChanged += OnNetworkPlayerSyncvarChanged;
        UpdateButton(networkPlayer);

        if (_networkPlayer.GetPlayerSymbol() == Symbol.CIRCLE)
        {
            Debug.Log("Circle");
            SymbolImage.GetComponent<Image>().sprite = CircleSprite;
        }
        else
        {
            Debug.Log("Cross");
            SymbolImage.GetComponent<Image>().sprite = CrossSprite;

        }

        if (_networkPlayer.hasAuthority)
        {
            SetUpLocalPlayer();
        }
        else
        {
            SetUpOtherPlayers();
        }
	}

    private void SetUpLocalPlayer()
    {
        ReadyButton.interactable = true;
        PlayerNameTxt.interactable = true;
    }

    private void SetUpOtherPlayers()
    {
        ReadyButton.interactable = false;
        PlayerNameTxt.interactable = false;
    }

    public void ButtonReady()
    {
        if (_networkPlayer == null)
            return;

        if (_networkPlayer.IsReady)
        {
            _networkPlayer.CmdSetReady(false);
        }
        else
        {
            _networkPlayer.CmdSetReady(true);
        }
    }

    private void OnNetworkPlayerSyncvarChanged(NetworkPlayer player)
    {
        // Update everything
        PlayerNameTxt.text = _networkPlayer.name;
    }

    public void UpdateButton(NetworkPlayer player)
    {
        if (player.IsReady)
        {
            PlayerNameTxt.enabled = false;
            player.Name = PlayerNameTxt.text;
            ReadyButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            PlayerNameTxt.enabled = true;
            ReadyButton.GetComponent<Image>().color = Color.red;
        }
    }
}
