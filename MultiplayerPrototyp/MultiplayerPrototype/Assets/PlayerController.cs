﻿using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    [Header("Jumping")]
    [SerializeField]
    private float jumpForce;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
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
        if(Input.GetButtonDown("Jump"))
        {
            motor.Jump(jumpForce);
        }

        if(Input.GetButton("Jump"))
        {
            motor.HoldJump(true);
        }
        else
        {
            motor.HoldJump(false);
        }

        //Sprinting
        if(Input.GetButton("Sprint") && Input.GetAxisRaw("Vertical") > 0)
        {
            motor.ApplySprinting(true);
            //Debug.Log("sprint");
        }

        if (Input.GetButtonUp("Sprint") || Input.GetAxisRaw("Vertical") <= 0)
        {
            motor.ApplySprinting(false);
        }

    }

}
