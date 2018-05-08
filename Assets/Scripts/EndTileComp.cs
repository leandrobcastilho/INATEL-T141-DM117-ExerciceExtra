﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTileComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo que o Tile permanece apos o objeto passar")]
    [Range(1, 10)]
    private float tempoVidaTile = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            GameObject.FindObjectOfType<ControllerComp>().SpawnsProxTile();

            Destroy(transform.parent.gameObject, tempoVidaTile);
        }
    }
}