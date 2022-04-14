using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public GameObject[] coinPoints;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.position = coinPoints[Random.Range(0, 5)].transform.position;
            GameObject.FindGameObjectWithTag("CoinsCounter").GetComponent<CoinsCounter>().CoinAdd();
        }
    }
}
