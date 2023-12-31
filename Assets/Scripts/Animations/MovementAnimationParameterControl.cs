using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationParameterControl : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.MovementEvent += SetAnimationParameters;
    }
    private void Ondisable()
    {
        EventHandler.MovementEvent -= SetAnimationParameters;
    }

    /// <summary>
    /// 控制角色动画的函数
    /// </summary>
    /// <param name="animationParameters"></param>
    private void SetAnimationParameters(float inputx, float inputy, bool isWalking, bool isRunning, bool isIdle, bool isCarrying,
        ToolEffect toolEffect,
        bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
        bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
        bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
        bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
        bool isIdleRight, bool isIdleLeft, bool isIdleUp, bool isIdleDown)
    {
        _animator.SetFloat(Settings.xInput, inputx);
        _animator.SetFloat(Settings.yInput, inputy);
        _animator.SetBool(Settings.isWalking, isWalking);
        _animator.SetBool(Settings.isRunning, isRunning);
        _animator.SetInteger(Settings.toolEffect, (int)toolEffect);

        if (isUsingToolRight)
            _animator.SetTrigger(Settings.isUsingToolRight);
        if (isUsingToolLeft)
            _animator.SetTrigger(Settings.isUsingToolLeft);
        if (isUsingToolUp)
            _animator.SetTrigger(Settings.isUsingToolUp);
        if (isUsingToolDown)
            _animator.SetTrigger(Settings.isUsingToolDown);
        if (isLiftingToolRight)
            _animator.SetTrigger(Settings.isLiftingToolRight);
        if (isLiftingToolLeft)
            _animator.SetTrigger(Settings.isLiftingToolLeft);
        if (isLiftingToolUp)
            _animator.SetTrigger(Settings.isLiftingToolUp);
        if (isLiftingToolDown)
            _animator.SetTrigger(Settings.isLiftingToolDown);
        if (isPickingRight)
            _animator.SetTrigger(Settings.isPickingRight);
        if (isPickingLeft)
            _animator.SetTrigger(Settings.isPickingLeft);
        if (isPickingUp)
            _animator.SetTrigger(Settings.isPickingUp);
        if (isPickingDown)
            _animator.SetTrigger(Settings.isPickingDown);
        if (isSwingingToolRight)
            _animator.SetTrigger(Settings.isSwingingToolRight);
        if (isSwingingToolLeft)
            _animator.SetTrigger(Settings.isSwingingToolLeft);
        if (isSwingingToolUp)
            _animator.SetTrigger(Settings.isSwingingToolUp);
        if (isSwingingToolDown)
            _animator.SetTrigger(Settings.isSwingingToolDown);
        if (isIdleRight)
            _animator.SetTrigger(Settings.isIdleRight);
        if (isIdleLeft)
            _animator.SetTrigger(Settings.isIdleLeft);
        if (isIdleUp)
            _animator.SetTrigger(Settings.isIdleUp);
        if (isIdleDown)
            _animator.SetTrigger(Settings.isIdleDown);
    }
    private void AnimationEventPlayFootstepSound() { }
}