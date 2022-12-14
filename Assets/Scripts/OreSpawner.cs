using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject naniteOre;
    [SerializeField]
    private GameObject rareOre;
    [SerializeField]
    private GameObject harvestBar;
    [SerializeField]
    private GameObject parentOre;
    [SerializeField]
    private GameObject armMiner;
    [SerializeField]
    private GameObject armScanner;
    public GameObject spawnArea;
    [SerializeField]
    private float nanitesOreCount = 300f, rareOreCount = 6;
    [SerializeField]
    private float naniteOreGenerated = 0f, rareOreGenerated = 0f, miningSessionTimerInSeconds;
    public List<Vector3> orePositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(naniteOreSpawner());
        StartCoroutine(rareOreSpawner());
    }

    void Update()
    {
        miningSessionTimerInSeconds = harvestBar.GetComponent<oreMiningSession>().miningSessionTimerInSeconds;
    }

    private void spawnOre(GameObject oreToSpawn, bool isRare)
    {
        var radiusBounds = spawnArea.GetComponent<CircleCollider2D>().bounds;
        var px = Random.Range(radiusBounds.min.x, radiusBounds.max.x);
        var py = Random.Range(radiusBounds.min.y, radiusBounds.max.y);
        Vector3 pos = new Vector3(px, py, 0);
        
        if (orePositions.Count > 0)
        {
            // foreach (Vector2 existingPos in orePositions)
            // for (int existingPos = 0; existingPos < orePositions.Count; existingPos++)
            // {
                // if(pos.x < orePositions[existingPos].x || pos.x > orePositions[existingPos].x || pos.y < orePositions[existingPos].y || pos.y > orePositions[existingPos].y)
                if(!orePositions.Contains(pos))
                {
                    if(!armMiner.GetComponent<Collider2D>().bounds.Contains(pos) && !armScanner.GetComponent<Collider2D>().bounds.Contains(pos))
                    {
                        GameObject spawnedOre = Instantiate(oreToSpawn, pos, Quaternion.identity, parentOre.transform);
                        spawnedOre.transform.SetSiblingIndex(0);
                        spawnedOre.GetComponent<oreClickMining>().isRare = isRare;
                        if (isRare)
                        {
                            rareOreGenerated -= 1;
                            rareOreCount -= 1;
                        }
                        else
                        {
                            naniteOreGenerated -= 1;
                            nanitesOreCount -= 1;
                        }
                        orePositions.Add(pos);
                    }
                }
            // }
        }
        else
        {
            if(!armMiner.GetComponent<Collider2D>().bounds.Contains(pos) && !armScanner.GetComponent<Collider2D>().bounds.Contains(pos))
            {
                GameObject spawnedOre = Instantiate(oreToSpawn, pos, Quaternion.identity, parentOre.transform);
                spawnedOre.GetComponent<oreClickMining>().isRare = isRare;
                if (isRare)
                {
                    rareOreGenerated -= 1;
                    rareOreCount -= 1;
                }
                else
                {
                    naniteOreGenerated -= 1;
                    nanitesOreCount -= 1;
                }
                orePositions.Add(pos);
            }
        }
        
    }

    IEnumerator naniteOreSpawner()
    {
        while(miningSessionTimerInSeconds >= 0)
        {
            yield return new WaitForSeconds(1);
            naniteOreGenerated += 0.5f;

            if (naniteOreGenerated > 1 && nanitesOreCount >= 1)
            {
                spawnOre(naniteOre, false);
            }
        }
    }

    IEnumerator rareOreSpawner()
    {
        while(miningSessionTimerInSeconds >= 0)
        {
            yield return new WaitForSeconds(1);
            rareOreGenerated += 0.01f;

            if (rareOreGenerated > 1 && rareOreCount >= 1)
            {
                spawnOre(rareOre, true);
            }
        }
    }
}
