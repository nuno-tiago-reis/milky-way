namespace MilkyWay
{
	/// <summary>
	/// Defines the necessary methods to implement a Runnable Object.
	/// 
	/// These methods are necessary to control the Objects Lifecycle.
	/// These events are used to initialized, update and destroy the Object.
	/// </summary>
	public interface IObjectController
	{
		/// <summary>
		/// Creates the Object.
		/// </summary>
		void ObjectCreate();

		/// <summary>
		/// Updates the Object.
		/// </summary>
		void ObjectUpdate();

		/// <summary>
		/// Destroys the Object.
		/// </summary>
		void ObjectDestroy();
	}
}