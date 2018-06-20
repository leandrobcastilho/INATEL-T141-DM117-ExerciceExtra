using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTileComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo que o Tile permanece apos o objeto passar")]
    [Range(1, 10)]
    private float tempoVidaTile = 1.0f;

    private ControllerComp controller;
    
    // Use this for initialization
    void Start()
    {
        controller = GameObject.FindObjectOfType<ControllerComp>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            if(controller.activeBonus)
            {
                --controller.numTilesWithBonus;

                if(controller.numTilesWithBonus == 0)
                {
                    controller.activeBonus = false;
                }

                controller.UpdateValueBonus();
            }

            controller.SpawnsProxTile();

            Destroy(transform.parent.gameObject, tempoVidaTile);
        }
    }
}
