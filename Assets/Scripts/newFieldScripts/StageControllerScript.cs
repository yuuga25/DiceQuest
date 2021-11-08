using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageControllerScript : MonoBehaviour
{
    /*このスクリプトに記述すること*/
    //難易度の切り替え
    //難易度ごとの障害物の表示切り替え
    //ステージクリアごとのカメラ移動
    //ステージクリアごとのプレイヤー移動
    //タグでフィールドの色変更
    //ゴール判定

    //報酬
    public int rewardMoney;
    public int rewardExp;

    public GameObject StartText;

    //・難易度ごとの障害物の表示
    public static int difficulty = 0; //難易度フラグ
    [Header("難易度　初級（デフォルト）で表示するオブジェクト")]
    [Header("※必ず障害物オブジェクトには[tag:obstacle]をつける※")]private GameObject[] basicObstacle;
    [Header("難易度　中級で表示するオブジェクト")]public GameObject[] normalObstacle;
    [Header("難易度　上級で表示するオブジェクト")]public GameObject[] hardObstacle;
    [Header("難易度　究極で表示するオブジェクト")]public GameObject[] ultimateObstacle;

    //・ステージカメラ移動、プレイヤーの移動
    //プレイヤーのステージ移動時のスタート位置
    public int secondStartPosX, secondStartPosZ, thirdStartPosX, thirdStartPosZ;
    public GameObject playerObject; //プレイヤーオブジェクト
    private Transform playerTransform; //プレイヤーオブジェクトのトランスフォーム
    private Vector3 playerPos; //上記のポジション
    public GameObject checkPoint1, checkPoint2, goal; //チェックポイント１・２　ゴール
    private Transform cp1Transform, cp2Transform, goalTransform; //チェックポイント１・２　ゴールのトランスフォーム
    private Vector3 cp1Pos, cp2Pos, goalPos; //上記のポジション
    public static bool clear1, clear2, stageClear; //ステージごとのクリア判定
    public GameObject mainCamera; //アニメーションで動かすカメラ
    private Animator anim = null; //アニメーション

    //リザルト画面
    public GameObject resultPanel;//リザルトパネル
    public Text TurnUsedText;//使用ターン数テキスト
    public Text QuantityText;//倒した敵の数テキスト
    public Text ScoreText;//合計スコアテキスト
    private bool ResultFlag; //リザルトフラッグ
    public Text RewardMoneyText; //報酬（money）のテキスト
    public Text RewardExpText; //報酬（Exp）のテキスト

    //プレイヤーリザルト値
    public static int TurnUsed; //使用したターン数
    public static int Quantity; //倒した敵の数
    public static int Score;    //ステージ合計スコア

    //バトルとフィールド移動
    private bool scene;
    public GameObject field;
    public GameObject battle;


    //スタート設定と色変更
    public static bool startFlag;

    //フィールドターンスクリプト
    private GameObject fieldTurnObject;

    /*-----フィールドの色-----*/
    //※フィールドの色を追加する時に、ここにゲームオブジェクトの配列も追加する。
    GameObject[] grassObjects; //草原フィールド
    GameObject[] stoneObjects; //石フィールド
    GameObject[] roughSoilObjects; //荒土フィールド
    GameObject[] waterObjects; //水フィールド

    //メニュー画面
    public GameObject menuObject;
    public GameObject confirmationScreen;
    public Text ConsumptionStaminaText;

    /*フィールドの色　表*/
    //[0].草原・[1].石
    [Header("[0].草原-[1].石-[2].荒土-[3].水")]
    public Material[] fieldMaterial; //フィールドの色

    void Start()
    {
        StartText.SetActive(true);
        menuObject.SetActive(false);
        confirmationScreen.SetActive(false);

        resultPanel.SetActive(false);
        ResultFlag = true;

        TurnUsed = 0;
        Quantity = 0;
        Score = 0;

        TurnUsedText.text = $"{TurnUsed}";
        QuantityText.text = $"{Quantity}";
        ScoreText.text    = $"{Score}";

        basicObstacle = GameObject.FindGameObjectsWithTag("obstacle");
        fieldTurnObject = GameObject.Find("GameController");

        scene = false;
        ChangeScene();

        clear1 = clear2 = stageClear = false;

        #region //オブジェクト表示
        //全てのオブジェクトを非表示にする
        for (int o = 0; o < basicObstacle.Length; o++)
        {
            basicObstacle[o].SetActive(true);
        }
        for (int o = 0; o < normalObstacle.Length; o++)
        {
            normalObstacle[o].SetActive(false);
        }
        for (int o = 0; o < hardObstacle.Length; o++)
        {
            hardObstacle[o].SetActive(false);
        }
        for (int o = 0; o < ultimateObstacle.Length; o++)
        {
            ultimateObstacle[o].SetActive(false);
        }

        //難易度によってオブジェクト表示
        if (difficulty >= 1)
        {
            for (int o = 0; o < normalObstacle.Length; o++)
            {
                normalObstacle[o].SetActive(true);
            }
        }
        if(difficulty >= 2)
        {
            for (int o = 0; o < hardObstacle.Length; o++)
            {
                hardObstacle[o].SetActive(true);
            }
        }
        if(difficulty >= 3)
        {
            for (int o = 0; o < ultimateObstacle.Length; o++)
            {
                ultimateObstacle[o].SetActive(true);
            }
        }
        #endregion

        //ゴールオブジェクトなどの座標取得
        cp1Transform = checkPoint1.transform;
        cp2Transform = checkPoint2.transform;
        goalTransform = goal.transform;
        cp1Pos = cp1Transform.position;
        cp2Pos = cp2Transform.position;
        goalPos = goalTransform.position;

        //カメラのアニメーターを取得
        anim = mainCamera.GetComponent<Animator>();

        playerObject.SetActive(false);

        #region//色変更
        grassObjects = GameObject.FindGameObjectsWithTag("grass");
        for (int i = 0; i < grassObjects.Length; i++)
        {
            grassObjects[i].GetComponent<Renderer>().material = fieldMaterial[0];
        }
        stoneObjects = GameObject.FindGameObjectsWithTag("stone");
        for (int i = 0; i < stoneObjects.Length; i++)
        {
            stoneObjects[i].GetComponent<Renderer>().material = fieldMaterial[1];
        }
        roughSoilObjects = GameObject.FindGameObjectsWithTag("roughSoil");
        for(int i = 0; i < roughSoilObjects.Length; i++)
        {
            roughSoilObjects[i].GetComponent<Renderer>().material = fieldMaterial[2];
        }
        waterObjects = GameObject.FindGameObjectsWithTag("water");
        for (int i = 0; i < waterObjects.Length; i++)
        {
            waterObjects[i].GetComponent<Renderer>().material = fieldMaterial[3];
        }

        #endregion

        if (FieldTurnScript.startGame)
        {
            StartCoroutine(PlayerStart());
        }
        else
        {
            playerObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーオブジェクトの座標取得
        playerTransform = playerObject.transform;
        playerPos = playerTransform.position;

        ConsumptionStaminaText.text = $"<color=#FF7800>{Mathf.Floor(MainMenu.questStamina / 2)}</color> のスタミナを消費します";

        if (playerPos.x == cp1Pos.x && playerPos.z == cp1Pos.z)
        {
            clear1 = true;
            anim.SetBool("clear1", true);
            playerPos.x = secondStartPosX;
            playerPos.z = secondStartPosZ;
            PlayerMoveScript.playerMoveNum = 0;
            playerTransform.position = playerPos;

            fieldTurnObject.GetComponent<FieldTurnScript>().TurnChange();
        }
        if(playerPos.x == cp2Pos.x && playerPos.z == cp2Pos.z)
        {
            clear1 = false;
            clear2 = true;
            anim.SetBool("clear2", true);
            playerPos.x = thirdStartPosX;
            playerPos.z = thirdStartPosZ;
            PlayerMoveScript.playerMoveNum = 0;
            playerTransform.position = playerPos;

            fieldTurnObject.GetComponent<FieldTurnScript>().TurnChange();
        }
        if(playerPos.x == goalPos.x && playerPos.z == goalPos.z)
        {
            clear1 = false;
            clear2 = false;
            //※ここにゴール後の判定を書きこむ
            if(ResultFlag)
            {
                ResultFlag = false;
                resultPanel.SetActive(true);
                StartCoroutine(ResultPanel());
            }
        }
    }

    IEnumerator PlayerStart()//プレイヤーが現れる
    {
        yield return new WaitForSeconds(2);
        StartText.SetActive(false);
        playerObject.SetActive(true);
        Transform playerTransform = playerObject.transform;
        Vector3 playerpos = playerTransform.position;
        playerPos.x = 0;
        playerPos.y = 2;
        playerPos.z = 12;
    }

    IEnumerator ResultPanel()//リザルト画面
    {
        int plusMoney = (int)Mathf.Floor((Score / (TurnUsed * 10)) * Quantity);
        int plusExp = 0;
        switch (difficulty)
        {
            case 0:
                plusExp = (int)Mathf.Floor((Quantity * 5));
                rewardMoney = rewardMoney * 1;
                rewardExp = rewardExp * 1;
                break;
            case 1:
                plusExp = (int)Mathf.Floor((Quantity * 5) * 1.3f);
                rewardMoney = (int)Mathf.Floor(rewardMoney * 1.5f);
                rewardExp = (int)Mathf.Floor(rewardExp * 1.5f);
                break;
            case 2:
                plusExp = (int)Mathf.Floor((Quantity * 5) * 1.4f);
                rewardMoney = rewardMoney * 2;
                rewardExp = rewardExp * 2;
                break;
            case 3:
                plusExp = (int)Mathf.Floor((Quantity * 5) * 1.5f);
                rewardMoney = rewardMoney * 3;
                rewardExp = rewardExp * 3;
                break;
        }
        RewardMoneyText.text = $"<color=#FFDE7A>Money</color>:  +{rewardMoney + plusMoney}";
        RewardExpText.text   = $"<color=#7AFF8F>Exp</color>:  +{rewardExp + plusExp}";
        playerStatusScript.playerMoney += rewardMoney + plusMoney;
        playerStatusScript.playerExp += rewardExp + plusExp;

        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i <= TurnUsed; i++)
        {
            TurnUsedText.text = $"{i}";
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        for(int i = 0; i <= Quantity; i++)
        {
            QuantityText.text = $"{i}";
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i <= Score; i++)
        {
            ScoreText.text = $"{i}";
            yield return new WaitForSeconds(0.0001f);
        }
    }

    public void ChangeScene()
    {
        if(scene)
        {
            PlayerMoveScript.playerTop = PlayerMoveScript.playerRight = PlayerMoveScript.playerLeft = PlayerMoveScript.playerBottom = true;

            field.SetActive(false);
            battle.SetActive(true);
            scene = false;
        }
        else if(!scene)
        {
            field.SetActive(true);
            battle.SetActive(false);
            scene = true;
        }
    }
    public void OnLoadMenu()
    {
        TurnUsedText.text = $"{TurnUsed}";
        QuantityText.text = $"{Quantity}";
        ScoreText.text = $"{Score}";
        FieldTurnScript.startGame = false;
        playerStatusScript.playerStamina -= MainMenu.questStamina;
        TurnUsed = 0;
        Quantity = 0;
        Score = 0;
        SceneManager.LoadScene("TitleScene");
    }
    public void ClickToMenu()
    {
        menuObject.SetActive(true);
    }
    public void ClickToBackGame()
    {
        menuObject.SetActive(false);
    }
    public void ClickToMainMenu()
    {
        playerStatusScript.playerStamina -= (int)Mathf.Floor(MainMenu.questStamina / 2);
        FieldTurnScript.startGame = true;
        TurnUsed = 0;
        Quantity = 0;
        Score = 0;
        SceneManager.LoadScene("TitleScene");
        PlayerMoveScript.playerMoveNum = 0;
    }
    public void OpenCs()
    {
        confirmationScreen.SetActive(true);
    }
    public void CloseCs()
    {
        confirmationScreen.SetActive(false);
    }
}
