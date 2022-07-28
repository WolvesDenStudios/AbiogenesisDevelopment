using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armScanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ore1" || other.tag == "ore2")
        {
            other.gameObject.GetComponent<oreClickMining>().isScanned = true;
            other.gameObject.GetComponent<oreClickMining>().toggleScan = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // if (other.tag == "ore1")
        // {
        //     other.gameObject.GetComponent<oreClickMining>().isScanned = false;
        // }
    }
}
