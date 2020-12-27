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

    [SerializeField] public Transform GameplayCamera;
    #endregion

    [SerializeField] public float SpeedFactor = 6f;

    private Vector2 _rawInput;
    private Vector2 _movementInput;
    private Vector3 _movementVector; //final movement Vector
    private CharacterController _characterController;

    [HideInInspector] public bool IsAcceleratedPressed;
    [HideInInspector] public bool IsAttackedPressed;
    [HideInInspector] public bool IsExtraActionPressed;
    [HideInInspector] public bool IsInteractionPressed;


    // Start is called before the first frame update
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = CalculateFinalSpeed();

        RecalculateMovement();

        _movementVector = _movementInput * speed;

        CalculateRotation();

        _characterController.Move(_movementVector * Time.deltaTime);
    }

    private void CalculateRotation()
    {
        if (_movementVector.sqrMagnitude >= ROTATION_TRESHOLD)
        {
            float targetRotation = Mathf.Atan2(_movementVector.x, _movementVector.z) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetRotation,
                ref _turnSmoothVelocity,
                TurnSmoothTime);
        }
    }

    private void RecalculateMovement()
    {
        //Get the two axes from the camera and flatten them on the XZ plane
        Vector3 cameraForward = GameplayCamera.forward;
        Vector3 cameraRight = GameplayCamera.right;
        //Use the two axes, modulated by the corresponding inputs, and construct the final vector
        Vector3 adjustedMovement = cameraRight.normalized * _rawInput.x +
            cameraForward.normalized * _rawInput.y;

        _movementInput = Vector3.ClampMagnitude(adjustedMovement, 1f);
    }

    public void  SetRawInput(Vector2 rawInput)
    {
        _rawInput = rawInput;
        Debug.Log(_rawInput);
    }

    public int GetPlayerIndex()
    {
        return PlayerIndex;
    }
    private float CalculateFinalSpeed()
    {
        if (Hunger <= 0)
        {
            return 0;
        }
        else
        {
            return (Hunger * 0.01f) + (SpeedFactor * 0.1f);
        }
    }

}
