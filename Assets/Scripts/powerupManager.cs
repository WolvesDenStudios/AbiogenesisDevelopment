using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupManager : MonoBehaviour
{
    public int powerUpCount = 0;
    public GameObject[] powerSlots = new GameObject[2];
    public GameObject[] powerSpawnPoints = new GameObject[3];
    [SerializeField]
    private GameObject powerupPrefab;
    public bool startSpawn = false;

    void Awake()
    {
        powerSlots[0] = GameObject.Find("slot_1");
        powerSlots[0].SetActive(false);
        powerSlots[1] = GameObject.Find("slot_2");
        powerSlots[1].SetActive(false);
        powerSpawnPoints[0] = GameObject.Find("spawn_1");
        powerSpawnPoints[1] = GameObject.Find("spawn_2");
        powerSpawnPoints[2] = GameObject.Find("spawn_3");
    }

    void Start()
    {
        StartCoroutine(randomSpawn());
    }

    public void updatePowerUpSlots(int powerCount)
    {
        for(int i = 0; i < 2; i++)
        {
            powerSlots[i].SetActive(false);
        }
        for(int i = 0; i < powerCount; i++)
        {
            powerSlots[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpCount <= 2)
        {
          updatePowerUpSlots(powerUpCount);
        }
        
        if(startSpawn)
        {
            powerupSpawn();
            startSpawn = false;
        }
    }

    void powerupSpawn()
    {
        int chosenSpawn = Random.Range(0, 3);
        GameObject spawnedOre = Instantiate(powerupPrefab, powerSpawnPoints[chosenSpawn].transform.position, Quaternion.identity, powerSpawnPoints[chosenSpawn].transform);
    }

    IEnumerator randomSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            float probability = 0.4f;
            if (Random.Range(0f, 2f) < probability)
            {
                startSpawn = true;
            }
        }
    }


}
