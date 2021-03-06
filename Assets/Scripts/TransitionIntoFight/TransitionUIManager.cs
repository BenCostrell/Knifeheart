﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionUIManager : MonoBehaviour {

	public GameObject fightBackground;
    public GameObject[] wordGroups;

	public GameObject transitionUI;
    public GameObject abilityBlurbs;
    public float blurbScaleInTime;
    public float blurbDelayBeforeFlipping;
    public float blurbFlipTime;
    public float blurbXSpacing;
    public float blurbYSpacing;
    public float blurbScale;
    public float blurbInitialOffset;
    public float blurbXStagger;
    public float blurbStaggerTime;
    public int numBlurbRotations;

	public GameObject readyPrompt_P1;
	public GameObject readyPrompt_P2;
	public GameObject ready_P1;
	public GameObject ready_P2;

    public Sprite[] blurbBoxes;

    [HideInInspector]
    public List<Dialogue>[] dialoguesAccumulated;
    [HideInInspector]
    public Dictionary<Ability.Type, string> abilityNameDict;
    [HideInInspector]
    public List<GameObject>[] blurbAbilityBoxes;

    public float fightBackgroundSlideInTime;
	public float fightWordGrowthTime;
	public float fightWordStaggerTime;
	public float readyPromptGrowTime;
	public float readyPromptShrinkTime;
	public float uiScaleOutTime;
	public float transitionEndTime;


	// Use this for initialization
	void Start () {
        dialoguesAccumulated = new List<Dialogue>[2]
        {
            new List<Dialogue>(),
            new List<Dialogue>()
        };
        InitializeAbilityNameDict();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUpUI()
    {
        fightBackground.SetActive(false);
        SetFightWordsStatus(false);
        transitionUI.GetComponent<RectTransform>().localScale = Vector2.one;
        ready_P1.GetComponent<RectTransform>().localScale = Vector2.one;
        ready_P2.GetComponent<RectTransform>().localScale = Vector2.one;
        readyPrompt_P1.SetActive(false);
        readyPrompt_P2.SetActive(false);
        ready_P1.SetActive(false);
        ready_P2.SetActive(false);
        transitionUI.SetActive(false);
    }

	public void SetFightWordsStatus(bool active){
		foreach(GameObject wordGroup in wordGroups)
        {
            foreach(Image wordImage in wordGroup.GetComponentsInChildren<Image>())
            {
                wordImage.gameObject.SetActive(active);
            }
        }
    }

    void InitializeAbilityNameDict()
    {
        abilityNameDict = new Dictionary<Ability.Type, string>
        {
            { Ability.Type.Blink, "Blink" },
            { Ability.Type.Fireball, "Fireball" },
            { Ability.Type.Lunge, "Lunge" },
            { Ability.Type.Pull, "Pull" },
            { Ability.Type.Shield, "Shield" },
            { Ability.Type.Sing, "Sing" },
            { Ability.Type.Wallop, "Wallop" }
        };
    }
}
