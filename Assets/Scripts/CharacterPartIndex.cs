////////////////////////////////////////////////////////////
// File: CharacterPartIndex.cs
// Author: Morgan Henry James
// Date Created: 29-02-2020
// Brief: Holds all the available parts for the character.
//////////////////////////////////////////////////////////// 

using System;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Holds all the available parts for the character.
/// </summary>
[CreateAssetMenu(fileName = "New Character Part Index", menuName = "Character Part Index")]
public class CharacterPartIndex : ScriptableObject
{
	#region Variables
	#region Private

	#endregion
	#region Public
	[Header("Head Parts")]
	/// <summary>
	/// All of the animation controllers.
	/// </summary>
	[Tooltip("All of the animation controllers.")]
	public AnimatorController[] animatorControllers;

	/// <summary>
	/// All of the hairstyle meshes.
	/// </summary>
	[Tooltip("All of the hairstyle meshes.")]
	public Mesh[] hairstyles;

	/// <summary>
	/// All of the eyes meshes.
	/// </summary>
	[Tooltip("All of the eyes meshes.")]
	public Mesh[] eyes;

	/// <summary>
	/// All of the nose meshes.
	/// </summary>
	[Tooltip("All of the nose meshes.")]
	public Mesh[] noses;

	/// <summary>
	/// All of the hat meshes.
	/// </summary>
	[Tooltip("All of the hat meshes.")]
	public Mesh[] hats;

	/// <summary>
	/// All of the facial hair meshes.
	/// </summary>
	[Tooltip("All of the facial hair meshes.")]
	public Mesh[] facialHairs;

	/// <summary>
	/// All of the face accessory meshes.
	/// </summary>
	[Tooltip("All of the face accessory meshes.")]
	public Mesh[] faceAccessorys;

	[Header("Torso Parts")]
	/// <summary>
	/// All of the shirt prefabs.
	/// </summary>
	[Tooltip("All of the shirt prefabs.")]
	public GameObject[] shirts;

	/// <summary>
	/// All of the back accessory prefabs.
	/// </summary>
	[Tooltip("All of the back accessory prefabs.")]
	public GameObject[] backAccessorys;

	/// <summary>
	/// All of the glove prefabs.
	/// </summary>
	[Tooltip("All of the glove prefabs.")]
	public GameObject[] gloves;

	[Header("Bottom Parts")]
	/// <summary>
	/// All of the pants prefabs.
	/// </summary>
	[Tooltip("All of the pants prefabs.")]
	public GameObject[] pants;

	/// <summary>
	/// All of the waist accessory meshes.
	/// </summary>
	[Tooltip("All of the waist accessory meshes.")]
	public Mesh[] waistAccessorys;

	/// <summary>
	/// All of the shoes prefabs.
	/// </summary>
	[Tooltip("All of the shoes prefabs.")]
	public GameObject[] shoes;
	#endregion
	#endregion

	#region Methods
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion
}