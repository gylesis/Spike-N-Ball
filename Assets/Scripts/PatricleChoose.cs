using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatricleChoose : MonoBehaviour
{
    [SerializeField]
    ParticleSystemRenderer PSR;

    private void Start()
    {
        if (transform.GetChild(0).TryGetComponent<ParticleSystemRenderer>(out var particleSystem))
        {
            PSR = particleSystem;
        }

        if (Styles.Stili[PlayerPrefs.GetInt("currentSkin", 3)].Particles != null)
        {
            PSR.enabled = true;
            PSR.material = ListOfStyles.CurrentParticles;
        }
        else PSR.enabled = false;


    }
   

    private void Update()
    {
        if (PSR != null)
        {
            if (PSR.material != ListOfStyles.CurrentParticles && ListOfStyles.CurrentParticles != null)
            {
                PSR.enabled = true;
                PSR.material = ListOfStyles.CurrentParticles;
                
            }
            else if(ListOfStyles.CurrentParticles == null)
            {
                PSR.enabled = false;
            }
        }
    }
}
