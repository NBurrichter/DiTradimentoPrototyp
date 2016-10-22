using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    //Movement
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;
    private PlayerShootController shootController;


    [Header("Jumping")]
    [SerializeField]
    private float jumpForce;

    //Shooting
    private bool shootInput = false;
    private bool canShoot = true;
    private float shootCooldown;
    private WeaponBase equipedWeapon;

    private PlayerEquipment equipment;
    private int selectedWeapon = 0;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        shootController = GetComponent<PlayerShootController>();

        equipment = GetComponent<PlayerEquipment>();
        equipedWeapon = equipment.GetWeapon(selectedWeapon);

    }

    void Update()
    {
        UpdateMovementInput();
        UpdateShootInput();
        UpdateWeaponSwitchInput();
        UpdateReloadInput();
    }

    void UpdateMovementInput()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVertical = transform.forward * _zMov;

        //Final movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3D vector (turning the player)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        //Apply rotation
        motor.RotateCamera(_cameraRotationX);

        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            motor.Jump(jumpForce);
        }

        if (Input.GetButton("Jump"))
        {
            motor.HoldJump(true);
        }
        else
        {
            motor.HoldJump(false);
        }

        //Sprinting
        if (Input.GetButton("Sprint") && Input.GetAxisRaw("Vertical") > 0)
        {
            motor.ApplySprinting(true);
            //Debug.Log("sprint");
        }

        if (Input.GetButtonUp("Sprint") || Input.GetAxisRaw("Vertical") <= 0)
        {
            motor.ApplySprinting(false);
        }
    }

    void UpdateShootInput()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            shootInput = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            shootInput = false;
            canShoot = true;
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        if (equipedWeapon.automatic)
        {
            if (shootInput && shootCooldown <= 0)
            {
                DoShot();               
            }
        }
        else
        {
            if(shootInput && canShoot && shootCooldown <= 0)
            {
                DoShot();
                canShoot = false;
            }
        }
    }

    private void UpdateWeaponSwitchInput()
    {
        if(Input.GetButtonDown("Switch Weapon"))
        {
            selectedWeapon = 1 - selectedWeapon;
            equipedWeapon = equipment.GetWeapon(selectedWeapon);
            canShoot = true;
            shootInput = false;
            shootCooldown = 0.5f;
        }
    }

    void UpdateReloadInput()
    {
        if(Input.GetButtonDown("Reload"))
        {
            ReloadWeapon();
        }
    }

    void DoShot()
    {
        if (equipedWeapon.currentMagazine > 0)
        {
            shootController.FireShot(equipedWeapon.damage);
            shootCooldown = equipedWeapon.fireRate;
            equipedWeapon.currentMagazine--;
        }
    }

    void ReloadWeapon()
    {
        if(equipedWeapon.currentMagazine != equipedWeapon.magazineSize)
        {
            if (equipedWeapon.availableMagazines > 0)
            {
                //equipedWeapon.ammunition -= equipedWeapon.magazineSize;
                equipedWeapon.availableMagazines--;
                equipedWeapon.currentMagazine = equipedWeapon.magazineSize;
            }
        }
    }
}
