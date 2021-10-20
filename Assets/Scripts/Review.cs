using UnityEngine;
using UnityEngine.UI;

public class Review : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _url;
    [SerializeField] private Button _exitButton;
        
    private void Awake()
    {
        _button.onClick.AddListener(Click);
        _exitButton.onClick.AddListener(() => Destroy(gameObject));
    }

    private void Click()
    {
        Application.OpenURL(_url);
        Destroy(gameObject,1);
    }
}