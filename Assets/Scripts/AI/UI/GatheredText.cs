using AI.Entities;
using TMPro;
using UnityEngine;

namespace AI.UI
{
    public class GatheredText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            GetComponentInParent<Gatherer>().OnGatheredChanged += (count) => _text.SetText(count.ToString());
        }
    }
}