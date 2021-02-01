using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatricleChoose : MonoBehaviour {
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
