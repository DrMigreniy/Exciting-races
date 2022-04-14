using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    public int coinsCount = 0;
    public Text text;

    private void Update()
    {
        text.text = "Coins: " + coinsCount;
    }

    public void CoinAdd()
    {
        coinsCount += 50;
    }

    public int getCoins()
    {
        return coinsCount;
    }
}
