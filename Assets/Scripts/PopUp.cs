using System;
using For_Unique_Objects;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Button _showRewardButton;
    [SerializeField] private Button _exitButton;
    private Action _action;

    private void Awake()
    {
        _exitButton.onClick.AddListener((() => gameObject.SetActive(false)));
        _showRewardButton.onClick.AddListener(OnClick);
    }

    public void OnAdClose(Action action)
    {
        _action = action;
    }
        
    private void OnClick()
    {
        AdController.Instance.TryPlayRewardedAd();
        AdController.Instance.OnAdClose += (() => _action?.Invoke());
    }
}