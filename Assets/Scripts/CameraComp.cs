using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour {

    private ConfigComp config;

    [SerializeField]
    private Transform alvo;

    //private GameObject alvo;

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        //alvo = GameObject.Find("Bola");
    }

    // Update is called once per frame
    void Update()
    {
        if(alvo != null)
        {
            //transform.LookAt(alvo.transform);
            transform.LookAt(alvo);
            transform.position = alvo.position + config.camOffset;
        }

    }
}
