using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region PUBLIC FIELDS
    [SerializeField] public int PlayerIndex = 0;
    [SerializeField] public float Statmina = 100; // Accelerator
    [SerializeField] public float Hunger = 100;
    [SerializeField] public float Sam = 100;
    [SerializeField] public float Mass = 1;
    [SerializeField] public float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private const float ROTATION_TRESHOLD = .02f; // Used to prevent NaN result causing rotation in a non direction
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] public Transform GameplayCamera;
    #endregion

    [SerializeField] public float SpeedControlFactor = 6f;

    private Vector2 _rawInput;
    private Vector2 _movementInput;
    private Vector3 _finalMoveVector; //final movement Vector


    [HideInInspector] public bool IsAcceleratedPressed;
    [HideInInspector] public bool IsAttackedPressed;
    [HideInInspector] public bool IsExtraActionPressed;
    [HideInInspector] public bool IsInteractionPressed;

    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidBody;
    private Vector3 lastDirection;




    // Start is called before the first frame update
    void Awake()
    {
        _capsuleCollider  = GetComponent<CapsuleCollider>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveMent();
    }

    private void UpdateMoveMent()
    {
        float speed = CalculateFinalSpeed();
        Vector3 direction = Rotating();
        _rigidBody.AddForce(direction * speed * Time.deltaTime * 100, ForceMode.Acceleration);
    }

    private Vector3 Rotating()
    {
        //Get the two axes from the camera and flatten them on the XZ plane
        Vector3 cameraForward = GameplayCamera.TransformDirection(Vector3.forward);
        // Camera forward Y component is relevant when flying.
        cameraForward = cameraForward.normalized;

        Vector3 cameraRight = new Vector3(cameraForward.z, 0, -cameraForward.x);
        //Use the two axes, modulated by the corresponding inputs, and construct the final vector
        // Calculate target direction based on camera forward and direction key.
        Vector3 targetDirection = cameraRight.normalized * _rawInput.x +
            cameraForward.normalized * _rawInput.y;

        // Rotate the player to the correct fly position.
        if (targetDirection != Vector3.zero)
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
        }
        // Return the current fly direction.
        return targetDirection;
    }
    public void  SetRawInput(Vector2 rawInput)
    {
        _rawInput = rawInput;
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
            var finalSpeed = (Hunger * 0.01f) + (SpeedControlFactor * 0.1f);
            return finalSpeed > maxSpeed ? maxSpeed : finalSpeed;
        }
    }

}
