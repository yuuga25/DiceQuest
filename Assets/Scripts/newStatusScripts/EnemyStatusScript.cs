using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusScript : MonoBehaviour
{
    public static float enemyHp, enemyHpMax;
    public static float enemyAtk, enemyDef;
    public static int enemyEsc, enemyLevel;

    public static int ldPEnemy;
    public static int dPEnemy;
    public static int poPEnemy;
    public static int paPEnemy;
    public static int flPEnemy;
    public static int frPEnemy;
    public static int sPEnemy;
    public static int daPEnemy;

    public static int enemyID;


    public GameObject[] enemy; //0=マッシュルーム,1=ウサギ,2=幽霊
    public void SetEnemyStatus()
    {
        for (int e = 0; e < enemy.Length; e++)
        {
            enemy[e].SetActive(false);
        }

        switch (enemyID)
        {
            case 0: //テストステータス
                //基本ステータス
                enemyHp = 100;
                enemyHpMax = 100;
                enemyAtk = 50;
                enemyDef = 50;
                enemyEsc = 0;
                enemyLevel = 3;
                enemy[0].SetActive(true);

                //各パネル枚数
                ldPEnemy = 5;
                dPEnemy = 5;
                poPEnemy = 5;
                paPEnemy = 5;
                flPEnemy = 5;
                frPEnemy = 5;
                sPEnemy = 5;
                daPEnemy = 5;

                break;
            case 1: //キノコ（弱）
                //基本ステータス
                enemyHp = 20;
                enemyHpMax = 20;
                enemyAtk = 12;
                enemyDef = 3;
                enemyEsc = 35;
                enemyLevel = 1;
                enemy[0].SetActive(true);

                //各パネル枚数
                ldPEnemy = 8;
                dPEnemy = 20;
                poPEnemy = 5;
                paPEnemy = 0;
                flPEnemy = 0;
                frPEnemy = 0;
                sPEnemy = 0;
                daPEnemy = 0;

                break;
            case 2: //キノコ（中）
                //基本ステータス
                enemyHp = 35;
                enemyHpMax = 35;
                enemyAtk = 15;
                enemyDef = 6;
                enemyEsc = 45;
                enemyLevel = 1;
                enemy[0].SetActive(true);

                //各パネル枚数
                ldPEnemy = 8;
                dPEnemy = 18;
                poPEnemy = 5;
                paPEnemy = 5;
                flPEnemy = 0;
                frPEnemy = 0;
                sPEnemy = 0;
                daPEnemy = 0;

                break;
            case 3: //ウサギ
                //基本ステータス
                enemyHp = 30;
                enemyHpMax = 30;
                enemyAtk = 10;
                enemyDef = 7;
                enemyEsc = 40;
                enemyLevel = 1;
                enemy[1].SetActive(true);

                //各パネル枚数
                ldPEnemy = 3;
                dPEnemy = 10;
                poPEnemy = 0;
                paPEnemy = 0;
                flPEnemy = 0;
                frPEnemy = 0;
                sPEnemy = 15;
                daPEnemy = 0;

                break;
            case 4: //ウサギ（強）
                //基本ステータス
                enemyHp = 30;
                enemyHpMax = 30;
                enemyAtk = 10;
                enemyDef = 7;
                enemyEsc = 40;
                enemyLevel = 1;
                enemy[1].SetActive(true);

                //各パネル枚数
                ldPEnemy = 3;
                dPEnemy = 10;
                poPEnemy = 0;
                paPEnemy = 0;
                flPEnemy = 0;
                frPEnemy = 0;
                sPEnemy = 15;
                daPEnemy = 0;

                break;
            case 5: //幽霊
                //基本ステータス
                enemyHp = 44;
                enemyHpMax =44;
                enemyAtk = 30;
                enemyDef = 15;
                enemyEsc = 70;
                enemyLevel = 1;
                enemy[2].SetActive(true);

                //各パネル枚数
                ldPEnemy = 3;
                dPEnemy = 20;
                poPEnemy = 0;
                paPEnemy = 3;
                flPEnemy = 0;
                frPEnemy = 0;
                sPEnemy = 0;
                daPEnemy = 13;

                break;
            case 6: //幽霊
                //基本ステータス
                enemyHp = 45;
                enemyHpMax = 45;
                enemyAtk = 44;
                enemyDef = 44;
                enemyEsc = 80;
                enemyLevel = 1;
                enemy[2].SetActive(true);

                //各パネル枚数
                ldPEnemy = 5;
                dPEnemy = 11;
                poPEnemy = 3;
                paPEnemy = 3;
                flPEnemy = 7;
                frPEnemy = 7;
                sPEnemy = 3;
                daPEnemy = 9;

                break;
        }
    }
}
