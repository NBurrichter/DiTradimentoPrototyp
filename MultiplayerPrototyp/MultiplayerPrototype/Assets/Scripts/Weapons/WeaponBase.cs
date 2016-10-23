using UnityEngine;

public class WeaponBase : ScriptableObject
{

    public string gunName;
    public float damage;
    public float reloadSpeed;
    public float fireRate;
    public bool automatic;
    public int magazineSize;
    public int currentMagazine;
    public int availableMagazines;
    public GameObject model;

    public void Setup(WeaponBase pref)
    {
        gunName = pref.gunName;
        damage = pref.damage;
        reloadSpeed = pref.reloadSpeed;
        fireRate = pref.fireRate;
        automatic = pref.automatic;
        magazineSize = pref.magazineSize;
        currentMagazine = pref.currentMagazine;
        availableMagazines = pref.availableMagazines;
        model = pref.model;
    }
}
