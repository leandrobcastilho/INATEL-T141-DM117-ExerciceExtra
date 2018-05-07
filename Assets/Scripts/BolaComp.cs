using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaComp : MonoBehaviour {

    private ConfigComp config;

    public float resultVelocidadeHorizontal;

    private float resultVelocidadeLateral;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if( Input.GetMouseButton(0))
        {
            
            float direcaoX = calculaMovimento(Input.mousePosition);
            resultVelocidadeLateral = direcaoX * config.velocidadeLateral;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch toque = Input.touches[0];
                float direcaoX = calculaMovimento(toque.position);
                resultVelocidadeLateral = direcaoX * config.velocidadeLateral;

            }
            else
            {
                var estimuloHorizontal = Input.GetAxis("Horizontal");
                var estimuloVertical = Input.GetAxis("Vertical");
                resultVelocidadeLateral = estimuloHorizontal * config.velocidadeLateral;
                resultVelocidadeHorizontal = config.velocidadeHorizontal + estimuloVertical * config.velocidadeHorizontal;
            }
        }
        


        rb.AddForce(resultVelocidadeLateral, 0, resultVelocidadeHorizontal);
    }

    private float calculaMovimento(Vector2 posScreenSpace)
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
