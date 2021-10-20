using System;
using DG.Tweening;
using UnityEngine;

public class CountdownTimer
{
    private readonly TimeContainer _time;
        
    public event Action<TimeContainer> OnTimeChange ;
    public event Action OnFinish ;
        
    public CountdownTimer(TimeContainer time)
    {
        _time = time;
        SetTime();
    }

    private void SetTime()
    {
        var timeRemaining = (float) _time.Minutes * 60 + _time.Seconds;
        
        UpdateTimer();

        if (timeRemaining < 0)
        {
            var timeContainer = new TimeContainer();

            OnTimeChange?.Invoke(timeContainer);
            OnFinish?.Invoke();
        }

        DOVirtual.DelayedCall(1, UpdateTimer).SetLoops((int) timeRemaining + 1);

        void UpdateTimer()
        {
            // var fromMinutes = TimeSpan.FromMinutes(timeRemaining);

            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
                
            var timeContainer = new TimeContainer
            {
                Minutes = minutes,
                Seconds = seconds
            };

            OnTimeChange?.Invoke(timeContainer);


            if (timeRemaining <= 0)
            {
                OnFinish?.Invoke();
            }

            timeRemaining -= 1;
        }
            
    }
}