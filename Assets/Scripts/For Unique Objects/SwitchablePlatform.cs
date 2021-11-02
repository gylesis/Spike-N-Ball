using UnityEngine;

namespace For_Unique_Objects
{
    public class SwitchablePlatform : MonoBehaviour
    {
        GameObject[] platforms;
        DoorAnimation[] DA;
        BoxCollider[] BC;
        bool switч;

        void Start()
        {
            InitializePlatforms();
        }

        public void SwitchPlatforms()
        {
            switч = !switч;
            for (int i = 0; i < platforms.Length; i++)
            {
                ToggleByName(i, "One", switч);
                ToggleByName(i, "Two", !switч);
            }
        }
        
        private void InitializePlatforms()
        {
            platforms = new GameObject[transform.childCount];
            DA = new DoorAnimation[transform.childCount];
            BC = new BoxCollider[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                platforms[i] = transform.GetChild(i).gameObject;
                DA[i] = platforms[i].transform.GetComponent<DoorAnimation>();
                BC[i] = platforms[i].transform.GetComponent<BoxCollider>();

                ToggleByName(i, "One", false);
                ToggleByName(i, "Two", true);
            }
        }

        void ToggleByName(int counter, string name, bool activate)
        {
            if (platforms[counter].name == name)
            {
                var switchSign = platforms[counter].transform.GetChild(1).gameObject;
                if (!activate) switchSign.SetActive(false);
                BC[counter].enabled = !activate;
                DA[counter].ToggleDoor(activate, () => { if (activate) switchSign.SetActive(true); });
            }
        }
    }
}