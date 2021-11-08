using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosiScript : MonoBehaviour
{
    public bool topPosi, leftPosi, rightPosi, bottomPosi;
    void Start()
    {

    }
    void Update()
    {


    }
    void OnTriggerEnter(Collider other)
    {
        if(topPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.top = false;
            Debug.Log("上");
        }
        else if (leftPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.left = false;
            Debug.Log("左");
        }
        else if (rightPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.right = false;
            Debug.Log("右");
        }
        else if (bottomPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.bottom = false;
            Debug.Log("下");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (topPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.top = true;
        }
        else if (leftPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.left = true;
        }
        else if (rightPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.right = true;
        }
        else if (bottomPosi && other.gameObject.tag == "obstacle")
        {
            PlayerScript.bottom = true;
        }
    }
}
