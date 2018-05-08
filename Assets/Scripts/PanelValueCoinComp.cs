using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelValueCoinComp : MonoBehaviour {

    private Text myText;

    public void updateValueCoins(float numCoins)
    {
        myText.text = numCoins.ToString();
    }

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
