using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject centerMenu; //センターメニュー
    public GameObject questMenu;  //クエストメニュー

    public Text questButtonText; //クエストボタンテキスト
    public Text questButtonTextEn; //クエストボタンテキスト（英語）

    //プレイヤー情報画面
    public Text playerRankText;
    public Text playerNameText;
    public Text playerHpText;
    public Text playerAtkText;
    public Text playerDefText;
    public Text playerDiceText;
    public Text playerMoneyText;

    public Slider staminaSlider;
    public Text staminaText;

    public Slider expSlider;
    public Text expText;

    //クエスト情報画面
    public GameObject QuestInformation;

    public static string loadQuestScene; //ロードするシーン
    public static string questName; //クエストの名前
    public static string questDifficulty; //クエストの難易度
    public static int    questStamina; //クエスト消費スタミナ
    public static int    questLevel; //クエスト推奨ランク
    public static bool   questMenuFlag = false;//クエスト情報画面の表示フラッグ

    public Text questNameText;
    public Text questDifficultyText;
    public Text questLevelText;
    public Text questStaminaText;
    public GameObject questStartButton;

    //クエストロード
    private AsyncOperation async;
    [SerializeField]
    private GameObject loadUI;
    [SerializeField]
    private Slider slider;


    private bool questOn = false; //クエストメニューが表示されているかのフラッグ

    public int minPage; //最小ページ
    public int maxPage; //最大ページ
    private int thisPage; //現在のページ

    private Animator centerMenuAnim = null;
    private Animator questAnim = null;

    private Vector3 touchStartPos; 
    private Vector3 touchEndPos;
    // Start is called before the first frame update
    void Start()
    {
        loadUI.SetActive(false);
        async = null;
        centerMenuAnim = centerMenu.GetComponent<Animator>();
        questAnim = questMenu.GetComponent<Animator>();
        thisPage = minPage;

        questMenu.SetActive(false);
        QuestInformation.SetActive(false);
        questMenuFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー情報
        playerRankText.text = $"{playerStatusScript.playerRank}";
        playerNameText.text = $"{playerStatusScript.playerName}";
        playerHpText.text   = $"{playerStatusScript.playerHpMax}";
        playerAtkText.text  = $"{playerStatusScript.playerAtk}";
        playerDefText.text  = $"{playerStatusScript.playerDef}";
        playerDiceText.text = $"{playerStatusScript.minDice} ～ {playerStatusScript.maxDice}";
        playerMoneyText.text = $"{playerStatusScript.playerMoney}";

        staminaSlider.value = playerStatusScript.playerStamina;
        staminaSlider.maxValue = playerStatusScript.playerMaxStamina;
        staminaText.text = $"{playerStatusScript.playerStamina} / {playerStatusScript.playerMaxStamina}";

        expSlider.value = playerStatusScript.playerExp;
        if(playerStatusScript.playerRank < 10)
        {
            expSlider.maxValue = playerStatusScript.playerMaxExp;
            expText.text = $"{playerStatusScript.playerExp} / {playerStatusScript.playerMaxExp}";
        }
        else
        {
            expSlider.maxValue = expSlider.value;
            expText.text = $"- Max -";
        }


        //クエスト情報
        questNameText.text = $"～ {questName} ～";
        switch (StageControllerScript.difficulty)
        {
            case 0:
                questDifficultyText.text = "easy";
                break;
            case 1:
                questDifficultyText.text = "normal";
                break;
            case 2:
                questDifficultyText.text = "hard";
                break;
            case 3:
                questDifficultyText.text = "ultimate";
                break;
        }
        questLevelText.text = $"{questLevel}↑";
        questStaminaText.text = $"{playerStatusScript.playerStamina} → {playerStatusScript.playerStamina - questStamina}";

        if(questMenuFlag)
        {
            questMenu.SetActive(false);
            QuestInformation.SetActive(true);
            centerMenuAnim.SetBool("selectQuest", true);
            if(playerStatusScript.playerStamina < questStamina)
            {
                questStartButton.SetActive(false);
            }
            else
            {
                questStartButton.SetActive(true);
            }
        }

        if(questOn)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                GetDirection();
            }
        }
    }

    private void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        if(30 < directionX)//右にフリック
        {
            thisPage++;
            if (thisPage >= maxPage)
            {
                thisPage = minPage;
            }
            questAnim.SetInteger("page", thisPage);
        }
        else if(-30 > directionX)//左にフリック
        {
            thisPage--;
            if (thisPage <= minPage)
            {
                thisPage = maxPage;
            }
            questAnim.SetInteger("page", thisPage);
        }
    }
    public void questButton()
    {
        if(!questOn)
        {
            centerMenuAnim.SetBool("QuestOn", true);

            questButtonText.text = "閉じる";
            questButtonTextEn.text = "Close";

            questOn = true;
        }
        else if(questOn)
        {
            centerMenuAnim.SetBool("QuestOn", false);
            questMenuFlag = false;
            QuestInformation.SetActive(false);
            centerMenuAnim.SetBool("selectQuest", false);

            questButtonText.text = "クエスト";
            questButtonTextEn.text = "Quest";

            questOn = false;
        }
    }
    public void QuestLoad()
    {
        loadUI.SetActive(true);

        StartCoroutine(LoadData());
    }
    public void DifficultyChange()
    {
        if (StageControllerScript.difficulty < 3)
        {
            StageControllerScript.difficulty++;
        }
        else
        {
            StageControllerScript.difficulty = 0;
        }
    }
    public void EnhancedScreen()
    {
        SceneManager.LoadScene("Enhancement Scene");
    }
    public void CreditScene()
    {
        SceneManager.LoadScene("Credit Scene");
    }public void SettingScene()
    {
        SceneManager.LoadScene("Setting Scene");
    }
    IEnumerator LoadData()
    {
        async = SceneManager.LoadSceneAsync(loadQuestScene);

        yield return new WaitForSeconds(0.1f);
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}
