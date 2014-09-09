using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour 
{
	public GameObject currentAniball;

	public LineRenderer leftRubber;
	public LineRenderer rightRubber;

	public float aniballRadius = 0.5f;

	private Vector3 rubberHoldPosition;
	private Ray slingshotToAniball;

	void Start()
	{
		slingshotToAniball = new Ray(leftRubber.transform.position, Vector3.zero);
		LineRendererSetup();
	}

	void LineRendererSetup()
	{
		leftRubber.SetPosition(0, leftRubber.transform.position);
		rightRubber.SetPosition(0, rightRubber.transform.position);
		leftRubber.SetPosition(1, leftRubber.transform.position);
		rightRubber.SetPosition(1, rightRubber.transform.position);

		leftRubber.sortingLayerName = "gameplay";
		rightRubber.sortingLayerName = "gameplay";

		leftRubber.sortingOrder = 2;
		rightRubber.sortingOrder = 4;
	}

	void LineRendererUpdate()
	{
		Vector3 dir = currentAniball.transform.position - leftRubber.transform.position;
		slingshotToAniball.direction = dir;
		rubberHoldPosition = slingshotToAniball.GetPoint(dir.magnitude + aniballRadius);

		leftRubber.SetPosition(1, rubberHoldPosition);
		rightRubber.SetPosition(1, rubberHoldPosition);
	}

	void Update()
	{
		if (!currentAniball)
		{
			leftRubber.SetPosition(1, rightRubber.transform.position);
			rightRubber.SetPosition(1, rightRubber.transform.position);
			return;
		}
		LineRendererUpdate();
	}
}
