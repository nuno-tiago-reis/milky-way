namespace MilkyWay.Utility
{
	/// <summary>
	/// Implements a class representing a bounded timer.
	/// </summary>
	///
	/// <seealso cref="Timer"/>
	public sealed class BoundedTimer : Timer
	{
		/// <summary>
		/// The timers minimum duration.
		/// </summary>
		public float Minimum { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BoundedTimer"/> class.
		/// </summary>
		/// 
		/// <param name="mode">The mode the timer is running on.</param>
		public BoundedTimer(TimerMode mode) : base(mode)
		{
			// Nothing to do here.
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// 
		/// <param name="currentDuration">The timers current duration.</param>
		/// <param name="minimumDuration">The timers minimum duration.</param>
		/// <param name="maximumDuration">The timers maximum duration.</param>
		public void Initialize(float currentDuration, float minimumDuration, float maximumDuration)
		{
			// Initialize the current, minimum and maximum durations
			this.Minimum = minimumDuration;
			this.Current = currentDuration;
			this.Maximum = maximumDuration;
		}

		/// <inheritdoc/>
		public override void Stop()
		{
			// Only stop if we're over the minimum duration
			if (this.Current < this.Minimum)
				return;

			// Reset the timer running flag
			this.Running = false;
			// Reset the timer finished flag
			this.Finished = true;
		}
	}
}