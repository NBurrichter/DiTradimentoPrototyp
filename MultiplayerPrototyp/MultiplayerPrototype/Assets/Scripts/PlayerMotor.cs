using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the players movement
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    //---References---
    private Rigidbody rb;
    private Animator anim;

    //--Editor setup--
    [Header("References")]
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Camera fovCamera;

    //--Field of View variables--
    /// <summary>
    /// FoV at a timespeed of 0
    /// </summary>
    [Header("Fov")]
    [SerializeField]
    private float baseFov = 60;
    [SerializeField]
    private float sprintExtraFov = 20;
    /// <summary>
    /// FoV multiplied by timespeed and added on top
    /// </summary>
    [SerializeField]
    private float timeExtraFov = 20;
    [SerializeField]
    private float maxFov = 120;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float fovLerp = 0.1f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currenRotationX = 0f;
    private Vector3 verticalMovement = Vector3.up;

    [Header("Jumping")]
    [SerializeField]
    private float gravity = 1;
    private float jumpForce = 0;

    //Grounding
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isHoldingJump = false; //meant for variable jump height the longer jump is held down, not jet implemented
    [SerializeField]
    private float groundRayLength = 0.5f;
    [SerializeField]
    private float rayThickness = 0.5f;
    [SerializeField]
    private LayerMask groundRayLayer;
    [SerializeField]
    private LayerMask headRayLayer;
    [SerializeField]
    private float slopeLimit;

    [Header("Camera Rotation Clamp")]
    [SerializeField]
    private float cameraRotationLimit = 85f;

    [Header("Sprinting")]
    [SerializeField]
    private float sprintSpeed = 1.5f;
    private bool isSprinting;

    [Header("Crouching")]
    [SerializeField]
    private CapsuleCollider playerCollider;
    [SerializeField]
    private float crouchHeight;
    private float defaultHeight;
    private bool isCrouhcing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        defaultHeight = playerCollider.height;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        PerformCrouching();
        PerformFOV();
        PerformGrounding();
    }

    void PerformGrounding()
    {
        //---Check if is grounded---
        RaycastHit _hitInfo;
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * groundRayLength,Color.red,1);
        float _editedGrounndRayLength = groundRayLength;
        float _editedGrounndRayThickness = rayThickness;
        if (isCrouhcing)
        {
            _editedGrounndRayLength = groundRayLength / 2 +0.1f;
            _editedGrounndRayThickness = rayThickness / 2 + 0.1f;
        }

        if (Physics.SphereCast(transform.position, _editedGrounndRayThickness, Vector3.down, out _hitInfo, _editedGrounndRayLength, groundRayLayer))
        {
            if (Vector3.Angle(Vector3.up, _hitInfo.normal) < slopeLimit)
            {
                if (!isJumping)
                {
                    jumpForce = -100;
                }

                isGrounded = true;
                //jumpForce = 0;

                Debug.DrawLine(transform.position, _hitInfo.point, Color.green);
            }
            else
            {
                Debug.DrawLine(transform.position, _hitInfo.point, Color.yellow);
            }         
        }
        else
        {
            isGrounded = false;
        }

        if (jumpForce <= 0)
        {
            isJumping = false;
        }

        //---Check if head hits a ceiling---
        if (Physics.SphereCast(transform.position, rayThickness, Vector3.up, out _hitInfo, groundRayLength, headRayLayer))
        {
            if (jumpForce > 0)
                jumpForce = 0;
        }
    }

    //Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets a rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets a rotation vector for the camera
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void ApplySprinting(bool _isSprinting)
    {
        isSprinting = _isSprinting;
    }

    public void Jump(float _force)
    {
        if (isGrounded)
        {
            jumpForce = _force;
            isJumping = true;
        }
    }

    public void HoldJump(bool _state)
    {
        isHoldingJump = _state;
    }

    public void ApplyCrouch(bool _state)
    {
        isCrouhcing = _state;
    }

    //Perform movemnt based on velocity variable
    void PerformMovement()
    {
        //Normal movement calculation
        if (velocity != Vector3.zero)
        {
            //anim.SetBool("Walking", true);
            //Normal Speed

            //Calculate speed modifier
            float _calculatedSpeed;
            if (isSprinting)
            {
                _calculatedSpeed = sprintSpeed;
            }
            else
            {
                _calculatedSpeed = 1;
            }
            rb.velocity = (velocity * Time.fixedDeltaTime * _calculatedSpeed);

        }
        else
        {
            //Standing still
            rb.velocity = Vector3.zero;
            //anim.SetBool("Walking", false);
        }

        //Jumpinng, works similar to walking by calculating the speed modifier
        if (!isGrounded || isJumping)
        {
            rb.velocity = rb.velocity + verticalMovement * Time.fixedDeltaTime * jumpForce;
            jumpForce -= gravity;
        }

    }

    //Change Player FOV dependent on speed, faster movement is higher FOV
    void PerformFOV()
    {
        //The fov value that should be lerped to
        float targetFov = 0;
        targetFov = baseFov + timeExtraFov;
        if (isSprinting)
        {
            targetFov += sprintExtraFov;
        }
        targetFov = Mathf.Clamp(targetFov, 0, maxFov);
        fovCamera.fieldOfView = Mathf.Lerp(fovCamera.fieldOfView, targetFov, fovLerp);

    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //Set rotation and clamp it
            currenRotationX -= cameraRotationX;
            currenRotationX = Mathf.Clamp(currenRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply rotation to camera
            cam.transform.localEulerAngles = new Vector3(currenRotationX, 0f, 0f);
        }
    }

    void PerformCrouching()
    {
        if (isCrouhcing)
        {
            playerCollider.height = crouchHeight;
        }
        else
        {
            playerCollider.height = defaultHeight;
        }
    }
}
