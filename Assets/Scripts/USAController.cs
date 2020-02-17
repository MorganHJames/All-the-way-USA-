////////////////////////////////////////////////////////////
// File: USAController.cs
// Author: Morgan Henry James
// Date Created: 12-02-2020
// Brief: Controls all of the states.
//////////////////////////////////////////////////////////// 

using TMPro;
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
	/// All of the states touch detectors.
	/// </summary>
	[Tooltip("All of the states touch detectors.")]
	[SerializeField] private TouchDetector[] touchDetectors;

	/// <summary>
	/// The Name toggle on button.
	/// </summary>
	[Tooltip("The Name toggle on button.")]
	[SerializeField] private Button nameToggleOn;

	/// <summary>
	/// The Name toggle off button.
	/// </summary>
	[Tooltip("The Name toggle off button.")]
	[SerializeField] private Button nameToggleOff;

	/// <summary>
	/// The abbreviation toggle on button.
	/// </summary>
	[Tooltip("The abbreviation toggle on button.")]
	[SerializeField] private Button abbreviationToggleOn;

	/// <summary>
	/// The abbreviation toggle off button.
	/// </summary>
	[Tooltip("The abbreviation toggle off button.")]
	[SerializeField] private Button abbreviationToggleOff;

	/// <summary>
	/// The capital toggle on button.
	/// </summary>
	[Tooltip("The capital toggle on button.")]
	[SerializeField] private Button capitalToggleOn;

	/// <summary>
	/// The capital toggle off button.
	/// </summary>
	[Tooltip("The capital toggle off button.")]
	[SerializeField] private Button capitalToggleOff;

	/// <summary>
	/// The info UI animator.
	/// </summary>
	[Tooltip("The info UI animator.")]
	[SerializeField] private Animator infoAnimator;

	/// <summary>
	/// If the info is shown or not.
	/// </summary>
	private bool infoShown = false;

	/// <summary>
	/// The state name and abbreviation info.
	/// </summary>
	[Tooltip("The state name and abbreviation info.")]
	[SerializeField] private TextMeshProUGUI stateNameAndAbbreviation;

	/// <summary>
	/// The capital info.
	/// </summary>
	[Tooltip("The capital info.")]
	[SerializeField] private TextMeshProUGUI capitalInfo;

	/// <summary>
	/// The summary info.
	/// </summary>
	[Tooltip("The summary info.")]
	[SerializeField] private TextMeshProUGUI summaryInfo;

	/// <summary>
	/// The flag info.
	/// </summary>
	[Tooltip("The flag info.")]
	[SerializeField] private Image flagInfo;

	/// <summary>
	/// The seal info.
	/// </summary>
	[Tooltip("The seal info.")]
	[SerializeField] private Image sealInfo;
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

		switch (PlayerPrefs.GetInt("StateShowCapital"))
		{
			case 0:
				capitalToggleOn.gameObject.SetActive(false);
				capitalToggleOff.gameObject.SetActive(true);
				break;
			case 1:
				capitalToggleOn.gameObject.SetActive(true);
				capitalToggleOff.gameObject.SetActive(false);
				break;
			default:
				break;
		}

		foreach (TouchDetector touchDetector in touchDetectors)
		{
			touchDetector.mouseDownEvent.AddListener(()=>
			{
				if (!infoShown)
				{
					infoShown = true;
					infoAnimator.Play("InfoShow");
					StateController stateController = touchDetector.transform.parent.parent.GetComponent<StateController>();
					stateController.PlayAnimation("Selected", 2);
					stateController.selected = true;

					if (stateController.stateInfo)
					{
						stateNameAndAbbreviation.text = stateController.stateInfo.name + " (" + stateController.stateInfo.abbreviation + ")";
						capitalInfo.text = "Capital: " + stateController.stateInfo.capital;
						flagInfo.sprite = stateController.stateInfo.flag;
						sealInfo.sprite = stateController.stateInfo.seal;
						summaryInfo.text = stateController.stateInfo.info;
					}

					foreach (StateController stateController1 in stateControllers)
					{
						if (stateController != stateController1)
						{
							stateController1.PlayAnimation("Minimize", 2);
						}
					}
				}
			});
		}
	}
	#endregion
	#region Public
	/// <summary>
	/// Hides the info UI.
	/// </summary>
	public void InfoHide()
	{
		infoShown = false;
		infoAnimator.Play("InfoHide");

		foreach (StateController stateController in stateControllers)
		{
			if (stateController.selected)
			{
				stateController.selected = false;
				stateController.PlayAnimation("Deselected", 2);
			}
			else
			{
				stateController.PlayAnimation("Maximize", 2);
			}
		}
	}

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

	/// <summary>
	/// Turns on the capital city display.
	/// </summary>
	public void TurnOnCapitals()
	{
		capitalToggleOn.gameObject.SetActive(true);
		capitalToggleOff.gameObject.SetActive(false);

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("ShowCapital", 1);
		}

		PlayerPrefs.SetInt("StateShowCapital", 1);
	}

	/// <summary>
	/// Turns off the capital city display.
	/// </summary>
	public void TurnOffCapitals()
	{
		capitalToggleOn.gameObject.SetActive(false);
		capitalToggleOff.gameObject.SetActive(true);

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("HideCapital", 1);
		}

		PlayerPrefs.SetInt("StateShowCapital", 0);
	}
	#endregion
	#endregion
}