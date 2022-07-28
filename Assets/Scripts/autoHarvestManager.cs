using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoHarvestManager : MonoBehaviour
{
    [SerializeField]
    private Text naniteCountTxt;
    [SerializeField]
    private Text rareCountTxt;
    private float naniteCount = 0f, rareCount = 0f;


    void Start()
    {
        naniteCount = oreCountStoring.naniteOreCount;
        naniteCountTxt.text = naniteCount.ToString();
        rareCountTxt.text = rareCount.ToString();
        StartCoroutine(automaticHarvestNaniteOre());
        // StartCoroutine(automaticHarvestRareOre());
    }


    void generateNaniteOre()
    {
        naniteCount += 1;
        naniteCountTxt.text = naniteCount.ToString();
    }

    void generateRareOre()
    {
        rareCount += 1;
        rareCountTxt.text = rareCount.ToString();
    }

    IEnumerator automaticHarvestNaniteOre()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            generateNaniteOre();
        }
    }

    IEnumerator automaticHarvestRareOre()
    {
        yield return new WaitForSeconds(110);
        generateRareOre();
    }
}
