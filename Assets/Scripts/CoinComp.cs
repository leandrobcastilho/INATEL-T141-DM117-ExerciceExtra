using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComp : MonoBehaviour {

    private ConfigComp config;

    private PanelValueCoinComp panelValueCoin;

    private PanelValueLifeComp panelValueLife;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallComp>())
        {
            ++config.numCoins;

            if(config.numCoins == 10)
            {
                config.numCoins = 0;
                ++config.numLifes;
                panelValueLife.updateValueLifes(config.numLifes);
            }
            panelValueCoin.updateValueCoins(config.numCoins);

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        panelValueCoin = GameObject.FindObjectOfType<PanelValueCoinComp>();
        panelValueLife = GameObject.FindObjectOfType<PanelValueLifeComp>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
