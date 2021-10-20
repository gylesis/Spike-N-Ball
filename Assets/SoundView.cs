using System;
using For_Unique_Objects;
using UnityEngine;
using UnityEngine.UI;

public class SoundView : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(SwitchAudio);
    }

    private void OnEnable()
    {
        _toggle.isOn = !AudioManager.Instance.Muted;
    }

    private void SwitchAudio(bool _)
    {
        AudioManager.Instance.SwitchState();
    }
}