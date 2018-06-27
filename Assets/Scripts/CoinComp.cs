using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComp : MonoBehaviour {

    private ControllerComp controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            ++controller.numCoins;

            if(controller.numCoins == 10)
            {
                controller.numCoins = 0;
                ++controller.numLifes;
                controller.UpdateValueLifes();
            }
            controller.UpdateValueCoins();

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        controller = GameObject.FindObjectOfType<ControllerComp>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
