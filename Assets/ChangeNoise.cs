using System;
using UnityEngine;

public class ChangeNoise : MonoBehaviour
{
    public ReaderManager readerManager;
    public float noiseFrequency;
    // public float noiseStrentgh;
   
    // Use this for initialization
    void Start ()
    {
        var readerObj = GameObject.FindWithTag("readerManager").GetComponent<ReaderManager>();
        readerManager = readerObj;
        var dynamicValue = Single.Parse(readerManager.GetReaderSetting("dynamic"));
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var no = ps.noise;
        no.enabled = true;
        noiseFrequency = dynamicValue;
        //no.strength = 1.0f;
        no.quality = ParticleSystemNoiseQuality.High;
        no.frequency = noiseFrequency;
    }
}
