////////////////////////////////////////////////////////////
// File: Character.cs
// Author: Morgan Henry James
// Date Created: 29-02-2020
// Brief: Holds and applies the character info to the character also handles the saving of the character.
//////////////////////////////////////////////////////////// 

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds and applies the character info to the character also handles the saving of the character.
/// </summary>
public class Character : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The TMP that holds the characters name.
	/// </summary>
	[Tooltip("The TMP that holds the characters name.")]
	[SerializeField] private TextMeshProUGUI nameText = null;

	/// <summary>
	/// The animator that controls the characters name.
	/// </summary>
	[Tooltip("The animator that controls the characters name.")]
	[SerializeField] private Animator nameTextAnimator = null;

	/// <summary>
	/// The character's user interaction script.
	/// </summary>
	[Tooltip("The character's user interaction script.")]
	[SerializeField] private CharacterUserInteraction characterUserInteraction = null;

	/// <summary>
	/// The position that the character is currently heading towards.
	/// </summary>
	private Vector3 positionHeadedTo = new Vector3(0.0f, 0.0f, 0.0f);

	/// <summary>
	/// Where the character was picked up from.
	/// </summary>
	private Vector3 pickedUpPoint = Vector3.zero;

	/// <summary>
	/// The characters picked up state.
	/// </summary>
	private CharacterStates pickedUpCharacterState = CharacterStates.Idle;

	/// <summary>
	/// The character's rotation on pick up.
	/// </summary>
	private Quaternion rotationOnPickUp = Quaternion.identity;

	/// <summary>
	/// The animation name played on pickup.
	/// </summary>
	private string animationOnPickup = "";

	/// <summary>
	/// Where the character should be looking.
	/// </summary>
	private Quaternion positionToLookAt;

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
		FaceUser,
		PickedUp,
		IsPickedUp
	}

	/// <summary>
	/// The current character state.
	/// </summary>
	private CharacterStates characterState = CharacterStates.Idle;
	#endregion
	#region Public
	[Header("Basic Info")]
	/// <summary>
	/// The Character's animator.
	/// </summary>
	[Tooltip("The Character's animator.")]
	public Animator animator = null;

	/// <summary>
	/// The Character's body.
	/// </summary>
	[Tooltip("The Character's body.")]
	public SkinnedMeshRenderer body = null;

	[Header("Head Parts")]
	/// <summary>
	/// The Character's hairstyle.
	/// </summary>
	[Tooltip("The Character's hairstyle.")]
	public SkinnedMeshRenderer hairstyle = null;

	/// <summary>
	/// The Character's eyes.
	/// </summary>
	[Tooltip("The Character's eyes.")]
	public SkinnedMeshRenderer eyes = null;

	/// <summary>
	/// The Character's nose.
	/// </summary>
	[Tooltip("The Character's nose.")]
	public SkinnedMeshRenderer nose = null;

	/// <summary>
	/// The Character's hat.
	/// </summary>
	[Tooltip("The Character's hat.")]
	public SkinnedMeshRenderer hat = null;

	/// <summary>
	/// The Character's facial hair.
	/// </summary>
	[Tooltip("The Character's facial hair.")]
	public SkinnedMeshRenderer facialHair = null;

	/// <summary>
	/// The Character's face accessory.
	/// </summary>
	[Tooltip("The Character's face accessory.")]
	public SkinnedMeshRenderer faceAccessory = null;

	[Header("Torso Parts")]
	/// <summary>
	/// The Character's shirt.
	/// </summary>
	[Tooltip("The Character's shirt.")]
	public GameObject shirtParent = null;

	/// <summary>
	/// The Character's back accessory.
	/// </summary>
	[Tooltip("The Character's back accessory.")]
	public GameObject backAccessoryParent = null;

	/// <summary>
	/// The Character's gloves.
	/// </summary>
	[Tooltip("The Character's gloves.")]
	public GameObject glovesParent = null;

	[Header("Bottom Parts")]
	/// <summary>
	/// The Character's pants.
	/// </summary>
	[Tooltip("The Character's pants.")]
	public GameObject pantsParent = null;

	/// <summary>
	/// The Character's waist accessory.
	/// </summary>
	[Tooltip("The Character's waist accessory.")]
	public GameObject waistAccessoryParent = null;

	/// <summary>
	/// The Character's shoes.
	/// </summary>
	[Tooltip("The Character's shoes.")]
	public GameObject shoesParent = null;
	/// <summary>
	/// The character part index.
	/// </summary>
	[Tooltip("The character part index.")]
	public CharacterPartIndex characterPartIndex = null;

	/// <summary>
	/// The characters info.
	/// </summary>
	[Tooltip("The characters info.")]
	public CharacterInfo characterInfo = null;

	/// <summary>
	/// The maximum distance the character can walk from world origin.
	/// </summary>
	[HideInInspector]
	public static Vector2 wanderLimit = new Vector2(5.0f, 5.0f);

	/// <summary>
	/// If the character is picked up or not.
	/// </summary>
	[HideInInspector]
	public bool isPickedUp = false;

	/// <summary>
	/// If the character is to be deleted.
	/// </summary>
	[HideInInspector]
	public bool isToBeDeleted = false;
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
					positionHeadedTo = new Vector3(Random.Range(-wanderLimit.x, wanderLimit.x), 0.0f, Random.Range(-wanderLimit.y + 2.5f, wanderLimit.y + 2.5f));
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
					positionToLookAt = Quaternion.LookRotation(Vector3.back);
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
			case CharacterStates.PickedUp:
				animator.Play("Falling");
				characterState = CharacterStates.IsPickedUp;
				break;
			case CharacterStates.IsPickedUp:
				transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y - 250.0f, Camera.main.nearClipPlane + 5f));
				break;
			default:
				break;
		}
	}
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
	/// Makes the character just stand there.
	/// </summary>
	public void Idle()
	{
		characterState = CharacterStates.Alert;
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

	/// <summary>
	/// Applies the character info to the character.
	/// </summary>
	public void ApplyCharacterInfo()
	{
		transform.localScale = new Vector3(transform.localScale.x, characterInfo.height, transform.localScale.z);

		transform.localScale= new Vector3(characterInfo.weight, transform.localScale.y, characterInfo.weight);

		animator.runtimeAnimatorController = characterPartIndex.animatorControllers[characterInfo.animationControllerIndex];

		body.material.color = characterInfo.skinColor;

		if (characterPartIndex.hairstyles.Length > 0)
		{
			hairstyle.sharedMesh = characterPartIndex.hairstyles[characterInfo.hairstyleIndex];
			hairstyle.material.color = characterInfo.hairstyleColor;
		}

		if (characterPartIndex.eyes.Length > 0)
		{
			eyes.sharedMesh = characterPartIndex.eyes[characterInfo.eyesIndex];
			eyes.material.color = characterInfo.eyeColor;
		}

		if (characterPartIndex.noses.Length > 0)
		{
			nose.sharedMesh = characterPartIndex.noses[characterInfo.noseIndex];
			nose.material.color = characterInfo.noseColor;
		}

		if (characterPartIndex.hats.Length > 0)
		{
			hat.sharedMesh = characterPartIndex.hats[characterInfo.hatIndex];
			hat.material.color = characterInfo.hatColor;
		}

		if (characterPartIndex.facialHairs.Length > 0)
		{
			facialHair.sharedMesh = characterPartIndex.facialHairs[characterInfo.facialHairIndex];
			facialHair.material.color = characterInfo.facialHairColor;
		}

		if (characterPartIndex.faceAccessorys.Length > 0)
		{
			faceAccessory.sharedMesh = characterPartIndex.faceAccessorys[characterInfo.faceAccessoryIndex];
			faceAccessory.material.color = characterInfo.faceAccessoryColor;
		}

		if (characterPartIndex.shirts.Length > 0)
		{
			if (shirtParent.transform.childCount > 0)
			{
				foreach (Transform child in shirtParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject shirt = Instantiate(characterPartIndex.shirts[characterInfo.shirtIndex], shirtParent.transform);
			shirt.GetComponentInChildren<SkinnedMeshRenderer>().material.color = characterInfo.shirtColor;
		}

		if (characterPartIndex.backAccessorys.Length > 0)
		{
			if (backAccessoryParent.transform.childCount > 0)
			{
				foreach (Transform child in backAccessoryParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject backAccessory = Instantiate(characterPartIndex.backAccessorys[characterInfo.backAccessoryIndex], backAccessoryParent.transform);
			backAccessory.GetComponentInChildren<SkinnedMeshRenderer>().material.color = characterInfo.backAccessoryColor;
		}

		if (characterPartIndex.gloves.Length > 0)
		{
			if (glovesParent.transform.childCount > 0)
			{
				foreach (Transform child in glovesParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject gloves = Instantiate(characterPartIndex.gloves[characterInfo.glovesIndex], glovesParent.transform);
			gloves.GetComponentInChildren<SkinnedMeshRenderer>().material.color = characterInfo.glovesColor;
		}

		if (characterPartIndex.pants.Length > 0)
		{
			if (pantsParent.transform.childCount > 0)
			{
				foreach (Transform child in pantsParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject pants = Instantiate(characterPartIndex.pants[characterInfo.pantsIndex], pantsParent.transform);
			pants.GetComponentInChildren<SkinnedMeshRenderer>().material.color = characterInfo.pantsColor;
		}

		if (characterPartIndex.waistAccessorys.Length > 0)
		{
			if (waistAccessoryParent.transform.childCount > 0)
			{
				foreach (Transform child in waistAccessoryParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject waistAccessory = Instantiate(characterPartIndex.waistAccessorys[characterInfo.waistAccessoryIndex], waistAccessoryParent.transform);
			waistAccessory.GetComponentInChildren<SkinnedMeshRenderer>().material.color = characterInfo.waistAccessoryColor;
		}

		if (characterPartIndex.shoes.Length > 0)
		{
			if (shoesParent.transform.childCount > 0)
			{
				foreach (Transform child in shoesParent.transform)
				{
					Destroy(child.gameObject);
				}
			}
			GameObject shoes = Instantiate(characterPartIndex.shoes[characterInfo.shoesIndex], shoesParent.transform);
			shoes.GetComponentInChildren<SkinnedMeshRenderer>().material.color = characterInfo.shoesColor;
		}

		nameText.text = characterInfo.name;

		characterUserInteraction.mouseEnterEvent.AddListener(() =>
		{
			if (SceneManager.GetActiveScene().name == "Plaza")
			{
				nameTextAnimator.Play("CharacterNameUp");
			}
		});

		characterUserInteraction.mouseExitEvent.AddListener(() =>
		{
			if (SceneManager.GetActiveScene().name == "Plaza")
			{
				nameTextAnimator.Play("CharacterNameDown");
			}
		});

		characterUserInteraction.mouseDownEvent.AddListener(() =>
		{
			if (SceneManager.GetActiveScene().name == "Plaza")
			{
				if (animator.GetCurrentAnimatorStateInfo(0).IsName("Alert"))
				{
					animationOnPickup = "Alert";
				}
				else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				{
					animationOnPickup = "Idle";
				}
				else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
				{
					animationOnPickup = "Walk";
				}
				else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
				{
					animationOnPickup = "Running";
				}

				pickedUpCharacterState = characterState;
				characterState = CharacterStates.PickedUp;
				isPickedUp = true;
				pickedUpPoint = transform.position;
				rotationOnPickUp = transform.rotation;

				transform.rotation = Quaternion.LookRotation(Vector3.back);
			}
		});

		characterUserInteraction.mouseUpEvent.AddListener(() =>
		{
			if (SceneManager.GetActiveScene().name == "Plaza")
			{
				if (CharacterManager.isOverDelete)
				{
					isToBeDeleted = true;
				}
				else if (CharacterManager.isOverEdit)
				{
					PlayerPrefs.SetInt("SelectedCharacter", characterInfo.id);
					SceneManager.LoadScene("CharacterCreation", LoadSceneMode.Single);
				}
				else
				{
					characterState = pickedUpCharacterState;
					isPickedUp = false;
					transform.position = pickedUpPoint;
					transform.rotation = rotationOnPickUp;
					animator.Play(animationOnPickup);
				}
			}
		});
	}
	#endregion
	#endregion
}