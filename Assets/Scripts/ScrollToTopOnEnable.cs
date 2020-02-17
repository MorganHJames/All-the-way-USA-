////////////////////////////////////////////////////////////
// File: ScrollToTopOnEnable.cs
// Author: Morgan Henry James
// Date Created: 17-02-2020
// Brief: Scrolls the content to the top on enable.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Scrolls the content to the top on enable.
/// </summary>
public class ScrollToTopOnEnable : MonoBehaviour
{
	#region Variables
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Scrolls to top.
	/// </summary>
	private void OnEnable()
	{
		GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, 0f);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}