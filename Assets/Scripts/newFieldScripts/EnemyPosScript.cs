using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosScript : MonoBehaviour
{
    public bool topPosi, leftPosi, rightPosi, bottomPosi;
    private GameObject Enemy;

    void Start()
    {
        Enemy = transform.parent.gameObject;
        Enemy.GetComponent<EnemyScript>().top = true;
        Enemy.GetComponent<EnemyScript>().left = true;
        Enemy.GetComponent<EnemyScript>().right = true;
        Enemy.GetComponent<EnemyScript>().bottom = true;

        //EnemyScript enemyScript = 
    }
    void OnTriggerStay(Collider other)
    {
        if(topPosi && other.gameObject.tag == "obstacle" || topPosi && other.gameObject.tag == "Enemy" || topPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().top = false;
            //Debug.Log("上に接触しています");
        }
        if(leftPosi && other.gameObject.tag == "obstacle" || leftPosi && other.gameObject.tag == "Enemy" || leftPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().left = false;
            //Debug.Log("左に接触しています");
        }
        if (rightPosi && other.gameObject.tag == "obstacle" || rightPosi && other.gameObject.tag == "Enemy" || rightPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().right = false;
            //Debug.Log("右に接触しています");
        }
        if (bottomPosi && other.gameObject.tag == "obstacle" || bottomPosi && other.gameObject.tag == "Enemy" || bottomPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().bottom = false;
            //Debug.Log("下に接触しています");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (topPosi && other.gameObject.tag == "obstacle" || topPosi && other.gameObject.tag == "Enemy" || topPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().top = true;
        }
        if (leftPosi && other.gameObject.tag == "obstacle" || leftPosi && other.gameObject.tag == "Enemy" || leftPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().left = true;
        }
        if (rightPosi && other.gameObject.tag == "obstacle" || rightPosi && other.gameObject.tag == "Enemy" || rightPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().right = true;
        }
        if (bottomPosi && other.gameObject.tag == "obstacle" || bottomPosi && other.gameObject.tag == "Enemy" || bottomPosi && other.gameObject.tag == "CheckPoint")
        {
            Enemy.GetComponent<EnemyScript>().bottom = true;
        }
    }
}
