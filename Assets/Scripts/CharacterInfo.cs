////////////////////////////////////////////////////////////
// File: CharacterInfo.cs
// Author: Morgan Henry James
// Date Created: 28-02-2020
// Brief: Holds all of the information pertaining to a specific character.
//////////////////////////////////////////////////////////// 

using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Holds all of the information pertaining to a specific character.
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New Character Info", menuName = "CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
	#region Variables
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// The Unique id of the character.
	/// </summary>
	public int id = -1;

	[Header("Basic Info")]
	/// <summary>
	/// The Character's name.
	/// </summary>
	[Tooltip("The Character's name.")]
	public new string name = "Default Dan";

	/// <summary>
	/// The Character's height.
	/// </summary>
	[Tooltip("The Character's height.")]
	public float height = 1;

	/// <summary>
	/// The Character's height.
	/// </summary>
	[Tooltip("The Character's height.")]
	public float weight = 1;

	/// <summary>
	/// The Character's animation controller.
	/// </summary>
	[Tooltip("The Character's animation controller.")]
	public int animationControllerIndex = 0;

	/// <summary>
	/// The Character's pitch of voice.
	/// </summary>
	[Tooltip("The Character's pitch of voice.")]
	public float pitchOfVoice = 1;

	/// <summary>
	/// The Character's skin color.
	/// </summary>
	[Tooltip("The Character's skin color.")]
	public Color skinColor = Color.white;

	[Header("Head Parts")]
	/// <summary>
	/// The Character's hairstyle index.
	/// </summary>
	[Tooltip("The Character's hairstyle index.")]
	public int hairstyleIndex = 0;

	/// <summary>
	/// The Character's hairstyle color.
	/// </summary>
	[Tooltip("The Character's hairstyle color.")]
	public Color hairstyleColor = Color.white;

	/// <summary>
	/// The Character's eyes index.
	/// </summary>
	[Tooltip("The Character's eyes index.")]
	public int eyesIndex = 0;

	/// <summary>
	/// The Character's eyes color.
	/// </summary>
	[Tooltip("The Character's eyes color.")]
	public Color eyeColor = Color.white;

	/// <summary>
	/// The Character's nose index.
	/// </summary>
	[Tooltip("The Character's nose index.")]
	public int noseIndex = 0;

	/// <summary>
	/// The Character's nose color.
	/// </summary>
	[Tooltip("The Character's nose color.")]
	public Color noseColor = Color.white;

	/// <summary>
	/// The Character's hat index.
	/// </summary>
	[Tooltip("The Character's hat index.")]
	public int hatIndex = 0;

	/// <summary>
	/// The Character's hat color.
	/// </summary>
	[Tooltip("The Character's hat color.")]
	public Color hatColor = Color.white;

	/// <summary>
	/// The Character's facial hair index.
	/// </summary>
	[Tooltip("The Character's facial hair index.")]
	public int facialHairIndex = 0;

	/// <summary>
	/// The Character's facial hair color.
	/// </summary>
	[Tooltip("The Character's facial hair color")]
	public Color facialHairColor = Color.white;

	/// <summary>
	/// The Character's face accessory index.
	/// </summary>
	[Tooltip("The Character's face accessory index.")]
	public int faceAccessoryIndex = 0;

	/// <summary>
	/// The Character's face accessory color.
	/// </summary>
	[Tooltip("The Character's face accessory color.")]
	public Color faceAccessoryColor = Color.white;

	[Header("Torso Parts")]
	/// <summary>
	/// The Character's shirt index.
	/// </summary>
	[Tooltip("The Character's shirt index.")]
	public int shirtIndex = 0;

	/// <summary>
	/// The Character's shirt color.
	/// </summary>
	[Tooltip("The Character's shirt color.")]
	public Color shirtColor = Color.white;

	/// <summary>
	/// The Character's back accessory index.
	/// </summary>
	[Tooltip("The Character's back accessory index.")]
	public int backAccessoryIndex = 0;

	/// <summary>
	/// The Character's back accessory color.
	/// </summary>
	[Tooltip("The Character's back accessory color.")]
	public Color backAccessoryColor = Color.white;

	/// <summary>
	/// The Character's gloves index.
	/// </summary>
	[Tooltip("The Character's gloves index.")]
	public int glovesIndex = 0;

	/// <summary>
	/// The Character's gloves color.
	/// </summary>
	[Tooltip("The Character's gloves color.")]
	public Color glovesColor = Color.white;

	[Header("Bottom Parts")]
	/// <summary>
	/// The Character's pants index.
	/// </summary>
	[Tooltip("The Character's pants index.")]
	public int pantsIndex = 0;

	/// <summary>
	/// The Character's pants color.
	/// </summary>
	[Tooltip("The Character's pants color.")]
	public Color pantsColor = Color.white;

	/// <summary>
	/// The Character's waist accessory index.
	/// </summary>
	[Tooltip("The Character's waist accessory index.")]
	public int waistAccessoryIndex = 0;

	/// <summary>
	/// The Character's waist accessory color.
	/// </summary>
	[Tooltip("The Character's waist accessory color.")]
	public Color waistAccessoryColor = Color.white;

	/// <summary>
	/// The Character's shoes index.
	/// </summary>
	[Tooltip("The Character's shoes index.")]
	public int shoesIndex = 0;

	/// <summary>
	/// The Character's shoes color.
	/// </summary>
	[Tooltip("The Character's shoes color.")]
	public Color shoesColor = Color.white;
	#endregion
	#endregion

	#region Methods
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// Saves the character.
	/// </summary>
	public void Save()
	{
		if (id == -1)
		{
			id = 0;
			id += PlayerPrefs.GetInt("CharacterCount");
			PlayerPrefs.SetInt("CharacterCount", id + 1);
		}
		string jsonSavePath = Application.persistentDataPath + "/Characters/" + id;

		string jsonData = JsonUtility.ToJson(this);
		File.WriteAllText(jsonSavePath, jsonData);
	}

	/// <summary>
	/// Copies all of the info from an instance to this instance.
	/// </summary>
	/// <param name="instanceToCopyFrom">The character info instance to copy from.</param>
	public void Copy(CharacterInfo instanceToCopyFrom)
	{
		name = instanceToCopyFrom.name;
		height = instanceToCopyFrom.height;
		weight = instanceToCopyFrom.weight;
		animationControllerIndex = instanceToCopyFrom.animationControllerIndex;
		pitchOfVoice = instanceToCopyFrom.pitchOfVoice;
		skinColor = instanceToCopyFrom.skinColor;
		hairstyleIndex = instanceToCopyFrom.hairstyleIndex;
		hairstyleColor = instanceToCopyFrom.hairstyleColor;
		eyesIndex = instanceToCopyFrom.eyesIndex;
		eyeColor = instanceToCopyFrom.eyeColor;
		noseIndex = instanceToCopyFrom.noseIndex;
		noseColor = instanceToCopyFrom.noseColor;
		hatIndex = instanceToCopyFrom.hatIndex;
		hatColor = instanceToCopyFrom.hatColor;
		facialHairIndex = instanceToCopyFrom.facialHairIndex;
		facialHairColor = instanceToCopyFrom.facialHairColor;
		faceAccessoryIndex = instanceToCopyFrom.faceAccessoryIndex;
		faceAccessoryColor = instanceToCopyFrom.faceAccessoryColor;
		shirtIndex = instanceToCopyFrom.shirtIndex;
		shirtColor = instanceToCopyFrom.shirtColor;
		backAccessoryIndex = instanceToCopyFrom.backAccessoryIndex;
		backAccessoryColor = instanceToCopyFrom.backAccessoryColor;
		glovesIndex = instanceToCopyFrom.glovesIndex;
		glovesColor = instanceToCopyFrom.glovesColor;
		pantsIndex = instanceToCopyFrom.pantsIndex;
		pantsColor = instanceToCopyFrom.pantsColor;
		waistAccessoryIndex = instanceToCopyFrom.waistAccessoryIndex;
		waistAccessoryColor = instanceToCopyFrom.waistAccessoryColor;
		shoesIndex = instanceToCopyFrom.shoesIndex;
		shoesColor = instanceToCopyFrom.shoesColor;
	}
	#endregion
	#endregion
}