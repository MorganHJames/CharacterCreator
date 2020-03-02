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

	/// <summary>
	/// The hair color picker.
	/// </summary>
	[Tooltip("The hair color picker.")]
	[SerializeField] private ColorPicker hairColorPicker = null;

	/// <summary>
	/// The eyes color picker.
	/// </summary>
	[Tooltip("The eyes color picker.")]
	[SerializeField] private ColorPicker eyesColorPicker = null;

	/// <summary>
	/// The nose color picker.
	/// </summary>
	[Tooltip("The nose color picker.")]
	[SerializeField] private ColorPicker noseColorPicker = null;

	/// <summary>
	/// The hat color picker.
	/// </summary>
	[Tooltip("The hat color picker.")]
	[SerializeField] private ColorPicker hatColorPicker = null;

	/// <summary>
	/// The facial hair color picker.
	/// </summary>
	[Tooltip("The facial hair color picker.")]
	[SerializeField] private ColorPicker facialHairColorPicker = null;

	/// <summary>
	/// The face accessory color picker.
	/// </summary>
	[Tooltip("The face accessory color picker.")]
	[SerializeField] private ColorPicker faceAccessoryColorPicker = null;

	/// <summary>
	/// The shirt color picker.
	/// </summary>
	[Tooltip("The shirt color picker.")]
	[SerializeField] private ColorPicker shirtColorPicker = null;

	/// <summary>
	/// The back accessory picker.
	/// </summary>
	[Tooltip("The back accessory color picker.")]
	[SerializeField] private ColorPicker backAccessoryColorPicker = null;

	/// <summary>
	/// The gloves color picker.
	/// </summary>
	[Tooltip("The gloves color picker.")]
	[SerializeField] private ColorPicker glovesColorPicker = null;

	/// <summary>
	/// The waist accessory color picker.
	/// </summary>
	[Tooltip("The waist accessory color picker.")]
	[SerializeField] private ColorPicker waistAccessoryColorPicker = null;

	/// <summary>
	/// The pants color picker.
	/// </summary>
	[Tooltip("The pants color picker.")]
	[SerializeField] private ColorPicker pantsColorPicker = null;

	/// <summary>
	/// The shoes color picker.
	/// </summary>
	[Tooltip("The shoes color picker.")]
	[SerializeField] private ColorPicker shoesColorPicker = null;
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

		hairColorPicker.AssignColor(currentCharacter.characterInfo.hairstyleColor);
		hairColorPicker.SendChangedEvent();

		eyesColorPicker.AssignColor(currentCharacter.characterInfo.eyeColor);
		eyesColorPicker.SendChangedEvent();

		noseColorPicker.AssignColor(currentCharacter.characterInfo.noseColor);
		noseColorPicker.SendChangedEvent();

		hatColorPicker.AssignColor(currentCharacter.characterInfo.hatColor);
		hatColorPicker.SendChangedEvent();

		facialHairColorPicker.AssignColor(currentCharacter.characterInfo.facialHairColor);
		facialHairColorPicker.SendChangedEvent();

		faceAccessoryColorPicker.AssignColor(currentCharacter.characterInfo.faceAccessoryColor);
		faceAccessoryColorPicker.SendChangedEvent();

		shirtColorPicker.AssignColor(currentCharacter.characterInfo.shirtColor);
		shirtColorPicker.SendChangedEvent();

		backAccessoryColorPicker.AssignColor(currentCharacter.characterInfo.backAccessoryColor);
		backAccessoryColorPicker.SendChangedEvent();

		glovesColorPicker.AssignColor(currentCharacter.characterInfo.glovesColor);
		glovesColorPicker.SendChangedEvent();

		waistAccessoryColorPicker.AssignColor(currentCharacter.characterInfo.waistAccessoryColor);
		waistAccessoryColorPicker.SendChangedEvent();

		pantsColorPicker.AssignColor(currentCharacter.characterInfo.pantsColor);
		pantsColorPicker.SendChangedEvent();

		shoesColorPicker.AssignColor(currentCharacter.characterInfo.shoesColor);
		shoesColorPicker.SendChangedEvent();
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
	/// Sets the character hair color.
	/// </summary>
	public void SetHairColor()
	{
		currentCharacter.characterInfo.hairstyleColor = hairColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character eye color.
	/// </summary>
	public void SetEyeColor()
	{
		currentCharacter.characterInfo.eyeColor = eyesColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character nose color.
	/// </summary>
	public void SetNoseColor()
	{
		currentCharacter.characterInfo.noseColor = noseColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character hat color.
	/// </summary>
	public void SetHatColor()
	{
		currentCharacter.characterInfo.hatColor = hatColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character facial hair color.
	/// </summary>
	public void SetFacialHairColor()
	{
		currentCharacter.characterInfo.facialHairColor = facialHairColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character face accessory color.
	/// </summary>
	public void SetFaceAccessoryColor()
	{
		currentCharacter.characterInfo.faceAccessoryColor = faceAccessoryColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character shirt color.
	/// </summary>
	public void SetShirtColor()
	{
		currentCharacter.characterInfo.shirtColor = shirtColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character back accessory color.
	/// </summary>
	public void SetBackAccessoryColor()
	{
		currentCharacter.characterInfo.backAccessoryColor = backAccessoryColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character gloves color.
	/// </summary>
	public void SetGlovesColor()
	{
		currentCharacter.characterInfo.glovesColor = glovesColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character waist accessory color.
	/// </summary>
	public void SetWaistAccessoryColor()
	{
		currentCharacter.characterInfo.waistAccessoryColor = waistAccessoryColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character pants color.
	/// </summary>
	public void SetPantsColor()
	{
		currentCharacter.characterInfo.pantsColor = pantsColorPicker.CurrentColor;
		ReloadCharacter();
	}

	/// <summary>
	/// Sets the character shoes color.
	/// </summary>
	public void SetShoesColor()
	{
		currentCharacter.characterInfo.shoesColor = shoesColorPicker.CurrentColor;
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

	/// <summary>
	/// Changes the character hair index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeHairIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.hairstyleIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.hairstyles.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.hairstyles.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.hairstyleIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character eyes index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeEyesIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.eyesIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.eyes.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.eyes.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.eyesIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character nose index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeNoseIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.noseIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.noses.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.noses.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.noseIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character hat index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeHatIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.hatIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.hats.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.hats.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.hatIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character facial hair index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeFacialHairIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.facialHairIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.facialHairs.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.facialHairs.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.facialHairIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character face accessory index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeFaceAccessoryIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.faceAccessoryIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.faceAccessorys.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.faceAccessorys.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.faceAccessoryIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character shirt index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeShirtIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.shirtIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.shirts.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.shirts.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.shirtIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character back accessory index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeBackAccessoryIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.backAccessoryIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.backAccessorys.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.backAccessorys.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.backAccessoryIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character gloves index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeGlovesIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.glovesIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.gloves.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.gloves.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.glovesIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character waist accessory index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeWaistAccessoryIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.waistAccessoryIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.waistAccessorys.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.waistAccessorys.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.waistAccessoryIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character pants index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangePantsIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.pantsIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.pants.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.pants.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.pantsIndex = changeValue;
		ReloadCharacter();
	}

	/// <summary>
	/// Changes the character shoes index.
	/// </summary>
	/// <param name="changeValue">What to change the index by.</param>
	public void ChangeShoesIndex(int changeValue)
	{
		changeValue += currentCharacter.characterInfo.shoesIndex;

		if (changeValue < 0)
		{
			changeValue = currentCharacter.characterPartIndex.shoes.Length - 1;
		}
		else if (changeValue == currentCharacter.characterPartIndex.shoes.Length)
		{
			changeValue = 0;
		}

		currentCharacter.characterInfo.shoesIndex = changeValue;
		ReloadCharacter();
	}
	#endregion
	#endregion
}