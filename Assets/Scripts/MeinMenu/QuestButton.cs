using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public string loadSceneName;
    [Header("クエストの名前（日本語）")]public string questNameJA;
    [Header("推奨ランク")] public int questLevel;
    [Header("消費スタミナ")] public int questStamina;
    public void ClickToQuestButton()
    {
        MainMenu.loadQuestScene = loadSceneName;
        MainMenu.questName = questNameJA;
        MainMenu.questLevel = this.questLevel;
        MainMenu.questStamina = this.questStamina;

        MainMenu.questMenuFlag = true;
    }
}
