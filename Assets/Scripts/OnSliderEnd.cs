////////////////////////////////////////////////////////////
// File: OnSliderEnd.cs
// Author: Morgan Henry James
// Date Created: 18-03-2020
// Brief: Plays a sound on slider end.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Plays a sound on slider end.
/// </summary>
public class OnSliderEnd : MonoBehaviour, IPointerUpHandler
{
	#region Variables
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// Plays a sound when the slider is done.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerUp(PointerEventData eventData)
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
	}
	#endregion
	#endregion
}