using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstatculoComp : MonoBehaviour {

    private ConfigComp config;

    private PainelValueLifeComp painelValueLife;

    private GameOverComp gameOver;

    private Rigidbody rb;

    /// <summary>
    /// Resetar a cena
    /// </summary>
    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BolaComp bolaComp = collision.gameObject.GetComponent<BolaComp>();
        if (bolaComp)
        {
            rb.AddForce(0, 0, bolaComp.resultVelocidadeHorizontal);

            --config.valueLifes;

            painelValueLife.updateValueLifes(config.valueLifes);

            if (config.valueLifes <= 0)
            {
                gameOver.Show();
                Destroy(collision.gameObject);
                Invoke("Reset", config.tempoEspera);
            }
            //print("lifes: "+ config.lifes);
        }
    }

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        painelValueLife = GameObject.FindObjectOfType<PainelValueLifeComp>();
        gameOver = GameObject.FindObjectOfType<GameOverComp>();

        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
