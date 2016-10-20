using UnityEngine;
using UnityEngine.Networking;

public class PlayerShootController : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public float damage;

    [SerializeField]
    private Camera cam;//shoot ray

    [SerializeField]
    private GameObject firePosition;

    [SerializeField]
    private LayerMask shootMask;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireShot();
        }
    }

    //Shoot
    [Client]
    void FireShot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out _hit, 9999, shootMask))
        {
            Debug.Log(_hit.transform.tag);
            if (_hit.transform.root.tag == PLAYER_TAG)
            {
                Debug.Log("Hit player: " + _hit.transform.root.name);
                CmdPlayerIsShot(_hit.transform.root.name, damage);
            }
            //CmdSpawnLine(_hit.point);
        }
    }

    [Command]
    void CmdPlayerIsShot(string _playerID, float _damage)
    {
        Player _player = GameManager.GetPlayer(_playerID);
        _player.TakeDamage(_damage);
    }
}
