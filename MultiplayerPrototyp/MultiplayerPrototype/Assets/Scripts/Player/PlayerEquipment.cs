using UnityEngine;
using System.Collections;

public class PlayerEquipment : MonoBehaviour {

    [SerializeField]
    private WeaponBase primaryWeapon;
    [SerializeField]
    private WeaponBase secondaryWeapon;

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
