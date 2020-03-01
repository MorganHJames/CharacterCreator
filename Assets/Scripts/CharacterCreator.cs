////////////////////////////////////////////////////////////
// File: CharacterCreator.cs
// Author: Morgan Henry James
// Date Created: 01-03-2020
// Brief: Allows for the customization of a character.
//////////////////////////////////////////////////////////// 

using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	/// The current character.
	/// </summary>
	private Character currentCharacter = null;

	/// <summary>
	/// The characters unique id.
	/// </summary>
	private int characterID = -1;

	/// <summary>
	/// The index of the currently open menu.
	/// </summary>
	private int currentMenuOpen = 0;

	/// <summary>
	/// All of the menu buttons in order.
	/// </summary>
	[SerializeField] private Button[] menuButtons = null;

	/// <summary>
	/// All of the menu animators in order.
	/// </summary>
	[SerializeField] private Animator[] menuAnimators = null;

	[Header("Basic Info")]
	/// <summary>
	/// The name input.
	/// </summary>
	[Tooltip("The name input.")]
	[SerializeField] private TMP_InputField nameInputField = null;

	/// <summary>
	/// The height slider.
	/// </summary>
	[Tooltip("The height slider.")]
	[SerializeField] private Slider heightSlider = null;

	/// <summary>
	/// The weight slider.
	/// </summary>
	[Tooltip("The weight slider.")]
	[SerializeField] private Slider weightSlider = null;

	/// <summary>
	/// The voice pitch slider.
	/// </summary>
	[Tooltip("The voice pitch slider.")]
	[SerializeField] private Slider voicePitchSlider = null;

	/// <summary>
	/// The skin color picker.
	/// </summary>
	[Tooltip("The skin color picker.")]
	[SerializeField] private ColorPicker skinColorPicker = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Initiates the character.
	/// Sets up the menu buttons.
	/// </summary>
	private void Start()
	{
		characterID = PlayerPrefs.GetInt("SelectedCharacter");
		CharacterInfo currentCharacterInfo = (CharacterInfo)ScriptableObject.CreateInstance("CharacterInfo");

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

		for (int i = 0; i < menuButtons.Length; i++)
		{
			int index = i;
			menuButtons[i].onClick.AddListener(() =>
			{
				ChangeMenu(index);
			});
		}

		nameInputField.text = currentCharacterInfo.name;
		heightSlider.value = currentCharacterInfo.height;
		weightSlider.value = currentCharacterInfo.weight;
		voicePitchSlider.value = currentCharacterInfo.pitchOfVoice;

		skinColorPicker.AssignColor(currentCharacter.characterInfo.skinColor);
		skinColorPicker.SendChangedEvent();
	}

	/// <summary>
	/// Changes the menu to the desired index.
	/// </summary>
	/// <param name="menuToChangeTo"></param>
	private void ChangeMenu(int menuToChangeTo)
	{
		if (currentMenuOpen != menuToChangeTo)
		{
			menuAnimators[currentMenuOpen].Play("Shrink");
			currentMenuOpen = menuToChangeTo;
			menuAnimators[currentMenuOpen].Play("Expand");
		}
	}

	/// <summary>
	/// Reloads the character applying all the changes.
	/// </summary>
	private void ReloadCharacter()
	{
		currentCharacter.ApplyCharacterInfo();
	}
	#endregion
	#region Public
	/// <summary>
	/// Saves the character and changes scene back to the plaza.
	/// </summary>
	public void SaveCharacter()
	{
		if (currentCharacter.characterInfo.name == "")
		{
			currentCharacter.characterInfo.name = defaultCharacterInfo.name;
		}
		currentCharacter.characterInfo.Save();
		SceneManager.LoadScene("Plaza", LoadSceneMode.Single);
	}

	/// <summary>
	/// Changes the character Name.
	/// </summary>
	public void ChangeName()
	{
		currentCharacter.characterInfo.name = nameInputField.text;
	}

	/// <summary>
	/// Sets the characters height.
	/// </summary>
	public void SetHeight()
	{
		currentCharacter.characterInfo.height = heightSlider.value;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the characters weight.
	/// </summary>
	public void SetWeight()
	{
		currentCharacter.characterInfo.weight = weightSlider.value;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the characters voice pitch.
	/// </summary>
	public void SetVoicePitch()
	{
		currentCharacter.characterInfo.pitchOfVoice = voicePitchSlider.value;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character skin color.
	/// </summary>
	public void SetSkinColor()
	{
		currentCharacter.characterInfo.skinColor = skinColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character animation index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeAnimationIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.animationControllerIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.animatorControllers.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.animatorControllers.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.animationControllerIndex = changeValue;
		ReloadCharacter();
	}
	#endregion
	#endregion
}