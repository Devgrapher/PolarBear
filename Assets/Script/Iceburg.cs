﻿using UnityEngine;
using System.Collections;

public class Iceburg : MonoBehaviour {
	public float initialScaleFix = 0;
	public bool melt = false;
	public bool move = false;

	int screenWith = 14;
	int screenWithFix = -1; // to melt ice early
	float initialScale = 1;
	float destroyDeadlineScale = 0.2f; // Delete itself if the scale reaches it.

	public float MoveMeterPerSec() {
		return (screenWith + screenWithFix) / LevelManager.instance.IceMeltTimeSec;
	}

	void Start () {
		initialScale = transform.localScale.x;
		initialScale += initialScaleFix;
	}

	float MeltScalePerSec() {
		return initialScale / LevelManager.instance.IceMeltTimeSec;
	}

	void MovePerFrame() {
		float translation = Time.deltaTime * MoveMeterPerSec();
		transform.Translate(-translation, 0, 0);
	}

	void MeltPerFrame() {
		float shrink = Time.deltaTime * MeltScalePerSec();
		transform.localScale += new Vector3 (-shrink, 0, -shrink);

		if (transform.localScale.x <= destroyDeadlineScale) {
			Destroy (this.gameObject);
		}
	}
		
	// Update is called once per frame
	void Update () {
		if (move)
			MovePerFrame ();
		if (melt)
			MeltPerFrame ();
	}

	void OnDestroy() {
		IceburgWatcher.instance.RemoveIceburg (this);
	}
}
