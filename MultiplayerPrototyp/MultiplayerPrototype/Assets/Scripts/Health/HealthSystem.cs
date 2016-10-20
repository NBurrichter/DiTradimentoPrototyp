using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthSystem : NetworkBehaviour
{
    [SerializeField]
    protected float maxHealth;
    [SyncVar]
    protected float currentHealth;

    void Awake()
    {
        if (isServer)
        {
            SetDefaults();
        }
    }

    public void TakeDamage(float _ammount)
    {
        currentHealth -= _ammount;
    }
    
    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }
}
