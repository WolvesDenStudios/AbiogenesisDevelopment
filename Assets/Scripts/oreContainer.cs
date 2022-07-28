using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oreContainer : MonoBehaviour
{
    [SerializeField]
    private Text naniteOreCounterText;
    [SerializeField]
    private Text rareOreCounterText;
    public float naniteOreCounter = 0f, rareOreCounter = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        naniteOreCounterText.text = naniteOreCounter.ToString();
        rareOreCounterText.text = rareOreCounter.ToString();
        oreCountStoring.naniteOreCount = naniteOreCounter;
    }
}
