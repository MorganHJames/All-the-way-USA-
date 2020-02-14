////////////////////////////////////////////////////////////
// File: StateController.cs
// Author: Morgan Henry James
// Date Created: 12-02-2020
// Brief: Controls all of the state info and displays.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using TMPro;

/// <summary>
/// Controls all of the state info and displays.
/// </summary>
public class StateController : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The state's information.
	/// </summary>
	[Tooltip("The state's information.")]
	[SerializeField] private StateInfo stateInfo;

	/// <summary>
	/// The state's animator.
	/// </summary>
	[Tooltip("The state's animator.")]
	[SerializeField] private Animator animator;

	/// <summary>
	/// The state's name in world space.
	/// </summary>
	[Tooltip("The state's name in world space.")]
	[SerializeField] private TextMeshProUGUI stateNameWorldSpace;

	/// <summary>
	/// The state's abbreviation in world space.
	/// </summary>
	[Tooltip("The state's abbreviation in world space.")]
	[SerializeField] private TextMeshProUGUI stateAbbreviationWorldSpace;

	/// <summary>
	/// The state's capital in world space.
	/// </summary>
	[Tooltip("The state's capital in world space.")]
	[SerializeField] private TextMeshProUGUI stateCapitalWorldSpace;

	/// <summary>
	/// All the different capital meshes.
	/// </summary>
	[Tooltip("All the different capital meshes.")]
	[SerializeField] private Mesh[] capitalMeshes;

	/// <summary>
	/// The mesh filter for the capital.
	/// </summary>
	[Tooltip("The mesh filter for the capital.")]
	[SerializeField] private MeshFilter capitalMeshFilter;

	/// <summary>
	/// All the different capital materials.
	/// </summary>
	[Tooltip("All the different capital materials.")]
	[SerializeField] private Material[] capitalMaterials;

	/// <summary>
	/// The mesh renderer for the capital.
	/// </summary>
	[Tooltip("The mesh renderer for the capital.")]
	[SerializeField] private MeshRenderer captialMeshRenderer;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the appropriate UI up.
	/// </summary>
	private void Start()
	{
		if (stateInfo)
		{
			stateNameWorldSpace.text = stateInfo.name;
			stateAbbreviationWorldSpace.text = stateInfo.abbreviation;
			stateCapitalWorldSpace.text = stateInfo.capital;
			capitalMeshFilter.mesh = capitalMeshes[Random.Range(0, capitalMeshes.Length)];
			captialMeshRenderer.material = capitalMaterials[Random.Range(0, capitalMaterials.Length)];
		}

		switch (PlayerPrefs.GetInt("StateShowName"))
		{
			case 0:
				animator.Play("ShowName");
				break;
			case 1:
				animator.Play("ShowAbbreviation");
				break;
			case 2:
				break;
			default:
				break;
		}

		switch (PlayerPrefs.GetInt("StateShowCapital"))
		{
			case 0:
				break;
			case 1:
				animator.Play("ShowCapital", 1);
				break;
			default:
				break;
		}
	}
	#endregion
	#region Public
	/// <summary>
	/// Plays a specific animation.
	/// </summary>
	/// <param name="animationToPlay">The animation to play.</param>
	/// <param name="layer">The animation layer to play from.</param>
	public void PlayAnimation(string animationToPlay, int layer = 0)
	{
		animator.Play(animationToPlay, layer);
	}
	#endregion
	#endregion
}