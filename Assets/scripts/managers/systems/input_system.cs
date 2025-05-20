using System;
using System.Collections.Generic;

using UnityEngine;

class InputSystem : GameSystem
{
    private bool _playerInput;
    public bool playerInput
    {
        get => _playerInput;
        set
        {
            if (value)
                return;
            foreach (var ax in _axis)
            {
                _axis[ax.Key].value = 0;
            }
        }
    }

    const string horizontalAxis = "Horizontal";
    const string verticalAxis = "Vertical";

    private Dictionary<string, InputAxis> _axis = new Dictionary<string, InputAxis>();

    private void Start()
    {
        _axis[horizontalAxis] = new InputAxis(Input.GetAxis(horizontalAxis));
        _axis[verticalAxis] = new InputAxis(Input.GetAxis(verticalAxis));
    }

    public float GetAxis(string axis)
    {
        if (!_axis[axis].enable)
            return 0;
        if (playerInput)
            _axis[axis].value = Input.GetAxis(axis);
        return _axis[axis].value;
    }

    public void EnableAxis(string axis, bool value)
    {
        _axis[axis].enable = value;
    }

    [Serializable]
    public class InputAxis
    {
        public float value { get; internal set; }
        public bool enable { get; internal set; }

        public InputAxis(float value)
        {
            this.value = value;
        }
    }
}