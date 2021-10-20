using System.Collections;
using UnityEngine;

namespace For_Unique_Objects
{
    public class MagneticSphere : MonoBehaviour
    {
        GameObject magneticSphere;
        PlayerControl player;
        private const float forceCoefficient = 21.5f;
        public bool isEnabled = true;
        public float radius;
        [SerializeField] private float _gravitationCoeff = 0.15f;
        private const float speedOfDisappear = 0.075f;

        void Start()
        {
            player = PlayerControl.Instance;
            magneticSphere = gameObject;
        }

        void FixedUpdate()
        {
            Vector2 distance = magneticSphere.transform.position - player.transform.position;
            Vector2 force = (forceCoefficient / distance.magnitude * distance.magnitude) * distance.normalized;
            if (distance.magnitude < radius && isEnabled)
            {
                player.Decelerate(_gravitationCoeff);
                PlayerControl.Instance.Rigidbody.AddForce(force, ForceMode.Force);
            }
        }

        public IEnumerator Disappear()
        {
            isEnabled = false;
            while (transform.localScale.x >= speedOfDisappear)
            {
                Vector2 newScale = new Vector2(transform.localScale.x - speedOfDisappear,
                    transform.localScale.x - speedOfDisappear);
                transform.localScale = newScale;

                yield return null;
            }

            transform.localScale = Vector3.zero;
        }

        public IEnumerator Recreate()
        {
            yield return new WaitForSeconds(10);
            isEnabled = true;
            while (transform.localScale.x < 0.4 - speedOfDisappear)
            {
                Vector2 newScale = new Vector2(transform.localScale.x + speedOfDisappear,
                    transform.localScale.x + speedOfDisappear);
                transform.localScale = newScale;

                yield return null;
            }
        }
    }
}