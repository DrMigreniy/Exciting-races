using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Shop : MonoBehaviour
{
    private int coins;

    public Image subaru;
    public Image chevrolette;

    public Image skyRoad;
    public Image spaceRoad;


    public Text playerMoney;


    public string carChoice;
    public string roadChoice;


    public Button secondCarChoice;
    public Button secondRoadChoice;

    private bool secondCarIsBlocked;
    private bool secondRoadIsBlocked;

    public GameObject firstPanel;
    public GameObject secondPanel;


    private void Start()
    {
        secondCarIsBlocked = Data.secondCarIsBlocked;
        secondRoadIsBlocked = Data.secondRoadIsBlocked;
        coins = Data.coins;
    }


    public void chooseFirstCar()
    {
        carChoice = "subaru";
    }


    public void chooseFirstRoad()
    {
        roadChoice = "sky";
    }

    public void chooseSecondCar()
    {
        if (secondCarIsBlocked && coins < 500)
            return;

        if (coins >= 500)
        {
            coins -= 500;
            secondCarIsBlocked = false;
        }
        carChoice = "chevrolette";
    }


    public void chooseSecondRoad()
    {
        if (secondRoadIsBlocked && coins < 500)
            return;

        if (coins >= 500)
        {
            coins -= 500;
            secondRoadIsBlocked = false;
        }
        roadChoice = "space";
    }


    public void confirm()
    {
        if (carChoice == null || roadChoice == null)
            return;

        if (carChoice == "subaru" && roadChoice == "sky")
            SceneManager.LoadScene(2);
        else if(carChoice == "subaru" && roadChoice == "space")
            SceneManager.LoadScene(3);
        else if (carChoice == "chevrolette" && roadChoice == "sky")
            SceneManager.LoadScene(4);
        else if (carChoice == "chevrolette" && roadChoice == "space")
            SceneManager.LoadScene(5);
    }


    private void Update()
    {
        playerMoney.text = "Coins: " + coins;

        if (secondCarIsBlocked && coins < 500)
            secondCarChoice.interactable = false;
        else if(!secondCarIsBlocked)
            secondCarChoice.interactable = true;
        else if(coins >= 500)
            secondCarChoice.interactable = true;

        if (secondRoadIsBlocked && coins < 500)
            secondRoadChoice.interactable = false;
        else if (!secondRoadIsBlocked)
            secondRoadChoice.interactable = true;
        else if(coins >= 500)
            secondRoadChoice.interactable = true;

        if (!secondCarIsBlocked)
            firstPanel.SetActive(false);
        else
            firstPanel.SetActive(true);
        if (!secondRoadIsBlocked)
            secondPanel.SetActive(false);
        else
            secondPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        Data.secondRoadIsBlocked = secondRoadIsBlocked;
        Data.secondCarIsBlocked = secondCarIsBlocked;
        Data.coins = coins;
    }


    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
