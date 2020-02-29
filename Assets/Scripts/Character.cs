////////////////////////////////////////////////////////////
// File: Character.cs
// Author: Morgan Henry James
// Date Created: 29-02-2020
// Brief: Holds and applies the character info to the character also handles the saving of the character.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Holds and applies the character info to the character also handles the saving of the character.
/// </summary>
public class Character : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The position that the character is currently heading towards.
	/// </summary>
	private Vector3 positionHeadedTo = new Vector3(0.0f, 0.0f, 0.0f);

	/// <summary>
	/// Where the character should be looking.
	/// </summary>
	private Quaternion positionToLookAt;

	/// <summary>
	/// The maximum distance the character can walk from world origin.
	/// </summary>
	private Vector2 wanderLimit = new Vector2(5.0f, 5.0f);

	/// <summary>
	/// The max the character can be idle for.
	/// </summary>
	private float maxIdleTime = 5f;

	/// <summary>
	/// How fast the character turns when on alert.
	/// </summary>
	private float alertTurnSpeed = 50f;

	/// <summary>
	/// How fast the character normal turns.
	/// </summary>
	private float normalTurnSpeed = 0.1f;

	/// <summary>
	/// How fast the character can run.
	/// </summary>
	private float runSpeed = 10.0f;

	/// <summary>
	/// How fast the character can walk.
	/// </summary>
	private float walkSpeed = 1.0f;

	/// <summary>
	/// The remaining time that the character should remain idle for.
	/// </summary>
	private float remainingIdleTime = 0.0f;

	/// <summary>
	/// All the states the character can turn.
	/// </summary>
	private enum CharacterStates
	{
		Idle,
		Walking,
		Running,
		Turning,
		AlertTurning,
		Alert,
		FaceUser
	}

	/// <summary>
	/// The current character state.
	/// </summary>
	private CharacterStates characterState = CharacterStates.Idle;

	/// <summary>
	/// The characters info.
	/// </summary>
	[SerializeField] private CharacterInfo characterInfo = null;

	[Header("Basic Info")]
	/// <summary>
	/// The Character's animator.
	/// </summary>
	[Tooltip("The Character's animator.")]
	[SerializeField] private Animator animator;

	/// <summary>
	/// The Character's body.
	/// </summary>
	[Tooltip("The Character's body.")]
	[SerializeField] private SkinnedMeshRenderer body;

	[Header("Head Parts")]
	/// <summary>
	/// The Character's hairstyle.
	/// </summary>
	[Tooltip("The Character's hairstyle.")]
	[SerializeField] private SkinnedMeshRenderer hairstyle;

	/// <summary>
	/// The Character's eyes.
	/// </summary>
	[Tooltip("The Character's eyes.")]
	[SerializeField] private SkinnedMeshRenderer eyes;

	/// <summary>
	/// The Character's nose.
	/// </summary>
	[Tooltip("The Character's nose.")]
	[SerializeField] private SkinnedMeshRenderer nose;

	/// <summary>
	/// The Character's hat.
	/// </summary>
	[Tooltip("The Character's hat.")]
	[SerializeField] private SkinnedMeshRenderer hat;

	/// <summary>
	/// The Character's facial hair.
	/// </summary>
	[Tooltip("The Character's facial hair.")]
	[SerializeField] private SkinnedMeshRenderer facialHair;

	/// <summary>
	/// The Character's face accessory.
	/// </summary>
	[Tooltip("The Character's face accessory.")]
	[SerializeField] private SkinnedMeshRenderer faceAccessory;

	[Header("Torso Parts")]
	/// <summary>
	/// The Character's shirt.
	/// </summary>
	[Tooltip("The Character's shirt.")]
	[SerializeField] private SkinnedMeshRenderer shirt;

	/// <summary>
	/// The Character's back accessory.
	/// </summary>
	[Tooltip("The Character's back accessory.")]
	[SerializeField] private SkinnedMeshRenderer backAccessory;

	/// <summary>
	/// The Character's gloves.
	/// </summary>
	[Tooltip("The Character's gloves.")]
	[SerializeField] private SkinnedMeshRenderer gloves;

	[Header("Bottom Parts")]
	/// <summary>
	/// The Character's pants.
	/// </summary>
	[Tooltip("The Character's pants.")]
	[SerializeField] private SkinnedMeshRenderer pants;

	/// <summary>
	/// The Character's waist accessory.
	/// </summary>
	[Tooltip("The Character's waist accessory.")]
	[SerializeField] private SkinnedMeshRenderer waistAccessory;

	/// <summary>
	/// The Character's shoes.
	/// </summary>
	[Tooltip("The Character's shoes.")]
	[SerializeField] private SkinnedMeshRenderer shoes;
	#endregion
	#region Public
	/// <summary>
	/// Plays the desired animation.
	/// </summary>
	/// <param name="animationName">Plays the desired animation.</param>
	public void PlayAnimation(string animationName)
	{
		animator.Play(animationName);
	}
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Applies the character info on startup.
	/// </summary>
	private void Start()
	{
		ApplyCharacterInfo();
	}

	/// <summary>
	/// Controls the movement of the character.
	/// </summary>
	private void Update()
	{
		remainingIdleTime -= Time.deltaTime;
		Mathf.Clamp(remainingIdleTime, 0.0f, maxIdleTime);

		switch (characterState)
		{
			case CharacterStates.Idle:
				if (remainingIdleTime <= 0.0f)
				{
					positionHeadedTo = new Vector3(Random.Range(-wanderLimit.x, wanderLimit.x), 0.0f, Random.Range(-wanderLimit.y, wanderLimit.y));
					positionToLookAt = Quaternion.LookRotation(positionHeadedTo - new Vector3(transform.position.x, 0.0f, transform.position.z));
					characterState = CharacterStates.Turning;
				}
				break;
			case CharacterStates.Walking:
				if (Vector3.Distance(new Vector3(transform.position.x, 0.0f, transform.position.z), positionHeadedTo) < 0.1f)
				{
					remainingIdleTime = Random.Range(0.0f, maxIdleTime);
					animator.Play("Idle");
					characterState = CharacterStates.Idle;
				}
				else
				{
					transform.position += transform.forward * (walkSpeed * Time.deltaTime);
				}
				break;
			case CharacterStates.Running:
				if (Vector3.Distance(new Vector3(transform.position.x, 0.0f, transform.position.z), positionHeadedTo) < 0.1f)
				{
					animator.Play("Alert");
					positionToLookAt = Quaternion.LookRotation(Vector3.back - new Vector3(transform.position.x, 0.0f, transform.position.z));
					characterState = CharacterStates.FaceUser;
				}
				else
				{
					transform.position += transform.forward * (runSpeed * Time.deltaTime);
				}
				break;
			case CharacterStates.Turning:
				if (Quaternion.Angle(transform.rotation, positionToLookAt) == 0.0f)
				{
					animator.Play("Walk");
					characterState = CharacterStates.Walking;
				}
				else
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, positionToLookAt, normalTurnSpeed);
				}
				break;
			case CharacterStates.AlertTurning:
				if (Quaternion.Angle(transform.rotation, positionToLookAt) == 0.0f)
				{
					animator.Play("Running");
					characterState = CharacterStates.Running;
				}
				else
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, positionToLookAt, alertTurnSpeed * Time.deltaTime);
				}
				break;
			case CharacterStates.Alert:
				break;
			case CharacterStates.FaceUser:
				if (Quaternion.Angle(transform.rotation, positionToLookAt) == 0.0f)
				{
					characterState = CharacterStates.Alert;
				}
				else
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, positionToLookAt, alertTurnSpeed * Time.deltaTime);
				}
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Applies the character info to the character.
	/// </summary>
	private void ApplyCharacterInfo()
	{
		transform.localScale.Set(transform.localScale.x, characterInfo.height, transform.localScale.z);
		transform.localScale.Set(characterInfo.weight, transform.localScale.y, characterInfo.weight);

		animator.runtimeAnimatorController = characterInfo.animationController;

		body.material.color = characterInfo.skinColor;

		hairstyle.sharedMesh = characterInfo.hairstyle.partMesh;
		hairstyle.material.color = characterInfo.hairstyle.partColor;

		eyes.sharedMesh = characterInfo.eyes.partMesh;
		eyes.material.color = characterInfo.eyes.partColor;

		nose.sharedMesh = characterInfo.nose.partMesh;
		nose.material.color = characterInfo.nose.partColor;

		hat.sharedMesh = characterInfo.hat.partMesh;
		hat.material.color = characterInfo.hat.partColor;

		facialHair.sharedMesh = characterInfo.facialHair.partMesh;
		facialHair.material.color = characterInfo.facialHair.partColor;

		faceAccessory.sharedMesh = characterInfo.faceAccessory.partMesh;
		faceAccessory.material.color = characterInfo.faceAccessory.partColor;

		shirt.sharedMesh = characterInfo.shirt.partMesh;
		shirt.material.color = characterInfo.shirt.partColor;

		backAccessory.sharedMesh = characterInfo.backAccessory.partMesh;
		backAccessory.material.color = characterInfo.backAccessory.partColor;

		gloves.sharedMesh = characterInfo.gloves.partMesh;
		gloves.material.color = characterInfo.gloves.partColor;

		pants.sharedMesh = characterInfo.pants.partMesh;
		pants.material.color = characterInfo.pants.partColor;

		waistAccessory.sharedMesh = characterInfo.waistAccessory.partMesh;
		waistAccessory.material.color = characterInfo.waistAccessory.partColor;

		shoes.sharedMesh = characterInfo.shoes.partMesh;
		shoes.material.color = characterInfo.shoes.partColor;
	}
	#endregion
	#region Public
	/// <summary>
	/// Indicate to the character to go to the alert position.
	/// </summary>
	/// <param name="alertPosition">The position that the character should run to and remain at alert.</param>
	public void Alert(Vector3 alertPosition)
	{
		positionToLookAt = Quaternion.LookRotation(alertPosition - new Vector3(transform.position.x, 0.0f, transform.position.z));
		positionHeadedTo = alertPosition;
		characterState = CharacterStates.AlertTurning;
	}

	/// <summary>
	/// Indicate to the character to stop being alert.
	/// </summary>
	public void StopAlert()
	{
		remainingIdleTime = Random.Range(0.0f, maxIdleTime);
		animator.Play("Idle");
		characterState = CharacterStates.Idle;
	}
	#endregion
	#endregion
}