using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class oreMiningSession : MonoBehaviour
{
    [SerializeField]
    private GameObject harvestBar;
    [SerializeField]
    private Text harvestPercentage;
    public float miningSessionTimerInSeconds = 10;

    void Start()
    {
        StartCoroutine(startMineSession());
    }

    void Update()
    {
        
    }

    IEnumerator startMineSession()
    {
        while(miningSessionTimerInSeconds >= 0)
        {
            yield return new WaitForSeconds(1);
            harvestBar.GetComponent<Slider>().value -= 0.5555555555555556f;
            harvestPercentage.text = (int)harvestBar.GetComponent<Slider>().value + "%";
            miningSessionTimerInSeconds -= 1;
            if (miningSessionTimerInSeconds <= 0)
            {
                SceneManager.LoadScene("AutoHarvest");
            }
        }
    }
}
