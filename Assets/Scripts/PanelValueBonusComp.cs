using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelValueBonusComp : MonoBehaviour {

    private Text myText;

    public void updateValueBonus(float numBonus)
    {
        myText.text = numBonus.ToString();
    }

    // Use this for initialization
    void Start () {
        myText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
