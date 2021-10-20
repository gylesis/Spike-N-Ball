using System;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using For_UI;
using UnityEngine;

public class ShopNotifier : MonoBehaviour
{
    [SerializeField] private GameObject _cart;

    [SerializeField] private float _duration;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _endValue;
    [SerializeField] private float _tweenMoveYModifier;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            NotifyShop();
        }
    }

    private void Start()
    {
        CheckForNotify();
    }
        
    private void CheckForNotify()
    {
        var boughtSkins = Skins.skins.Where(skin => skin.bought == false);

        var money = Money.Instance.money;
        foreach (Skin skin in boughtSkins)
        {
            if (skin.cost <= money) 
                NotifyShop();
        }
    }
        
    public void NotifyShop()
    {
        float scaleX = _cart.transform.localScale.x;

        var positionY = _cart.transform.position.y;

        var scaleTween = _cart.transform.DOScaleX(_endValue,_duration).SetEase(_curve);
        scaleTween.OnComplete(() => _cart.transform.DOScaleX(scaleX,1)).SetLoops(10);

        var positionTween = _cart.transform.DOMoveY(positionY + _tweenMoveYModifier,1);
        positionTween.OnComplete(() => _cart.transform.DOMoveY(positionY, 1)).SetLoops(10);
    }
}