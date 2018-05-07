using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComp : MonoBehaviour {

    private ConfigComp config;

    private PainelValueLifeComp painelValueLife;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BolaComp>())
        {
            ++config.valueLifes;

            painelValueLife.updateValueLifes(config.valueLifes);

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        painelValueLife = GameObject.FindObjectOfType<PainelValueLifeComp>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
