using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Referencia tile basico")]
    private Transform tile;

    [SerializeField]
    [Tooltip("Referencia obstaculo")]
    private Transform obstaculo;

    [SerializeField]
    [Tooltip("Referencia life")]
    private Transform life;

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

    private PainelValueLifeComp painelValueLife;

    private GameOverComp gameOver;

    // Use this for initialization
    void Start()
    {
        config = GameObject.FindObjectOfType<ConfigComp>();
        painelValueLife = GameObject.FindObjectOfType<PainelValueLifeComp>();
        gameOver = GameObject.FindObjectOfType<GameOverComp>();

        gameOver.Hide();

        //preparando o ponto inicial
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        config.valueLifes = config.initLifes;
        painelValueLife.updateValueLifes(config.valueLifes);
        for (int i = 0; i < config.numInicialSpawn; i++)
        {
            SpawnsProxTile( i >= config.numTileSemObstaculo, i >= config.numTileSemLife);
        }
    }

    public void SpawnsProxTile(bool spawnObstatuclo = true, bool spawnLife = true)
    {
        var novoTile = Instantiate(tile, proxTilePos, proxTileRot);
        //detectar qual a posição do proximo
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

        if (spawnObstatuclo)
        {
            var pontosObstaculos = new List<GameObject>();

            foreach(Transform filho in novoTile)
            {
                if( filho.CompareTag("Obstaculo"))
                {
                    pontosObstaculos.Add(filho.gameObject);
                }
            }

            if (pontosObstaculos.Count > 0)
            {
                var pontoObstaculo = pontosObstaculos[Random.Range(0, pontosObstaculos.Count)];

                var pontoSpawnPos = pontoObstaculo.transform.position;

                var novoObstaculo = Instantiate(obstaculo, pontoSpawnPos, Quaternion.identity);

                novoObstaculo.SetParent(pontoObstaculo.transform);
            }
        }

        //print("spawnLife: " + spawnLife);
        //print("bonusLife: " + config.bonusLife);
        if (spawnLife && config.bonusLife )
        {
            int rand = Random.Range(0, 10);
            //print("rand: " + rand);
            if (rand > 4)
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
