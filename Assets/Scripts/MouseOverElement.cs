////////////////////////////////////////////////////////////
// File: MouseOverElement.cs
// Author: Morgan Henry James
// Date Created: 16-03-2020
// Brief: Allows for the indication if the mouse is over the element or not.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Allows for the indication if the mouse is over the element or not.
/// </summary>
public class MouseOverElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	#region Variables
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// The event that will be called on mouse over on the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseOverEvent = new UnityEvent();

	/// <summary>
	/// The event that will be called on mouse exit on the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseExitEvent = new UnityEvent();
	#endregion
	#endregion

	#region Methods
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// Called when the pointer is over the element.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOverEvent.Invoke();
	}

	/// <summary>
	/// Called when the pointer exits the element.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData)
	{
		mouseExitEvent.Invoke();
	}
	#endregion
	#endregion
}