using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleScript : MonoBehaviour
{
    public Material[] materials;
    GameObject[] battlePanels;
    public int savePosi;
    GameObject[] SafePoints;
    GameObject[] ActiveSafePoints;

    bool playerTurn, setSafePoint, damage;
    public GameObject Text;
    public static int diceNum;


    public GameObject playerPanel;
    public GameObject diceBottonCover , itemCover, escapeCover, skilCover, turnEndCover;
    public GameObject AttackBotton;
    public Text DiceNumText;
    public GameObject topPosi, leftPosi, rightPosi, bottomPosi;
    // Start is called before the first frame update
    void Start()
    {
        SafePoints = GameObject.FindGameObjectsWithTag("SafePoint");
        for(int i = 0; i < SafePoints.Length; i++)
        {
            SafePoints[i].SetActive(false);
        }
        playerTurn = true;
        playerPanel.SetActive(false);
        diceBottonCover.SetActive(false);
        itemCover.SetActive(true);
        escapeCover.SetActive(true);
        skilCover.SetActive(true);
        turnEndCover.SetActive(true);
        AttackBotton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTurn)
        {
            playerPanel.SetActive(false);
            if(!setSafePoint)
            {
                for(int j = 0;j < 10; j++)
                {
                    Invoke("SetSafePoint", 0.1f);
                }
            }
            if(PlayerScript.plaerMove <= 0)
            {
                itemCover.SetActive(true);
                escapeCover.SetActive(true);
                skilCover.SetActive(true);
            }
        }
        if(!playerTurn)
        {
            setSafePoint = false;
            diceBottonCover.SetActive(false);
            itemCover.SetActive(true);
            escapeCover.SetActive(true);
            skilCover.SetActive(true);
            turnEndCover.SetActive(true);
            playerPanel.SetActive(true);
            AttackBotton.SetActive(false);
        }
        DiceNumText.text = ($"{PlayerScript.plaerMove}");

        if (PlayerScript.plaerMove > 0)
        {
            topPosi.SetActive(true);
            leftPosi.SetActive(true);
            rightPosi.SetActive(true);
            bottomPosi.SetActive(true);
        }
        else if (PlayerScript.plaerMove <= 0)
        {
            topPosi.SetActive(false);
            leftPosi.SetActive(false);
            rightPosi.SetActive(false);
            bottomPosi.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(!playerTurn)
        {
            if(other.gameObject.tag == "SafePoint" && !damage)
            {
                Invoke("Damage", 1f);
                damage = true;
            }
            playerTurn = true;
        }
    }

    void Damage()
    {
        playerStatusScript.playerHp -= 1;
    }

    void SetSafePoint()
    {
        battlePanels = GameObject.FindGameObjectsWithTag("BattlePanel");
        for (int i = 0; i < SafePoints.Length; i++)
        {
            SafePoints[i].SetActive(false);
        }
        ActiveSafePoints = GameObject.FindGameObjectsWithTag("SafePoint");
        for (int i = 0; i < battlePanels.Length; i++)
        {
            battlePanels[i].GetComponent<Renderer>().material = materials[0];
        }

        for (; ActiveSafePoints.Length < savePosi;)
        {
            int r2 = Random.Range(0, 49);
            battlePanels[r2].GetComponent<Renderer>().material = materials[1];
            SafePoints[r2].SetActive(true);
            ActiveSafePoints = GameObject.FindGameObjectsWithTag("SafePoint");
        }
        setSafePoint = true;
        damage = false;
    }

    public void RollDice()
    {
        diceNum = Random.Range(playerStatusScript.minDice, playerStatusScript.maxDice + 1);
        itemCover.SetActive(false);
        escapeCover.SetActive(false);
        skilCover.SetActive(false);
        turnEndCover.SetActive(false);
        DiceNumText.text = ($"{PlayerScript.plaerMove}");
        AttackBotton.SetActive(true);
        playerTurn = true;
    }
    public void TurnEnd()
    {
        playerTurn = false;
        PlayerScript.plaerMove = 0;
        Invoke("ChangeTurn", 1.5f);
    }
    public void Attack()
    {
        if(PlayerScript.plaerMove > 0)
        {
            int damage = (int)playerStatusScript.playerAtk * PlayerScript.plaerMove / 10;
            if(damage > EnemyStatusScript.enemyDef)
            {
                EnemyStatusScript.enemyHp -= damage - EnemyStatusScript.enemyDef;
            }
            else
            {
                EnemyStatusScript.enemyHp -= damage;
            }
            PlayerScript.plaerMove = 0;
            playerTurn = false;
            Invoke("ChangeTurn", 1.5f);
        }
        else
        {
            playerTurn = false;
            PlayerScript.plaerMove = 0;
            Invoke("ChangeTurn", 1.5f);
        }
    }

    void ChangeTurn()
    {
        playerTurn = true;
    }
}
