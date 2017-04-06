﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionComicManager : MonoBehaviour {

    public GameObject comicBackground;
    public GameObject[] fightEndComics;
    public GameObject continueButton;

    public List<List<List<Vector2>>> comicShifts;

    public float panelAppearTime;
    public float continuePromptGrowTime;
    public float continuePromptShrinkTime;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        TurnOffComicPanels();
        InitializeComicShiftArray();
    }

    void TurnOffComicPanels()
    {
        comicBackground.SetActive(false);
        continueButton.SetActive(false);

        for(int i = 0; i <fightEndComics.Length; i++)
        {
            foreach (Transform t in fightEndComics[i].GetComponentsInChildren<Transform>())
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    void InitializeComicShiftArray()
    {
        comicShifts = new List<List<List<Vector2>>>
        {
            /// hallway end comic
            new List<List<Vector2>>{
                new List<Vector2>{1600 * Vector2.left}
            },
            /// cafeteria end comic
            new List<List<Vector2>>
            {
                new List<Vector2> {1600 * Vector2.left }
            },
            /// rooftop end comic
            new List<List<Vector2>>
            {
                new List<Vector2>{ 1600 * Vector2.left, 1600 * Vector2.right }
            },
            /// parking lot end comic
            new List<List<Vector2>>
            {
                new List<Vector2>{ 1600 * Vector2.left, 1600 * Vector2.right }
            },
            /// hell end (win) comic
            new List<List<Vector2>>
            {
                new List<Vector2>{1600 * Vector2.left, 1600 * Vector2.right },
                new List<Vector2>{1600 * Vector2.left, 1600 * Vector2.right }
            }
         };
    }
}
