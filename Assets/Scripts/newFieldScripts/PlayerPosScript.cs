using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosScript : MonoBehaviour
{
    public bool top, left, right, bottom; //ポジションのチェックフラッグ
    void OnTriggerEnter(Collider other)
    {
        if(top && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerTop = false;
        }
        if(left && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerLeft = false;
        }
        if(right && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerRight = false;
        }
        if(bottom && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerBottom = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (top && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerTop = true;
        }
        if (left && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerLeft = true;
        }
        if (right && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerRight = true;
        }
        if (bottom && other.gameObject.tag == "obstacle")
        {
            PlayerMoveScript.playerBottom = true;
        }
    }
}
