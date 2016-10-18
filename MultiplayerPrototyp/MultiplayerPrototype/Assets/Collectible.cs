using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Collectible : NetworkBehaviour
{

    [SyncVar]
    [SerializeField]
    private bool isPickedUp;
    [SerializeField]
    private GameObject pickedUpBy;
    public int ID = 0;

    void Start()
    {
        int SetID = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerInput>().ID > SetID)
            {
                SetID = player.GetComponent<PlayerInput>().ID;
            }
        }
        ID = SetID + 1;
    }

    void FixedUpdate()
    {
        if (!isServer)
        {
            enabled = false;
            if (isPickedUp)
            {
                transform.position = pickedUpBy.transform.position;
                transform.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized;
            }
        }
    }

    [Command]
    void CmdPickup(int by)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerInput>().ID == by)
            {
                Debug.Log(player.GetComponent<PlayerInput>().ID);
                pickedUpBy = player.GetComponent<PlayerInput>().pickupPosition;
            }
        }

        isPickedUp = true;
    }

    [Command]
    void CmdDrop()
    {
        isPickedUp = false;
    }

    public void Pickup(int by)
    {
        CmdPickup(by);
    }

    public void Drop()
    {
        CmdDrop();
    }
}
