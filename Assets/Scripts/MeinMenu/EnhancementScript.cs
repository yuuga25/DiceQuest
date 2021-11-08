using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnhancementScript : MonoBehaviour
{
    [SerializeField]
    private Text playerStatusText;

    [SerializeField]
    private Text playerAtkText;
    [SerializeField]
    private Text playerDefText;
    [SerializeField]
    private Text UpAtkText;
    [SerializeField]
    private Text UpDefText;
    [SerializeField]
    private Text costAtkText;
    [SerializeField]
    private Text costDefText;
    [SerializeField]
    private Text AtkRemainingText;
    [SerializeField]
    private Text DefRemainingText;
    [SerializeField]
    private Text playerMinDiceText;
    [SerializeField]
    private Text playerMaxDiceText;

    [SerializeField]
    private Text costText;

    [SerializeField]
    private GameObject quesOb;

    private int atkCost;
    private int defCost;
    private int diceCost;

    private int minDiceCount;
    private int maxDiceCount;

    public static int UpAtkCount = 5;
    public static int UpDefCount = 5;

    private void Start()
    {
        quesOb.SetActive(false);
    }

    void Update()
    {
        playerStatusText.text = $"Rank: {playerStatusScript.playerRank} /<color=#FFDC86> money: {playerStatusScript.playerMoney}</color>";
        playerAtkText.text = $"{playerStatusScript.playerAtk}";
        playerDefText.text = $"{playerStatusScript.playerDef}";
        AtkRemainingText.text = $"残り：{UpAtkCount}";
        DefRemainingText.text = $"残り：{UpDefCount}";
        playerMinDiceText.text = $"{playerStatusScript.minDice + minDiceCount}";
        playerMaxDiceText.text = $"{playerStatusScript.maxDice + maxDiceCount}";
        UpAtkText.text = $"+2";
        UpDefText.text = $"+1";
        costAtkText.text = $"money:<color=#FFDC86> {atkCost}</color>";
        costDefText.text = $"money:<color=#FFDC86> {defCost}</color>";
        costText.text = $"money:<color=#FFDC86> {diceCost}</color>";
        switch (playerStatusScript.playerRank)
        {
            case 1:
                atkCost = 500;
                defCost = 800;
                break;
            case 2:
                atkCost = 550;
                defCost = 900;
                break;
            case 3:
                atkCost = 600;
                defCost = 1000;
                break;
            case 4:
                atkCost = 650;
                defCost = 1100;
                break;
            case 5:
                atkCost = 700;
                defCost = 1200;
                break;
            case 6:
                atkCost = 750;
                defCost = 1300;
                break;
            case 7:
                atkCost = 800;
                defCost = 1400;
                break;
            case 8:
                atkCost = 850;
                defCost = 1500;
                break;
            case 9:
                atkCost = 900;
                defCost = 1600;
                break;
            case 10:
                atkCost = 950;
                defCost = 1700;
                break;
        }
    }

    public void UpAtk()
    {
        if (playerStatusScript.playerMoney >= atkCost && UpAtkCount > 0)
        {
            playerStatusScript.playerAtk += 2;
            playerStatusScript.playerMoney -= atkCost;
            UpAtkCount--;
        }
    }
    public void UpDef()
    {
        if (playerStatusScript.playerMoney >= defCost && UpDefCount > 0)
        {
            playerStatusScript.playerDef += 1;
            playerStatusScript.playerMoney -= defCost;
            UpDefCount--;
        }
    }
    public void minDice(int Arrow)
    {
        //上昇
        if(playerStatusScript.minDice + minDiceCount < playerStatusScript.maxDice + maxDiceCount && Arrow == 1)
        {
            minDiceCount++;
            if (minDiceCount >= 0 && playerStatusScript.minDice + minDiceCount > playerStatusScript.minDice)
            {
                diceCost += 1000;
            }
        }
        //下降
        if (playerStatusScript.minDice + minDiceCount > 0 && Arrow == 2)
        {
            minDiceCount--;
            if (minDiceCount >= 0 && playerStatusScript.minDice + minDiceCount >= playerStatusScript.minDice)
            {
                diceCost -= 1000;
            }
        }
    }
    public void maxDice(int Arrow)
    {
        //上昇
        if (playerStatusScript.maxDice + maxDiceCount < 100 && Arrow == 1)
        {
            maxDiceCount++;
            if (maxDiceCount >= 0 && playerStatusScript.maxDice + maxDiceCount > playerStatusScript.maxDice)
            {
                diceCost += 1000;
            }
        }
        //下降
        if (playerStatusScript.maxDice + maxDiceCount > playerStatusScript.minDice + minDiceCount && playerStatusScript.maxDice + maxDiceCount > 1 && Arrow == 2)
        {
            maxDiceCount--;
            if (maxDiceCount >= 0 && playerStatusScript.maxDice + maxDiceCount >= playerStatusScript.maxDice)
            {
                diceCost -= 1000;
            }
        }
    }
    public void EnterButton()
    {
        if(playerStatusScript.playerMoney >= diceCost)
        {
            playerStatusScript.playerMoney -= diceCost;
            playerStatusScript.minDice += minDiceCount;
            playerStatusScript.maxDice += maxDiceCount;
            minDiceCount = 0;
            maxDiceCount = 0;
            diceCost = 0;
        }
    }
    public void YesORNo(int a)
    {
        if(a == 1)
        {
            SceneManager.LoadScene("TitleScene");
        }
        if(a == 2)
        {
            quesOb.SetActive(false);
        }

    }
    public void ClickToStartMenu()
    {
        if(diceCost > 0)
        {
            quesOb.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
