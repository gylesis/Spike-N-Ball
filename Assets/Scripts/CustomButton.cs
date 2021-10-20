using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class CustomButton : MonoBehaviour
{
    [SerializeField] protected Button _button;

    private void Reset() => 
        _button = GetComponent<Button>();

    public void OnClick(Action action) => 
        _button.onClick.AddListener(() => action?.Invoke());

    private void OnDestroy() => 
        _button.onClick.RemoveAllListeners();
}