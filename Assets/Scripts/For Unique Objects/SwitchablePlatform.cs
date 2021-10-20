using UnityEngine;

namespace For_Unique_Objects
{
    public class SwitchablePlatform : MonoBehaviour
    {
        GameObject[] platforms;
        MeshRenderer[] MR;
        BoxCollider[] BC;
        bool switч;

        void Start()
        {
            InitializePlatforms();
        }

        public void SwitchPlatforms()
        {
            switч = !switч;
            if (switч)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    ActivateByName(i, "One");
                    DiactivateByName(i, "Two");
                }
            }
            else
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    ActivateByName(i, "Two");
                    DiactivateByName(i, "One");
                }
            }
        }

        private void InitializePlatforms()
        {
            platforms = new GameObject[transform.childCount];
            MR = new MeshRenderer[transform.childCount];
            BC = new BoxCollider[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                platforms[i] = transform.GetChild(i).gameObject;
                MR[i] = platforms[i].transform.GetComponent<MeshRenderer>();
                BC[i] = platforms[i].transform.GetComponent<BoxCollider>();

                DiactivateByName(i, "One");
            }
        }

        void DiactivateByName(int counter ,string name)
        {
            if (platforms[counter].name == name)
            {
                platforms[counter].transform.GetChild(1).gameObject.SetActive(false);
                BC[counter].isTrigger = true;
                MR[counter].enabled = false;
            }
        }
        void ActivateByName(int counter, string name)
        {
            if (platforms[counter].name == name)
            {
                platforms[counter].transform.GetChild(1).gameObject.SetActive(true);
                BC[counter].isTrigger = false;
                MR[counter].enabled = true;
            }
        }
    }
}
