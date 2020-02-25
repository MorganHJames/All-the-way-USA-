////////////////////////////////////////////////////////////
// File: USAController.cs
// Author: Morgan Henry James
// Date Created: 12-02-2020
// Brief: Controls all of the states.
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	/// The test state controllers.
	/// </summary>
	private List<StateController> testStateControllers = new List<StateController>();

	/// <summary>
	/// The current Question.
	/// </summary>
	private int currentQuestion = 0;

	/// <summary>
	/// The amount of correct answers.
	/// </summary>
	private int correctAnswers = 0;

	/// <summary>
	/// The correct answer.
	/// </summary>
	private StateController correctStateController = null;

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
	/// The test UI animator.
	/// </summary>
	[Tooltip("The test UI animator.")]
	[SerializeField] private Animator testAnimator;

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
	/// The state name and capital test.
	/// </summary>
	[Tooltip("The state name and capital test.")]
	[SerializeField] private TextMeshProUGUI stateNameAndCapitalTest;

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
	/// The flag test.
	/// </summary>
	[Tooltip("The flag test.")]
	[SerializeField] private Image flagTest;

	/// <summary>
	/// The seal info.
	/// </summary>
	[Tooltip("The seal info.")]
	[SerializeField] private Image sealInfo;

	/// <summary>
	/// The current test type.
	/// </summary>
	private TestType currentTestType;

	/// <summary>
	/// The different test types.
	/// </summary>
	private enum TestType
	{
		Name,
		Capital,
		Flag
	}

	/// <summary>
	/// The name test score.
	/// </summary>
	[Tooltip("The name test score.")]
	[SerializeField] private TextMeshProUGUI nameTestScore;

	/// <summary>
	/// The capital test score.
	/// </summary>
	[Tooltip("The capital test score.")]
	[SerializeField] private TextMeshProUGUI capitalTestScore;

	/// <summary>
	/// The flag test score.
	/// </summary>
	[Tooltip("The flag test score.")]
	[SerializeField] private TextMeshProUGUI flagTestScore;

	/// <summary>
	/// The correct material.
	/// </summary>
	[Tooltip("The correct material.")]
	[SerializeField] private Material correctMaterial;

	/// <summary>
	/// The incorrect material.
	/// </summary>
	[Tooltip("The incorrect material.")]
	[SerializeField] private Material incorrectMaterial;

	/// <summary>
	/// The state material.
	/// </summary>
	[Tooltip("The state material.")]
	[SerializeField] private Material stateMaterial;
	#endregion
	#region Public
	/// <summary>
	/// Is the user taking a test?
	/// </summary>
	[Tooltip("Is the user taking a test?")]
	static public bool testing = false;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the buttons to the correct state.
	/// </summary>
	private void Start()
	{
		nameTestScore.text = "" + PlayerPrefs.GetInt("NameTestScore");

		capitalTestScore.text = "" + PlayerPrefs.GetInt("CapitalTestScore");

		flagTestScore.text = "" + PlayerPrefs.GetInt("FlagTestScore");

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
				if (!infoShown && !testing)
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

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.ZoomIn);
				}
				else if (!infoShown && testing && !touchDetector.GetComponent<MeshRenderer>().sharedMaterial.Equals(correctMaterial) && !touchDetector.GetComponent<MeshRenderer>().sharedMaterial.Equals(incorrectMaterial))
				{
					if (AnswerQuestion(touchDetector.transform.parent.parent.GetComponent<StateController>()))
					{
						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Correct);
						if (currentQuestion == 51)
						{
							bool gotAnswerRightFirstTime = true;

							foreach (TouchDetector touchDetector1 in touchDetectors)
							{
								if (touchDetector1.GetComponent<MeshRenderer>().sharedMaterial.Equals(incorrectMaterial))
								{
									touchDetector1.GetComponent<MeshRenderer>().material = stateMaterial;
									gotAnswerRightFirstTime = false;
								}
							}

							if (gotAnswerRightFirstTime)
							{
								correctAnswers++;
							}

							switch (currentTestType)
							{
								case TestType.Name:
									if (correctAnswers > PlayerPrefs.GetInt("NameTestScore"))
									{
										PlayerPrefs.SetInt("NameTestScore", correctAnswers);
										nameTestScore.text = "" + correctAnswers;
									}
									break;
								case TestType.Capital:
									if (correctAnswers > PlayerPrefs.GetInt("CapitalTestScore"))
									{
										PlayerPrefs.SetInt("CapitalTestScore", correctAnswers);
										capitalTestScore.text = "" + correctAnswers;
									}
									break;
								case TestType.Flag:
									if (correctAnswers > PlayerPrefs.GetInt("FlagTestScore"))
									{
										PlayerPrefs.SetInt("FlagTestScore", correctAnswers);
										flagTestScore.text = "" + correctAnswers;
									}
									break;
								default:
									break;
							}

							foreach (TouchDetector touchDetector1 in touchDetectors)
							{
								touchDetector1.GetComponent<MeshRenderer>().material = stateMaterial;
							}
						}
						else
						{
							// Set color to green.
							touchDetector.GetComponent<MeshRenderer>().material = correctMaterial;

							bool gotAnswerRightFirstTime = true;

							foreach (TouchDetector touchDetector1 in touchDetectors)
							{
								if (touchDetector1.GetComponent<MeshRenderer>().sharedMaterial.Equals(incorrectMaterial))
								{
									touchDetector1.GetComponent<MeshRenderer>().material = stateMaterial;
									gotAnswerRightFirstTime = false;
								}
							}

							if (gotAnswerRightFirstTime)
							{
								correctAnswers++;
							}

							switch (currentTestType)
							{
								case TestType.Name:
									if (correctAnswers > PlayerPrefs.GetInt("NameTestScore"))
									{
										PlayerPrefs.SetInt("NameTestScore", correctAnswers);
										nameTestScore.text = "" + correctAnswers;
									}
									break;
								case TestType.Capital:
									if (correctAnswers > PlayerPrefs.GetInt("CapitalTestScore"))
									{
										PlayerPrefs.SetInt("CapitalTestScore", correctAnswers);
										capitalTestScore.text = "" + correctAnswers;
									}
									break;
								case TestType.Flag:
									if (correctAnswers > PlayerPrefs.GetInt("FlagTestScore"))
									{
										PlayerPrefs.SetInt("FlagTestScore", correctAnswers);
										flagTestScore.text = "" + correctAnswers;
									}
									break;
								default:
									break;
							}
						}
					}
					else
					{
						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Incorrect);
						// Set color the red.
						touchDetector.GetComponent<MeshRenderer>().material = incorrectMaterial;
					}
				}
			});
		}
	}

	/// <summary>
	/// Start a test.
	/// </summary>
	/// <param name="test">The test type to set the controller to.</param>
	private IEnumerator StartTesting(TestType test)
	{
		if (infoShown)
		{
			InfoHide();
			yield return new WaitForSeconds(0.25f);
		}

		currentTestType = test;
		testing = true;
		testAnimator.Play("StartTest");

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("Separate", 2);

			switch (PlayerPrefs.GetInt("StateShowName"))
			{
				case 0:
					stateController.PlayAnimation("HideName");
					break;
				case 1:
					stateController.PlayAnimation("HideAbbreviation");
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
					stateController.PlayAnimation("HideCapital", 1);
					break;
				default:
					break;
			}
		}

		//Create list of state controllers in random order.
		testStateControllers = stateControllers.ToList();
		testStateControllers = testStateControllers.OrderBy(x => Random.value).ToList();

		//Set the question number to 0.
		currentQuestion = 0;

		//Reset correct amount.
		correctAnswers = 0;

		NextQuesiton();

		switch (currentTestType)
		{
			case TestType.Name:
				testAnimator.Play("ShowText", 1);
				break;
			case TestType.Capital:
				testAnimator.Play("ShowText", 1);
				break;
			case TestType.Flag:
				testAnimator.Play("ShowFlag", 1);
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Turns of the testing boolean.
	/// </summary>
	private void TurnOffTesting()
	{
		testing = false;
	}

	/// <summary>
	/// Answers the current question on the screen.
	/// </summary>
	/// <param name="stateController">The answer to the question.</param>
	/// <returns></returns>
	private bool AnswerQuestion(StateController stateController)
	{
		if (stateController == correctStateController)
		{
			NextQuesiton();
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Shows the next question on the screen.
	/// </summary>
	private void NextQuesiton()
	{
		if (currentQuestion < 50)
		{
			correctStateController = testStateControllers[currentQuestion];

			switch (currentTestType)
			{
				case TestType.Name:
					stateNameAndCapitalTest.text = correctStateController.stateInfo.name;
					break;
				case TestType.Capital:
					stateNameAndCapitalTest.text = correctStateController.stateInfo.capital;
					break;
				case TestType.Flag:
					flagTest.sprite = correctStateController.stateInfo.flag;
					break;
				default:
					break;
			}
		}
		else
		{
			StopTest();
		}

		currentQuestion++;
	}
	#endregion
	#region Public
	/// <summary>
	/// Hides the info UI.
	/// </summary>
	/// <param name="playSound">True when you want a sound to play.</param>
	public void InfoHide(bool playSound = false)
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

		if (playSound)
		{
			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.ZoomOut);
		}
	}

	/// <summary>
	/// Turns on the state names.
	/// </summary>
	public void	TurnOnNames()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
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
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
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
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
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
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		abbreviationToggleOn.gameObject.SetActive(false);
		abbreviationToggleOff.gameObject.SetActive(true);

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("HideAbbreviation");
		}

		PlayerPrefs.SetInt("StateShowName", 2);
	}

	/// <summary>
	/// Turns on the capital city display.
	/// </summary>
	public void TurnOnCapitals()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
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
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		capitalToggleOn.gameObject.SetActive(false);
		capitalToggleOff.gameObject.SetActive(true);

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("HideCapital", 1);
		}

		PlayerPrefs.SetInt("StateShowCapital", 0);
	}

	/// <summary>
	/// Starts the test about the state names.
	/// </summary>
	public void TestName()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		StartCoroutine(StartTesting(TestType.Name));
	}

	/// <summary>
	/// Starts the test about the state capitals.
	/// </summary>
	public void TestCapital()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		StartCoroutine(StartTesting(TestType.Capital));
	}

	/// <summary>
	/// Starts the test about the state flags.
	/// </summary>
	public void TestFlag()
	{
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		StartCoroutine(StartTesting(TestType.Flag));
	}

	/// <summary>
	/// Stops the current test.
	/// </summary>
	/// <param name="playSound">True if you want a sound to play.</param>
	public void StopTest(bool playSound = false)
	{
		switch (currentTestType)
		{
			case TestType.Name:
				testAnimator.Play("HideText", 1);
				break;
			case TestType.Capital:
				testAnimator.Play("HideText", 1);
				break;
			case TestType.Flag:
				testAnimator.Play("HideFlag", 1);
				break;
			default:
				break;
		}

		Invoke("TurnOffTesting", 0.25f);
		testAnimator.Play("StopTest");

		foreach (StateController stateController in stateControllers)
		{
			stateController.PlayAnimation("Unseparate", 2);

			switch (PlayerPrefs.GetInt("StateShowName"))
			{
				case 0:
					stateController.PlayAnimation("ShowName");
					break;
				case 1:
					stateController.PlayAnimation("ShowAbbreviation");
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
					stateController.PlayAnimation("ShowCapital", 1);
					break;
				default:
					break;
			}
		}

		foreach (TouchDetector touchDetector in touchDetectors)
		{
			touchDetector.GetComponent<MeshRenderer>().material = stateMaterial;
		}
		if (playSound)
		{
			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
		}
	}
	#endregion
	#endregion
}