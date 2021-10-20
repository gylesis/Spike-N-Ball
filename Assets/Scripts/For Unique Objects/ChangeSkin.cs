using UnityEngine;

namespace For_Unique_Objects
{
    public class ChangeSkin : MonoBehaviour
    {
        [SerializeField] Texture[] ListOfTextures;
        [SerializeField] int IndexOfCurrentTexture;

        public void ChangingSkin(int _IndexOfCurrentTexture)
        {
            IndexOfCurrentTexture = _IndexOfCurrentTexture;
        }

   

    }
}
