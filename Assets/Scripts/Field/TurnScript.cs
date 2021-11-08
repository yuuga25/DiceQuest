using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnScript : MonoBehaviour
{
    public GameObject[] enemy;
    public EnemyScript[] enemies;
    public static bool turn = true;
    public static bool EnemyTurn;
    public GameObject player;

    private int enemyDice;
    public static bool enemyTop, enemyLeft, enemyRight, enemyBottom;
    bool changeEnemy;
    int enemyNo;
    int enemyQu = -1;

    public static int MonsterNum;
    public string sceneName;

    GameObject topPosiObject, leftPosiObject, rightPosiObject, bottomPosiObject;
    public GameObject topPosi, leftPosi, rightPosi, bottomPosi;

    private Transform playerTransform;
    private Transform enemtTransform;
    private Vector3 playerPos;
    private Vector3 enemyPos;


    //Loadingの素材
    //非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    //シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject loadUI;
    //読み込み率を表示するスライダー
    [SerializeField]
    private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        enemyTop = enemyLeft = enemyRight = enemyBottom = true;
        loadUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(turn)
        {
            EnemyTurn = false;
            enemyNo = enemy.Length;
            changeEnemy = true;
            enemyQu = -1;
        }
        else if(EnemyTurn)
        {
            turn = false;

            if(playerPos != enemyPos && 0 < enemyDice)
            {
                if (playerPos.z < enemyPos.z && enemyDice > 0 && enemyBottom)
                {
                    enemyDice--;
                    enemyPos.z -= 2;
                    enemtTransform.position = enemyPos;
                }
                else if (playerPos.x > enemyPos.x && enemyDice > 0 && enemyRight)
                {
                    enemyDice--;
                    enemyPos.x += 2;
                    enemtTransform.position = enemyPos;
                }
                else if (playerPos.x < enemyPos.x && enemyDice > 0 && enemyLeft)
                {
                    enemyDice--;
                    enemyPos.x -= 2;
                    enemtTransform.position = enemyPos;
                }
                else if (playerPos.z > enemyPos.z && enemyDice > 0 && enemyTop)
                {
                    enemyDice--;
                    enemyPos.z += 2;
                    enemtTransform.position = enemyPos;
                }

                if (playerPos.z == enemyPos.z && enemyDice > 0 && !enemyBottom)
                {
                    for (; enemyBottom == true; )
                    {
                        if (enemyRight || !enemyLeft)
                        {
                            enemyDice--;
                            enemyPos.x += 2;
                            Debug.Log("右");
                            enemtTransform.position = enemyPos;
                        }
                        else if (enemyLeft || !enemyRight)
                        {
                            enemyDice--;
                            enemyPos.x -= 2;
                            Debug.Log("左");
                            enemtTransform.position = enemyPos;
                        }
                        else if (!enemyLeft && !enemyRight && enemyTop)
                        {
                            enemyDice--;
                            enemyPos.z += 2;
                            Debug.Log("上");
                            enemtTransform.position = enemyPos;
                        }
                        else if (!enemyLeft && !enemyRight && !enemyTop)
                        {
                            enemyDice--;
                            enemtTransform.position = enemyPos;
                        }

                        if(enemyDice <= 0)
                        {
                            break;
                        }
                    }
                }
                enemtTransform.position = enemyPos;
            }

            if (changeEnemy)
            {
                enemyTurn();
                enemyNo--;
                changeEnemy = false;
            }

            if(playerPos == enemyPos || 0 >= enemyDice)
            {
                if(playerPos == enemyPos)
                {
                    loadUI.SetActive(true);
                    StartCoroutine(LoadData());
                }
                //topPosiObject.SetActive(false);
                //leftPosiObject.SetActive(false);
                //rightPosiObject.SetActive(false);
                //bottomPosiObject.SetActive(false);
                if (0 < enemyNo)
                {
                    changeEnemy = true;
                }
                else
                {
                    Invoke("ChangeTurn", 1.5f);
                }

            }
        }

        if (PlayerScript.plaerMove > 0)
        {
            topPosi.SetActive(true);
            leftPosi.SetActive(true);
            rightPosi.SetActive(true);
            bottomPosi.SetActive(true);
        }
        else if (PlayerScript.plaerMove <= 0)
        {
            topPosi.SetActive(false);
            leftPosi.SetActive(false);
            rightPosi.SetActive(false);
            bottomPosi.SetActive(false);
        }
    }

    void enemyTurn()
    {
        enemyQu++;
        int minMove = enemies[enemyQu].GetMinMove();
        int maxMove = enemies[enemyQu].GetMaxMove();
        enemyDice = Random.Range(minMove, maxMove + 1);
        playerTransform = player.transform;
        enemtTransform = enemy[enemyQu].transform;
        playerPos = playerTransform.position;
        enemyPos = enemtTransform.position;
        topPosiObject = enemy[enemyQu].transform.Find("TopPosi").gameObject;
        leftPosiObject = enemy[enemyQu].transform.Find("LeftPosi").gameObject;
        rightPosiObject = enemy[enemyQu].transform.Find("RightPosi").gameObject;
        bottomPosiObject = enemy[enemyQu].transform.Find("BottomPosi").gameObject;
        //topPosiObject.SetActive(true);
        //leftPosiObject.SetActive(true);
        //rightPosiObject.SetActive(true);
        //bottomPosiObject.SetActive(true);
    }

    void ChangeTurn()
    {
        EnemyTurn = false;
        turn = true;
    }

    IEnumerator LoadData()
    {
        yield return new WaitForSeconds(1f);

        //シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(sceneName);

        //読み込みが終わるまで進歩状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}
