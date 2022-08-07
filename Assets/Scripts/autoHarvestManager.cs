using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Http;

public class autoHarvestManager : MonoBehaviour
{
    [SerializeField]
    private Text naniteCountTxt;
    [SerializeField]
    private Text rareCountTxt;
    private float naniteCount = 0f, rareCount = 0f;
    private IEnumerator co;
    private bool canAutoHarvest = false;
    public GameObject loadingScreen;

    void Awake()
    {
        loadingScreen = GameObject.Find("loading_Screen");
        loadingScreen.SetActive(false);
    }

    void Start()
    {
        canAutoHarvest = true;
        naniteCount = oreCountStoring.naniteOreCount;
        naniteCountTxt.text = naniteCount.ToString();
        rareCountTxt.text = rareCount.ToString();
        co = automaticHarvestNaniteOre();
        StartCoroutine(co);
    }


    void generateNaniteOre()
    {
        naniteCount += 1;
        naniteCountTxt.text = naniteCount.ToString();
    }

    void generateRareOre()
    {
        rareCount += 1;
        rareCountTxt.text = rareCount.ToString();
    }

    IEnumerator automaticHarvestNaniteOre()
    {
        while(canAutoHarvest)
        {
            yield return new WaitForSeconds(3);
            if (canAutoHarvest) {
                generateNaniteOre();
            }
        }
    }

    IEnumerator automaticHarvestRareOre()
    {
        yield return new WaitForSeconds(110);
        generateRareOre();
    }

    public class claimingDetails
    {
        public string to;
        public int amount;
    }

    public void request_Token()
    {
        co = automaticHarvestNaniteOre();
        StopCoroutine(co);
        canAutoHarvest = false;
        loadingScreen.SetActive(true);
        StartCoroutine(claimHarvestedMaterials());
    }

    public UnityWebRequest CreateApiRequest(string url, string method, object body)
    {
        string bodyString = null;
        if (body is string)
        {
            bodyString = (string)body;
        }
        else if (body != null)
        {
            bodyString = JsonUtility.ToJson(body);
        }
    
        var request = new UnityWebRequest();
        request.url = url;
        request.method = method;
        request.downloadHandler = new DownloadHandlerBuffer();
        request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(bodyString) ? null : Encoding.UTF8.GetBytes(bodyString));
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 60;
        return request;
    }

    public UnityWebRequest CreateApiPostRequest(string actionUrl, object body = null)
    {
        return CreateApiRequest("http://3.1.97.140:8081/api/v1/bsc/testnet/withdrawtoken/?api_key=VS7EX34-BPBM5S6-GR4Q6NK-ZTY6TZM ", UnityWebRequest.kHttpVerbPOST, body);
    }

    IEnumerator claimHarvestedMaterials()
    {
        string accountToken = PlayerPrefs.GetString("Account");
        var request = CreateApiPostRequest("", new claimingDetails { to = accountToken, amount = (int)naniteCount});
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            canAutoHarvest = true;
            loadingScreen.SetActive(false);
            co = automaticHarvestNaniteOre();
            StartCoroutine(co);
        }
        else
        {
            while(!request.isDone)
            {
                loadingScreen.SetActive(true);
                canAutoHarvest = false;
            }
            loadingScreen.SetActive(false);
            canAutoHarvest = true;
            naniteCount = 0;
            naniteCountTxt.text = naniteCount.ToString();
            co = automaticHarvestNaniteOre();
            StartCoroutine(co);
        }
    }
}
