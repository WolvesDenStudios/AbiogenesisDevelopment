using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loginScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loginContainer;

    void Awake()
    {
        loginContainer.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(showLoginContainer());
    }

    IEnumerator showLoginContainer()
    {
        yield return new WaitForSeconds(0.1f);
        loginContainer.SetActive(true);
    }
}
