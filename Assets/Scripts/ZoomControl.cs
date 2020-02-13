////////////////////////////////////////////////////////////
// File: ZoomControl.cs
// Author: Morgan Henry James
// Date Created: 11-02-2020
// Brief: Controls the model size.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Controls the model size.
/// </summary>
public class ZoomControl : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// Where the touch starts.
	/// </summary>
	private Vector3 touchStart;

	/// <summary>
	/// How small the model can be.
	/// </summary>
	[SerializeField] private float zoomOutMin = 0f;

	/// <summary>
	/// How big the model can be.
	/// </summary>
	[SerializeField] private float zoomOutMax = 1f;

	/// <summary>
	/// How fast the model zooms.
	/// </summary>
	[SerializeField] private float zoomSpeed = 0.001f;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Allows the model to scale up or down.
	/// </summary>
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

			float difference = currentMagnitude - prevMagnitude;

			Zoom(difference * zoomSpeed);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Camera.main.transform.position += direction;
		}
		Zoom(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
	}

	/// <summary>
	/// Changes the size of the model.
	/// </summary>
	/// <param name="increment">How much to increase the size by.</param>
	private void Zoom(float increment)
	{
		if (!(transform.localScale.x + increment > zoomOutMax) && !(transform.localScale.x + increment < zoomOutMin))
		{
			transform.localScale += new Vector3(increment, increment, increment);
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}