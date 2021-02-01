using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {
    public static AchievementButton Instance { get; set; }
    public GameObject achButton;
    public Transform prefabAch;

    [SerializeField]
    Text awardAmount;

    [SerializeField]
    GameObject onGetAwardParticlesPrefab;

    Achievement achievement;

    void Initialize() {
        foreach (Achievement achievement in Achievements.achievements) {
            Transform texts = achButton.transform.GetChild(0);
            Text header = texts.gameObject.GetComponent<Text>();
            if (achievement.header == header.text) {
                this.achievement = achievement;
            }
        }
    }

    void Start() {
        Instance = this;
        Initialize();

        awardAmount.text = achievement.awardAmount.ToString();
    }

    private void OnMouseDown() {
        Instantiate(onGetAwardParticlesPrefab, transform.root);
        achievement.GetAward();

        Debug.Log(achievement.header);
        gameObject.SetActive(false);

    }


}