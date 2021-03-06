﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Attack {

	public float speed;
    private float angle;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (!GetComponent<Renderer> ().isVisible && GetComponent<SpriteRenderer>().enabled) {
			Destroy (gameObject);
		}
	}

	public override void Init(Player player){
        GetComponent<SpriteRenderer>().enabled = false;

		base.Init (player);
        angle = parentPlayer.effectiveRotation;
    }

	protected override Vector3 GetDirectionHit(Player playerHit){
		return GetComponent<Rigidbody2D> ().velocity.normalized;
	}

	protected override void HitPlayer(Player player){
		base.HitPlayer (player);
		Destroy (gameObject);
	}

    public override void SetActive()
    {
		base.SetActive();
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponentInChildren<ParticleSystem>().Play();

        Vector3 direction = new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad));

        GetComponent<Rigidbody2D>().velocity = speed * direction;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += 1.5f * direction;
    }

    public override void OnCastFinish ()
	{
	}
}
