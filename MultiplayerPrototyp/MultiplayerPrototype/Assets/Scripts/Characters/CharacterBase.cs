using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterBase : NetworkBehaviour {

    [SerializeField]
    protected HealthSystem health;

    virtual public void Start()
    {
        health = GetComponent<HealthSystem>();
    }

    public void TakeDamage(float _damage)
    {
        health.TakeDamage(_damage);
    }
}
