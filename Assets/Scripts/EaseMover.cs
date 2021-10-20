using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class EaseMover : MonoBehaviour
{
    [SerializeField] private float _targetXValue = 270f;
    [SerializeField] private float _initXValue = 700f;
        
    [SerializeField] private int _delayBeforeFadeOut = 3;

    [SerializeField] private AnimationCurve _ease;

    private RectTransform _rect;

    public void Move(RectTransform rect)
    {
        _rect = rect; //getRect btw
            
        rect.DOLocalMoveX(_targetXValue, 1.5f).OnComplete(() => MoveBack()).SetEase(_ease);
    }

    private async Task MoveBack()
    {
        await Task.Delay(TimeSpan.FromSeconds(_delayBeforeFadeOut));

        _rect.DOLocalMoveX(_initXValue, 1.2f).SetEase(_ease);
    }
}