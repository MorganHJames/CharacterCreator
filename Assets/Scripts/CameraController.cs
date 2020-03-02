////////////////////////////////////////////////////////////
// File: CameraController.cs
// Author: Morgan Henry James
// Date Created: 02-03-2020
// Brief: Moves the camera to the desired location.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Moves the camera to the desired location.
/// </summary>
public class CameraController : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The positions for the camera to go to in order.
	/// </summary>
	[Tooltip("The positions for the camera to go to in order.")]
	[SerializeField] private Vector3[] positions = null;

	/// <summary>
	/// The index of positions that the camera should be moving to or is at.
	/// </summary>
	private int positionIndex = 0;

	/// <summary>
	/// The speed that the camera should be moving at.
	/// </summary>
	[Tooltip("The speed that the camera should be moving at.")]
	[SerializeField] private float speed = 100.0f;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Moves the camera to the correct position.
	/// </summary>
	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, positions[positionIndex], Time.deltaTime * speed);
	}
	#endregion
	#region Public
	/// <summary>
	/// Sets the current positional index for the camera.
	/// </summary>
	/// <param name="index">The new position index you want the camera to adhere to.</param>
	public void SetCameraPositionIndex(int index)
	{
		positionIndex = index;
	}
	#endregion
	#endregion
}