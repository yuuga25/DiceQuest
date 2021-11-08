using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleUIScript : MonoBehaviour
{
    public Slider playerHpSlider;
    public Text playerHpText;
    public Slider enemyHpSlider;
    void Update()
    {
        playerHpSlider.value = playerStatusScript.playerHp;
        playerHpSlider.maxValue = playerStatusScript.playerHpMax;
        playerHpText.text = ($"{playerStatusScript.playerHp}/{playerStatusScript.playerHpMax}");

        enemyHpSlider.value = EnemyStatusScript.enemyHp;
        enemyHpSlider.maxValue = EnemyStatusScript.enemyHpMax;
    }
}
