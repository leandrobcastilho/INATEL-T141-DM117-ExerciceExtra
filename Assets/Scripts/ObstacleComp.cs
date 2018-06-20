using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleComp : MonoBehaviour {

    private ControllerComp controller;

    [SerializeField]
    [Tooltip("Reference to explosion")]
    private GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<BallComp>())
        {
            if (explosion)
            {
                var particles = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(particles, 1.0f);
            }
            controller.DestroyObj(this.gameObject);

            if (!controller.activeBonus)
            {
                --controller.numLifes;

                controller.UpdateValueLifes();

                if (controller.numLifes <= 0)
                {
                    //Destroy(collision.gameObject);
                    //Invoke("Reset", controller.waitTime);
                    //controller.PauseGame(true);
                    collision.gameObject.SetActive(false);
                    controller.ResetGame(collision.gameObject);
                }
                //else
                //{
                //    (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                //}
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        controller = GameObject.FindObjectOfType<ControllerComp>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
