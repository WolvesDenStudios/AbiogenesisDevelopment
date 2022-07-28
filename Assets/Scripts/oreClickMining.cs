using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oreClickMining : MonoBehaviour
{
    public bool isScanned = false, toggleScan = false, canBeMined = false, toggleMine = false, isRare = false; 
    private bool isVisible = false, startDestroyTimer = false;
    private Image oreImg;
    private Button oreBtn;
    [SerializeField]
    private float destroyTimer = 1.5f;
    [SerializeField]
    private GameObject oreCounter;

    void Awake()
    {
        oreImg = GetComponent<Image>();
        oreBtn = GetComponent<Button>();
        oreCounter = GameObject.Find("ore_Counter");
    }

    void Start()
    {
        oreImg.color = new Color(255, 255, 255, 0);
    }

    void Update()
    {
        if (startDestroyTimer)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (isScanned && toggleScan)
        {
            startDestroyTimer = true;
            oreImg.color = new Color(255, 255, 255, 255);
            toggleScan = false;
        }
        if (canBeMined && toggleMine)
        {
            oreBtn.interactable = true;
            toggleMine = false;
        }
        if (!canBeMined)
        {
            oreBtn.interactable = false;
        }
        if (!isScanned)
        {
            oreImg.color = new Color(255, 255, 255, 0);
        }
    }

    public void mineOre()
    {
        if (isRare)
        {
            oreCounter.GetComponent<oreContainer>().rareOreCounter += 1;
        }
        else
        {
            oreCounter.GetComponent<oreContainer>().naniteOreCounter += 1;
        }
        Destroy(gameObject);
    }
}
