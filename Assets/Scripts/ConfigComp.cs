using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo de espara antes de reiniciar o jogo")]
    [Range(1, 5)]
    public float tempoEspera = 2.0f;

    [SerializeField]
    [Tooltip("Numero inicial de vidas")]
    [Range(1, 5)]
    public float initLifes = 3;

    public float valueLifes = 0;

    [SerializeField]
    [Tooltip("Distancia relativa entre camera e o objeto")]
    public Vector3 cameraOffset = new Vector3(0, 3, -6);

    [SerializeField]
    [Tooltip("A velocidade que a bola ira se rolar lateralmente")]
    [Range(1, 10)]
    public float velocidadeLateral = 5.0f;
    
    [SerializeField]
    [Tooltip("A velocidade que a bola ira se rolar horizontalmente")]
    [Range(1, 10)]
    public float velocidadeHorizontal = 5.0f;


    [SerializeField]
    [Tooltip("Numero de tiles sem obstaculo")]
    [Range(1, 10)]
    public int numTileSemObstaculo = 2;

    [SerializeField]
    [Tooltip("Referencia tile basico")]
    [Range(10, 50)]
    public int numInicialSpawn = 10;

    [SerializeField]
    [Tooltip("Adciona bonus de vida")]
    public bool bonusLife = true;

    [SerializeField]
    [Tooltip("Numero de tiles sem bonus de vida")]
    [Range(5, 50)]
    public int numTileSemLife = 10;

    // Use this for initialization
    void Start()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
