using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl Instance { get; private set; }

    [SerializeField]
    AudioLowPassFilter LowPass;

    [SerializeField]
    AudioSource MainTheme;

    float Volume;
    float LowPassFilterVolume;
    [SerializeField] [Range(0, 1)] float modifierVolume;
    [SerializeField] [Range(30, 100)] float modifierLowPasFilter;

    [SerializeField]
    float lowestVolumePoint;

    void Start()
    {
        Instance = this;       
        Volume = MainTheme.volume;
        LowPassFilterVolume = LowPass.cutoffFrequency;
    }
    public void MusicStart()
    {
        MainTheme.Play();
    }
    public void VolumingDownDeath()
    {
        if (MainTheme.volume > 0) MainTheme.volume -= modifierVolume;
    }
    public void VolumingDown()
    {
        if (MainTheme.volume > lowestVolumePoint) MainTheme.volume -= modifierVolume;
        if (LowPass.cutoffFrequency > 750) LowPass.cutoffFrequency -= modifierLowPasFilter;
    }
    public void VolumingUp()
    {
        if (MainTheme.volume < Volume) MainTheme.volume += modifierVolume;
        if (LowPass.cutoffFrequency < LowPassFilterVolume) LowPass.cutoffFrequency += modifierLowPasFilter;

    }
    void Update()
    {
        if (MenuScript.GameIsPaused) VolumingDown();
        else if (!MenuScript.GameIsPaused && EnviromentAct.Instance.GameIsStarted) VolumingUp();

        if (PlayerControl.Instance.BoolOnDeath2) VolumingDownDeath(); 
    }
}
