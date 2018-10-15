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
        _networkPlayer.syncVarsChanged += OnNetworkPlayerSyncvarChanged;
    }

    public void Init(NetworkPlayer networkPlayer)
    {
        if(_lobbyMenu == null)
        {
            _lobbyMenu = LobbyMenu.Instance;
        }
        _networkPlayer = networkPlayer;
        PlayerNameTxt.text = playerName;

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
        if (_networkPlayer.IsReady)
        {
            PlayerNameTxt.enabled = true;
            _networkPlayer.Ready(false);
            ReadyButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            PlayerNameTxt.enabled = false;
            _networkPlayer.Ready(true);
            _networkPlayer.Name = PlayerNameTxt.text;
            ReadyButton.GetComponent<Image>().color = Color.green;
        }
    }

    private void OnNetworkPlayerSyncvarChanged(NetworkPlayer player)
    {
        // Update everything
        UpdateValues();
    }

    private void UpdateValues()
    {
        PlayerNameTxt.text = _networkPlayer.name;

    }
}
