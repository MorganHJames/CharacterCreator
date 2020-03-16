////////////////////////////////////////////////////////////
// File: CharacterUserInteraction.cs
// Author: Morgan Henry James
// Date Created: 16-03-2020
// Brief: Handles the character interaction with the user.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the character interaction with the user.
/// </summary>
public class CharacterUserInteraction : MonoBehaviour
{
	#region Variables
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// The event that will be called on mouse enter over the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseEnterEvent = new UnityEvent();

	/// <summary>
	/// The event that will be called on mouse exit over the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseExitEvent = new UnityEvent();

	/// <summary>
	/// The event that will be called on mouse up over the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseUpEvent = new UnityEvent();

	/// <summary>
	/// The event that will be called on mouse down over the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseDownEvent = new UnityEvent();
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Called on mouse enter.
	/// </summary>
	private void OnMouseEnter()
	{
		mouseEnterEvent.Invoke();
	}

	/// <summary>
	/// Called on mouse exit.
	/// </summary>
	private void OnMouseExit()
	{
		mouseExitEvent.Invoke();
	}

	/// <summary>
	/// Called on mouse down.
	/// </summary>
	private void OnMouseDown()
	{
		mouseDownEvent.Invoke();
	}

	/// <summary>
	/// Called on mouse up.
	/// </summary>
	private void OnMouseUp()
	{
		mouseUpEvent.Invoke();
	}
	#endregion
	#region Public

	#endregion
	#endregion
}