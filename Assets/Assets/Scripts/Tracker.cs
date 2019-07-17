using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour {

    //Obstacles
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public ChangeLane changeLane;
        
    public List<GameObject> newObstacles;

    //Coin
    public GameObject[] coins;
    public Vector2 numberOfCoins;

    public List<GameObject> newCoins;

    //Coffee  
    public GameObject coffee;
    public Vector2 numberOfCoffee;

    public List<GameObject> newCoffee;
    
    //PowerUp
    public GameObject[] PowerUp;
    public Vector2 numberOfPowerUp;

    public List<GameObject> newPowerUp;

	// Use this for initialization
	void Start () {

        //Instanciar a quantidade de obstaculos na pista
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);

        for (int i = 0; i < newNumberOfObstacles; i++){
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }

        PositionateObstacles();

        //Instanciar a quantidade de coins na pista
        int newNumberOfCoins = (int)Random.Range(numberOfCoins.x, numberOfCoins.y);

        for (int i = 0; i < newNumberOfCoins; i++) {
            newCoins.Add(Instantiate(coins[Random.Range(0, coins.Length)], transform));
            newCoins[i].SetActive(false);
        }

        PositionateCoins();

        //Instanciar a quantidade de café na pista
        int newNumberOfCoffee = (int)Random.Range(numberOfCoffee.x, numberOfCoffee.y);

        for (int i = 0; i < newNumberOfCoffee; i++)
        {
            newCoffee.Add(Instantiate(coffee, transform));
            newCoffee[i].SetActive(false);
        }

        PositionateCoffee();

        //Instanciar a quantidade de livros na pista
        int newNumberOfPowerUp = (int)Random.Range(numberOfPowerUp.x, numberOfPowerUp.y);

        for (int i = 0; i < newNumberOfPowerUp; i++)
        {
            newPowerUp.Add(Instantiate(PowerUp[Random.Range(0, PowerUp.Length)], transform));
            newPowerUp[i].SetActive(false);
        }

        PositionatePowerUp();
	}

    // Function for positioning obstacles
    void PositionateObstacles()
    {
        for (int i = 0; i < newObstacles.Count; i++ )
        {
            float posZMin = (200f / newObstacles.Count) + (200f / newObstacles.Count) * i ;
            float posZMax = (200f / newObstacles.Count) + (200f / newObstacles.Count) * i + 1 ;
            //Mesma posição em X
            newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
            newObstacles[i].SetActive(true);
 
            //Diferente posição em X
            if(newObstacles[i].GetComponent<ChangeLane>() != null){
                newObstacles[i].GetComponent<ChangeLane>().PositionLane();
            }
        }  
	}

    //Função para posicionar as moedas
    void PositionateCoins() {

        //menor posição em Z 
        float minZPos = 20f;

        //loop para posicionar
        for (int i = 0; i < newCoins.Count; i++)
        {
            //Maior posição em Z
            float maxZPos = minZPos + 20f;
            //Aleatorio entre a menor e a mioor posição em Z
            float randomZPos = Random.Range(minZPos, maxZPos);
            
            newCoins[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZPos);
            newCoins[i].GetComponent<ChangeLane>().PositionLane();

            minZPos = randomZPos + 5;
            newCoins[i].SetActive(true);

        }
    
    }
      
    //Função para posicionar o café
    void PositionateCoffee() {
        //menor posição em Z 
        float ZPosmin = 20f;

        //loop para posicionar
        for (int i = 0; i < newCoffee.Count; i++)
        {
            //Maior posição em Z
            float ZPosmax = ZPosmin + 20f;
            //Aleatorio entre a menor e a mior posição em Z
            float random = Random.Range(ZPosmin, ZPosmax);
            newCoffee[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y,random );
            newCoffee[i].GetComponent<ChangeLane>().PositionLane();

            ZPosmin = random + 20;
            newCoffee[i].SetActive(true);
        }
    
    }

    //Função para posicionar o livro
    void PositionatePowerUp()
    {
        //menor posição em Z 
        float Posmin = 50f;

        //loop para posicionar
        for (int i = 0; i < newPowerUp.Count; i++)
        {
            //Maior posição em Z
            float Posmax = Posmin + 100f;
            //Aleatorio entre a menor e a mior posição em Z
            float random = Random.Range(Posmin, Posmax);

            newPowerUp[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, random);
            newPowerUp[i].GetComponent<ChangeLane>().PositionLane();

            Posmin = random + 100;
            newPowerUp[i].SetActive(true);
        }

    }

    // Function to allocate the map
    private void OnTriggerEnter(Collider other)
    {
        // Map End Check
        if (other.CompareTag("Player")){
            transform.position = new Vector3(0,0,transform.position.z + 199*2);
            Invoke("PositionateObstacles", 5f);
            Invoke("PositionateCoins", 5f);
            Invoke("PositionateCoffee", 5f);

            //Aumentar a velocidade
            other.GetComponent<Player>().Speed();
        }
    
    }
}