using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour {
    public static Joystick Instance;
    public bool OnIcon;

    [SerializeField]
    private GameObject tapPrefab;

    [SerializeField]
    Camera camera;

    TapPrefab tap;

    bool isOn;

    private void Start() {
        Instance = this;
    }

    private void OnEnable() {
        isOn = PlayerPrefs.GetInt("JoystickState") == 1 ? true : false;
    }

    public void SwitchState() {
        isOn = !isOn;
        PlayerPrefs.SetInt("JoystickState", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }


    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!isOn) {
                return;
            }

            if (MenuScript.GameIsPaused) {
                return;
            }

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit)) {

                if (hit.collider.tag == "NonJoystickable") {
                    return;
                }

                tap = Instantiate(tapPrefab, hit.point, Quaternion.identity, transform).GetComponent<TapPrefab>();
            }

        }
        if (Input.GetMouseButtonUp(0)) {
            if (tap != null) {
                tap.FadeCoroutine();
            }
        }

    }


}
