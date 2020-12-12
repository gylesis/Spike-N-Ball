using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AchievementButton : MonoBehaviour 
{
    public static AchievementButton Instance { get; set; }
    public GameObject achButton;
    public Transform prefabAch;

    RaycastHit2D ray2d;
    void Start()
    {
        Instance = this;
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                foreach (Achievement achievement in Achievements.achievements)
                {
                    Transform texts = achButton.transform.GetChild(0);
                    Text header = texts.gameObject.GetComponent<Text>();
                    if (achievement.header == header.text)
                    {
                        achievement.GetAward();
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}