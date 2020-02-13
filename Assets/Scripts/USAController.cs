////////////////////////////////////////////////////////////
// File: USAController.cs
// Author: Morgan Henry James
// Date Created: 12-02-2020
// Brief: Controls all of the states.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls all of the states.
/// </summary>
public class USAController : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// All of the state controllers.
	/// </summary>
	[Tooltip("All of the state controllers.")]
	[SerializeField] private StateController[] stateControllers;

	/// <summary>
	/// The Name toggle on button.
	/// </summary>
	[Tooltip("The Name toggle on button.")]
	[SerializeField] private Button nameToggleOn;

	/// <summary>
	/// The Name toggle on button.
	/// </summary>
	[Tooltip("The Name toggle on button.")]
	[SerializeField] private Button nameToggleOff;

	/// <summary>
	/// The abbreviation toggle on button.
	/// </summary>
	[Tooltip("The abbreviation toggle on button.")]
	[SerializeField] private Button abbreviationToggleOn;

	/// <summary>
	/// The abbreviation toggle on button.
	/// </summary>
	[Tooltip("The abbreviation toggle on button.")]
	[SerializeField] private Button abbreviationToggleOff;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the buttons to the correct state.
	/// </summary>
	private void Start()
	{
		switch (PlayerPrefs.GetInt("StateShowName"))
		{
			case 0:
				nameToggleOn.gameObject.SetActive(true);
				abbreviationToggleOff.gameObject.SetActive(true);
				break;
			case 1:
				abbreviationToggleOn.gameObject.SetActive(true);
				nameToggleOff.gameObject.SetActive(true);
				break;
			case 2:
				nameToggleOff.gameObject.SetActive(true);
				abbreviationToggleOff.gameObject.SetActive(true);
				break;
			default:
				break;
		}
	}
	#endregion
	#region Public
	/// <summary>
	/// Turns on the state names.
	/// </summary>
	public void	TurnOnNames()
	{
		nameToggleOn.gameObject.SetActive(true);
		nameToggleOff.gameObject.SetActive(false);
		abbreviationToggleOff.gameObject.SetActive(true);
		abbreviationToggleOn.gameObject.SetActive(false);

		switch (PlayerPrefs.GetInt("StateShowName"))
		{
			case 0:
				break;
			case 1:
				foreach (StateController stateController in stateControllers)
				{
					stateController.PlayAnimation("HideAbbreviationShowName");
				}
				break;
			case 2:
				foreach (StateController stateController in stateControllers)
				{
					stateController.PlayAnimation("ShowName");
				}
				break;
			default:
				break;
		}

		PlayerPrefs.SetInt("StateShowName", 0);
	}

	/// <summary>
	/// Turns off the state names.
	/// </summary>
	public void TurnOffNames()
	{
		nameToggleOn.gameObject.SetActive(false);
		nameToggleOff.gameObject.SetActive(true);

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("HideName");
		}

		PlayerPrefs.SetInt("StateShowName", 2);
	}

	/// <summary>
	/// Turns on the state abbreviations.
	/// </summary>
	public void TurnOnAbbreviations()
	{
		nameToggleOn.gameObject.SetActive(false);
		nameToggleOff.gameObject.SetActive(true);
		abbreviationToggleOff.gameObject.SetActive(false);
		abbreviationToggleOn.gameObject.SetActive(true);

		switch (PlayerPrefs.GetInt("StateShowName"))
		{
			case 0:
				foreach (StateController stateController in stateControllers)
				{
					stateController.PlayAnimation("HideNameShowAbbreviation");
				}
				break;
			case 1:
				break;
			case 2:
				foreach (StateController stateController in stateControllers)
				{
					stateController.PlayAnimation("ShowAbbreviation");
				}
				break;
			default:
				break;
		}

		PlayerPrefs.SetInt("StateShowName", 1);
	}

	/// <summary>
	/// Turns on the state abbreviations.
	/// </summary>
	public void TurnOffAbbreviations()
	{
		abbreviationToggleOn.gameObject.SetActive(false);
		abbreviationToggleOff.gameObject.SetActive(true);

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("HideAbbreviation");
		}

		PlayerPrefs.SetInt("StateShowName", 0);
	}
	#endregion
	#endregion
}