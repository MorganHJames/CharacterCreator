////////////////////////////////////////////////////////////
// File: CharacterManager.cs
// Author: Morgan Henry James
// Date Created: 29-02-2020
// Brief: Controls every character in the scene and spawns them in on start.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Controls every character in the scene and spawns them in on start.
/// </summary>
public class CharacterManager : MonoBehaviour
{
	#region Variables
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Spawn in all of the created characters on start.
	/// </summary>
	private void Start()
	{
		SpawnAllCharacters();
	}

	/// <summary>
	/// Spawn in all of the created characters.
	/// </summary>
	private void SpawnAllCharacters()
	{
		//For each char

		//spawn character at random location
		// set the info of that char to the correct info
		//add character to the list
	}
	#endregion
	#region Public

	#endregion
	#endregion
}