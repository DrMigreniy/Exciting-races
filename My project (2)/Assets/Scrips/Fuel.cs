using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public GameObject[] fuelPoints;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.position = fuelPoints[Random.Range(0, 4)].transform.position;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().takeFuel();
        }
    }
}
