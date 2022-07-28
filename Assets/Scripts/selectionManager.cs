using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectionManager : MonoBehaviour
{

    public void manualHarvesting()
    {
        SceneManager.LoadScene("ManualHarvest");
    }

    public void autoHarvesting()
    {
        SceneManager.LoadScene("AutoHarvest");
    }
}
