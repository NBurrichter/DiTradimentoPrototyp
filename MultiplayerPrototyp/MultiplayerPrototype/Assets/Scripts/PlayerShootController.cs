using UnityEngine;
using UnityEngine.Networking;

public class PlayerShootController : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private Camera cam;//shoot ray

    [SerializeField]
    private GameObject firePosition;

    [SerializeField]
    private LayerMask shootMask;

    [SerializeField]
    private GameObject trail;

    //Shoot
    [Client]
    public void FireShot(float _damage)
    {
        RaycastHit _hit;
        if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out _hit, 9999, shootMask))
        {
            Debug.Log(_hit.transform.tag);
            if (_hit.transform.root.tag == PLAYER_TAG)
            {
                Debug.Log("Hit player: " + _hit.transform.root.name);
                CmdPlayerIsShot(_hit.transform.root.name, _damage);
            }
            CmdSpawnLine(_hit.point);
        }
    }

    //Let the _playerID take damage
    [Command]
    void CmdPlayerIsShot(string _playerID, float _damage)
    {
        Player _player = GameManager.GetPlayer(_playerID);
        _player.TakeDamage(_damage);
    }

    //Create Bullet trail
    [Command]
    void CmdSpawnLine(Vector3 _hitPoint)
    {
        GameObject _trailInst = Instantiate(trail, transform.position, transform.rotation) as GameObject;
        _trailInst.GetComponent<BulletTrailSetup>().startPosition = transform.position;
        _trailInst.GetComponent<BulletTrailSetup>().endPosition = _hitPoint;
        NetworkServer.Spawn(_trailInst);
    }

}
