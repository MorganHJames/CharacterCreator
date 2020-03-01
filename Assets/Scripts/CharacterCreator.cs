////////////////////////////////////////////////////////////
// File: CharacterCreator.cs
// Author: Morgan Henry James
// Date Created: 01-03-2020
// Brief: Allows for the customization of a character.
//////////////////////////////////////////////////////////// 

using System.IO;
using UnityEngine;

/// <summary>
/// Allows for the customization of a character.
/// </summary>
public class CharacterCreator : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The character prefab to spawn in.
	/// </summary>
	[Tooltip("The character prefab to spawn in.")]
	[SerializeField] private GameObject characterPrefab = null;

	/// <summary>
	/// Where the character should spawn.
	/// </summary>
	[Tooltip("Where the character should spawn.")]
	[SerializeField] private GameObject characterHolder = null;

	/// <summary>
	/// The starting information for a new character.
	/// </summary>
	[Tooltip("The starting information for a new character.")]
	[SerializeField] private CharacterInfo defaultCharacterInfo = null;

	/// <summary>
	/// The current characters information.
	/// </summary>
	private CharacterInfo currentCharacterInfo = null;

	/// <summary>
	/// The current character.
	/// </summary>
	private Character currentCharacter = null;

	/// <summary>
	/// The characters unique id.
	/// </summary>
	private int characterID = -1;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	private void Start()
	{
		characterID = PlayerPrefs.GetInt("SelectedCharacter");
		currentCharacterInfo = (CharacterInfo)ScriptableObject.CreateInstance("CharacterInfo");

		if (characterID == -1)
		{
			currentCharacterInfo.Copy(defaultCharacterInfo);
		}
		else
		{
			JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/Characters/" + characterID), currentCharacterInfo);
		}

		GameObject character = Instantiate(characterPrefab, characterHolder.transform);
		currentCharacter = character.GetComponent<Character>();
		currentCharacter.characterInfo = currentCharacterInfo;
		currentCharacter.Idle();
	}
	#endregion
	#region Public

	#endregion
	#endregion
}