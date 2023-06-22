#pragma warning disable 0414
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    #region 动画参数

    private float _xInput;
    private float _yInput;
    private bool _isCarring = false;
    private bool _isIdle;
    private bool _isLiftingToolRight;
    private bool _isLiftingToolLeft;
    private bool _isLiftingToolUp;
    private bool _isLiftingToolDown;
    private bool _isRunning;
    private bool _isUsingToolRight;
    private bool _isUsingToolLeft;
    private bool _isUsingToolUp;
    private bool _isUsingToolDown;
    private bool _isSwingingToolRight;
    private bool _isSwingingToolLeft;
    private bool _isSwingingToolUp;
    private bool _isSwingingToolDown;
    private bool _isWalking;
    private bool _isPickingRight;
    private bool _isPickingLeft;
    private bool _isPickingUp;
    private bool _isPickingDown;
    private ToolEffect _toolEffect;

    #endregion 动画参数

    private Camera _mainCamera;

    private Rigidbody2D _rigidbody2D;
    private Direction _direction;
    private float _moveSpeed;
    private bool _playerInputIsDisabled = false;
    public bool PlayerInputIsDisabled
    {
        get { return _playerInputIsDisabled; }
        set { _playerInputIsDisabled = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        #region 玩家输入
        if (!PlayerInputIsDisabled)
        {
            ResetAnimationTrigger();
            PlayerMovementInput();
            PlayerWalkingInput();
            EventHandler.CallMovementEvent(_xInput, _yInput, _isWalking, _isRunning, _isIdle, _isCarring, _toolEffect,
                _isUsingToolRight, _isUsingToolLeft, _isUsingToolUp, _isUsingToolDown,
                _isLiftingToolRight, _isLiftingToolLeft, _isLiftingToolUp, _isLiftingToolDown,
                _isPickingRight, _isPickingLeft, _isPickingUp, _isPickingDown,
                _isSwingingToolRight, _isSwingingToolLeft, _isSwingingToolUp, _isSwingingToolDown,
                false, false, false, false);
        }

        #endregion 玩家输入
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    /// <summary>
    /// 重置动画参数
    /// </summary>
    private void ResetAnimationTrigger()
    {
        _isPickingRight = false;
        _isPickingLeft = false;
        _isPickingUp = false;
        _isPickingDown = false;
        _isUsingToolRight = false;
        _isUsingToolLeft = false;
        _isUsingToolUp = false;
        _isUsingToolDown = false;
        _isLiftingToolRight = false;
        _isLiftingToolLeft = false;
        _isLiftingToolUp = false;
        _isLiftingToolDown = false;
        _isSwingingToolRight = false;
        _isSwingingToolLeft = false;
        _isSwingingToolUp = false;
        _isSwingingToolDown = false;
        _toolEffect = ToolEffect.None;
    }

    /// <summary>
    /// 玩家移动控制
    /// </summary>
    private void PlayerMovementInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");
        if (_xInput != 0 && _yInput != 0)
        {
            _xInput = _xInput * 0.7f;
            _yInput = _yInput * 0.7f;
        }
        if (_xInput != 0 || _yInput != 0)
        {
            _isRunning = true;
            _isWalking = false;
            _isIdle = false;
            _moveSpeed = Settings._runningSpeed;
            if (_xInput < 0)
            {
                _direction = Direction.Left;
            }
            else if (_xInput > 0)
            {
                _direction = Direction.Right;
            }
            else if (_yInput > 0)
            {
                _direction = Direction.Up;
            }
            else if (_yInput < 0)
            {
                _direction = Direction.Down;
            }
        }
        else if (_xInput == 0 && _yInput == 0)
        {
            _isRunning = false;
            _isWalking = false;
            _isIdle = true;
        }

    }

    private void PlayerWalkingInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _isRunning = false;
            _isWalking = true;
            _isIdle = false;
            _moveSpeed = Settings._walkingSpeed;
        }
        else
        {
            _isRunning = true;
            _isWalking = false;
            _isIdle = false;
            _moveSpeed = Settings._runningSpeed;
        }
    }

    private void PlayerMovement()
    {
        Vector2 move = new Vector2(_xInput * _moveSpeed * Time.deltaTime, _yInput * _moveSpeed * Time.deltaTime);
        _rigidbody2D.MovePosition(_rigidbody2D.position + move);
    }

    public Vector3 GetPlayerViempointPosition()
    {
        return _mainCamera.WorldToViewportPoint(transform.position);
    }
    public void EnablePlayerInput()
    {
        PlayerInputIsDisabled = false;
    }
    public void DisablePlayerInput()
    {
        PlayerInputIsDisabled = true;
    }
    public void DisablePlayerInputAndResetMovement()
    {
        DisablePlayerInput();
        ResetMovement();
        EventHandler.CallMovementEvent(_xInput, _yInput, _isWalking, _isRunning, _isIdle, _isCarring, _toolEffect,
                _isUsingToolRight, _isUsingToolLeft, _isUsingToolUp, _isUsingToolDown,
                _isLiftingToolRight, _isLiftingToolLeft, _isLiftingToolUp, _isLiftingToolDown,
                _isPickingRight, _isPickingLeft, _isPickingUp, _isPickingDown,
                _isSwingingToolRight, _isSwingingToolLeft, _isSwingingToolUp, _isSwingingToolDown,
                false, false, false, false);
    }
    public void ResetMovement()
    {
        _xInput = 0f;
        _yInput = 0f;
        _isRunning = false;
        _isWalking = false;
        _isIdle = true;
    }
}
