using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

    [SerializeField]
    private GameObject cam;
    public GameObject pickupPosition;
    private bool isCarrying = false;
    private GameObject holdingObject;
    public int ID = 0;

    void Awake()
    {
        int SetID = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if(player.GetComponent<PlayerInput>().ID > SetID)
            {
                SetID = player.GetComponent<PlayerInput>().ID;
            }
        }
        ID = SetID + 1;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isCarrying)
            {
                CmdDropObject();
                isCarrying = false;
            }
            else
            {
                Debug.Log("Pickup");
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2))
                {
                    if (hit.transform.tag == "Collectable")
                    {
                        isCarrying = true;
                        holdingObject = hit.transform.gameObject;
                        CmdPickupObject(holdingObject.GetComponent<Collectible>().ID);
                    }

                }
            }
        }
	}

    [Command]
    void CmdPickupObject(int collectID)
    {
        Debug.Log("Pickup");
        
        holdingObject.GetComponent<Collectible>().Pickup(ID);
    }

    [Command]
    void CmdDropObject()
    {
        holdingObject.GetComponent<Collectible>().Drop();
    }
}
