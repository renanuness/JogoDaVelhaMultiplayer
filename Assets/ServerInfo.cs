using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerInfo : MonoBehaviour {

    public Text GameName;
    public string _gameName
    {
        get { return _gameName; }
        set { _gameName = value; }
    }
    
    public void Init()
    {
        GameName.text = _gameName;
    }
}
