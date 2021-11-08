using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private bool ldP = false;
    [SerializeField]
    private bool dP  = false;
    [SerializeField]
    private bool poP = false;
    [SerializeField]
    private bool paP = false;
    [SerializeField]
    private bool flP = false;
    [SerializeField]
    private bool frP = false;
    [SerializeField]
    private bool sP  = false;
    [SerializeField]
    private bool daP = false;

    public void SetldP(bool ldp)
    {
        this.ldP = ldp;
    }
    public void SetdP(bool dp)
    {
        this.dP = dp;
    }
    public void SetpoP(bool pop)
    {
        this.poP = pop;
    }
    public void SetpaP(bool pap)
    {
        this.paP = pap;
    }
    public void SetflP(bool flp)
    {
        this.flP = flp;
    }
    public void SetfrP(bool frp)
    {
        this.frP = frp;
    }
    public void SetsP(bool sp)
    {
        this.sP = sp;
    }
    public void SetdaP(bool dap)
    {
        this.daP = dap;
    }

    private void OnTriggerStay(Collider other)
    {
        if (BattleTurnScript.enemyTurn)
        {
            if (ldP && other.gameObject.tag == "Player")//大ダメージ
            {
                //ダメージ計算式
                float damage = EnemyStatusScript.enemyLevel * 2 / 5 + 2; damage = Mathf.Floor(damage); //エネミーレベル*２/５+２→切り捨て
                damage = damage * 50 * EnemyStatusScript.enemyAtk / playerStatusScript.playerDef; damage = Mathf.Floor(damage); //50*エネミー攻撃力/プレイヤー防御力→切り捨て
                damage = damage / 50 + 2; damage = Mathf.Floor(damage); //damage/50+2→切り捨て
                float random = Random.Range(80, 101); //乱数
                damage = damage * (random / 100); damage = Mathf.Floor(damage); //damage*乱数→切り捨て
                damage = damage * 2; //※ダメージ二倍

                //難易度補正
                switch (StageControllerScript.difficulty)
                {
                    case 0:
                        damage = damage * 1;
                        break;
                    case 1:
                        damage = damage * 1.3f;
                        break;
                    case 2:
                        damage = damage * 1.4f;
                        break;
                    case 3:
                        damage = damage * 1.5f;
                        break;
                }
                damage = Mathf.Floor(damage);

                playerStatusScript.playerHp -= damage;//ダメージ
                BattleTurnScript.playerDamage = (int)damage;

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if(dP && other.gameObject.tag == "Player")//ダメージ
            {
                //ダメージ計算式
                float damage = EnemyStatusScript.enemyLevel * 2 / 5 + 2; damage = Mathf.Floor(damage);//エネミーレベル*２/５+２→切り捨て
                damage = damage * 50 * EnemyStatusScript.enemyAtk / playerStatusScript.playerDef; damage = Mathf.Floor(damage);//50*エネミー攻撃力/プレイヤー防御力→切り捨て
                damage = damage / 50 + 2; damage = Mathf.Floor(damage); //damage/50+2→切り捨て
                float random = Random.Range(80, 101); //乱数
                damage = damage * (random / 100); damage = Mathf.Floor(damage);//damage*乱数→切り捨て

                //難易度補正
                switch (StageControllerScript.difficulty)
                {
                    case 0:
                        damage = damage * 1;
                        break;
                    case 1:
                        damage = damage * 1.3f;
                        break;
                    case 2:
                        damage = damage * 1.4f;
                        break;
                    case 3:
                        damage = damage * 1.5f;
                        break;
                }
                damage = Mathf.Floor(damage);

                playerStatusScript.playerHp -= damage;//ダメージ
                BattleTurnScript.playerDamage = (int)damage;

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if(poP && other.gameObject.tag == "Player")//毒
            {
                if(!playerStatusScript.AbnormalCondition)
                {
                    playerStatusScript.playerPoison = true;
                    playerStatusScript.AbnormalCondition = true;
                }

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if (paP && other.gameObject.tag == "Player")//麻痺
            {
                if (!playerStatusScript.AbnormalCondition)
                {
                    playerStatusScript.playerParalyzed = true;
                    playerStatusScript.AbnormalCondition = true;
                }

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if (flP && other.gameObject.tag == "Player")//火炎
            {
                if (!playerStatusScript.AbnormalCondition)
                {
                    playerStatusScript.playerFlame = true;
                    playerStatusScript.AbnormalCondition = true;
                }

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if (frP && other.gameObject.tag == "Player")//凍結
            {
                if (!playerStatusScript.AbnormalCondition)
                {
                    playerStatusScript.playerFrozen = true;
                    playerStatusScript.AbnormalCondition = true;

                    playerStatusScript.stopTime = Random.Range(1, 5);
                }

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if (sP && other.gameObject.tag == "Player")//睡眠
            {
                if (!playerStatusScript.AbnormalCondition)
                {
                    playerStatusScript.playerSleep = true;
                    playerStatusScript.AbnormalCondition = true;

                    playerStatusScript.stopTime = Random.Range(4, 7);
                }

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if (daP && other.gameObject.tag == "Player")//暗闇
            {
                int r = Random.Range(1, 101);

                if(r <= 60)
                {
                    //ダメージ計算式
                    float damage = EnemyStatusScript.enemyLevel * 2 / 5 + 2; damage = Mathf.Floor(damage);//エネミーレベル*２/５+２→切り捨て
                    damage = damage * 50 * EnemyStatusScript.enemyAtk / playerStatusScript.playerDef; damage = Mathf.Floor(damage);//50*エネミー攻撃力/プレイヤー防御力→切り捨て
                    damage = damage / 50 + 2; damage = Mathf.Floor(damage); //damage/50+2→切り捨て
                    float random = Random.Range(80, 101); //乱数
                    damage = damage * (random / 100); damage = Mathf.Floor(damage);//damage*乱数→切り捨て

                    //難易度補正
                    switch (StageControllerScript.difficulty)
                    {
                        case 0:
                            damage = damage * 1;
                            break;
                        case 1:
                            damage = damage * 1.3f;
                            break;
                        case 2:
                            damage = damage * 1.4f;
                            break;
                        case 3:
                            damage = damage * 1.5f;
                            break;
                    }
                    damage = Mathf.Floor(damage);

                    playerStatusScript.playerHp -= damage;//ダメージ
                    BattleTurnScript.playerDamage = (int)damage;
                }

                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
            else if(BattleTurnScript.countEnemyTurn >= 3)
            {
                BattleTurnScript.enemyTurn = false;
                BattleTurnScript.settingFlag = true;
            }
        }
    }
}
