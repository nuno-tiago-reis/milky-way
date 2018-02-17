// Unity
using UnityEngine;

namespace MilkyWay.Managers.Menu
{
	/// <summary>
	/// Manages the menus in MilkyWay.
	/// </summary>
	public class MenuController : MonoBehaviour
	{
		#region [Attributes]
		/// <summary>
		/// The animator.
		/// </summary>
		private Animator Animator;
		/// <summary>
		/// The canvas group.
		/// </summary>
		private CanvasGroup CanvasGroup;

		/// <summary>
		/// Whether this menu is open.
		/// </summary>
		public bool IsOpen
		{
			get { return Animator.GetBool("isOpen"); }
			set { Animator.SetBool("isOpen", value); }
		}
		#endregion

		#region [Methods]
		/// <summary>
		/// Awakes this instance.
		/// </summary>
		public virtual void Awake()
		{
			/*this.Animator = GetComponent<Animator>();
			this.CanvasGroup = GetComponent<CanvasGroup>();

			// Disable the Menu
			this.IsOpen = false;*/
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public virtual void Update()
		{
			/*if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
			{
				this.CanvasGroup.blocksRaycasts = CanvasGroup.interactable = false;
			}
			else
			{
				this.CanvasGroup.blocksRaycasts = CanvasGroup.interactable = true;
			}*/
		}
		#endregion
	}
}