using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySetup : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Enemy _enemy = GetComponent<Enemy>();
        GameManager.RegisterEnemy(_netID, _enemy);
    }

    void OnDisable()
    {
        GameManager.UnRegisterEnemy(transform.name);
    }
}
