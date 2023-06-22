using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTest : MonoBehaviour
{
    public float _inputx, _inputy;
    public bool _isWalking, _isRunning, _isIdle, _isCarrying;
    public ToolEffect _toolEffect;
    public bool _isUsingToolRight, _isUsingToolLeft, _isUsingToolUp, _isUsingToolDown;
    public bool _isLiftingToolRight, _isLiftingToolLeft, _isLiftingToolUp, _isLiftingToolDown;
    public bool _isPickingRight, _isPickingLeft, _isPickingUp, _isPickingDown;
    public bool _isSwingingToolRight, _isSwingingToolLeft, _isSwingingToolUp, _isSwingingToolDown;
    public bool _isIdleRight, _isIdleLeft, _isIdleUp, _isIdleDown;
    private void Update()
    {
        EventHandler.CallMovementEvent(_inputx, _inputy, _isWalking, _isRunning, _isIdle, _isCarrying,
            _toolEffect,
            _isUsingToolRight, _isUsingToolLeft, _isUsingToolUp, _isUsingToolDown,
            _isLiftingToolRight, _isLiftingToolLeft, _isLiftingToolUp, _isLiftingToolDown,
            _isPickingRight, _isPickingLeft, _isPickingUp, _isPickingDown,
            _isSwingingToolRight, _isSwingingToolLeft, _isSwingingToolUp, _isSwingingToolDown,
            _isIdleRight, _isIdleLeft, _isIdleUp, _isIdleDown);
    }
}
