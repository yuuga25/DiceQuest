using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStatusScript : MonoBehaviour
{
    public static string playerName; //プレイヤーの名前

    public static float playerHp = 35, playerAtk = 6, playerDef = 4; //プレイヤーHP・プレイヤー攻撃力・プレイヤー防御力
    public static float playerHpMax  = 35; //プレイヤー最大HP
    public static int minDice = 1, maxDice = 6; //プレイヤー最小移動数・プレイヤー最大移動数
    public static int playerRank = 1; //プレイヤーランク

    public static float posX, posY, posZ;//プレイヤー座標
    
    public static bool playerPoison;    //毒状態確認
    public static bool playerParalyzed; //麻痺状態確認
    public static bool playerFlame;     //火炎状態確認
    public static bool playerFrozen;    //凍結状態確認
    public static bool playerSleep;     //睡眠状態確認
    public static bool AbnormalCondition; //全状態異常確認

    public static int stopTime = 0; //停止している時間


    public static int playerMoney; //所持金
    public static int playerStamina = 35; //スタミナ
    public static int playerMaxStamina = 50; //スタミナ最大上限
    public static int playerExp = 0; //プレイヤー経験値
    public static int playerMaxExp = 50; //プレイヤー経験値上限

    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void Update()
    {
        if (playerExp >= playerMaxExp && playerRank < 10)
        {
            playerRank++;
            playerHpMax += 8;
            playerHp = playerHpMax;
            playerAtk += 2;
            playerDef += 2;
            EnhancementScript.UpAtkCount = 5;
            EnhancementScript.UpDefCount = 5;
            playerExp = playerExp - playerMaxExp;
            RankUp();
        }

        PlayerPrefs.SetFloat("Hp", playerHp);
        PlayerPrefs.SetFloat("HpMax", playerHpMax);
        PlayerPrefs.SetFloat("Atk", playerAtk);
        PlayerPrefs.SetFloat("Def", playerDef);
        PlayerPrefs.SetInt("minDice", minDice);
        PlayerPrefs.SetInt("maxDice", maxDice);
        PlayerPrefs.SetInt("Rank", playerRank);
        PlayerPrefs.SetInt("Money", playerMoney);
        PlayerPrefs.SetInt("Stamina", playerStamina);
        PlayerPrefs.SetInt("maxStamina", playerMaxStamina);
        PlayerPrefs.SetInt("Exp", playerExp);
        PlayerPrefs.SetInt("MaxExp", playerMaxExp);

        PlayerPrefs.Save();

        playerHp = PlayerPrefs.GetFloat("Hp");
        playerHpMax = PlayerPrefs.GetFloat("HpMax");
        playerAtk = PlayerPrefs.GetFloat("Atk");
        playerDef = PlayerPrefs.GetFloat("Def");
        minDice = PlayerPrefs.GetInt("minDice");
        maxDice = PlayerPrefs.GetInt("maxDice");
        playerRank = PlayerPrefs.GetInt("Rank");
        playerMoney = PlayerPrefs.GetInt("Money");
        playerStamina = PlayerPrefs.GetInt("Stamina");
        playerMaxStamina = PlayerPrefs.GetInt("maxStamina");
        playerExp = PlayerPrefs.GetInt("Exp", playerExp);
        playerMaxExp = PlayerPrefs.GetInt("MaxExp", playerMaxExp);
    }
    private void RankUp()
    {
        switch (playerRank)
        {
            case 1:
                playerMaxExp = 50;
                playerMaxStamina = 50;
                break;
            case 2:
                playerMaxExp = 220;
                playerMaxStamina = 71;
                break;
            case 3:
                playerMaxExp = 410;
                playerMaxStamina = 92;
                break;
            case 4:
                playerMaxExp = 820;
                playerMaxStamina = 112;
                break;
            case 5:
                playerMaxExp = 1290;
                playerMaxStamina = 133;
                break;
            case 6:
                playerMaxExp = 1860;
                playerMaxStamina = 154;
                break;
            case 7:
                playerMaxExp = 2200;
                playerMaxStamina = 175;
                break;
            case 8:
                playerMaxExp = 2670;
                playerMaxStamina = 196;
                break;
            case 9:
                playerMaxExp = 3000;
                playerMaxStamina = 217;
                break;
            case 10:
                playerMaxExp = 3000;
                playerMaxStamina = 238;
                break;
        }

        if(playerStamina + playerMaxStamina > 999)
        {
            playerStamina += playerMaxStamina;
        }
    }
}
