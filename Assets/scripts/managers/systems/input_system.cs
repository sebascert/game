using System;
using System.Collections.Generic;

using UnityEngine;

class InputSystem : GameSystem
{
    private bool _playerInput;
    public bool PlayerInput
    {
        get => _playerInput;
        set
        {
            if (value)
                return;
            foreach (var ax in _axis)
            {
                _axis[ax.Key].Value = 0;
            }
        }
    }

    const string HorizontalAxis = "Horizontal";
    const string VerticalAxis = "Vertical";

    private Dictionary<string, InputAxis> _axis = new Dictionary<string, InputAxis>();

    private void Start()
    {
        _axis[HorizontalAxis] = new InputAxis(Input.GetAxis(HorizontalAxis));
        _axis[VerticalAxis] = new InputAxis(Input.GetAxis(VerticalAxis));
    }

    public float GetAxis(string axis)
    {
        if (!_axis[axis].Enable)
            return 0;
        if (PlayerInput)
            _axis[axis].Value = Input.GetAxis(axis);
        return _axis[axis].Value;
    }

    public void EnableAxis(string axis, bool value)
    {
        _axis[axis].Enable = value;
    }

    [Serializable]
    public class InputAxis
    {
        public float Value { get; internal set; }
        public bool Enable { get; internal set; }

        public InputAxis(float value)
        {
            this.Value = value;
        }
    }
}