using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] public int PlayerIndex = 0;
    [SerializeField] public float Statmina = 100; // Accelerator
    [SerializeField] public float Hunger = 100;
    [SerializeField] public float Sam = 100;
    [SerializeField] public float Mass = 1;
    [SerializeField] public float TurnSmoothTime = 0.1f;

    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] public float AccelerateFactor = 0;
    [SerializeField] private float accelerateFOV = 100f;                       // the FOV to use on the camera when player is Accelerate.

    private Vector2 _rawInput;


    private Rigidbody _rigidBody;
    private Animator _animator;
    private Vector3 lastDirection;
    private float _defaultFOV;
    private float _targetFOV;
    private float _itemSpeedFactor = 0f;

    [HideInInspector] public Camera GameplayCamera;
    [HideInInspector] public CinemachineFreeLook FreeLookVCam;
    [HideInInspector] public bool IsUsingSkillPressed;
    [HideInInspector] public bool IsExtraActionPressed;
    [HideInInspector] public bool IsInteractionPressed;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _defaultFOV = FreeLookVCam.m_Lens.FieldOfView;
        ResetFOV();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        FreeLookVCam.m_Lens.FieldOfView = Mathf.Lerp(FreeLookVCam.m_Lens.FieldOfView, _targetFOV, Time.deltaTime);
        UpdateMoveMent();
    }

    private void UpdateMoveMent()
    {
        float speed = CalculateFinalSpeed();
        Vector3 direction = Rotating();
        //_rigidBody.AddForce(direction * speed  * 100,ForceMode.Acceleration);
        _rigidBody.MovePosition(transform.position + direction * speed * Time.deltaTime * 10);
    }

    private Vector3 Rotating()
    {
        //Get the two axes from the camera and flatten them on the XZ plane
        Vector3 cameraForward = GameplayCamera.transform.TransformDirection(Vector3.forward);
        // Camera forward Y component is relevant when flying.
        cameraForward = cameraForward.normalized;

        Vector3 cameraRight = new Vector3(cameraForward.z, 0, -cameraForward.x);
        //Use the two axes, modulated by the corresponding inputs, and construct the final vector
        // Calculate target direction based on camera forward and direction key.
        Vector3 targetDirection = cameraRight.normalized * _rawInput.x +
            cameraForward.normalized * _rawInput.y;

        // Rotate the player to the correct fly position.
        if (IsMoving() && targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(_rigidBody.rotation, targetRotation, TurnSmoothTime);

            _rigidBody.MoveRotation(newRotation);
            lastDirection = targetDirection;
        }

        // Player is flying and idle?
        if (!(Mathf.Abs(_rawInput.x) > 0.2 || Mathf.Abs(_rawInput.y) > 0.2))
        {
            if (lastDirection != Vector3.zero)
            {
                lastDirection.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
                Quaternion newRotation = Quaternion.Slerp(_rigidBody.rotation, targetRotation, TurnSmoothTime);
                _rigidBody.MoveRotation(newRotation);
            
            }
            //idle
            _animator.SetBool("IsMoving", false);
        }
        else
        {
            //Swim
            _animator.SetBool("IsMoving", true);
        }
        // Return the current fly direction.
        return targetDirection;
    }
    public void  SetRawInput(Vector2 rawInput)
    {
        _rawInput = rawInput;
    }

    public void IsUsingSkill()
    {
        _animator.SetBool("IsUsingSkill", IsUsingSkillPressed);

        //AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        //if (info.normalizedTime >= 1.0f)
        //{
        //    _animator.SetBool("IsUsingSkill", false);
        //}
    }
    public void Accelerate()
    {
        Debug.Log("Accelerate");
        SetFOV();
        AccelerateFactor = 10;
        _animator.SetBool("IsAccelerate", true);
    }
    public void DeAccelerate()
    {
        Debug.Log("DeAccelerate");
        ResetFOV();
        AccelerateFactor = 0;
        _animator.SetBool("IsAccelerate", false);
    }
    private void ResetFOV()
    {
        _targetFOV = _defaultFOV;
    }
    private void SetFOV()
    {
        _targetFOV = accelerateFOV;
    }
    private bool IsMoving()
    {
        return (_rawInput.x != 0) || (_rawInput.y != 0);
    }
    public int GetPlayerIndex()
    {
        return PlayerIndex;
    }
    private float CalculateFinalSpeed()
    {
        if (Hunger <= 0)
        {
            return minSpeed;
        }
        else
        {
            var finalSpeed = (Hunger * 0.01f) + (AccelerateFactor * 0.1f) + (_itemSpeedFactor * 0.1f) + defaultSpeed;
            return finalSpeed > maxSpeed ? maxSpeed : finalSpeed;
        }
    }
}
