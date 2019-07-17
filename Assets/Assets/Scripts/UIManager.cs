using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject GameOverPanel;
    public GameObject PowerUpText;

    public Text scoreText;
    public Text coinText;
    public Text CoinAText;
    public Text CoinBText;
    public Text CoinCText;
    public Text CoinDText;
    public Text CoinEText;

    public Text tutorialText;

    public Image EyeImage;
    
    public void UpdateScore(int score){
        scoreText.text = score.ToString();    
    }

    public void UpdateCoins(int coin) {
        coinText.text = coin.ToString();
    }

    public void UpdateValueCoins(int value) {
        CoinAText.gameObject.SetActive(false);
        CoinBText.gameObject.SetActive(false);
        CoinCText.gameObject.SetActive(false);
        CoinDText.gameObject.SetActive(false);
        CoinEText.gameObject.SetActive(false);
        switch (value)
        {
            case 10:
                CoinAText.gameObject.SetActive(true);;
                break;
            case 8:
                CoinBText.gameObject.SetActive(true);;
                break;
            case 6:
                CoinCText.gameObject.SetActive(true);;
                break;
            case 12:
                CoinDText.gameObject.SetActive(true);;
                break;
            case 20:
                CoinEText.gameObject.SetActive(true);;
                break; 
        }
    }

    public void UpdateTutorial(int n) {
        switch (n)
        {
            case 1:
                tutorialText.text = "You're a student\nTo survive you'll need to not sleep";
                break;
            case 2:
                tutorialText.text = "The square is your sleep level";
                break;
            case 3:
                tutorialText.text = "Gray means you're sleepy";
                break;
            case 4:
                tutorialText.text = "To not sleep you must collect coffee";
                break;
            case 5:
                tutorialText.text = "This is the coffee";
                break;
            case 6:
                tutorialText.text = "Red means to awake";
                break;
            case 7:
                tutorialText.text = "You should avoid collect more";
                break;
            case 8:
                tutorialText.text = "The grades are worth coins";
                break;
            case 9:
                tutorialText.text = "This is 10";
                break;
            case 10:
                tutorialText.text = "This is 8";
                break;
            case 11:
                tutorialText.text = "This is 6";
                break;
            case 12:
                tutorialText.text = "This is -12";
                break;
            case 13:
                tutorialText.text = "This is -20";
                break; 
            case 14:
                tutorialText.text ="You must avois obstacles";
                break;
            case 15:
                tutorialText.text = "Jump";
                break;
            case 16:
                tutorialText.text = "Slide";
                break;
            case 17:
                tutorialText.text = "Go to a different lane";
                break;
            case 18:
                tutorialText.text = "That's all folks!";
                break;
            case 19:
                tutorialText.text = " ";
                break;
        }
    }

    public void UpdateEye(int eye) {
        if (eye == 0)
        {
            EyeImage.color = Color.black;
        }
        else if (eye > 0 && eye <= 7)
        {
            EyeImage.color = Color.gray;
        }
        else if (eye > 7 && eye <= 12)
        {
            EyeImage.color = Color.green;
        }
        else if (eye > 12 && eye <= 17)
        {
            EyeImage.color = Color.white;
        }
        else if (eye > 17 && eye <= 22)
        {
            EyeImage.color = Color.magenta;
        }
        else {
            EyeImage.color = Color.red;
        }
    
    }
}
