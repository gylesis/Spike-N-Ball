using System;
using For_UI;
using UnityEngine;
using UnityEngine.UI;

public class JoystickView : MonoBehaviour
{
    [SerializeField] private Toggle _switchJoystick;
    [SerializeField] private Joystick _joystick;
        
    private void Awake()
    {
        _switchJoystick.onValueChanged.AddListener(SwitchJoystickState);
    }

    private void OnEnable()
    {
        _switchJoystick.isOn = !_joystick.IsOn;
    }

    private void SwitchJoystickState(bool _)
    {
        _joystick.SwitchState();
    }
}