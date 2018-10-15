using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using System.Linq;

public class ListMatchesUI : MonoBehaviour
{
    //prefab da partida
    //listar os jogos 
    //talvez seja melhor o botão chamar isso aqui e isso aqui chamar o networkmanager.
    public GameObject ServerObject;
    public Transform Content;

    private MyNetworkManager _networkManager;
    private int _startPage;
    private int _pageSize = 5;
    private float _listAutoRefreshTime = 10f;
    private float _nextRefreshTime = 0;
    private int _currentPage = 1;
    private void Start ()
    {
        _networkManager = MyNetworkManager.Instance;
        _networkManager.StartMatchMaker();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (_nextRefreshTime <= Time.time)
        {
            RequestPage(_currentPage);

            _nextRefreshTime = Time.time + _listAutoRefreshTime;
        }
    }

    //OnCLick do botão de findServer, chama o networkManager e preenche aqui. para de colocar as pata em tudo.
    public void RequestPage(int currentPage)
    {
        _networkManager.matchMaker.ListMatches(0, _pageSize, string.Empty, false, 0, 0, OnMatchListed);

    }

    public void OnMatchListed(bool success, string extraInfo, List<MatchInfoSnapshot> response)
    {
        
        if (success)
        {
            Debug.Log(response.Count);
            Debug.Log(extraInfo);
            if(response.Count > 0)
            {
                foreach(var game in response)
                {
                    var go = Instantiate(ServerObject, Content);
                    ServerInfo si = go.GetComponent<ServerInfo>();
                    si.SetMatchInfo(game);
                    si.Init();
                }
            }
        }
        else
        {
            Debug.Log(extraInfo);
        }
    }
}
