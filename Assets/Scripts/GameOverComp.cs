using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverComp : MonoBehaviour {

    private Text myText;

    public void Show()
    {
        if (myText != null)
            myText.text = "GAME OVER";
    }

    public void Hide()
    {
        if(myText != null)
            myText.text = "";
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
