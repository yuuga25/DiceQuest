using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaTurnScript : MonoBehaviour
{
    private bool arenaPlayerTurn;
    public static bool arenaEnemyTurn;
    public static bool arenaPanelSetting;
    public static bool arenaCheck;
    public static bool arenaPlayerSet;
    public static int arenaTurnCount;

    //UI
    public Slider playerHpSlider;
    public Text playerHpText;
    public Text playerMoveText;

    public Slider enemyHpSlider;

    public GameObject rollButton;
    public GameObject rollButtonCover;
    public GameObject turnEndButtonCover;
    public GameObject attackButtonCover;

    public GameObject[] Arrows;

    public Text turnCountText;
    public Text damageText;
    private Animator animText = null;
    public GameObject paralysisText;
    private Animator paText = null;
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

    public string loseScene;

    GameObject StageManager;
    GameObject BattlePanel;

    void Start()
    {
        arenaPanelSetting = true;
        arenaPlayerTurn = false;
        arenaEnemyTurn = false;
        arenaCheck = true;
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }
        rollButtonCover.SetActive(true);
        rollButton.SetActive(true);

        arenaTurnCount = 0;

        animText = damageText.GetComponent<Animator>();
        damageText.text = $"";
        paralysisText.SetActive(false);
        playerDamageText.SetActive(false);
        playerRecoveryText.SetActive(false);
        frozenPanel.SetActive(false);

        winText.SetActive(false);
        loseText.SetActive(false);
        runAwayText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerHpSlider.maxValue = playerStatusScript.playerHpMax;
        playerHpSlider.value = playerStatusScript.playerHp;
        playerHpText.text = $"{playerStatusScript.playerHp}/{playerStatusScript.playerHpMax}";
        playerMoveText.text = $"{PlayerMoveScript.playerMoveNum}";
        enemyHpSlider.maxValue = ArenaEnemyScript.AenemyHpMax;
        enemyHpSlider.value = ArenaEnemyScript.AenemyHp;
        turnCountText.text = $"{arenaTurnCount}";
        scoreText.text = $"{StageControllerScript.Score}";

        if (ArenaEnemyScript.AenemyHp <= 0)
        {
            arenaEnemyTurn = false;
            arenaPlayerTurn = false;

            arenaTurnCount = 0;

            winText.SetActive(true);

            PlayerMoveScript.playerMoveNum = 0;
        }
        else if (playerStatusScript.playerHp <= 0)
        {
            arenaEnemyTurn = false;
            arenaPlayerTurn = false;

            playerStatusScript.playerHp = 0;

            loseText.SetActive(true);

            PlayerMoveScript.playerMoveNum = 0;
            for (int i = 0; i < Arrows.Length; i++)
            {
                Arrows[i].SetActive(false);
            }
        }

        if (arenaEnemyTurn)
        {
            rollButton.SetActive(false);
            rollButtonCover.SetActive(true);
            turnEndButtonCover.SetActive(true);

            arenaTurnCount++;
        }

        if (arenaPanelSetting)
        {
            arenaPanelSetting = false;
            StartCoroutine(setPanel());
        }

        if (arenaPlayerTurn)
        {
            arenaCheck = true;
            if (PlayerMoveScript.playerMoveNum <= 0)
            {
                rollButtonCover.SetActive(true);
            }
        }

        if (arenaPlayerSet)
        {
            rollButton.SetActive(true);
            rollButtonCover.SetActive(false);
        }

        if (playerStatusScript.AbnormalCondition)
        {
            if (playerStatusScript.playerPoison)
            {
                poisonDisplay.SetActive(true);
            }
            else if (playerStatusScript.playerParalyzed)
            {
                paralysisDisplay.SetActive(true);
            }
            else if (playerStatusScript.playerFlame)
            {
                flameDisplay.SetActive(true);
            }
            else if (playerStatusScript.playerFrozen)
            {
                frozenDisplay.SetActive(true);
            }
            else if (playerStatusScript.playerSleep)
            {
                sleepDisplay.SetActive(true);
            }
        }
        if (!playerStatusScript.AbnormalCondition)
        {
            if (!playerStatusScript.playerPoison)
            {
                poisonDisplay.SetActive(false);
            }
            else if (!playerStatusScript.playerParalyzed)
            {
                paralysisDisplay.SetActive(false);
            }
            else if (!playerStatusScript.playerFlame)
            {
                flameDisplay.SetActive(false);
            }
            else if (!playerStatusScript.playerFrozen)
            {
                frozenDisplay.SetActive(false);
            }
            else if (!playerStatusScript.playerSleep)
            {
                sleepDisplay.SetActive(false);
            }
        }

        if (playerDamage > 0)
        {
            playerDamageText.SetActive(true);
            playerDamageText.GetComponent<Text>().text = $"{playerDamage}";
            playerDamage = 0;
            StartCoroutine(setPlayerDamage());
        }

        if (playerStatusScript.stopTime > 0)
        {
            frozenPanel.SetActive(true);
            attackButtonCover.SetActive(true);
        }
        else if (playerStatusScript.stopTime <= 0)
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

        if (playerStatusScript.playerParalyzed || playerStatusScript.playerFlame)
        {
            paText = paralysisText.GetComponent<Animator>();
            paText.SetBool("ParalysisOn", true);
            paralysisText.SetActive(true);
            StartCoroutine(setParalysis());
        }

        if (playerStatusScript.playerPoison || playerStatusScript.playerFlame)
        {
            float sDamage = playerStatusScript.playerHpMax / 8;
            sDamage = Mathf.Floor(sDamage);
            playerDamage = (int)sDamage;

            playerStatusScript.playerHp -= sDamage;
        }

        if (playerStatusScript.playerSleep)
        {
            float sRecovery = playerStatusScript.playerHp / 16;
            sRecovery = Mathf.Floor(sRecovery);

            playerStatusScript.playerHp += sRecovery;
            if (playerStatusScript.playerHp > playerStatusScript.playerHpMax)
            {
                playerStatusScript.playerHp = playerStatusScript.playerHpMax;
            }

            playerRecoveryText.SetActive(true);
            playerRecoveryText.GetComponent<Text>().text = $"{sRecovery}";
            StartCoroutine(setPlayerRecovery());
        }

        rollButton.SetActive(false);
        turnEndButtonCover.SetActive(false);
        arenaPlayerTurn = true;
        arenaPlayerSet = false;
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(true);
        }
        arenaTurnCount++;
    }
    public void TurnChange()//ターンエンドボタン入力
    {
        arenaPlayerTurn = false;
        arenaEnemyTurn = true;
        PlayerMoveScript.playerMoveNum = 0;
        rollButtonCover.SetActive(true);
        turnEndButtonCover.SetActive(true);
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }
    }
    public void AttackButton()//攻撃力ボタン
    {
        float damage = playerStatusScript.playerRank * 2 / 5 + 2; damage = Mathf.Floor(damage);//プレイヤーランク*２/５+２→切り捨て
        float moveNum = PlayerMoveScript.playerMoveNum; //行動力取得
        damage = damage * (moveNum * 10) * playerStatusScript.playerAtk / ArenaEnemyScript.AenemyDef; damage = Mathf.Floor(damage);//行動力*10*プレイヤー攻撃力/エネミー防御力→切り捨て
        damage = damage / 50 + 2; damage = Mathf.Floor(damage);//damage/50+2→切り捨て
        float random = Random.Range(80, 101); //乱数
        damage = damage * (random / 100); damage = Mathf.Floor(damage);//damage*乱数→切り捨て
        //ここに補正を追加する

        ArenaEnemyScript.AenemyHp -= damage;//ダメージ

        animText.SetBool("Damage", true);
        damageText.text = $"{damage}";

        int scoreC = Random.Range(1, 7);
        StageControllerScript.Score += ((int)damage * scoreC);

        StartCoroutine(setDamage());
        TurnChange();
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
        if (ArenaEnemyScript.AenemyHp <= 0)
        {
            StageControllerScript.Quantity++;
        }

        StageManager.GetComponent<StageControllerScript>().ChangeScene();
        BattlePanel.GetComponent<BattlePanelScript>().endBattle();

        arenaEnemyTurn = true;
        arenaPlayerTurn = false;

        playerStatusScript.AbnormalCondition = false;
        playerStatusScript.playerPoison = false;
        playerStatusScript.playerParalyzed = false;
        playerStatusScript.playerFlame = false;
        playerStatusScript.playerFrozen = false;
        playerStatusScript.playerSleep = false;

        winText.SetActive(false);
        loseText.SetActive(false);
        runAwayText.SetActive(false);
    }
    public void loseResult()
    {
        SceneManager.LoadScene(loseScene);
    }
}
