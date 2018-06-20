using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComp : MonoBehaviour {

    private ControllerComp controller;

    public float resultVelocidadeHorizontal;

    private float resultVelocidadeLateral;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.FindObjectOfType<ControllerComp>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if( Input.GetMouseButton(0))
        {
            
            float direcaoX = CalculaMovimento(Input.mousePosition);
            resultVelocidadeLateral = direcaoX * controller.lateralSpeed;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch toque = Input.touches[0];
                float direcaoX = CalculaMovimento(toque.position);
                resultVelocidadeLateral = direcaoX * controller.lateralSpeed;

            }
            else
            {
                var estimuloHorizontal = Input.GetAxis("Horizontal");
                var estimuloVertical = Input.GetAxis("Vertical");
                resultVelocidadeLateral = estimuloHorizontal * controller.lateralSpeed;
                resultVelocidadeHorizontal = controller.horizontalSpeed + estimuloVertical * controller.horizontalSpeed;
            }
        }

        rb.AddForce(resultVelocidadeLateral, 0, resultVelocidadeHorizontal);
    }

    private float CalculaMovimento(Vector2 posScreenSpace)
    {
        float direcaoX = 0;
        var posClick = Camera.main.ScreenToViewportPoint(posScreenSpace);
        var posBola = transform.position;
        //if (posClick.x > posBola.x)
        //{
        //
        //}
        if (posClick.x < 0.5)
        {
            direcaoX = -1;
        }
        else
        {
            direcaoX = 1;
        }
        return direcaoX;
    }
}
