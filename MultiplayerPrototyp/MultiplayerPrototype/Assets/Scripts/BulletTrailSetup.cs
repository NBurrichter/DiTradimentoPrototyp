using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletTrailSetup : NetworkBehaviour
{
    [SyncVar]
    public Vector3 startPosition;
    [SyncVar]
    public Vector3 endPosition;
	
    void Start()
    {
        GetComponent<LineRenderer>().SetPosition(0, startPosition);
        GetComponent<LineRenderer>().SetPosition(1,endPosition);

        Destroy(gameObject, 1);
    }
}
