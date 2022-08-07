using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armMiner : MonoBehaviour
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
            other.gameObject.GetComponent<oreClickMining>().canBeMined = true;
            other.gameObject.GetComponent<oreClickMining>().toggleMine = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "ore1" || other.tag == "ore2")
        {
            other.gameObject.GetComponent<oreClickMining>().canBeMined = true;
            other.gameObject.GetComponent<oreClickMining>().toggleMine = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ore1" || other.tag == "ore2")
        {
            other.gameObject.GetComponent<oreClickMining>().canBeMined = false;
            other.gameObject.GetComponent<oreClickMining>().toggleMine = true;
        }
    }
}
