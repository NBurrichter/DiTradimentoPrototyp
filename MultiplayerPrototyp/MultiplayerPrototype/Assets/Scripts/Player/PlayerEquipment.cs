using UnityEngine;
using System.Collections;

public class PlayerEquipment : MonoBehaviour {
    [SerializeField]
    private WeaponBase primaryWeaponPrefab;
    [SerializeField]
    private WeaponBase secondaryWeaponPrefab;

    private WeaponBase primaryWeapon;    
    private WeaponBase secondaryWeapon;

    void Awake()
    {
        CreateWeaponInstance();
    }

    //Creates a copy from the prefabs
    void CreateWeaponInstance()
    {
        primaryWeapon = new WeaponBase();
        primaryWeapon.Setup(primaryWeaponPrefab);
        secondaryWeapon = new WeaponBase();
        secondaryWeapon.Setup(secondaryWeaponPrefab);
    }

    public WeaponBase GetWeapon(int number)
    {
        if(number == 0)
        {
            return primaryWeapon;
        }
        else
        {
            return secondaryWeapon;
        }
    }

}
