using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class overheatManager : MonoBehaviour
{
    public int misclickCount = 0, countDownTimer = 5;
    public GameObject[] overheatBars = new GameObject[5];
    private bool isCooldown = false;
    private GameObject overheatAlpha;

    void Awake()
    {
        overheatAlpha = GameObject.Find("overheatAlpha");
        overheatAlpha.SetActive(false);
        for(int i = 0; i < transform.GetChild(1).gameObject.transform.childCount; i++)
        {
            overheatBars[i] = transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;
            overheatBars[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }

    void updateOverheatBar(int overheatCount)
    {
        for(int i = 0; i < overheatCount; i++)
        {
            overheatBars[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    void decreaseOverheatBar(int overheatCount)
    {
        overheatBars[overheatCount - 1].GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }
    
    void Update()
    {
        if (misclickCount < 5 && !isCooldown)
        {
            updateOverheatBar(misclickCount);
        }
        else if (misclickCount >= 5 && !isCooldown)
        {
            overheatAlpha.SetActive(true);
            isCooldown = true;
            StartCoroutine(decreaseMisclickCount());
        }
    }

    IEnumerator decreaseMisclickCount()
    {
        overheatBars[4].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(1);
        while(countDownTimer > 0)
        {
            decreaseOverheatBar(countDownTimer);
            countDownTimer -= 1;
            misclickCount -= 1;
            yield return new WaitForSeconds(1);
        }
        overheatAlpha.SetActive(false);
        isCooldown = false;
        countDownTimer = 5;
    }
}
