using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldTurnScript : MonoBehaviour
{
    /*このスクリプトに記述すること*/
    //UIでターンを切り替えられるようにする。
    //プレイヤーのダイスの値を決定する
    //エネミーのダイスの値を決定する
    //ターンごとの行動
    //その他フィールド側でのUI




    //プレイヤー
    public Button RollButton, TurnEndButton; //UIのプレイヤー側のボタン
    public GameObject RollButtonCover, TurnEndButtonCover, ItemButtonCover; //プレイヤー側ボタンのカバー
    public Slider playerHpSlider; //プレイヤーのHPを表示するスライダー
    public Text playerHpText, diceNumText; //プレイヤーのHPを表示するテキスト / ダイスの値を表示するテキスト
    public GameObject[] Arrows; //行動先を表示する矢印
    public GameObject player;

    //ゲーム
    public static bool startGame = true;

    //共通
    public static bool playerTurn, enemyTurn; //ターン切り替えフラッグ
    

    //エネミー
    public static int enemyDiceNum; //エネミーダイスの値
    public static int enemysNum; //エネミーのID
    private GameObject[] enemyObjects; //エネミーオブジェクト


    // Start is called before the first frame update
    void Start()
    {
        startGame = true;
        playerTurn = true;
        enemyTurn = false;
        TurnEndButtonCover.SetActive(true);
        ItemButtonCover.SetActive(true);
        StartCoroutine(PlayerStart());

        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }

        if (startGame)
        {
            playerStatusScript.posX = 0;
            playerStatusScript.posY = 2;
            playerStatusScript.posZ = -12;

            startGame = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHpSlider.value = playerStatusScript.playerHp;
        playerHpSlider.maxValue = playerStatusScript.playerHpMax;
        playerHpText.text = $"{playerStatusScript.playerHp}/{playerStatusScript.playerHpMax}";
        diceNumText.text = $"{PlayerMoveScript.playerMoveNum}";

        if(playerTurn)
        {
            enemysNum = 0;
            if (PlayerMoveScript.playerMoveNum <= 0)
            {
                ItemButtonCover.SetActive(true);
                for(int i = 0; i < Arrows.Length; i++)
                {
                    Arrows[i].SetActive(false);
                }
            }
        }
        else if(enemyTurn)
        {
            enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            enemyTurn = false;
            StartCoroutine(enemyChanger());
            if (enemyObjects.Length <= 0)
            {
                playerTurn = true;
                RollButtonCover.SetActive(false);
            }
        }
    }

    public void RollDice()//Rollボタン入力
    {
        int DiceNum = Random.Range(playerStatusScript.minDice, playerStatusScript.maxDice);
        PlayerMoveScript.playerMoveNum = DiceNum;
        RollButtonCover.SetActive(true);
        TurnEndButtonCover.SetActive(false);
        ItemButtonCover.SetActive(false);

        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(true);
        }
        StageControllerScript.TurnUsed++;
    }
    public void TurnChange()//ターンエンドボタン入力
    {
        playerTurn = !playerTurn;
        enemyTurn = !enemyTurn;
        PlayerMoveScript.playerMoveNum = 0;
        RollButtonCover.SetActive(true);
        TurnEndButtonCover.SetActive(true);
        ItemButtonCover.SetActive(true);

        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }
    }
    IEnumerator enemyChanger()//エネミーの個別判定
    {
        for(int i = 0; i < enemyObjects.Length; i++)
        {
            int enemyMinMove = enemyObjects[i].GetComponent<EnemyScript>().GetMinMove();
            int enemyMaxMove = enemyObjects[i].GetComponent<EnemyScript>().GetMaxMove();
            enemyDiceNum = 0;
            enemyDiceNum = Random.Range(enemyMinMove, enemyMaxMove + 1);
            enemyObjects[i].GetComponent<EnemyScript>().GetEnemyMoveCount(enemyDiceNum);
            yield return new WaitForSeconds(1f);
            enemysNum++;
            if (enemyObjects.Length <= enemysNum)
            {
                yield return new WaitForSeconds(0);
                playerTurn = true;
                RollButtonCover.SetActive(false);
            }
        }
    }

    IEnumerator PlayerStart()
    {
        yield return new WaitForSeconds(2);
        RollButtonCover.SetActive(false);
    }
}
