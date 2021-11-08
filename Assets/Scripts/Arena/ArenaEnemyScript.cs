using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemyScript : MonoBehaviour
{
    public static float AenemyHp, AenemyHpMax;
    public static float AenemyAtk, AenemyDef;
    public static int AenemyEsc, AenemyLevel;

    public static int AldPEnemy;
    public static int AdPEnemy;
    public static int ApoPEnemy;
    public static int ApaPEnemy;
    public static int AflPEnemy;
    public static int AfrPEnemy;
    public static int AsPEnemy;
    public static int AdaPEnemy;

    public static int AenemyID;


    public GameObject[] enemy; //0=マッシュルーム

    public void Start()
    {
        for (int e = 0; e < enemy.Length; e++)
        {
            enemy[e].SetActive(false);
        }

        AenemyID = 0;

        switch (AenemyID)
        {
            case 0:
                //基本ステータス
                AenemyHp = 100;
                AenemyHpMax = 100;
                AenemyAtk = 50;
                AenemyDef = 50;
                AenemyEsc = 0;
                AenemyLevel = 3;
                enemy[0].SetActive(true);

                //各パネル枚数
                AldPEnemy = 5;
                AdPEnemy = 5;
                ApoPEnemy = 5;
                ApaPEnemy = 5;
                AflPEnemy = 5;
                AfrPEnemy = 5;
                AsPEnemy = 5;
                AdaPEnemy = 5;

                break;
        }
    }
}
