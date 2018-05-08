using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelValueLifeComp : MonoBehaviour {

    private Text myText;

    public void updateValueLifes(float numLifes)
    {
        myText.text = numLifes.ToString();
    }

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
