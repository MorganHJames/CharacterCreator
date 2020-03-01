////////////////////////////////////////////////////////////
// File: ColorPickerAnimator.cs
// Author: Morgan Henry James
// Date Created: 01-03-2020
// Brief: Controls the color picker animation.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the color picker animation.
/// </summary>
public class ColorPickerAnimator : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The initial position of the color picker.
	/// </summary>
	private Vector3 startingPosition;

	/// <summary>
	/// The initial position of the color picker.
	/// </summary>
	private Vector3 startingScale = new Vector3(0.15f, 0.15f, 1.0f);
	#endregion
	#region Public
	/// <summary>
	/// How far along the animation should the color picker be.
	/// </summary>
	[Tooltip("How far along the animation should the color picker be.")]
	public float percentageToSelected;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the initial position.
	/// </summary>
	private void Start()
	{
		startingPosition = transform.localPosition;
	}

	/// <summary>
	/// Scales and moves the color picker properly.
	/// </summary>
	private void Update()
	{
		transform.localPosition = Vector3.Lerp(startingPosition, Vector3.zero, percentageToSelected);
		transform.localScale = Vector3.Lerp(startingScale, Vector3.one, percentageToSelected);
	}
	#endregion
	#region Public
	/// <summary>
	/// Sets the sibling index to be one before last so that it appears above all else.
	/// </summary>
	public void SetSiblingIndexOneBeforeLast()
	{
		transform.SetSiblingIndex(transform.parent.childCount - 2);
	}
	#endregion
	#endregion
}