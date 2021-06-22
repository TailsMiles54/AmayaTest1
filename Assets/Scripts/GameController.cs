using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [HideInInspector]public int CurrentLevel = 1;
    public GameObject Quest;
    public string QuestText;
    public GameObject[] Cards;
    public Sprite[] SpritesType;
    public GameObject RestartScreen;
    public Sprite jjj;
    public bool check;
    [HideInInspector] List <Sprite> SpritesOnLevel = new List<Sprite>();
    [HideInInspector] List <Sprite> QuestSprite = new List<Sprite>();
    
    [Serializable]
    public struct MyStruct
    {
        public string Name;
        public Sprite[] Images;
    }

    public MyStruct[] DataType;

    private void Start()
    {
        Refresh();
        StartLevel();
    }

    public void Clean()
    {
        StartCoroutine(Restart());
    }

    public IEnumerator Restart()
    {
        Quest.GetComponent<Text>().DOFade(0, 0);
        CurrentLevel = 1;
        for (int i = 0; i < 9; i++)
        {
            Cards[i].SetActive(false);
        }
        Refresh();
        StartLevel();
        RestartScreen.GetComponent<Image>().DOFade(0, 2);
        RestartScreen.transform.Find("Restart").GetComponent<Image>().DOFade(0, 2);
        yield return new WaitForSeconds(2f);
        RestartScreen.SetActive(false);
    }

    public void Refresh()
    {
        QuestSprite.Clear();
        SpritesType = DataType[Random.Range(0, DataType.Length)].Images;
        for (int i = 0; i < SpritesType.Length; i++)
        {
           //SpritesOnLevel.Add(SpritesType[i]);
            QuestSprite.Add(SpritesType[i]);
        }
    }
    
    public void StartLevel()
    {
        SpritesOnLevel.Clear();
        for (int i = 0; i < SpritesType.Length; i++)
        {
            SpritesOnLevel.Add(SpritesType[i]); 
            //QuestSprite.Add(SpritesType[i]);
        }
        if (CurrentLevel > 3)
        {
            RestartScreen.SetActive(true);
            RestartScreen.GetComponent<Image>().DOFade(1, 2);
            RestartScreen.transform.Find("Restart").GetComponent<Image>().DOFade(1, 2);
        }
        else
        {

            for (int i = 0; i < CurrentLevel * 3; i++)
            {
                jjj = SpritesOnLevel[Random.Range(0, SpritesOnLevel.Count)];
                Cards[i].GetComponent<CardScript>().Image.sprite = jjj;
                Cards[i].SetActive(true);
                SpritesOnLevel.Remove(jjj);
                if (CurrentLevel == 1)
                {
                    Cards[i].transform.DOShakeScale(2.0f, strength: new Vector3(0.5f, 0.5f, 0), vibrato: 3,
                        randomness: 0, fadeOut: true);
                }
            }

            check = false;
            while (check != true)
            {
                for (int i = 0; i < QuestSprite.Count; i++)
                {
                    if (Cards[Random.Range(0, CurrentLevel * 3)].GetComponent<CardScript>().Image.sprite.name ==
                        QuestSprite[i].name)
                    {
                        SpritesOnLevel.Remove(jjj);
                        QuestSprite.Remove(jjj);//список задач
                        QuestText = Cards[Random.Range(0, CurrentLevel * 3)].GetComponent<CardScript>().Image.sprite.name;
                        Quest.GetComponent<Text>().text = "Find " + QuestText;
                        Quest.GetComponent<Text>().DOFade(1, 4);
                        check = true;
                        break;
                    }
                }
            }
        }
    }
}
