using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : CharacterBase
{

    [SyncVar]
    public int profession = 0;
    [SerializeField]
    private Text professionText;

    [Header("Crouching")]
    [SerializeField]
    private CapsuleCollider playerCollider;
    [SerializeField]
    private float crouchHeight;
    private float defaultHeight;
    [SyncVar]
    private bool isCrouching;


    override public void Start()
    {
        base.Start();
        if (isLocalPlayer)
        {
            professionText = GameObject.Find("ProfessionText").GetComponent<Text>();
        }
        else
        {
            defaultHeight = playerCollider.height;
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            professionText.text = profession.ToString();
        }
        else
        {
            //Update Crouching on for remotes
            if (isCrouching)
            {
                playerCollider.height = crouchHeight;
            }
            else
            {
                playerCollider.height = defaultHeight;
            }
        }
    }

    [Command]
    public void CmdSetCrouch(bool _state)
    {
        isCrouching = _state;
    }

}
