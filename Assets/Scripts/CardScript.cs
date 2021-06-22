using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardScript : MonoBehaviour
{
    private GameObject Controller;
    private GameController ControllerScript;
    public GameObject Partcl;
    public Image Image;
    private void Start()
    {
        Controller = GameObject.Find("[GameController]");
        ControllerScript = Controller.GetComponent<GameController>();
    }

    public void Click()
    {
        if (Image.sprite.name == ControllerScript.QuestText)
        {
            Partcl.GetComponent<UIParticleSystem>().Kvak();
            Image.transform.DOShakeScale(2.0f, strength: new Vector3(0.5f, 0.5f, 0), vibrato: 3,
                randomness: 0, fadeOut: true);
            ControllerScript.CurrentLevel++;
            ControllerScript.StartLevel();
        }
        else
        {
            Image.transform.DOShakePosition(4.0f, strength: new Vector3(5, 2, 0), vibrato: 5, randomness: 3, snapping: false, fadeOut: true);
        }
    }
}
