using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

	public Camera MyCamera;
	public UnitArray[] PlayerUnits;
	public LayerMask HittableLayer;
	public LayerMask GroundLayer;

	[Serializable]
	public struct UnitArray {
		public KeyCode MyKey;
		public Unit MyUnit;
	}

	void Update () {

		foreach (UnitArray currentUnit in PlayerUnits) {
			if (Input.GetKeyDown(currentUnit.MyKey)) {
				Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, HittableLayer)) {
					if ( GroundLayer == (GroundLayer | (1 << hit.collider.gameObject.layer))) {
						currentUnit.MyUnit.Destination(hit.point);
					}
				}
			}
		}
	}
}
