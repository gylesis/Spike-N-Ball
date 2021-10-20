using For_UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementNotifier : MonoBehaviour
{
    public static AchievementNotifier Instance { get; set; }

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private EaseMover _mover;
        
    private void Awake()
    {
        Instance = this;
    }

    public void Show(Achievement achievement)
    {
        _header.text = achievement.header;

        _icon.sprite = achievement.sprite;

        Debug.Log(Time.timeScale);
            
        _mover.Move(_rect);
    }
        
}