using For_UI;
using UnityEngine;
using UnityEngine.UI;

namespace For_Unique_Objects
{
    public class ImagesColorSetter : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Image _image;
        
        private void Start()
        {
            if (TryGetComponent<Image>(out var image))
            {
                if (image.sprite != null)
                {
                    _image = image;
                    image.color = ListOfStyles.Instance.CurrentMaterialForWalls.color;
                }
            }
            
            if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                _spriteRenderer = spriteRenderer;
                spriteRenderer.color = ListOfStyles.Instance.CurrentMaterialForWalls.color;
            }
            
            ListOfStyles.Instance.WallsColorChange += OnWallsColorChange;
        }

        private void OnWallsColorChange(Color color)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = color;
            }
            else if (_image != null)
            {
                _image.color = color;
            }
        }
    }
    
}