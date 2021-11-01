using System;
using For_UI;
using For_Unique_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeReward : MonoBehaviour
{
    [SerializeField] private TimeContainer _waitTimeToOpenChest;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PopUp _rewardPopUp;
    [SerializeField] private Button _openPopUpButton;
    [SerializeField] private Image _openPopUpButtonImage;
    [SerializeField] private Sprite _rewardSprite;
    [SerializeField] private Sprite _waitSprite;
        
    private const string Freereward = "FreeReward";

    private bool _isRewarded;
    private TimeContainer _leftTime;
        
    private void Load()
    {
        var json = PlayerPrefs.GetString(Freereward);

        if (json == String.Empty)
        {
            _isRewarded = false;
            _leftTime = _waitTimeToOpenChest;
            return;
        }

        var data = Data.FromJson(json);

        _isRewarded = data.IsRewarded;
        _leftTime = data.TimeContainer;
    }

    private void Start()
    {
        _openPopUpButtonImage = _openPopUpButton.GetComponent<Image>();
        Load();
        CheckChestCountdown();
        _openPopUpButton.onClick.AddListener(OpenPopUp);
    }

    private void CheckChestCountdown()
    {
        if (_isRewarded)
            StartTimer(_leftTime);
        else
            SetActivePopUpButton(true);
    }

    private void StartTimer(TimeContainer timeContainer)
    {
        TimerUpdate(timeContainer);
        var countdownTimer = new CountdownTimer(timeContainer);

        countdownTimer.OnTimeChange += TimerUpdate;
        countdownTimer.OnFinish += () => SetActivePopUpButton(true);
    }

    private void SetActivePopUpButton(bool isActive)
    {
        if (isActive)
        {
            _openPopUpButtonImage.sprite = _rewardSprite;
            _text.text = "GET REWARD";
        }
        else _openPopUpButtonImage.sprite = _waitSprite;
            
        _openPopUpButton.interactable = isActive;
        _isRewarded = !isActive;
    }

    private void OpenPopUp()
    {
        _rewardPopUp.gameObject.SetActive(true);
        _rewardPopUp.OnAdClose((() =>
        {
            Money.Instance.money += 200;
            _isRewarded = true;
            _rewardPopUp.gameObject.SetActive(false);
            SetActivePopUpButton(false);
            StartTimer(_waitTimeToOpenChest);
        }));
    }

    private void TimerUpdate(TimeContainer leftTime)
    {
        var text = $"{leftTime.Minutes:00}:{leftTime.Seconds:00}";

        _text.text = text;
            
        _leftTime.Minutes = leftTime.Minutes;
        _leftTime.Seconds = leftTime.Seconds;
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Save()
    {
        var data = new Data
        {
            IsRewarded = _isRewarded,
            TimeContainer = _leftTime
        };

        PlayerPrefs.SetString(Freereward, data.ToJson());

        PlayerPrefs.Save();
    }

    [Serializable]
    public class Data
    {
        public bool IsRewarded;
        public TimeContainer TimeContainer;

        public string ToJson() =>
            JsonUtility.ToJson(this);

        public static Data FromJson(string json) =>
            JsonUtility.FromJson<Data>(json);
    }
}