using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ERC20BalanceOfExample : MonoBehaviour
{

    async void Start()
    {
        string chain = "emerald";
        string network = "testnet";
        string contract = "0x0C5064242763bC1A3a3ab97945ed7c9682E47770";
        string account = "0x94a56B5067C19490447ac2De321467465449673a";

        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contract, account);
        // print(balanceOf); 
        GetComponent<Text>().text = (balanceOf / 18).ToString();
    }
}
