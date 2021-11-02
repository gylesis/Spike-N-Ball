using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] platforms;

    public void ToggleDoor(bool activate, Action callback)
    {
        foreach (var platform in platforms)
        {
            platform.DOScaleX(activate ? 0.5f : 0, 0.1f).OnComplete(() =>  callback());
        }
    }

}
