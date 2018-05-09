using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusComp : MonoBehaviour {

    private ConfigComp config;

    private PanelValueBonusComp panelValueBonus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            config.activeBonus = true;

            config.numTilesWithBonus = config.bonusDuration;

            panelValueBonus.updateValueBonus(config.numTilesWithBonus);

            Destroy(gameObject);
        }
    }

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
}
