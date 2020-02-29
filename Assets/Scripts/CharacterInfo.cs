////////////////////////////////////////////////////////////
// File: CharacterInfo.cs
// Author: Morgan Henry James
// Date Created: 28-02-2020
// Brief: Holds all of the information pertaining to a specific character.
//////////////////////////////////////////////////////////// 

using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Holds all of the information pertaining to a specific character.
/// </summary>
[CreateAssetMenu(fileName = "New Character Info", menuName = "CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
	#region Variables
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// A structure that defines customizable part of the character.
	/// </summary>
	[System.Serializable]
	public struct CharacterPart
	{
		public Color partColor;
		public Mesh partMesh;
	}

	[Header("Basic Info")]
	/// <summary>
	/// The Character's name.
	/// </summary>
	[Tooltip("The Character's name.")]
	public new string name;

	/// <summary>
	/// The Character's height.
	/// </summary>
	[Tooltip("The Character's height.")]
	public float height;

	/// <summary>
	/// The Character's height.
	/// </summary>
	[Tooltip("The Character's height.")]
	public float weight;

	/// <summary>
	/// The Character's animation controller.
	/// </summary>
	[Tooltip("The Character's animation controller.")]
	public AnimatorController animationController;

	/// <summary>
	/// The Character's pitch of voice.
	/// </summary>
	[Tooltip("The Character's pitch of voice.")]
	public float pitchOfVoice;

	/// <summary>
	/// The Character's skin color.
	/// </summary>
	[Tooltip("The Character's skin color.")]
	public Color skinColor;

	[Header("Head Parts")]
	/// <summary>
	/// The Character's hairstyle.
	/// </summary>
	[Tooltip("The Character's hairstyle.")]
	public CharacterPart hairstyle;

	/// <summary>
	/// The Character's eyes.
	/// </summary>
	[Tooltip("The Character's eyes.")]
	public CharacterPart eyes;

	/// <summary>
	/// The Character's nose.
	/// </summary>
	[Tooltip("The Character's nose.")]
	public CharacterPart nose;

	/// <summary>
	/// The Character's hat.
	/// </summary>
	[Tooltip("The Character's hat.")]
	public CharacterPart hat;

	/// <summary>
	/// The Character's facial hair.
	/// </summary>
	[Tooltip("The Character's facial hair.")]
	public CharacterPart facialHair;

	/// <summary>
	/// The Character's face accessory.
	/// </summary>
	[Tooltip("The Character's face accessory.")]
	public CharacterPart faceAccessory;

	[Header("Torso Parts")]
	/// <summary>
	/// The Character's shirt.
	/// </summary>
	[Tooltip("The Character's shirt.")]
	public CharacterPart shirt;

	/// <summary>
	/// The Character's back accessory.
	/// </summary>
	[Tooltip("The Character's back accessory.")]
	public CharacterPart backAccessory;

	/// <summary>
	/// The Character's gloves.
	/// </summary>
	[Tooltip("The Character's gloves.")]
	public CharacterPart gloves;

	[Header("Bottom Parts")]
	/// <summary>
	/// The Character's pants.
	/// </summary>
	[Tooltip("The Character's pants.")]
	public CharacterPart pants;

	/// <summary>
	/// The Character's waist accessory.
	/// </summary>
	[Tooltip("The Character's waist accessory.")]
	public CharacterPart waistAccessory;

	/// <summary>
	/// The Character's shoes.
	/// </summary>
	[Tooltip("The Character's shoes.")]
	public CharacterPart shoes;
	#endregion
	#endregion

	#region Methods
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion
}