using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 pos;

    //public Text massCountText;
    public static bool top, left, right, bottom;

    public static int plaerMove;
    // Start is called before the first frame update
    void Start()
    {
        top  = true;
        left = true;
        right = true;
        bottom = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if(FieldPlayerScript.diceNum > 0)
        {
            plaerMove = FieldPlayerScript.diceNum;
            FieldPlayerScript.diceNum = 0;
        }
        if(battleScript.diceNum > 0)
        {
            plaerMove = battleScript.diceNum;
            battleScript.diceNum = 0;
        }
        //massCountText.text = $"playerMove:{plaerMove}";
    }

    void PlayerMove()
    {
        myTransform = this.transform;
        pos = myTransform.position;
        if (Input.GetKeyDown(KeyCode.W) && plaerMove != 0 && top)
        {
            pos.z += 2;
            plaerMove--;
        }
        else if (Input.GetKeyDown(KeyCode.A) && plaerMove != 0 && left)
        {
            pos.x -= 2;
            plaerMove--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && plaerMove != 0 && right)
        {
            pos.x += 2;
            plaerMove--;
        }
        else if (Input.GetKeyDown(KeyCode.S) && plaerMove != 0 && bottom)
        {
            pos.z -= 2;
            plaerMove--;
        }
        myTransform.position = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal1" && changeFieldScript.clear1 == false)
        {
            changeFieldScript.clear1 = true;
        }
        else if (other.gameObject.tag == "Goal2" && changeFieldScript.clear2 == false)
        {
            changeFieldScript.clear2 = true;
        }
    }
    public void moveTop()
    {
        if(top && plaerMove > 0)
        {
            myTransform = this.transform;
            pos = myTransform.position;
            pos.z += 2;
            plaerMove--;
            myTransform.position = pos;
        }
    }
    public void moveLeft()
    {
        if (left && plaerMove > 0)
        {
            myTransform = this.transform;
            pos = myTransform.position;
            pos.x -= 2;
            plaerMove--;
            myTransform.position = pos;
        }
    }
    public void moveRight()
    {
        if(right && plaerMove > 0)
        {
            myTransform = this.transform;
            pos = myTransform.position;
            pos.x += 2;
            plaerMove--;
            myTransform.position = pos;
        }
    }
    public void moveBottom()
    {
        if(bottom && plaerMove > 0)
        {
            myTransform = this.transform;
            pos = myTransform.position;
            pos.z -= 2;
            plaerMove--;
            myTransform.position = pos;
        }
    }
}
