using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	private SpringJoint2D spring;
	private GameObject slingshotObj;
	private Slingshot slingshot;
	
	private bool dragging = false;

	private Ray slingshotToAniball;
	private float maxStretch = 3f;

	private Vector3 prevVel;

	void Start()
	{
		spring = GetComponent<SpringJoint2D>();
		slingshotObj = GameObject.Find("slingshot");
		slingshot = slingshotObj.GetComponent<Slingshot>();
		slingshotToAniball = new Ray(spring.connectedBody.transform.position, Vector3.zero);
	}

	void OnMouseDown()
	{
		if (!rigidbody2D.isKinematic) return;
		slingshot.currentAniball = gameObject;
		dragging = true;
		rigidbody2D.isKinematic = true;
	}

	void OnMouseUp()
	{
		dragging = false;
		rigidbody2D.isKinematic = false;
	}

	void Update()
	{
		if (dragging)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0f;
			Vector3 dir = pos - spring.connectedBody.transform.position;
			slingshotToAniball.direction = dir;
			if (dir.magnitude < maxStretch)
			{
				transform.position = pos;
			}
			else
			{
				transform.position = slingshotToAniball.GetPoint(maxStretch);
			}
		}
		else
		{
			if (!rigidbody2D.isKinematic)
			{
				if (rigidbody2D.velocity.magnitude < prevVel.magnitude && spring.enabled)
				{
					spring.enabled = false;
					slingshot.currentAniball = null;
					rigidbody2D.velocity = prevVel;
				}
				else
				{
					prevVel = rigidbody2D.velocity;
				}
			}
		}
	}
}
