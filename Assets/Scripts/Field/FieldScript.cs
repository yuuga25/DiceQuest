using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldScript : MonoBehaviour
{
    public float WaitTime;
    public GameObject player;
    GameObject[] grassObjects;
    GameObject[] stoneObjects;
    public Material[] Material; //0=grass, 1=stone

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        grassObjects = GameObject.FindGameObjectsWithTag("grass");
        for (int i = 0; i < grassObjects.Length; i++)
        {
            grassObjects[i].GetComponent<Renderer>().material = Material[0];
        }
        stoneObjects = GameObject.FindGameObjectsWithTag("stone");
        for(int i = 0; i < stoneObjects.Length; i++)
        {
            stoneObjects[i].GetComponent<Renderer>().material = Material[1];
        }
        
        StartCoroutine("Active");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(WaitTime);
        player.SetActive(true);
    }
}
