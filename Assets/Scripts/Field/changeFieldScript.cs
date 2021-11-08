using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeFieldScript : MonoBehaviour
{
    public GameObject player;
    public static bool clear1, clear2, clear3;
    public int XstartField2, ZstartField2, XstartField3, ZstartField3;

    public new GameObject camera;
    private Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        clear1 = clear2 = clear3 = false;
        anim = camera.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clear1 || clear2)
        {
            ChangeField();
        }
    }

    void ChangeField()
    {
        Transform myTransform = player.transform;
        Vector3 pos = myTransform.position;
        if (clear1)
        {
            pos.x = XstartField2;
            pos.z = ZstartField2;
            anim.SetBool("clear1", true);
            PlayerScript.plaerMove = 0;
            clear1 = false;
        }
        else if(clear2)
        {
            pos.x = XstartField3;
            pos.z = ZstartField3;
            anim.SetBool("clear2", true);
            PlayerScript.plaerMove = 0;
            clear2 = false;
        }
        myTransform.position = pos;
    }
}
