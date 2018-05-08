﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComp : MonoBehaviour {

    private ConfigComp config;

    private PanelValueLifeComp panelValueLife;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            ++config.numLifes;

            panelValueLife.updateValueLifes(config.numLifes);

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        panelValueLife = GameObject.FindObjectOfType<PanelValueLifeComp>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
