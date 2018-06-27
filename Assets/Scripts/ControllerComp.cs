using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerComp : MonoBehaviour {

    [Header("Game Config")]

    [SerializeField]
    [Tooltip("Initial number of lives")]
    [Range(1, 5)]
    public float initialLifes = 3;

    [SerializeField]
    [Tooltip("Basic Tile Reference")]
    private Transform tile;

    [SerializeField]
    [Tooltip("Obstacle Reference")]
    private Transform obstacle;

    [SerializeField]
    [Tooltip("Life Reference")]
    private Transform life;

    [SerializeField]
    [Tooltip("Coin Reference")]
    private Transform coin;

    [SerializeField]
    [Tooltip("Bonus Reference")]
    private Transform bonus;


    [Header("Camera Config")]

    [SerializeField]
    [Tooltip("Relative distance between camera and object")]
    public Vector3 camOffset = new Vector3(0, 3, -6);




    [Header("Ball Config")]

    [SerializeField]
    [Tooltip("The speed that the ball will roll laterally")]
    [Range(1, 10)]
    public float lateralSpeed = 5.0f;

    [SerializeField]
    [Tooltip("The speed that the ball will roll horizontally")]
    [Range(1, 10)]
    public float horizontalSpeed = 5.0f;




    [Header("Game Environment Config")]

    [SerializeField]
    [Tooltip("Waiting time before starting the game")]
    [Range(1, 5)]
    public float waitTime = 2.0f;

    [SerializeField]
    [Tooltip("Number of tiles initial")]
    [Range(10, 50)]
    public int numInitialSpawn = 10;




    [Header("Life Config")]

    [SerializeField]
    [Tooltip("Add bonus life")]
    public bool bonusLife = true;

    [SerializeField]
    [Tooltip("Number of tiles with no life bonus")]
    [Range(5, 50)]
    public int numTileWithoutLife = 15;




    [Header("Obstacle Config")]

    [SerializeField]
    [Tooltip("Number of tiles without obstacle")]
    [Range(1, 10)]
    public int numTileWithoutObstacle = 2;



    [Header("Coins Config")]

    [SerializeField]
    [Tooltip("Number of tiles without coin")]
    [Range(5, 50)]
    public int numTileWithoutCoins = 5;

    [SerializeField]
    [Tooltip("Number of coins to earn a living")]
    [Range(5, 50)]
    public int numCoinToEarnLife = 10;




    [Header("Bonus Config")]

    [SerializeField]
    [Tooltip("Number of tiles between the bonus")]
    [Range(10, 50)]
    public int numTilesBetweenBonus = 10;

    [SerializeField]
    [Tooltip("Number of tiles without the bonus")]
    [Range(10, 50)]
    public float numTilesWithoutBonus = 10;

    private float countTilesWithoutBonus = 0;

    [SerializeField]
    [Tooltip("Bonus Duration")]
    [Range(3, 10)]
    public int bonusDuration = 10;



    [Header("Current Info")]

    public float numLifes = 0;

    public float numCoins = 0;

    public float numTilesWithBonus = 0;

    public bool activeBonus = false;

    public static bool paused;
    
    /// <summary>
    /// ponto inicial primeiro tile
    /// </summary>
    private Vector3 pontoInicial = new Vector3(0, 0, 5);

    /// <summary>
    /// posição inicial do proximo tile
    /// </summary>
    private Vector3 proxTilePos;

    /// <summary>
    /// Rotação do proximo tile
    /// </summary>
    private Quaternion proxTileRot;

    private GameObject ball;

    [Header("References")]

    [Header("PauseMenuPainel")]
    [SerializeField]
    [Tooltip("PauseMenuPainel Reference")]
    public GameObject pauseMenuPainel;

    [Header("GameOverMenuPainel")]
    [SerializeField]
    [Tooltip("GameOverMenuPainel Reference")]
    public GameObject gameOverMenuPainel;

    [Header("InfoPanel")]
    [SerializeField]
    [Tooltip("InfoPanel Reference")]
    public GameObject infoPanel;

    private Text InfoLifeValue;
    private Text InfoCoinValue;
    private Text InfoBonusValue;

    // Use this for initialization
    void Start()
    {
        //preparando o ponto inicial
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        paused = false;

        InfoLifeValue = GameObject.FindGameObjectWithTag("InfoLifeValue").GetComponent<Text>();
        InfoCoinValue = GameObject.FindGameObjectWithTag("InfoCoinValue").GetComponent<Text>();
        InfoBonusValue = GameObject.FindGameObjectWithTag("InfoBonusValue").GetComponent<Text>();

        InitializeTiles();

        numLifes = initialLifes;

        UpdateValueLifes();
        UpdateValueCoins();
        UpdateValueBonus();
     
    }
  
    // Update is called once per frame
    void Update()
    {

    }
    
    public void UpdateValueLifes()
    {
        InfoLifeValue.text = numLifes.ToString();
    }

    public void UpdateValueCoins()
    {
        InfoCoinValue.text = numCoins.ToString();
    }

    public void UpdateValueBonus()
    {
        InfoBonusValue.text = numTilesWithBonus.ToString();
    }

    /// <summary>
    /// Metodo para pausar o jogo
    /// </summary>
    /// <param name="isPaused"></param>
    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;
        PauseGame(paused);
        //Habilita/Desabilita o menu pause
        pauseMenuPainel.SetActive(paused);
    }


    public static void PauseGame(bool isPaused)
    {
        paused = isPaused;
        //Se o jogo estiver paused, timescale recebe 0
        Time.timeScale = (isPaused) ? 0 : 1;
    }




    public void Restart()
    {
        PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneByName(string nameScene)
    {
        PauseGame(false);
        SceneManager.LoadScene(nameScene);
    }



    //////////////////////////////////////////////////////////////////////

    public void DestroyObj(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void ResetGame(GameObject goBall)
    {
        ball = goBall;

        gameOverMenuPainel.SetActive(true);

        var buttons = gameOverMenuPainel.transform.GetComponentsInChildren<Button>();

        Button continueButton = null;
        foreach (var button in buttons)
        {
            if (button.name.Equals("ContinueButton"))
            {
                continueButton = button;
                break;
            }
        }

        if (continueButton != null)
        {
#if UNITY_ADS
            //Se o button continue for clicado, iremos tocar o anúncio
            StartCoroutine(ShowContinue(continueButton));
            //buttonContinue.onClick.AddListener(UnityAdControler.ShowRewardAd);
#else
            //Se nao existe add, nao precisa mostrar o botao Continue
            continueButton.gameObject.SetActive(false);
#endif
        }

    }

    /// <summary>
    /// Metodo para reiniciar o jogo
    /// </summary>
    private void Reset()
    {
        paused = false;
        //Reinicia o level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    /// <summary>
    /// Faz o reset do jogo
    /// </summary>
    public void Continue()
    {
        gameOverMenuPainel.SetActive(false);
        if(ball)
            ball.SetActive(true);
        numLifes = 1;
        UpdateValueLifes();
    }

    public IEnumerator ShowContinue(Button continueButton)
    {
        var btnText = continueButton.GetComponentInChildren<Text>();
        while (true)
        {
            if (UnityAdControler.nextTimeReward.HasValue && (DateTime.Now < UnityAdControler.nextTimeReward.Value))
            {
                continueButton.interactable = false;

                TimeSpan restante = UnityAdControler.nextTimeReward.Value - DateTime.Now;

                var contagemRegressiva = string.Format("{0:D2}:{1:D2}", restante.Minutes, restante.Seconds);
                btnText.text = contagemRegressiva;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                continueButton.interactable = true;
                continueButton.onClick.AddListener(UnityAdControler.ShowRewardAd);
                btnText.text = "Continue (Ver Ad)";
                break;
            }
        }
    }

    public void InitializeTiles()
    {
        for (int i = 0; i < numInitialSpawn; i++)
        {
            SpawnsProxTile(i >= numTileWithoutObstacle, i >= numTileWithoutLife, i >= numTileWithoutCoins, i >= numTilesWithoutBonus);
        }
    }

    public void SpawnsProxTile(bool spawnObstacle = true, bool spawnLife = true, bool spawnCoin = true, bool spawnBonus = true)
    {
        var novoTile = Instantiate(tile, proxTilePos, proxTileRot);
        //detectar qual a posição do proximo
        var proxTile = novoTile.Find("SpotSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

        if (spawnObstacle)
        {
            AddObstacle(novoTile);
        }

        if (spawnLife && bonusLife)
        {
            AddLife(novoTile);
        }

        if (spawnCoin)
        {
            AddCoin(novoTile);
        }

        if (spawnBonus)
        {
            bool addBonus = false;
            ++countTilesWithoutBonus;
            if (countTilesWithoutBonus == numTilesWithoutBonus)
            {
                addBonus = true;
                countTilesWithoutBonus = 0;
            }
            if (addBonus)
            {
                AddBonus(novoTile);
            }
        }

    }

    private void AddObstacle(Transform novoTile)
    {
        var pontosObstacles = new List<GameObject>();

        foreach (Transform filho in novoTile)
        {
            if (filho.CompareTag("Obstacle"))
            {
                pontosObstacles.Add(filho.gameObject);
            }
        }

        if (pontosObstacles.Count > 0)
        {
            var pontoObstacle = pontosObstacles[UnityEngine.Random.Range(0, pontosObstacles.Count)];

            var pontoSpawnPos = pontoObstacle.transform.position;

            var novoObstacle = Instantiate(obstacle, pontoSpawnPos, Quaternion.identity);

            novoObstacle.SetParent(pontoObstacle.transform);
        }
    }

    private void AddLife(Transform novoTile)
    {
        int rand = UnityEngine.Random.Range(0, 30);
        //print("rand: " + rand);
        if (rand > 25)
        {
            var pontosLifes = new List<GameObject>();

            foreach (Transform filho in novoTile)
            {
                if (filho.CompareTag("Life"))
                {
                    pontosLifes.Add(filho.gameObject);
                }
            }

            if (pontosLifes.Count > 0)
            {
                var pontoLife = pontosLifes[UnityEngine.Random.Range(0, pontosLifes.Count)];

                var pontoSpawnPos = pontoLife.transform.position;

                //print("ADD novoLife: ");
                var novoLife = Instantiate(life, pontoSpawnPos, Quaternion.identity);

                novoLife.SetParent(pontoLife.transform);
            }
        }
    }

    private void AddCoin(Transform novoTile)
    {
        var pontosCoins = new List<GameObject>();

        foreach (Transform filho in novoTile)
        {
            if (filho.CompareTag("Coin"))
            {
                pontosCoins.Add(filho.gameObject);
            }
        }

        if (pontosCoins.Count > 0)
        {
            var pontoCoin = pontosCoins[UnityEngine.Random.Range(0, pontosCoins.Count)];

            var pontoSpawnPos = pontoCoin.transform.position;

            var novoCoin = Instantiate(coin, pontoSpawnPos, Quaternion.identity);

            novoCoin.SetParent(pontoCoin.transform);
        }
    }

    private void AddBonus(Transform novoTile)
    {
        var pontosBonus = new List<GameObject>();

        foreach (Transform filho in novoTile)
        {
            if (filho.CompareTag("Bonus"))
            {
                pontosBonus.Add(filho.gameObject);
            }
        }

        if (pontosBonus.Count > 0)
        {
            var pontoBonus = pontosBonus[UnityEngine.Random.Range(0, pontosBonus.Count)];

            var pontoSpawnPos = pontoBonus.transform.position;

            var novoBonus = Instantiate(bonus, pontoSpawnPos, Quaternion.identity);

            novoBonus.SetParent(pontoBonus.transform);
        }
    }

}
