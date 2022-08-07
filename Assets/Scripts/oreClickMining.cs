using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oreClickMining : MonoBehaviour
{
    public bool isScanned = false, toggleScan = false, canBeMined = false, toggleMine = false, isRare = false, startShrinking = false; 
    public bool isVisible = false, startDestroyTimer = false, isShieldOff = false;
    private Image oreImg;
    private Button oreBtn;
    [SerializeField]
    private float destroyTimer = 1.5f, shrinkingDuration = 0;
    [SerializeField]
    private GameObject oreCounter;
    private GameObject shield;
    private GameObject overheatBarGameObject;
    private GameObject malfunctionGameObject;
    private GameObject oreMaterialSpawner;
    private GameObject armMiner;

    void Awake()
    {
        armMiner = GameObject.FindWithTag("armMiner");
        oreImg = GetComponent<Image>();
        oreBtn = GetComponent<Button>();
        oreCounter = GameObject.Find("ore_Counter");
        shield = transform.GetChild(0).gameObject;
        overheatBarGameObject = GameObject.FindWithTag("overheatManager");
        malfunctionGameObject = GameObject.FindWithTag("malfunctionManager");
        oreMaterialSpawner = GameObject.Find("ore_Spawn");
    }

    void Start()
    {
        oreImg.color = new Color(255, 255, 255, 0);
        shield.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        if (armMiner.GetComponent<Collider2D>().bounds.Contains(gameObject.transform.position))
        {
            Destroy(gameObject);
        }
    }

    public int getIndex(List<Vector3> list, Vector3 item)
    {
        for(int i=0;i<list.Count;i++)
        {
            if(list[i] == item) return i;
        }
        return -1;
    }

    void Update()
    {
        if (startDestroyTimer)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                int posIndex = getIndex(oreMaterialSpawner.GetComponent<OreSpawner>().orePositions, gameObject.transform.position);
                oreMaterialSpawner.GetComponent<OreSpawner>().orePositions.RemoveAt(posIndex);
                Destroy(gameObject);
            }
        }

        if (startShrinking)
        {
            shrinkingDuration += Time.deltaTime;
            // shrink from 1 to 0 over 3 seconds;
            shield.transform.localScale = new Vector3(Mathf.Lerp(1f, 0f, shrinkingDuration/3f), Mathf.Lerp(1f, 0f, shrinkingDuration/3f), 1);
            if (shield.transform.localScale.x <= 0.58f)
            {
                isShieldOff = true;
            }
        }

        if (isScanned && toggleScan)
        {
            startShrinking = true;
            startDestroyTimer = true;
            oreImg.color = new Color(255, 255, 255, 255);
            shield.GetComponent<Image>().color = new Color(255, 255, 255, 170);
            toggleScan = false;
        }
        if (canBeMined && toggleMine)
        {
            oreBtn.interactable = true;
            toggleMine = false;
        }
        if (!canBeMined && toggleMine)
        {
            oreBtn.interactable = false;
            toggleMine = false;
        }
        if (!isScanned)
        {
            oreImg.color = new Color(255, 255, 255, 0);
            shield.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }

    public void mineOre()
    {
        if (isShieldOff)
        {
            if (isRare)
            {
                oreCounter.GetComponent<oreContainer>().rareOreCounter += 1;
            }
            else
            {
                oreCounter.GetComponent<oreContainer>().naniteOreCounter += 1;
            }
            int posIndex = getIndex(oreMaterialSpawner.GetComponent<OreSpawner>().orePositions, gameObject.transform.position);
            oreMaterialSpawner.GetComponent<OreSpawner>().orePositions.RemoveAt(posIndex);
            Destroy(gameObject);
        }
        else
        {
            overheatBarGameObject.GetComponent<overheatManager>().misclickCount += 1;
            malfunctionGameObject.GetComponent<malfunctionManager>().misclickCount += 1;
            malfunctionGameObject.GetComponent<malfunctionManager>().rotationUpdated = true;
        }
    }
}
