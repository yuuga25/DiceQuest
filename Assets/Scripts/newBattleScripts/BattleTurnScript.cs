using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleTurnScript : MonoBehaviour
{
    private bool playerTurn; //プレイヤーターンフラッグ
    public static bool enemyTurn; //エネミーターンフラッグ
    public static bool settingFlag; //タイルをセットするフラッグ
    public static bool checkFrag; //効果処理を判断するフラッグ
    public static bool playerSetFlag; //プレイヤーターンのセットをするフラッグ
    public static int countEnemyTurn; //エネミーターンの回数を記録する

    //UI
    public Slider playerHpSlider; //プレイヤーのHPスライダー
    public Text playerHpText; //プレイヤーのHPテキスト
    public Text playerMoveText; //プレイヤーの行動力のテキスト

    public Slider enemyHpSlider; //エネミーのHPスライダー

    //Button類(Coverも含む)
    public GameObject rollButton; //Rollボタン

    public GameObject rollButtonCover; //Rollボタンのカバー
    public GameObject skilButtonCover; //スキルボタンのカバー
    public GameObject itemButtonCover; //アイテムボタンのカバー
    public GameObject turnEndButtonCover; //ターンエンドのカバー
    public GameObject escapeButtonCover; //逃げるボタンのカバー
    public GameObject attackButtonCover; //攻撃ボタンのカバー

    public GameObject[] Arrows; //行動先を表示する矢印

    public static int turnCount; //ターン数をカウントする
    public Text turnCountText;
    public Text damageText;
    private Animator animText = null;
    public GameObject paralysisText;
    private Animator paText;
    public GameObject playerDamageText;
    public static int playerDamage;
    public GameObject playerRecoveryText;
    public GameObject frozenPanel;

    public GameObject poisonDisplay;
    public GameObject paralysisDisplay;
    public GameObject flameDisplay;
    public GameObject frozenDisplay;
    public GameObject sleepDisplay;

    public GameObject winText;
    public GameObject loseText;
    public GameObject runAwayText;

    public Text scoreText;

    public string winScene;
    public string loseScene;

    GameObject StageManager;
    GameObject BattlePanel;

    // Start is called before the first frame update
    void Start()
    {
        settingFlag = true;
        playerTurn = false;
        enemyTurn = false;
        checkFrag = true;
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }
        rollButtonCover.SetActive(true);
        rollButton.SetActive(true);

        countEnemyTurn = 0;
        turnCount = 0;

        animText = damageText.GetComponent<Animator>();
        damageText.text = $"";
        paralysisText.SetActive(false);
        playerDamageText.SetActive(false);
        playerRecoveryText.SetActive(false);
        frozenPanel.SetActive(false);

        winText.SetActive(false);
        loseText.SetActive(false);
        runAwayText.SetActive(false);

        StageManager = GameObject.Find("GameController");
        BattlePanel = GameObject.Find("GameManeger");
    }

    // Update is called once per frame
    void Update()
    {
        playerHpSlider.maxValue = playerStatusScript.playerHpMax;
        playerHpSlider.value = playerStatusScript.playerHp;
        playerHpText.text = $"{playerStatusScript.playerHp}/{playerStatusScript.playerHpMax}";
        playerMoveText.text = $"{PlayerMoveScript.playerMoveNum}";
        enemyHpSlider.maxValue = EnemyStatusScript.enemyHpMax;
        enemyHpSlider.value = EnemyStatusScript.enemyHp;
        turnCountText.text = $"{turnCount}";
        scoreText.text = $"{StageControllerScript.Score}";

        if(EnemyStatusScript.enemyHp <= 0)
        {
            enemyTurn = false;
            playerTurn = false;

            turnCount = 0;

            winText.SetActive(true);

            PlayerMoveScript.playerMoveNum = 0;
        }
        else if(playerStatusScript.playerHp <= 0)
        {
            enemyTurn = false;
            playerTurn = false;

            playerStatusScript.playerHp = 0;

            loseText.SetActive(true);

            PlayerMoveScript.playerMoveNum = 0;
            for (int i = 0; i < Arrows.Length; i++)
            {
                Arrows[i].SetActive(false);
            }
        }

        if (enemyTurn)
        {
            rollButton.SetActive(false);
            rollButtonCover.SetActive(true);
            skilButtonCover.SetActive(true);
            itemButtonCover.SetActive(true);
            turnEndButtonCover.SetActive(true);
            escapeButtonCover.SetActive(true);

            countEnemyTurn++;
        }

        if(settingFlag)
        {
            settingFlag = false;
            StartCoroutine(setPanel());
        }

        if(playerTurn)
        {
            checkFrag = true;
            if (PlayerMoveScript.playerMoveNum <= 0)
            {
                rollButtonCover.SetActive(true);
            }
        }

        if(playerSetFlag)
        {
            countEnemyTurn = 0;
            rollButton.SetActive(true);
            rollButtonCover.SetActive(false);
        }

        if(playerStatusScript.AbnormalCondition)
        {
            if(playerStatusScript.playerPoison)
            {
                poisonDisplay.SetActive(true);
            }
            else if(playerStatusScript.playerParalyzed)
            {
                paralysisDisplay.SetActive(true);
            }
            else if(playerStatusScript.playerFlame)
            {
                flameDisplay.SetActive(true);
            }
            else if(playerStatusScript.playerFrozen)
            {
                frozenDisplay.SetActive(true);
            }
            else if(playerStatusScript.playerSleep)
            {
                sleepDisplay.SetActive(true);
            }
        }
        if(!playerStatusScript.AbnormalCondition)
        {
            if(!playerStatusScript.playerPoison)
            {
                poisonDisplay.SetActive(false);
            }
            else if(!playerStatusScript.playerParalyzed)
            {
                paralysisDisplay.SetActive(false);
            }
            else if(!playerStatusScript.playerFlame)
            {
                flameDisplay.SetActive(false);
            }
            else if(!playerStatusScript.playerFrozen)
            {
                frozenDisplay.SetActive(false);
            }
            else if(!playerStatusScript.playerSleep)
            {
                sleepDisplay.SetActive(false);
            }
        }

        if(playerDamage > 0)
        {
            playerDamageText.SetActive(true);
            playerDamageText.GetComponent<Text>().text = $"{playerDamage}";
            playerDamage = 0;
            StartCoroutine(setPlayerDamage());
        }

        if(playerStatusScript.stopTime > 0)
        {
            frozenPanel.SetActive(true);
            attackButtonCover.SetActive(true);
        }
        else if(playerStatusScript.stopTime <= 0)
        {
            frozenPanel.SetActive(false);
            attackButtonCover.SetActive(false);
            if (playerStatusScript.playerFrozen)
            {
                playerStatusScript.playerFrozen = false;
                playerStatusScript.AbnormalCondition = false;
                frozenDisplay.SetActive(false);
            }
            else if (playerStatusScript.playerSleep)
            {
                playerStatusScript.playerSleep = false;
                playerStatusScript.AbnormalCondition = false;
                sleepDisplay.SetActive(false);
            }
        }
    }
    public void RollDice()//Rollボタン入力
    {
        int DiceNum = Random.Range(playerStatusScript.minDice, playerStatusScript.maxDice + 1);
        PlayerMoveScript.playerMoveNum = DiceNum;

        if(playerStatusScript.playerParalyzed || playerStatusScript.playerFlame)
        {
            paText = paralysisText.GetComponent<Animator>();
            paText.SetBool("ParalysisOn", true);
            paralysisText.SetActive(true);
            StartCoroutine(setParalysis());
        }

        if(playerStatusScript.playerPoison || playerStatusScript.playerFlame)
        {
            float sDamage = playerStatusScript.playerHpMax / 8;
            sDamage = Mathf.Floor(sDamage);
            playerDamage = (int)sDamage;

            playerStatusScript.playerHp -= sDamage;
        }

        if(playerStatusScript.playerSleep)
        {
            float sRecovery = playerStatusScript.playerHp / 16;
            sRecovery = Mathf.Floor(sRecovery);

            playerStatusScript.playerHp += sRecovery;
            if(playerStatusScript.playerHp > playerStatusScript.playerHpMax)
            {
                playerStatusScript.playerHp = playerStatusScript.playerHpMax;
            }

            playerRecoveryText.SetActive(true);
            playerRecoveryText.GetComponent<Text>().text = $"{sRecovery}";
            StartCoroutine(setPlayerRecovery());
        }

        rollButton.SetActive(false);
        turnEndButtonCover.SetActive(false);
        itemButtonCover.SetActive(false);
        escapeButtonCover.SetActive(false);
        playerTurn = true;
        playerSetFlag = false;
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(true);
        }
        turnCount++;
    }
    public void TurnChange()//ターンエンドボタン入力
    {
        playerTurn = false;
        enemyTurn = true;
        PlayerMoveScript.playerMoveNum = 0;
        rollButtonCover.SetActive(true);
        turnEndButtonCover.SetActive(true);
        itemButtonCover.SetActive(true);
        escapeButtonCover.SetActive(true);
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }
    }

    public void AttackButton()//攻撃力ボタン
    {
        float damage = playerStatusScript.playerRank * 2 / 5 + 2; damage = Mathf.Floor(damage);//プレイヤーランク*２/５+２→切り捨て
        float moveNum = PlayerMoveScript.playerMoveNum; //行動力取得
        damage = damage * (moveNum * 10) * playerStatusScript.playerAtk / EnemyStatusScript.enemyDef; damage = Mathf.Floor(damage);//行動力*10*プレイヤー攻撃力/エネミー防御力→切り捨て
        damage = damage / 50 + 2; damage = Mathf.Floor(damage);//damage/50+2→切り捨て
        float random = Random.Range(80, 101); //乱数
        damage = damage * (random / 100); damage = Mathf.Floor(damage);//damage*乱数→切り捨て
        //ここに補正を追加する

        EnemyStatusScript.enemyHp -= damage;//ダメージ

        animText.SetBool("Damage", true);
        damageText.text = $"{damage}";

        int scoreC = Random.Range(1, 7);
        StageControllerScript.Score += ((int)damage * scoreC);

        StartCoroutine(setDamage());
        TurnChange();
    }

    public void Escape()//逃げる
    {
        int r1 = Random.Range(0, 101);
        int r2 = Random.Range(0, 101);
        int r3 = Random.Range(0, 101);
        if(r1 + r2 + r3 > (EnemyStatusScript.enemyEsc * 3))
        {
            runAwayText.SetActive(true);
        }
        else
        {
            TurnChange();
        }
    }

    IEnumerator setPanel()
    {
        yield return new WaitForSeconds(1f);
        BattlePanelScript.changePanelFlag = true;
    }

    IEnumerator setDamage()
    {
        yield return new WaitForSeconds(0.45f);
        damageText.text = $"";
        animText.SetBool("Damage", false);
    }

    IEnumerator setParalysis()
    {
        PlayerMoveScript.playerMoveNum--;
        yield return new WaitForSeconds(0.45f);
        {
            paText.SetBool("ParalysisOn", false);
            paralysisText.SetActive(false);
        }
    }

    IEnumerator setPlayerDamage()
    {
        yield return new WaitForSeconds(0.45f);
        {
            playerDamageText.SetActive(false);
        }
    }
    IEnumerator setPlayerRecovery()
    {
        yield return new WaitForSeconds(0.45f);
        {
            playerRecoveryText.SetActive(false);
        }
    }
    public void winResult()
    {
        if(EnemyStatusScript.enemyHp <= 0) //倒した判定
        {
            StageControllerScript.Quantity++;
            StageControllerScript.Score = (int)Mathf.Floor(StageControllerScript.Score * 1.5f);
        }

        StageManager.GetComponent<StageControllerScript>().ChangeScene();
        BattlePanel.GetComponent<BattlePanelScript>().endBattle();

        enemyTurn = true;
        playerTurn = false;

        playerStatusScript.AbnormalCondition = false;
        playerStatusScript.playerPoison = false;
        playerStatusScript.playerParalyzed = false;
        playerStatusScript.playerFlame = false;
        playerStatusScript.playerFrozen = false;
        playerStatusScript.playerSleep = false;

        winText.SetActive(false);
        loseText.SetActive(false);
        runAwayText.SetActive(false);
        rollButtonCover.SetActive(true);
    }
    public void loseResult()
    {
        SceneManager.LoadScene(loseScene);
    }
}
