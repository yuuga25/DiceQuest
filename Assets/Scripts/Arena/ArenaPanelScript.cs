using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaPanelScript : MonoBehaviour
{
    public Material sfPMaterial; //生存パネルマテリアル
    public Material ldPMaterial; //大ダメージパネルマテリアル
    public Material dPMaterial; //ダメージパネルマテリアル
    public Material poPMaterial; //毒パネルマテリアル
    public Material paPMaterial; //麻痺パネルマテリアル
    public Material flPMaterial; //火炎パネルマテリアル
    public Material frPMaterial; //凍結パネルマテリアル
    public Material sPMaterial; //睡眠パネルマテリアル
    public Material daPMaterial; //暗闇パネルマテリアル

    GameObject[] allPanels; //全パネル
    GameObject[] allFieldPanels; //全反応パネル
    GameObject[] fieldPanel; //フィールドのパネル
    GameObject[] materialPanel; //マテリアルを適応させるパネル

    public static bool changePanelFlag;

    // Start is called before the first frame update
    void Start()
    {
        allPanels = GameObject.FindGameObjectsWithTag("BattlePanel");
        allFieldPanels = GameObject.FindGameObjectsWithTag("SafePoint");
        fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
        materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
        for (int i = 0; i < allPanels.Length; i++)
        {
            allPanels[i].SetActive(true);
            allFieldPanels[i].SetActive(true);
        }
        for (int m = 0; m < materialPanel.Length; m++)
        {
            materialPanel[m].GetComponent<Renderer>().material = sfPMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (changePanelFlag)
        {
            changePanelFlag = false;
            //生存パネル設置
            if (materialPanel.Length < allPanels.Length)
            {
                materialPanel = allPanels;
                fieldPanel = allFieldPanels;
                //fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
                for (int m = 0; m < materialPanel.Length; m++)
                {
                    materialPanel[m].GetComponent<Renderer>().material = sfPMaterial;

                    fieldPanel[m].GetComponent<PanelScript>().SetldP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetdP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetpoP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetpaP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetflP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetfrP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetsP(false);
                    fieldPanel[m].GetComponent<PanelScript>().SetdaP(false);
                }
            }
            StartCoroutine(changeLDP());
            StartCoroutine(changeDP());
            StartCoroutine(changePOP());
            StartCoroutine(changePAP());
            StartCoroutine(changeFLP());
            StartCoroutine(changeFRP());
            StartCoroutine(changeSP());
            StartCoroutine(changeDAP());

            for (int i = 0; i < allPanels.Length; i++)
            {
                allPanels[i].SetActive(true);
                allFieldPanels[i].SetActive(true);
            }

            StartCoroutine(changeTurn());
        }
    }

    IEnumerator changeTurn()
    {
        yield return new WaitForSeconds(0.5f);
        {
            ArenaTurnScript.arenaPlayerSet = true;
        }
    }

    IEnumerator changeLDP()
    {

        //大ダメージパネル設置
        if (ArenaEnemyScript.AldPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.AldPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = ldPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetldP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changeDP()
    {
        //ダメージパネル設置
        if (ArenaEnemyScript.AdPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.AdPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = dPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetdP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changePOP()
    {
        //毒パネル設置
        if (ArenaEnemyScript.ApoPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.ApoPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = poPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetpoP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changePAP()
    {
        //麻痺パネル設置
        if (ArenaEnemyScript.ApaPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.ApaPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = paPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetpaP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changeFLP()
    {
        //火炎パネル設置
        if (ArenaEnemyScript.AflPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.AflPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = flPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetflP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changeFRP()
    {
        //凍結パネル設置
        if (ArenaEnemyScript.AfrPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.AfrPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = frPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetfrP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changeSP()
    {
        //睡眠パネル設置
        if (ArenaEnemyScript.AsPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.AsPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = sPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetsP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }
    IEnumerator changeDAP()
    {
        //暗闇パネル設置
        if (ArenaEnemyScript.AdaPEnemy > 0)
        {
            for (int m = 0; m < ArenaEnemyScript.AdaPEnemy; m++)
            {
                int r = Random.Range(0, materialPanel.Length);
                materialPanel[r].GetComponent<Renderer>().material = daPMaterial;
                fieldPanel[r].GetComponent<PanelScript>().SetdaP(true);
                materialPanel[r].SetActive(false);
                fieldPanel[r].SetActive(false);
                materialPanel = GameObject.FindGameObjectsWithTag("BattlePanel");
                fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
            }
            yield return new WaitForSeconds(0f);
        }
    }

    public void endBattle()
    {
        materialPanel = allPanels;
        fieldPanel = allFieldPanels;
        //fieldPanel = GameObject.FindGameObjectsWithTag("SafePoint");
        for (int m = 0; m < materialPanel.Length; m++)
        {
            materialPanel[m].GetComponent<Renderer>().material = sfPMaterial;

            fieldPanel[m].GetComponent<PanelScript>().SetldP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetdP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetpoP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetpaP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetflP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetfrP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetsP(false);
            fieldPanel[m].GetComponent<PanelScript>().SetdaP(false);
        }
    }
}
