using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

	public GameObject CirclePrefab;
	public UnitArray[] PlayerUnits;
	public LayerMask HittableLayer;
	public LayerMask GroundLayer;

	[Serializable]
	public struct UnitArray {
		public KeyCode MyKey;
		public Unit MyUnit;
	}

	Camera myCamera;

	void Start () {
		myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update () {

		foreach (UnitArray currentUnit in PlayerUnits) {
			if (Input.GetKeyDown(currentUnit.MyKey)) {
				Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, HittableLayer)) {
					if ( GroundLayer == (GroundLayer | (1 << hit.collider.gameObject.layer))) {
						currentUnit.MyUnit.Destination(hit.point);
						Instantiate(CirclePrefab, hit.point, Quaternion.identity);
					}
				}
			}
		}
	}
}
