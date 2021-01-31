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

    public void JoystickSwitch() {

        /* PlayerPrefs.SetInt("JoystickSwitch", isEnabled ? 1 : 0);

         isEnabled = PlayerPrefs.GetInt("JoystickSwitch") == 1 ? true : false;
         isEnabled = !isEnabled;

         PlayerPrefs.Save();*/
    }




    private void Start() {
        Instance = this;
    }


    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
           
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit)) {
                tap = Instantiate(tapPrefab, hit.point, Quaternion.identity).GetComponent<TapPrefab>();
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            tap.FadeCoroutine();
        }


        /*if (isEnabled && !MenuScript.Instance.GameIsPaused && !OnIcon)
        {

            if (Input.GetMouseButtonDown(0))
            {
                transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                isFade = false;
                color.a = transparency;
                imageOfJoyStick.color = color;        
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = transform.position - Input.mousePosition;
                Stick.transform.position = transform.position - direction.normalized * (500 * transform.localScale.x);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isFade = true;
            }
            if (isFade)
            {
                color.a -= fadeRate * transparency;
                imageOfJoyStick.color = color;
            }
        }*/
    }


}
