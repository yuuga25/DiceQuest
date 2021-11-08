using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int minMove; //エネミー最小移動数

    [SerializeField]
    private int maxMove; //エネミー最大移動数

    [SerializeField]
    [Header("[6].幽霊強/")]
    [Header("[3].ウサギ/[4].ウサギ強/[5].幽霊弱/")]
    [Header("[0].テスト/[1].キノコ弱/[2].キノコ")]private int monsterNum; //エネミーのID（モンスターの種類判定などで使う）

    [Header("[1]追従・[2]ランダム")]
    [Header("移動方法")]public int MovingMethod;
    [SerializeField]
    private int EnemyMoveCount; //エネミーの行動回数

    public GameObject player;
    //エネミーの上下左右判定を取得（個別）
    public GameObject enemyTop, enemyleft, enemyRight, enemyBottom;
    //エネミーの上下左右判定フラグ
    public bool top, left, right, bottom;

    private Transform playerTransform;
    private Vector3 playerPos;
    private Transform enemyTransform;
    [SerializeField]
    private GameObject enemyObject;
    private Transform enemyObjectTransform;
    private Vector3 enemyPos, enemyRot;
    private bool enemyMove = true;

    GameObject StageManager; //ステージコントローラー
    GameObject EnemyStatus; //エネミーステータス

    

    public int GetMinMove()
    {
        return minMove;
    }
    public int GetMaxMove()
    {
        return maxMove;
    }
    public int GetMonsterNum()
    {
        return monsterNum;
    }

    public bool GetTop()
    {
        return top;
    }
    public bool GetLeft()
    {
        return left;
    }
    public bool GetRight()
    {
        return right;
    }
    public void GetEnemyMoveCount(int enemyMoveCount)
    {
        this.EnemyMoveCount = enemyMoveCount;
    }

    private void Awake()
    {
        if(StageControllerScript.difficulty >= 2)
        {
            minMove = minMove * 2;
            maxMove = maxMove * 2;
        }
    }
    void Start()
    {
        StageManager = GameObject.Find("GameController");
        EnemyStatus = GameObject.Find("GameController");
    }

    void Update()
    {
        playerTransform = player.transform;
        playerPos = playerTransform.position;
        enemyTransform = this.transform;
        enemyObjectTransform = enemyObject.transform;
        enemyPos = enemyTransform.position;
        enemyRot = enemyObjectTransform.localEulerAngles;

        /*行っていること*/
        //1.上下左右に障害物がないかチェック
        //2.障害物のない方向に、上左右下の順で一歩ずつ移動していく
        //3.フラッグで、0.5fの間を移動ごとに開けている


        //プレイヤーの位置とエネミーの位置で判別
        if (playerPos != enemyPos && 0 < EnemyMoveCount)
        {
            if (MovingMethod == 1)
            {
                #region//上
                //top・エネミーの上へ移動するか判別
                if (playerPos.z > enemyPos.z && enemyMove)
                {
                    if (top)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(topMove());
                    }
                    else if (!top)
                    {
                        if (playerPos.x < enemyPos.x && left)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(leftMove());
                        }
                        else if (playerPos.x > enemyPos.x && right)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(rightMove());
                        }
                        else if (bottom)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(bottomMove());
                        }
                    }
                }
                #endregion
                #region//左
                //left・エネミーの左へ移動するか判別
                else if (playerPos.x < enemyPos.x && enemyMove)
                {
                    if (left)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(leftMove());
                    }
                    else if (!left)
                    {
                        if (playerPos.z < enemyPos.z && bottom)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(bottomMove());
                        }
                        else if (playerPos.z > enemyPos.z && top)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(topMove());
                        }
                        else if (right)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(rightMove());
                        }
                    }
                }
                #endregion
                #region//右
                //right・エネミーの右へ移動するか判別
                else if (playerPos.x > enemyPos.x && enemyMove)
                {
                    if (right)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(rightMove());
                    }
                    else if (!right)
                    {
                        if (playerPos.z > enemyPos.z && top)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(topMove());
                        }
                        else if (playerPos.z < enemyPos.z && bottom)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(bottomMove());
                        }
                        else if (left)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(leftMove());
                        }
                    }
                }
                #endregion
                #region//下
                //bottom・エネミーの下へ移動するか判別
                else if (playerPos.z < enemyPos.z && enemyMove)
                {
                    if (bottom)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(bottomMove());
                    }
                    else if (!bottom)
                    {
                        if (playerPos.x > enemyPos.x && right)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(rightMove());
                        }
                        else if (playerPos.x < enemyPos.x && left)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(leftMove());
                        }
                        else if (top)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(topMove());
                        }
                    }
                }
                else if (!top && !left && !right && !bottom)
                {
                    EnemyMoveCount--;
                }
                #endregion
            }
            else if(MovingMethod == 2)
            {
                int r = Random.Range(1, 41);

                #region//31～40
                if (r > 30 && enemyMove)
                {
                    if (bottom)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(bottomMove());
                    }
                    else if (!bottom)
                    {
                        if (playerPos.x > enemyPos.x && right)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(rightMove());
                        }
                        else if (playerPos.x < enemyPos.x && left)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(leftMove());
                        }
                        else if (top)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(topMove());
                        }
                    }
                }
                #endregion
                #region//21～30
                else if (r > 20 && enemyMove)
                {
                    if (left)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(leftMove());
                    }
                    else if (!left)
                    {
                        if (playerPos.z < enemyPos.z && bottom)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(bottomMove());
                        }
                        else if (playerPos.z > enemyPos.z && top)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(topMove());
                        }
                        else if (right)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(rightMove());
                        }
                    }
                }
                #endregion
                #region//11～20
                else if (r > 10 && enemyMove)
                {
                    if (right)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(rightMove());
                    }
                    else if (!right)
                    {
                        if (playerPos.z > enemyPos.z && top)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(topMove());
                        }
                        else if (playerPos.z < enemyPos.z && bottom)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(bottomMove());
                        }
                        else if (left)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(leftMove());
                        }
                    }
                }
                #endregion
                #region//0～10
                else if (r > 0 && enemyMove)
                {
                    if (top)
                    {
                        EnemyMoveCount--;
                        enemyMove = false;
                        StartCoroutine(topMove());
                    }
                    else if (!top)
                    {
                        if (playerPos.x < enemyPos.x && left)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(leftMove());
                        }
                        else if (playerPos.x > enemyPos.x && right)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(rightMove());
                        }
                        else if (bottom)
                        {
                            EnemyMoveCount--;
                            enemyMove = false;
                            StartCoroutine(bottomMove());
                        }
                    }
                }
                #endregion
            }
        }

        //プレイヤーとエンカウント
        if(playerPos == enemyPos)
        {
            PlayerMoveScript.playerMoveNum = 0;
            EnemyStatusScript.enemyID = monsterNum;

            playerStatusScript.posX = player.transform.position.x;
            playerStatusScript.posY = player.transform.position.y;
            playerStatusScript.posZ = player.transform.position.z;

            StartCoroutine(changeScene());
        }
    }

    IEnumerator topMove()
    {
        yield return new WaitForSeconds(0.3f);
        {
            enemyMove = true;
        }
        if(!top)
        {
            yield break;
        }
        enemyPos.z += 2;
        enemyTransform.position = enemyPos;
        enemyRot.y = 180;
        enemyObjectTransform.localEulerAngles = enemyRot;
    }
    IEnumerator leftMove()
    {
        yield return new WaitForSeconds(0.3f);
        {
            enemyMove = true;
        }
        if(!left)
        {
            yield break;
        }
        enemyPos.x -= 2;
        enemyTransform.position = enemyPos;
        enemyRot.y = 90;
        enemyObjectTransform.localEulerAngles = enemyRot;
    }
    IEnumerator rightMove()
    {
        yield return new WaitForSeconds(0.3f);
        {
            enemyMove = true;
        }
        if(!right)
        {
            yield break;
        }
        enemyPos.x += 2;
        enemyTransform.position = enemyPos;
        enemyRot.y = -90;
        enemyObjectTransform.localEulerAngles = enemyRot;
    }
    IEnumerator bottomMove()
    {
        yield return new WaitForSeconds(0.3f);
        {
            enemyMove = true;
        }
        if(!bottom)
        {
            yield break;
        }
        enemyPos.z -= 2;
        enemyTransform.position = enemyPos;
        enemyRot.y = 0;
        enemyObjectTransform.localEulerAngles = enemyRot;
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(0.5f);
        {
            StageManager.GetComponent<StageControllerScript>().ChangeScene();
            EnemyStatus.GetComponent<EnemyStatusScript>().SetEnemyStatus();

            this.gameObject.SetActive(false);
        }
    }
}
