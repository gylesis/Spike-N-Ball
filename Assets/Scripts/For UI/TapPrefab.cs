using System.Collections;
using UnityEngine;

namespace For_UI
{
    public class TapPrefab : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite;

        public void FadeCoroutine()
        {
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            while (true)
            {
                sprite.color -= new Color(0, 0, 0, 0.05f);
                yield return new WaitForSeconds(0.1f);
                if (sprite.color.a < 0.001f)
                {
                    Destroy(gameObject, 2f);
                    break;
                }
            }
        }
    }
}