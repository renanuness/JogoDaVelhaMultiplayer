using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMatchUI : MonoBehaviour
{
    public InputField MatchName;

    public void OnCreateMatchClicked()
    {
        MyNetworkManager.Instance.StartMatchMakerGame(MatchName.text);
    }
}
