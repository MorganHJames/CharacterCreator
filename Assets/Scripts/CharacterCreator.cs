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
	/// First name syllables.
	/// </summary>
	[Tooltip("First name syllables.")]
	[SerializeField] private string[] firstNameSyllables = null;

	/// <summary>
	/// Last name syllables.
	/// </summary>
	[Tooltip("Last name syllables.")]
	[SerializeField] private string[] lastNameSyllables = null;

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
			currentCharacter.characterInfo.name = RandomName();
		}
		currentCharacter.characterInfo.Save();
		SceneManager.LoadScene("Plaza", LoadSceneMode.Single);
	}

	/// <summary>
	/// Goes back to the plaza.
	/// </summary>
	public void Exit()
	{
		SceneManager.LoadScene("Plaza", LoadSceneMode.Single);
	}

	/// <summary>
	/// Randomizes the character.
	/// </summary>
	public void Randomise()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		// Basic Info.
		currentCharacter.characterInfo.name = RandomName();
		nameInputField.text = currentCharacter.characterInfo.name;

		heightSlider.value = Random.Range(heightSlider.minValue, heightSlider.maxValue);
		currentCharacter.characterInfo.height = heightSlider.value;

		weightSlider.value = Random.Range(weightSlider.minValue, weightSlider.maxValue);
		currentCharacter.characterInfo.height = heightSlider.value;

		voicePitchSlider.value = Random.Range(voicePitchSlider.minValue, voicePitchSlider.maxValue);
		SetVoicePitch();
		currentCharacter.PlayPickUpNoise();

		currentCharacter.characterInfo.animationControllerIndex = Random.Range(0, currentCharacter.characterPartIndex.animatorControllers.Length);

		currentCharacter.characterInfo.skinColor = Random.ColorHSV(0,1,0,1,0,1,1,1);
		skinColorPicker.CurrentColor = currentCharacter.characterInfo.skinColor;

		// Head Parts.
		currentCharacter.characterInfo.hairstyleIndex = Random.Range(0, currentCharacter.characterPartIndex.hairstyles.Length);
		currentCharacter.characterInfo.hairstyleColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		hairColorPicker.CurrentColor = currentCharacter.characterInfo.hairstyleColor;

		currentCharacter.characterInfo.eyesIndex = Random.Range(0, currentCharacter.characterPartIndex.eyes.Length);
		currentCharacter.characterInfo.eyeColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		eyesColorPicker.CurrentColor = currentCharacter.characterInfo.eyeColor;

		currentCharacter.characterInfo.noseIndex = Random.Range(0, currentCharacter.characterPartIndex.noses.Length);
		currentCharacter.characterInfo.noseColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		noseColorPicker.CurrentColor = currentCharacter.characterInfo.noseColor;

		currentCharacter.characterInfo.hatIndex = Random.Range(0, currentCharacter.characterPartIndex.hats.Length);
		currentCharacter.characterInfo.hatColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		hatColorPicker.CurrentColor = currentCharacter.characterInfo.hatColor;

		currentCharacter.characterInfo.facialHairIndex = Random.Range(0, currentCharacter.characterPartIndex.facialHairs.Length);
		currentCharacter.characterInfo.facialHairColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		facialHairColorPicker.CurrentColor = currentCharacter.characterInfo.facialHairColor;

		currentCharacter.characterInfo.faceAccessoryIndex = Random.Range(0, currentCharacter.characterPartIndex.faceAccessorys.Length);
		currentCharacter.characterInfo.faceAccessoryColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		faceAccessoryColorPicker.CurrentColor = currentCharacter.characterInfo.faceAccessoryColor;

		currentCharacter.characterInfo.shirtIndex = Random.Range(0, currentCharacter.characterPartIndex.shirts.Length);
		currentCharacter.characterInfo.shirtColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		shirtColorPicker.CurrentColor = currentCharacter.characterInfo.shirtColor;

		currentCharacter.characterInfo.backAccessoryIndex = Random.Range(0, currentCharacter.characterPartIndex.backAccessorys.Length);
		currentCharacter.characterInfo.backAccessoryColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		backAccessoryColorPicker.CurrentColor = currentCharacter.characterInfo.backAccessoryColor;

		currentCharacter.characterInfo.glovesIndex = Random.Range(0, currentCharacter.characterPartIndex.gloves.Length);
		currentCharacter.characterInfo.glovesColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		glovesColorPicker.CurrentColor = currentCharacter.characterInfo.glovesColor;

		currentCharacter.characterInfo.waistAccessoryIndex = Random.Range(0, currentCharacter.characterPartIndex.waistAccessorys.Length);
		currentCharacter.characterInfo.waistAccessoryColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		waistAccessoryColorPicker.CurrentColor = currentCharacter.characterInfo.waistAccessoryColor;

		currentCharacter.characterInfo.pantsIndex = Random.Range(0, currentCharacter.characterPartIndex.pants.Length);
		currentCharacter.characterInfo.pantsColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		pantsColorPicker.CurrentColor = currentCharacter.characterInfo.pantsColor;

		currentCharacter.characterInfo.shoesIndex = Random.Range(0, currentCharacter.characterPartIndex.shoes.Length);
		currentCharacter.characterInfo.shoesColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		shoesColorPicker.CurrentColor = currentCharacter.characterInfo.shoesColor;

		ReloadCharacter();
	}

	/// <summary>
	/// Creates a random name.
	/// </summary>
	/// <returns>The newly created name.</returns>
	public string RandomName()
	{
		string firstName = "";
		int numberOfSyllablesInFirstName = Random.Range(2, 4);

		for (int i = 0; i < numberOfSyllablesInFirstName; i++)
		{
			firstName += firstNameSyllables[Random.Range(0, firstNameSyllables.Length)];
		}

		string firstNameLetter = firstName.Substring(0, 1);
		firstName = firstName.Remove(0, 1);
		firstNameLetter = firstNameLetter.ToUpper();
		firstName = firstNameLetter + firstName;


		string lastName = "";
		int numberOfSyllablesInLastName = Random.Range(2, 3);

		for (int i = 0; i < numberOfSyllablesInLastName; i++)
		{
			lastName += lastNameSyllables[Random.Range(0, lastNameSyllables.Length)];
		}

		string lastNameLetter = lastName.Substring(0, 1);
		lastName = lastName.Remove(0, 1);
		lastNameLetter = lastNameLetter.ToUpper();
		lastName = lastNameLetter + lastName;

		return firstName + " " + lastName;
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
		currentCharacter.transform.localScale = new Vector3(transform.localScale.x, currentCharacter.characterInfo.height, transform.localScale.z);
	}

	/// <summary>
	/// Sets the characters weight.
	/// </summary>
	public void SetWeight()
	{
		currentCharacter.characterInfo.weight = weightSlider.value;
		currentCharacter.transform.localScale = new Vector3(currentCharacter.characterInfo.weight, transform.localScale.y, currentCharacter.characterInfo.weight);
	}

	/// <summary>
	/// Sets the characters voice pitch.
	/// </summary>
	public void SetVoicePitch()
	{
		currentCharacter.characterInfo.pitchOfVoice = voicePitchSlider.value;
	}

	/// <summary>
	/// Sets the character skin color.
	/// </summary>
	public void SetSkinColor()
	{
		currentCharacter.characterInfo.skinColor = skinColorPicker.CurrentColor;
		currentCharacter.body.material.color = currentCharacter.characterInfo.skinColor;
	}

	/// <summary>
	/// Sets the character hair color.
	/// </summary>
	public void SetHairColor()
	{
		currentCharacter.characterInfo.hairstyleColor = hairColorPicker.CurrentColor;
		currentCharacter.hairstyle.material.color = currentCharacter.characterInfo.hairstyleColor;
	}

	/// <summary>
	/// Sets the character eye color.
	/// </summary>
	public void SetEyeColor()
	{
		currentCharacter.characterInfo.eyeColor = eyesColorPicker.CurrentColor;
		currentCharacter.eyes.material.color = currentCharacter.characterInfo.eyeColor;
	}

	/// <summary>
	/// Sets the character nose color.
	/// </summary>
	public void SetNoseColor()
	{
		currentCharacter.characterInfo.noseColor = noseColorPicker.CurrentColor;
		currentCharacter.nose.material.color = currentCharacter.characterInfo.noseColor;
	}

	/// <summary>
	/// Sets the character hat color.
	/// </summary>
	public void SetHatColor()
	{
		currentCharacter.characterInfo.hatColor = hatColorPicker.CurrentColor;
		currentCharacter.hat.material.color = currentCharacter.characterInfo.hatColor;
	}

	/// <summary>
	/// Sets the character facial hair color.
	/// </summary>
	public void SetFacialHairColor()
	{
		currentCharacter.characterInfo.facialHairColor = facialHairColorPicker.CurrentColor;
		currentCharacter.facialHair.material.color = currentCharacter.characterInfo.facialHairColor;
	}

	/// <summary>
	/// Sets the character face accessory color.
	/// </summary>
	public void SetFaceAccessoryColor()
	{
		currentCharacter.characterInfo.faceAccessoryColor = faceAccessoryColorPicker.CurrentColor;
		currentCharacter.faceAccessory.material.color = currentCharacter.characterInfo.faceAccessoryColor;
	}

	/// <summary>
	/// Sets the character shirt color.
	/// </summary>
	public void SetShirtColor()
	{
		currentCharacter.characterInfo.shirtColor = shirtColorPicker.CurrentColor;

		SkinnedMeshRenderer skinnedMeshRenderer = currentCharacter.shirtParent.GetComponentInChildren<SkinnedMeshRenderer>();
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.material.color = currentCharacter.characterInfo.shirtColor;
		}
	}

	/// <summary>
	/// Sets the character back accessory color.
	/// </summary>
	public void SetBackAccessoryColor()
	{
		currentCharacter.characterInfo.backAccessoryColor = backAccessoryColorPicker.CurrentColor;

		SkinnedMeshRenderer skinnedMeshRenderer = currentCharacter.backAccessoryParent.GetComponentInChildren<SkinnedMeshRenderer>();
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.material.color = currentCharacter.characterInfo.backAccessoryColor;
		}
	}

	/// <summary>
	/// Sets the character gloves color.
	/// </summary>
	public void SetGlovesColor()
	{
		currentCharacter.characterInfo.glovesColor = glovesColorPicker.CurrentColor;

		SkinnedMeshRenderer skinnedMeshRenderer = currentCharacter.glovesParent.GetComponentInChildren<SkinnedMeshRenderer>();
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.material.color = currentCharacter.characterInfo.glovesColor;
		}
	}

	/// <summary>
	/// Sets the character waist accessory color.
	/// </summary>
	public void SetWaistAccessoryColor()
	{
		currentCharacter.characterInfo.waistAccessoryColor = waistAccessoryColorPicker.CurrentColor;

		SkinnedMeshRenderer skinnedMeshRenderer = currentCharacter.waistAccessoryParent.GetComponentInChildren<SkinnedMeshRenderer>();
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.material.color = currentCharacter.characterInfo.waistAccessoryColor;
		}
	}

	/// <summary>
	/// Sets the character pants color.
	/// </summary>
	public void SetPantsColor()
	{
		currentCharacter.characterInfo.pantsColor = pantsColorPicker.CurrentColor;

		SkinnedMeshRenderer skinnedMeshRenderer = currentCharacter.pantsParent.GetComponentInChildren<SkinnedMeshRenderer>();
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.material.color = currentCharacter.characterInfo.pantsColor;
		}
	}

	/// <summary>
	/// Sets the character shoes color.
	/// </summary>
	public void SetShoesColor()
	{
		currentCharacter.characterInfo.shoesColor = shoesColorPicker.CurrentColor;

		SkinnedMeshRenderer skinnedMeshRenderer = currentCharacter.shoesParent.GetComponentInChildren<SkinnedMeshRenderer>();
		if (skinnedMeshRenderer)
		{
			skinnedMeshRenderer.material.color = currentCharacter.characterInfo.shoesColor;
		}
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
		currentCharacter.animator.runtimeAnimatorController = currentCharacter.characterPartIndex.animatorControllers[currentCharacter.characterInfo.animationControllerIndex];
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

		if (currentCharacter.characterPartIndex.hairstyles.Length > 0)
		{
			currentCharacter.hairstyle.sharedMesh = currentCharacter.characterPartIndex.hairstyles[currentCharacter.characterInfo.hairstyleIndex];
			currentCharacter.hairstyle.material.color = currentCharacter.characterInfo.hairstyleColor;
		}
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

		if (currentCharacter.characterPartIndex.hairstyles.Length > 0)
		{
			currentCharacter.eyes.sharedMesh = currentCharacter.characterPartIndex.eyes[currentCharacter.characterInfo.eyesIndex];
			currentCharacter.eyes.material.color = currentCharacter.characterInfo.eyeColor;
		}
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

		if (currentCharacter.characterPartIndex.noses.Length > 0)
		{
			currentCharacter.nose.sharedMesh = currentCharacter.characterPartIndex.noses[currentCharacter.characterInfo.noseIndex];
			currentCharacter.nose.material.color = currentCharacter.characterInfo.noseColor;
		}
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

		if (currentCharacter.characterPartIndex.hats.Length > 0)
		{
			currentCharacter.hat.sharedMesh = currentCharacter.characterPartIndex.hats[currentCharacter.characterInfo.hatIndex];
			currentCharacter.hat.material.color = currentCharacter.characterInfo.hatColor;
		}
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

		if (currentCharacter.characterPartIndex.facialHairs.Length > 0)
		{
			currentCharacter.facialHair.sharedMesh = currentCharacter.characterPartIndex.facialHairs[currentCharacter.characterInfo.facialHairIndex];
			currentCharacter.facialHair.material.color = currentCharacter.characterInfo.facialHairColor;
		}
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

		if (currentCharacter.characterPartIndex.faceAccessorys.Length > 0)
		{
			currentCharacter.faceAccessory.sharedMesh = currentCharacter.characterPartIndex.faceAccessorys[currentCharacter.characterInfo.faceAccessoryIndex];
			currentCharacter.faceAccessory.material.color = currentCharacter.characterInfo.faceAccessoryColor;
		}
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

		if (currentCharacter.characterPartIndex.shirts.Length > 0)
		{
			if (currentCharacter.shirtParent.transform.childCount > 0)
			{
				foreach (Transform child in currentCharacter.shirtParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject shirt = Instantiate(currentCharacter.characterPartIndex.shirts[currentCharacter.characterInfo.shirtIndex], currentCharacter.shirtParent.transform);
			shirt.GetComponentInChildren<SkinnedMeshRenderer>().material.color = currentCharacter.characterInfo.shirtColor;
		}
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

		if (currentCharacter.characterPartIndex.backAccessorys.Length > 0)
		{
			if (currentCharacter.backAccessoryParent.transform.childCount > 0)
			{
				foreach (Transform child in currentCharacter.backAccessoryParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject backAccessory = Instantiate(currentCharacter.characterPartIndex.backAccessorys[currentCharacter.characterInfo.backAccessoryIndex], currentCharacter.backAccessoryParent.transform);
			backAccessory.GetComponentInChildren<SkinnedMeshRenderer>().material.color = currentCharacter.characterInfo.backAccessoryColor;
		}
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

		if (currentCharacter.characterPartIndex.gloves.Length > 0)
		{
			if (currentCharacter.glovesParent.transform.childCount > 0)
			{
				foreach (Transform child in currentCharacter.glovesParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject gloves = Instantiate(currentCharacter.characterPartIndex.gloves[currentCharacter.characterInfo.glovesIndex], currentCharacter.glovesParent.transform);
			gloves.GetComponentInChildren<SkinnedMeshRenderer>().material.color = currentCharacter.characterInfo.glovesColor;
		}
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

		if (currentCharacter.characterPartIndex.waistAccessorys.Length > 0)
		{
			if (currentCharacter.waistAccessoryParent.transform.childCount > 0)
			{
				foreach (Transform child in currentCharacter.waistAccessoryParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject waistAccessory = Instantiate(currentCharacter.characterPartIndex.waistAccessorys[currentCharacter.characterInfo.waistAccessoryIndex], currentCharacter.waistAccessoryParent.transform);
			waistAccessory.GetComponentInChildren<SkinnedMeshRenderer>().material.color = currentCharacter.characterInfo.waistAccessoryColor;
		}
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

		if (currentCharacter.characterPartIndex.pants.Length > 0)
		{
			if (currentCharacter.pantsParent.transform.childCount > 0)
			{
				foreach (Transform child in currentCharacter.pantsParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject pants = Instantiate(currentCharacter.characterPartIndex.pants[currentCharacter.characterInfo.pantsIndex], currentCharacter.pantsParent.transform);
			pants.GetComponentInChildren<SkinnedMeshRenderer>().material.color = currentCharacter.characterInfo.pantsColor;
		}
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

		if (currentCharacter.characterPartIndex.shoes.Length > 0)
		{
			if (currentCharacter.shoesParent.transform.childCount > 0)
			{
				foreach (Transform child in currentCharacter.shoesParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject shoes = Instantiate(currentCharacter.characterPartIndex.shoes[currentCharacter.characterInfo.shoesIndex], currentCharacter.shoesParent.transform);
			shoes.GetComponentInChildren<SkinnedMeshRenderer>().material.color = currentCharacter.characterInfo.shoesColor;
		}
	}

	/// <summary>
	/// Plays the button sound.
	/// </summary>
	public void PlayButtonNoise()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
	}
	#endregion
	#endregion
}