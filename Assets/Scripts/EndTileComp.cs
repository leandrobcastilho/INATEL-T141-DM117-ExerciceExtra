using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTileComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo que o Tile permanece apos o objeto passar")]
    [Range(1, 10)]
    private float tempoVidaTile = 1.0f;

    private ConfigComp config;

    private PanelValueBonusComp panelValueBonus;

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        panelValueBonus = GameObject.FindObjectOfType<PanelValueBonusComp>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            if(config.activeBonus)
            {
                --config.numTilesWithBonus;

                if(config.numTilesWithBonus == 0)
                {
                    config.activeBonus = false;
                }

                panelValueBonus.updateValueBonus(config.numTilesWithBonus);
            }

            GameObject.FindObjectOfType<ControllerComp>().SpawnsProxTile();

            Destroy(transform.parent.gameObject, tempoVidaTile);
        }
    }
}
