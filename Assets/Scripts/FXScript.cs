using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXScript : MonoBehaviour {

	public ParticleSystem myParticle;

	void Start () {
		myParticle.Play();
		Destroy(gameObject, myParticle.main.duration);
	}
}
