using UnityEngine;
using System.Collections;

/// <summary>
/// Set sorting layer.
/// Allows to set sorting layer & order in layer for non-sprites objects
/// </summary>

[ExecuteInEditMode]
public class SetSortingLayer : MonoBehaviour 
{
	public string sortingLayerName;
	public int orderOnLayer;
	
	void Start()
	{
		SetLayer();
	}
	
	void SetLayer()
	{
		if (sortingLayerName == "")
			return;
		
		renderer.sortingLayerName = sortingLayerName;
		renderer.sortingOrder = orderOnLayer;
	}
	
	#if UNITY_EDITOR
	void Update()
	{
		SetLayer();
	}
	#endif
}