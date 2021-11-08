using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    /*このスクリプトに記述すること*/
    //プレイヤーの移動（ボタン入力）
    //




    public GameObject playerObject; //プレイヤーとなるオブジェクト
    //プレイヤーの上下左右の障害物を判定するフラッグ
    public static bool playerTop, playerLeft, playerRight, playerBottom;

    private Transform playerTransform; //プレイヤーオブジェクトのトランスフォーム
    private Vector3 playerPos;　　　　 //プレイヤーオブジェクトのポジション

    public static int playerMoveNum;

    void Start()
    {
        playerTop = playerLeft = playerRight = playerBottom = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void moveTopButton()//上ボタン
    {
        if(playerStatusScript.stopTime > 0)
        {
            playerStatusScript.stopTime--;
            playerMoveNum--;
        }
        else if(playerMoveNum > 0 && playerTop && playerStatusScript.stopTime == 0)
        {
            playerTransform = this.transform;
            playerPos = playerTransform.position;
            playerPos.z += 2;
            playerMoveNum--;
            playerTransform.position = playerPos;
        }
    }
    public void moveLeftButton()//左ボタン
    {
        if (playerStatusScript.stopTime > 0)
        {
            playerStatusScript.stopTime--;
            playerMoveNum--;
        }
        else if (playerMoveNum > 0 && playerLeft && playerStatusScript.stopTime == 0)
        {
            playerTransform = this.transform;
            playerPos = playerTransform.position;
            playerPos.x -= 2;
            playerMoveNum--;
            playerTransform.position = playerPos;
        }
    }
    public void moveRightButton()//右ボタン
    {
        if (playerStatusScript.stopTime > 0)
        {
            playerStatusScript.stopTime--;
            playerMoveNum--;
        }
        else if (playerMoveNum > 0 && playerRight && playerStatusScript.stopTime == 0)
        {
            playerTransform = this.transform;
            playerPos = playerTransform.position;
            playerPos.x += 2;
            playerMoveNum--;
            playerTransform.position = playerPos;
        }
    }
    public void moveBottomButton()//下ボタン
    {
        if (playerStatusScript.stopTime > 0)
        {
            playerStatusScript.stopTime--;
            playerMoveNum--;
        }
        else if (playerMoveNum > 0 && playerBottom && playerStatusScript.stopTime == 0)
        {
            playerTransform = this.transform;
            playerPos = playerTransform.position;
            playerPos.z -= 2;
            playerMoveNum--;
            playerTransform.position = playerPos;
        }
    }
}
