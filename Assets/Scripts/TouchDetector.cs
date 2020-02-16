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
	/// <summary>
	/// Boolean to indicated if the state was dragged to stop pressing after rotation.
	/// </summary>
	private bool shouldActivate = false;
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
	/// Calls the mouse up event on mouse up.
	/// </summary>
	private void OnMouseUp()
	{
		if (shouldActivate)
		{
			mouseDownEvent.Invoke();
		}

		shouldActivate = false;
	}

	/// <summary>
	/// If mouse down.
	/// </summary>
	private void OnMouseDown()
	{
		shouldActivate = true;
	}

	/// <summary>
	/// if exited don't activate.
	/// </summary>
	private void OnMouseExit()
	{
		shouldActivate = false;
	}
	#endregion
	#region Public

	#endregion
	#endregion
}