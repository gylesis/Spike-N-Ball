using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl Instance { get; private set; }

    AudioLowPassFilter LowPass;
    AudioSource MainTheme;
    float Volume;
    float LowPassFilterVolume;
    [SerializeField] [Range(0, 1)] float ModifierVolume;
    [SerializeField] [Range(30, 100)] float ModifierLowPasFilter;

    void Start()
    {
        Instance = this;
        MainTheme = gameObject.GetComponent<AudioSource>();   
        LowPass = GetComponent<AudioLowPassFilter>();
        Volume = MainTheme.volume;
        LowPassFilterVolume = LowPass.cutoffFrequency;
    }
    public void MusicStart()
    {
        MainTheme.Play();
    }
    public void VolumingDownDeath()
    {
        if (MainTheme.volume > 0) MainTheme.volume -= ModifierVolume;
    }
    public void VolumingDown()
    {
        if (MainTheme.volume > 0.005) MainTheme.volume -= ModifierVolume ;
        if (LowPass.cutoffFrequency > 750) LowPass.cutoffFrequency -= ModifierLowPasFilter;
    }
    public void VolumingUp()
    {
        if (MainTheme.volume < Volume) MainTheme.volume += ModifierVolume ;
        if (LowPass.cutoffFrequency < LowPassFilterVolume) LowPass.cutoffFrequency += ModifierLowPasFilter;

    }
    void Update()
    {
        if (MenuScript.Instance.GameIsPaused) VolumingDown();
        else if (!MenuScript.Instance.GameIsPaused && EnviromentAct.Instance.GameIsStarted) VolumingUp();

        if (PlayerControl.Instance.BoolOnDeath2) VolumingDownDeath(); 
    }
}
