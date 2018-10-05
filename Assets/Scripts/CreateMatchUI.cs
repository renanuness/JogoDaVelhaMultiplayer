using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMatchUI : MonoBehaviour
{
    public InputField MatchName;
    private MyNetworkManager _networkManager;

    private void Start()
    {
        _networkManager = MyNetworkManager.Instance;
    }

    public void OnCreateMatchClicked()
    {
        _networkManager.StartMatchMakerGame(MatchName.text);
    }

    public void OnFindMatchClicked()
    {
        MainMenuManager.Instance.ShowListMatchesPanel();
    }
}
