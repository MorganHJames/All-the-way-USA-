////////////////////////////////////////////////////////////
// File: TouchDetector.cs
// Author: Morgan Henry James
// Date Created: 14-02-2020
// Brief: Allows the objects on mouse down function to have logic attached.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows the objects on mouse down function to have logic attached.
/// </summary>
public class TouchDetector : MonoBehaviour
{
	#region Variables
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// The event that will be called on mouse down on the object.
	/// </summary>
	[HideInInspector] public UnityEvent mouseDownEvent = new UnityEvent();
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Calls the mouse down event on mouse down.
	/// </summary>
	private void OnMouseDown()
	{
		mouseDownEvent.Invoke();
	}
	#endregion
	#region Public

	#endregion
	#endregion
}