using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleComp : MonoBehaviour {

    private ConfigComp config;

    private PanelValueLifeComp painelValueLife;

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
        BallComp bolaComp = collision.gameObject.GetComponent<BallComp>();
        if (bolaComp)
        {
            rb.AddForce(0, 0, bolaComp.resultVelocidadeHorizontal);

            --config.numLifes;

            painelValueLife.updateValueLifes(config.numLifes);

            if (config.numLifes <= 0)
            {
                gameOver.Show();
                Destroy(collision.gameObject);
                Invoke("Reset", config.waitTime);
            }
            else
            {
                (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
            }

            //print("lifes: "+ config.lifes);
        }
    }

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        painelValueLife = GameObject.FindObjectOfType<PanelValueLifeComp>();
        gameOver = GameObject.FindObjectOfType<GameOverComp>();

        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
