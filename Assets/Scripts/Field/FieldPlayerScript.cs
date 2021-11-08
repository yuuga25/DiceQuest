using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldPlayerScript : MonoBehaviour
{
    public Slider HpSlider;
    public Text playerHpText;
    public GameObject RollBottnCover, ItemBottonCover, TurnEndButtonCover;
    public GameObject playerPanelCover;
    public static int diceNum;
    public Text diceNumText;
    // Start is called before the first frame update
    void Start()
    {
        HpSlider.value = playerStatusScript.playerHp;
        HpSlider.maxValue = playerStatusScript.playerHpMax;
        playerHpText.text = ($"{playerStatusScript.playerHp}/{playerStatusScript.playerHpMax}");

        ItemBottonCover.SetActive(true);
        TurnEndButtonCover.SetActive(true);
        Invoke("OneTurn", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        HpSlider.value = playerStatusScript.playerHp;
        HpSlider.maxValue = playerStatusScript.playerHpMax;
        playerHpText.text = ($"{playerStatusScript.playerHp}/{playerStatusScript.playerHpMax}");

        diceNumText.text = ($"{PlayerScript.plaerMove}");

        if (TurnScript.turn)
        {
            ItemBottonCover.SetActive(false);
            playerPanelCover.SetActive(false);
        }
        if(TurnScript.EnemyTurn)
        {
            playerPanelCover.SetActive(true);
            RollBottnCover.SetActive(false);
        }
        if(PlayerScript.plaerMove <= 0)
        {
            ItemBottonCover.SetActive(true);
        }
        if(PlayerScript.plaerMove <= 0 && TurnScript.EnemyTurn)
        {
            TurnEndButtonCover.SetActive(true);
        }
    }

    void OneTurn()
    {
        RollBottnCover.SetActive(false);
    }

    public void playerMove()
    {
        diceNum = Random.Range(playerStatusScript.minDice, playerStatusScript.maxDice + 1);
        TurnEndButtonCover.SetActive(false);
        RollBottnCover.SetActive(true);
    }

    public void TurnEnd()
    {
        Invoke("ChangeTurn", 1f);
    }

    void ChangeTurn()
    {
        PlayerScript.plaerMove = 0;
        TurnScript.EnemyTurn = true;
        TurnScript.turn = false;
    }
}
