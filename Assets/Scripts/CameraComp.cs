using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour {

    private ControllerComp controller;

    [SerializeField]
    private Transform alvo;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.FindObjectOfType<ControllerComp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alvo != null)
        {
            transform.LookAt(alvo);
            transform.position = alvo.position + controller.camOffset;
        }

    }
}
