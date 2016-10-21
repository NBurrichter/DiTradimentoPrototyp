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

    [SerializeField]
    private GameObject trail;

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
            CmdSpawnLine(_hit.point);
        }
    }

    [Command]
    void CmdPlayerIsShot(string _playerID, float _damage)
    {
        Player _player = GameManager.GetPlayer(_playerID);
        _player.TakeDamage(_damage);
    }

    [Command]
    void CmdSpawnLine(Vector3 _hitPoint)
    {
        GameObject _trailInst = Instantiate(trail, transform.position, transform.rotation) as GameObject;
        _trailInst.GetComponent<BulletTrailSetup>().startPosition = transform.position;
        _trailInst.GetComponent<BulletTrailSetup>().endPosition = _hitPoint;
        NetworkServer.Spawn(_trailInst);
    }
}
