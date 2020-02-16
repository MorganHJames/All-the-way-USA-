////////////////////////////////////////////////////////////
// File: StateInfo.cs
// Author: Morgan Henry James
// Date Created: 11-02-2020
// Brief: A scriptable object for the information on each state.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// A scriptable object for the information on each state.
/// </summary>
[CreateAssetMenu(fileName = "New State Info", menuName = "StateInfo")]
public class StateInfo : ScriptableObject
{
	#region Variables
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// The state's name.
	/// </summary>
	[Tooltip("The state's name.")]
	public new string name;

	/// <summary>
	/// The state's name abbreviation.
	/// </summary>
	[Tooltip("The state's name abbreviation.")]
	public string abbreviation;

	/// <summary>
	/// The name of the state's capital city.
	/// </summary>
	[Tooltip("The name of the state's capital city.")]
	public string capital;

	/// <summary>
	/// The state's flag.
	/// </summary>
	[Tooltip("The state's flag.")]
	public Sprite flag;

	/// <summary>
	/// The state's seal.
	/// </summary>
	[Tooltip("The state's seal.")]
	public Sprite seal;

	/// <summary>
	/// Short state info.
	/// </summary>
	[Tooltip("Short state info.")]
	public string info;
	#endregion
	#endregion

	#region Methods
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion
}