using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using DG.Tweening;

[NetworkSettings (sendInterval = .03f, channel = 0)]
public class NetworkInput : NetworkBehaviour
{
	[SyncVar]
	public Vector3 realPosition = Vector3.zero;
	[SyncVar]
	public Vector3 realVelocity;
	[SyncVar]
	public Vector3 realRotation;

	Rigidbody _rb;

	void Start () {
		_rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update ()
	{

		if (isServer) {
			realPosition = transform.position;
			realVelocity = _rb.velocity;
			realRotation = transform.eulerAngles;
		}
		else {

			transform.DOKill ();
			transform.DOMove (realPosition, 0.1f, false);
			transform.DORotate (realRotation, 0.1f);
			_rb.velocity = realVelocity;	
		}
	}
}