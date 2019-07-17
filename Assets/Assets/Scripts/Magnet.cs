/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

    private Player player;
    public float speed;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	public void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            book = true;
            bookStart = transform.position.z;
            other.transform.parent.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Friend"))
        {
            magnet = true;
            magnetStart = transform.position.z;
            other.transform.parent.gameObject.SetActive(false);
            Magnet.Update();
        }

        if (other.CompareTag("Coffee"))
        {
            eye += 5;
            speed += 2;
            other.transform.parent.gameObject.SetActive(false);
        }

        //Colisão para coletar a moeda
        if (other.CompareTag("CoinA"))
        {
            coins = coins + 10;
            UIManager.UpdateCoins(coins);
            UIManager.UpdateValueCoins(10);
            other.transform.parent.gameObject.SetActive(false);
        }
        else if (other.CompareTag("CoinB"))
        {
            coins = coins + 8;
            UIManager.UpdateCoins(coins);
            UIManager.UpdateValueCoins(8);
            other.transform.parent.gameObject.SetActive(false);

        }
        else if (other.CompareTag("CoinC"))
        {
            coins = coins + 6;
            UIManager.UpdateCoins(coins);
            UIManager.UpdateValueCoins(6);
            other.transform.parent.gameObject.SetActive(false);
        }
        else if (other.CompareTag("CoinD"))
        {
            coins = coins - 12;
            if (coins < 0)
            {
                coins = 0;
            }
            UIManager.UpdateCoins(coins);
            UIManager.UpdateValueCoins(12);
            other.transform.parent.gameObject.SetActive(false);
        }
        else if (other.CompareTag("CoinE"))
        {
            coins = coins - 20;
            if (coins < 0)
            {
                coins = 0;
            }
            UIManager.UpdateCoins(coins);
            UIManager.UpdateValueCoins(20);
            other.transform.parent.gameObject.SetActive(false);
        }

        //Colisão com obstaculos
        if (other.CompareTag("Obstacle1"))
        {
            speed = 0;
            currentLife = currentLife - 2;
            GameOver();
        }
        else if (other.CompareTag("Obstacle2"))
        {
            speed = 0;
            amin.SetTrigger("Hit");
            currentLife--;
            other.transform.gameObject.SetActive(false);
            if (currentLife <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(Blinking(lifeTime));
            }
            //Check se está vivo
        } //END COLISAO OBSTACULO

    } //END ONTRIGGERENTER
}
*/ 