using UnityEngine;

public class Notifier : MonoBehaviour
{
    [SerializeField] private Review _ratePrefab;

    private void Start()
    {
        var rated = PlayerPrefs.GetInt("Review") == 1;

        if (rated == false)
        {
            var deaths = Stats.Instance.Deaths;

            if (deaths % 5 == 0)
            {
                var timeSpend = Stats.Instance.TimeSpend;
                    
                CheckTimeSpend(timeSpend);
            }
        }
    }

    private void CheckTimeSpend(float value)
    {
        if (value >= 420)
        {
            Notify();
            PlayerPrefs.SetInt("Review", 1);
            PlayerPrefs.Save();
        }
    }

    private void Notify()
    {
        Instantiate(_ratePrefab,transform);
    }
}