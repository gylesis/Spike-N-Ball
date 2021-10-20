using UnityEngine;
using UnityEngine.UI;

class ImageButton : CustomButton
{
    [SerializeField] private Image _image;
        
    public void TurnOn()
    {
        _image.color = Color.white;
    }

    public void TurnOff()
    {
        Color color = _image.color;
        color.a = 0.1f;
        _image.color = color;
    }        
}