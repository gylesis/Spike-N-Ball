using System.Collections;
using System.Collections.Generic;
using For_UI;
using UnityEngine;

public class ParticleChoose : MonoBehaviour {
    [SerializeField]
    ParticleSystemRenderer PSR;

    private void Update() {

        if (PSR != null) {
            if (ListOfStyles.CurrentParticles == null) {
                PSR.enabled = false;
            }
            else {
                PSR.enabled = true;
                PSR.material = ListOfStyles.CurrentParticles;
            }
        }
    }

}
