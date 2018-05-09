using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Referencia tile basico")]
    private Transform tile;

    [SerializeField]
    [Tooltip("Referencia obstaculo")]
    private Transform obstacle;

    [SerializeField]
    [Tooltip("Referencia life")]
    private Transform life;

    [SerializeField]
    [Tooltip("Referencia coin")]
    private Transform coin;

    [SerializeField]
    [Tooltip("Referencia bonus")]
    private Transform bonus;

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

    private ConfigComp config;

    private PanelValueLifeComp painelValueLife;

    private GameOverComp gameOver;

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        painelValueLife = GameObject.FindObjectOfType<PanelValueLifeComp>();
        gameOver = GameObject.FindObjectOfType<GameOverComp>();

        gameOver.Hide();

        //preparando o ponto inicial
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        config.numLifes = config.initialLifes;
        painelValueLife.updateValueLifes(config.numLifes);
        for (int i = 0; i < config.numInitialSpawn; i++)
        {
            SpawnsProxTile( i >= config.numTileWithoutObstacle, i >= config.numTileWithoutLife);
        }
    }

    public void SpawnsProxTile(bool spawnObstacle = true, bool spawnLife = true, bool spawnCoin = true)
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

        if (spawnLife && config.bonusLife )
        {
            AddLife(novoTile);
        }

        if (spawnCoin)
        {
            AddCoin(novoTile);
        }

        ++config.numTilesWithoutBonus;
        bool addBonus = false;
        if (config.numTilesWithoutBonus == config.numTilesBetweenBonus)
        {
            addBonus = true;
            config.numTilesWithoutBonus = 0;
        }

        if (addBonus)
        {
            AddBonus(novoTile);
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
            var pontoObstacle = pontosObstacles[Random.Range(0, pontosObstacles.Count)];

            var pontoSpawnPos = pontoObstacle.transform.position;

            var novoObstacle = Instantiate(obstacle, pontoSpawnPos, Quaternion.identity);

            novoObstacle.SetParent(pontoObstacle.transform);
        }
    }

    private void AddLife(Transform novoTile)
    {
        int rand = Random.Range(0, 30);
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
                var pontoLife = pontosLifes[Random.Range(0, pontosLifes.Count)];

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
            var pontoCoin = pontosCoins[Random.Range(0, pontosCoins.Count)];

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
            var pontoBonus = pontosBonus[Random.Range(0, pontosBonus.Count)];

            var pontoSpawnPos = pontoBonus.transform.position;

            var novoBonus = Instantiate(bonus, pontoSpawnPos, Quaternion.identity);

            novoBonus.SetParent(pontoBonus.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
