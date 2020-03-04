////////////////////////////////////////////////////////////
// File: CharacterManager.cs
// Author: Morgan Henry James
// Date Created: 29-02-2020
// Brief: Controls every character in the scene and spawns them in on start.
//////////////////////////////////////////////////////////// 

using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Controls every character in the scene and spawns them in on start.
/// </summary>
public class CharacterManager : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// List of all characters.
	/// </summary>
	private List<Character> characterList = new List<Character>();

	/// <summary>
	/// The character prefab to spawn in.
	/// </summary>
	[Tooltip("The character prefab to spawn in.")]
	[SerializeField] private GameObject characterPrefab = null;

	/// <summary>
	/// The scene camera.
	/// </summary>
	[Tooltip("The scene camera.")]
	[SerializeField] private Camera cam = null;

	/// <summary>
	/// The plane that contains the area the characters can move.
	/// </summary>
	[Tooltip("The plane that contains the area the characters can move.")]
	[SerializeField] private GameObject screenPlane = null;
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
		PlayerPrefs.SetInt("SelectedCharacter", -1);
		SpawnAllCharacters();
	}

	/// <summary>
	/// Applies the fix character plane function if it is needed.
	/// </summary>
	private void Update()
	{
		FixCharacterPlaneScale();
	}

	/// <summary>
	/// Scales the character plane to fit the screen and sets the character wander limit to account for changes.
	/// </summary>
	private bool FixCharacterPlaneScale()
	{
		Vector3 screenPos = cam.WorldToScreenPoint(screenPlane.transform.GetChild(0).position);

		bool isFixed = true;

		if (screenPos.x < Screen.width - 0.5f && screenPos.y > 0.5f)
		{
			screenPlane.transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);
			isFixed = false;
		}

		if (screenPos.x > Screen.width + 0.5f && screenPos.y < -0.5f)
		{
			screenPlane.transform.localScale -= new Vector3(0.025f, 0.025f, 0.025f);
			isFixed = false;
		}

		Character.wanderLimit = new Vector2(screenPlane.transform.GetChild(0).position.x, screenPlane.transform.GetChild(0).position.z);

		return isFixed;
	}

	/// <summary>
	/// Spawn in all of the created characters.
	/// </summary>
	private void SpawnAllCharacters()
	{
		while (!FixCharacterPlaneScale())
		{

		}

		foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath + "/Characters/"))
		{
			int pos = file.LastIndexOf("/") + 1;

			GameObject character = Instantiate(characterPrefab, new Vector3(Random.Range(-Character.wanderLimit.x, Character.wanderLimit.x), 0.0f, Random.Range(-Character.wanderLimit.y + 2.5f, Character.wanderLimit.y + 2.5f)), Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));
			Character spawnedCharacter = character.GetComponent<Character>();
			spawnedCharacter.characterInfo = LoadCharacterData(int.Parse(file.Substring(pos, file.Length - pos)));
			characterList.Add(spawnedCharacter);
		}
	}

	/// <summary>
	/// Loads the character data.
	/// </summary>
	/// <param name="characterId">The character ID of the character you want to spawn.</param>
	/// <returns>The character info of the character.</returns>
	private CharacterInfo LoadCharacterData(int characterId = 0)
	{
		CharacterInfo characterInfo = (CharacterInfo)ScriptableObject.CreateInstance("CharacterInfo");
		JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/Characters/" + characterId), characterInfo);
		return characterInfo;
	}
	#endregion
	#region Public

	#endregion
	#endregion
}