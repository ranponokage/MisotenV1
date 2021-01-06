using Cinemachine;
using System;
using UnityEngine;
using Wyt.CharacterStats;
using Rewired;
using System.Collections;
using LuxWater;

public class PlayerControl : MonoBehaviour
{
    private Player _player;

    [SerializeField] public int PlayerIndex = 0;

    [SerializeField] public GameObject CharacterModel;


    [SerializeField] public CharacterStat Hunger;
    [SerializeField] public CharacterStat Statmina; // Accelerator
    [SerializeField] public CharacterStat Sam;
    [SerializeField] public CharacterStat Mass;
    [SerializeField] public CharacterStat Size;
    [SerializeField] public CharacterStat TurnSmoothTime;
    [SerializeField] public CharacterStat Speed;
    [SerializeField] public Rigidbody _rigidBody;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] public  float AccelerateFactor = 1;

    private StatModifier _accelerateModifile;
    [SerializeField] private float accelerateFOV = 100f;                       // the FOV to use on the camera when player is Accelerate.

    [HideInInspector] public StatusPanel _statusPanel;

    private Vector2 _rawInput;

    private Animator _animator;
    private Vector3 lastDirection;
    private float _defaultFOV;
    private float _targetFOV;

    [HideInInspector] public Camera GameplayCamera;
    [HideInInspector] public CinemachineFreeLook FreeLookVCam;
    private bool IsUsingSkillPressed;
    private bool IsExtraActionPressed;
    private bool IsInteractionPressed;

    [SerializeField] private bool _invertAxis;

    private int _InvertFactor = 1;

    [SerializeField] private UsableItemSO _usableItemSO;

    // Start is called before the first frame update
    private void Start()
    {
        _statusPanel.SetStats(Hunger, Statmina, Sam, Mass, Size, Speed);
        _statusPanel.UpdateStatVlues();

        _player = ReInput.players.GetPlayer(PlayerIndex);
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _defaultFOV = FreeLookVCam.m_Lens.FieldOfView;
        ResetFOV();

        //_rigidBody.mass = Mass.Value;
        //CharacterModel.transform.localScale *= Size.Value;

        _accelerateModifile =  new StatModifier((AccelerateFactor), StatModType.Flat, this);
    }

    void Update()
    {
        SetRawInput();
        HandleButtonInput();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        FreeLookVCam.m_Lens.FieldOfView = Mathf.Lerp(FreeLookVCam.m_Lens.FieldOfView, _targetFOV, Time.deltaTime);
        UpdateMoveMent();
    }

    private void UpdateMoveMent()
    {
        Vector3 direction = Rotating();
        //_rigidBody.AddForce(direction * speed  * 100,ForceMode.Acceleration);
        _rigidBody.MovePosition(transform.position + direction * Speed.Value * Time.deltaTime);
    }

    private void HandleButtonInput()
    {
        if (_player.GetButtonDown("Accelerate"))
        {
            Accelerate();
            _statusPanel.UpdateStatVlues();
        }
        else if (_player.GetButtonUp("Accelerate"))
        {
            DeAccelerate();
            _statusPanel.UpdateStatVlues();
        }

        if (_usableItemSO != null && _player.GetButton("Item"))
        {
            UseItem();
            _statusPanel.UpdateStatVlues();
        }
  
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

            Quaternion newRotation = Quaternion.Slerp(_rigidBody.rotation, targetRotation, TurnSmoothTime.Value);

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
                Quaternion newRotation = Quaternion.Slerp(_rigidBody.rotation, targetRotation, TurnSmoothTime.Value);
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
    private void  SetRawInput()
    {
        _rawInput.x = _player.GetAxis("Move X") * _InvertFactor;
        _rawInput.y = _player.GetAxis("Move Y") * _InvertFactor;

    }

    public void IsUsingSkill()
    {
        _animator.SetBool("IsUsingSkill", IsUsingSkillPressed);//eat animation

        //AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        //if (info.normalizedTime >= 1.0f)
        //{
        //    _animator.SetBool("IsUsingSkill", false);
        //}
    }
    private void Accelerate()
    {
        Debug.Log("Accelerate");
        SetFOV();
        Speed.AddModifier(_accelerateModifile);
        _animator.SetBool("IsAccelerate", true);
    }
    private void DeAccelerate()
    {
        Debug.Log("DeAccelerate");
        ResetFOV();

        Speed.RemoveModifier(_accelerateModifile);
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
        if (Hunger.Value <= 0)
        {
            return minSpeed;
        }
        else
        {
            Speed.AddModifier(new StatModifier((Hunger.Value * 0.01f), StatModType.Flat, this));
            Speed.AddModifier(new StatModifier((AccelerateFactor * 0.1f), StatModType.Flat, this));

            //var finalSpeed = (Hunger.Value * 0.01f) + (AccelerateFactor * 0.1f) + Speed.Value;
            return Speed.Value > maxSpeed ? maxSpeed : Speed.Value;
        }
    }

    public void InVertAxis(float duration)
    {
        _InvertFactor = -1;
        StartCoroutine(InVertAxisTimer(duration));
    }

    private IEnumerator InVertAxisTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _InvertFactor = 1;
    }

    public void BlurCamera(float duration)
    {
        GameplayCamera.gameObject.GetComponent<LuxWater_UnderWaterBlur>().enabled = true;
        StartCoroutine(BlurCameraTimer(duration));
    }

    private IEnumerator BlurCameraTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        GameplayCamera.gameObject.GetComponent<LuxWater_UnderWaterBlur>().enabled = false;
    }

    public void PickedUp(UsableItemSO itemSO)
    {
        _usableItemSO = itemSO;
        AddUsableItemUI();
    }
    private void UseItem()
    {
        _usableItemSO.UseItem(this);

        RemoveItemUI(this);
    }

    public void AddUsableItemUI()
    {
       
    }

    private void RemoveItemUI(PlayerControl playerControl)
    {

    }
    public void UpdateBufferItemUI(UsableItemSO buffItemSO)
    {

    }
}
