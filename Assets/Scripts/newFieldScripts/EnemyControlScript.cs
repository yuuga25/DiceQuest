using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlScript : MonoBehaviour
{
    /*このスクリプトに記述すること*/
    //エネミーオブジェクトたちのステージごとの表示切り替え
    //

    [Header("第一ステージに出現するエネミーを入れる")]
    [Header("※必ずエネミーには[tag:Enemy]をつける※")]public GameObject[] firstEnemyObjects;
    [Header("第ニステージに出現するエネミーを入れる")] public GameObject[] secondEnemyObjects;
    [Header("第三ステージに出現するエネミーを入れる")] public GameObject[] thirdEnemyObjects;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < firstEnemyObjects.Length; i++)
        {
            firstEnemyObjects[i].SetActive(true);
        }
        for (int i = 0; i < secondEnemyObjects.Length; i++)
        {
            secondEnemyObjects[i].SetActive(false);
        }
        for (int i = 0; i < thirdEnemyObjects.Length; i++)
        {
            thirdEnemyObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StageControllerScript.clear1)
        {
            if(firstEnemyObjects.Length > 0)
            {
                for (int i = 0; i < firstEnemyObjects.Length; i++)
                {
                    firstEnemyObjects[i].SetActive(false);
                }
            }
            for (int i = 0; i < secondEnemyObjects.Length; i++)
            {
                secondEnemyObjects[i].SetActive(true);
            }
            StageControllerScript.clear1 = false;
        }
        if (StageControllerScript.clear2)
        {
            if(secondEnemyObjects.Length > 0)
            {
                for (int i = 0; i < secondEnemyObjects.Length; i++)
                {
                    secondEnemyObjects[i].SetActive(false);
                }
            }
            
            for (int i = 0; i < thirdEnemyObjects.Length; i++)
            {
                thirdEnemyObjects[i].SetActive(true);
            }
            StageControllerScript.clear2 = false;
        }
        if (StageControllerScript.stageClear)
        {
            if(thirdEnemyObjects.Length > 0)
            {
                for (int i = 0; i < thirdEnemyObjects.Length; i++)
                {
                    thirdEnemyObjects[i].SetActive(false);
                    StageControllerScript.stageClear = false;
                }
            }
        }

    }
}
