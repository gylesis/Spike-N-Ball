using UnityEngine;

namespace For_UI
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private TapPrefab _tapPrefab;
        [SerializeField] Camera _camera;

        private TapPrefab _tapInstance;
        private bool _isOn;
        public bool IsOn => PlayerPrefs.GetInt("JoystickState") == 1;

        private const string NonJoystickableTag = "NonJoystickable";

        private void OnEnable()
        {
            _isOn = IsOn;
        }

        public void SwitchState()
        {
            _isOn = !_isOn;
            PlayerPrefs.SetInt("JoystickState", _isOn ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void Update()
        {
            if (MenuScript.GameIsPaused) return;

            if (_isOn == false) return;
        
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.CompareTag(NonJoystickableTag))
                    {
                        return;
                    }

                    _tapInstance = Instantiate(_tapPrefab, hit.point, Quaternion.identity, transform);
                }

            }
        
            if (Input.GetMouseButton(0))
            {       
                if(_tapInstance == null) return;
                
                Vector2 direction = PlayerControl.Instance.direction;
                _tapInstance.transform.eulerAngles = direction.y >= 0 ? 
                    new Vector3(0, 0, -Mathf.Asin(direction.x) * 180 / Mathf.PI) : 
                    new Vector3(0, 0, 180 + Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_tapInstance == null) return;

                _tapInstance.FadeCoroutine();
            }
        }
    }
}