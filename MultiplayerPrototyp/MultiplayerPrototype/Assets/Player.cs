using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{

    [SyncVar]
    public int profession = 0;
    [SerializeField]
    private Text professionText;

    void Start()
    {
        if (isLocalPlayer)
        {
            professionText = GameObject.Find("ProfessionText").GetComponent<Text>();
        }
    }

    void Update()
    {if (isLocalPlayer)
        {
            professionText.text = profession.ToString();
        }
    }

}
