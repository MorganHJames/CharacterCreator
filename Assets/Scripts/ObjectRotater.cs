////////////////////////////////////////////////////////////
// File: ObjectRotater.cs
// Author: Morgan Henry James
// Date Created: 01-03-2020
// Brief: Rotates an object if the click starts in the right position.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Rotates an object if the click starts in the right position.
/// </summary>
public class ObjectRotater : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// This will determine max rotation speed.
	/// </summary>
	private float rotationSpeed = 250.0f;

	/// <summary>
	/// Object to rotate.
	/// </summary>
	[Tooltip("Object to rotate.")]
	[SerializeField] private GameObject objectToRotate = null;

	/// <summary>
	/// The scene camera.
	/// </summary>
	[Tooltip("The scene camera.")]
	[SerializeField] private Camera cam = null;

	/// <summary>
	/// The bounding transform.
	/// </summary>
	[Tooltip("The bounding transform.")]
	[SerializeField] private RectTransform boundingTransform = null;

	/// <summary>
	/// True when the object should be rotating.
	/// </summary>
	private bool rotating = false;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Checks to see if the rotation should occur and if so applies it.
	/// </summary>
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 localMousePosition = boundingTransform.InverseTransformPoint(Input.mousePosition);
			if (boundingTransform.rect.Contains(localMousePosition))
			{
				rotating = true;
			}
		}

		if (rotating && Input.GetMouseButtonUp(0))
		{
			rotating = false;
		}

		if (rotating)
		{
			RotateObject();
		}
	}

	/// <summary>
	/// Rotates the selected object.
	/// </summary>
	private void RotateObject()
	{
		objectToRotate.transform.Rotate(new Vector3(0.0f, -Input.GetAxis("Mouse X"), 0.0f) * Time.deltaTime * rotationSpeed);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}