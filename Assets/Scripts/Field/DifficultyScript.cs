using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
    public static bool easy, normal, hard;
    public GameObject[] goal;
    public GameObject[] easyObstacle, normalObstacle, hardObstacle;
    // Start is called before the first frame update
    void Start()
    {
        easy = normal = hard = false;
        for (int i = 0; i < easyObstacle.Length; i++)
        {
            easyObstacle[i].SetActive(false);
        }
        for (int i = 0; i < normalObstacle.Length; i++)
        {
            normalObstacle[i].SetActive(false);
        }
        for (int i = 0; i < hardObstacle.Length; i++)
        {
            hardObstacle[i].SetActive(false);
        }
        for (int i = 0; i < goal.Length; i++)
        {
            goal[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            easy = false;
            normal = false;
            hard = false;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            easy = true;
            normal = false;
            hard = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            easy = false;
            normal = true;
            hard = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            easy = false;
            normal = false;
            hard = true;
        }

        SetObstacle();
    }

    void SetObstacle()
    {
        //表示
        if(easy || normal || hard)
        {
            for (int i = 0; i < goal.Length; i++)
            {
                goal[i].SetActive(true);
            }
            for (int i = 0; i < easyObstacle.Length; i++)
            {
                easyObstacle[i].SetActive(true);
            }
        }
        if(normal || hard)
        {
            for (int i = 0; i < normalObstacle.Length; i++)
            {
                normalObstacle[i].SetActive(true);
            }
        }
        if(hard)
        {
            for (int i = 0; i < hardObstacle.Length; i++)
            {
                hardObstacle[i].SetActive(true);
            }
        }
    }
}
