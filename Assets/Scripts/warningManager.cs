using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warningManager : MonoBehaviour
{
    private GameObject[] warningSigns = new GameObject[2];
    private GameObject[] warningTriggerManagers = new GameObject[2];
    private GameObject backgroundRadar;
    private bool startOverheatWarning = true, startMalfunctionWarning = true;
    private IEnumerator co;

    void Awake()
    {
        backgroundRadar = GameObject.FindWithTag("mainRadar");
        warningSigns[0] = GameObject.FindWithTag("overheatWarning");
        warningSigns[1] = GameObject.FindWithTag("malfunctionWarning");

        warningSigns[0].SetActive(false);
        warningSigns[1].SetActive(false);

        warningTriggerManagers[0] = GameObject.FindWithTag("overheatManager");
        warningTriggerManagers[1] = GameObject.FindWithTag("malfunctionManager");
    }

    void Update()
    {
        if (warningTriggerManagers[0].GetComponent<overheatManager>().misclickCount >= 5 && startOverheatWarning)
        {
            co = flashOverheatWarning(true);
            StartCoroutine(co);
            startOverheatWarning = false;
        }
        else if (warningTriggerManagers[0].GetComponent<overheatManager>().misclickCount <= 0)
        {
            co = flashOverheatWarning(false);
            StartCoroutine(co);
        }

        if (warningTriggerManagers[1].GetComponent<malfunctionManager>().misclickCount >= 2 && startMalfunctionWarning)
        {
            co = flashMalfunctionWarning(true);
            StartCoroutine(co);
            startMalfunctionWarning = false;
        }
        else if (warningTriggerManagers[1].GetComponent<malfunctionManager>().misclickCount <= 0)
        {
            co = flashMalfunctionWarning(false);
            StartCoroutine(co);
        }
    }

    IEnumerator flashOverheatWarning(bool isVisible)
    {
        warningSigns[0].SetActive(isVisible);
        if (isVisible)
        {
            backgroundRadar.GetComponent<Image>().color = Color.red;
        }
        else
        {
            backgroundRadar.GetComponent<Image>().color = Color.white;
        }
        
        yield return new WaitForSeconds(5);
        startOverheatWarning = true;
        startMalfunctionWarning = true;
    }

    IEnumerator flashMalfunctionWarning(bool isVisible)
    {
        warningSigns[1].SetActive(isVisible);
        if (isVisible)
        {
            backgroundRadar.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            backgroundRadar.GetComponent<Image>().color = Color.white;
        }
        
        yield return new WaitForSeconds(3);
        startMalfunctionWarning = true;
    }
}
