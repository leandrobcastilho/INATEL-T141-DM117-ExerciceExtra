using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComp : MonoBehaviour {

    private ControllerComp controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            ++controller.numLifes;

            controller.UpdateValueLifes();

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
