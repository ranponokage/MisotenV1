using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    #region PUBLIC FIELDS
    [SerializeField] public int PlayerIndex = 0;
    [SerializeField] public float Statmina = 100; // Accelerator
    [SerializeField] public float Hunger = 100;
    [SerializeField] public float Sam = 100;
    [SerializeField] public float Mass = 1;
    [SerializeField] public float TurnSmoothing = 0.06f;
    #endregion

    [SerializeField] public float SpeedFactor = 6f;

    private Vector2 _rawInput;
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizantal = _rawInput.x;
        float vertical = _rawInput.y;
        float FinalSpeed = CalculateFinalSpeed();

        Vector3 direction = new Vector3(horizantal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            _characterController.Move(direction * FinalSpeed * Time.deltaTime);
        }
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
