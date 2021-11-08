using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosiScript : MonoBehaviour
{
    public bool topPosi, leftPosi, rightPosi, bottomPosi;
    void OnTriggerEnter(Collider other)
    {
        if(topPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.top = false;
        }
        if (leftPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.left = false;
        }
        if (rightPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.right = false;
        }
        if (bottomPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.bottom = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (topPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.top = true;
        }
        if (leftPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.left = true;
        }
        if (rightPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.right = true;
        }
        if (bottomPosi && other.gameObject.tag == "obstacle")
        {
            //TurnScript.bottom = true;
        }
    }
}
