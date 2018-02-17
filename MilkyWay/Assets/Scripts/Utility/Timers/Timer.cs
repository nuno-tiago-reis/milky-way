// Unity
using UnityEngine;

namespace MilkyWay.Utility
{
	/// <summary>
	/// Enumerates all the possible timer modes.
	/// </summary>
	public enum TimerMode
	{
		Counter,
		Countdown
	}

	/// <summary>
	/// Implements a class representing a timer.
	/// </summary>
	public class Timer
	{
		/// <summary>
		/// Indicates the mode the timer is running on.
		/// </summary>
		public TimerMode Mode { get; protected set; }

		/// <summary>
		/// Indicates whether this <see cref="Timer"/> is running.
		/// </summary>
		public bool Running { get; protected set; }

		/// <summary>
		/// Indicates whether this <see cref="Timer"/> has finished.
		/// </summary>
		public bool Finished { get; protected set; }

		/// <summary>
		/// The timers current duration.
		/// </summary>
		public float Current { get; set; }

		/// <summary>
		/// The timers maximum duration.
		/// </summary>
		public float Maximum { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Timer" /> class.
		/// </summary>
		/// 
		/// <param name="mode">The mode the timer is running on.</param>
		public Timer(TimerMode mode) : this(mode, 0.0f)
		{
			// Nothing to do here.
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Timer" /> class.
		/// </summary>
		/// 
		/// <param name="mode">The mode the timer is running on.</param>
		/// <param name="maximum">The timers maximum duration.</param>
		public Timer(TimerMode mode, float maximum)
		{
			// Initialize the mode
			this.Mode = mode;
			// Initialize the maximum
			this.Maximum = maximum;

			// Initialize the running flag (not running by default)
			this.Running = false;
			// Initialize the finished flag (finished by default)
			this.Finished = true;
		}

		/// <summary>
		/// Starts the clock.
		/// </summary>
		public virtual void Start()
		{
			// Countdown timers start with no duration
			if (this.Mode == TimerMode.Counter)
				Start(0.0f);
			// Countdown timers start with  the maxinum duration
			else if (this.Mode == TimerMode.Countdown)
				Start(this.Maximum);
		}

		/// <summary>
		/// Starts the clock.
		/// </summary>
		/// 
		/// <param name="currentDuration">The current duration to use instead of the default value.</param>
		public virtual void Start(float currentDuration)
		{
			// Reset the timer
			this.Current = currentDuration;

			// Reset the timer running flag
			this.Running = true;
			// Reset the timer finished flag
			this.Finished = false;
		}

		/// <summary>
		/// Stops the clock.
		/// </summary>
		public virtual void Stop()
		{
			// Reset the timer running flag
			this.Running = false;
			// Reset the timer finished flag
			this.Finished = true;
		}

		/// <summary>
		/// Resumes the clock.
		/// </summary>
		public virtual void Resume()
		{
			// Reset the timer running flag
			this.Running = true;
		}

		/// <summary>
		/// Pauses the clock.
		/// </summary>
		public virtual void Pause()
		{
			// Reset the timer running flag
			this.Running = false;
		}

		/// <summary>
		/// Updates the clock.
		/// </summary>
		public virtual void Update()
		{
			// Don't update the timer when its not running
			if (this.Running == false)
				return;

			// In the counter mode the duration is incremented
			if (this.Mode == TimerMode.Counter)
			{
				// Check if the timer has finished
				if (this.Current == this.Maximum)
					Stop();

				// Check if the timer should be stopped.
				if (this.Current > this.Maximum)
					this.Current = this.Maximum;
				// Check if the timer should be incremented.
				if (this.Current != this.Maximum)
					this.Current += Time.deltaTime;
			}
			// In the countdown mode the duration is decremented
			else if (this.Mode == TimerMode.Countdown)
			{
				// Check if the timer has finished
				if (this.Current == 0.0f)
					Stop();

				// Check if the timer should be stopped.
				if (this.Current < 0.0f)
					this.Current = 0.0f;
				// Check if the timer should be decremented.
				if (this.Current != 0.0f)
					this.Current -= Time.deltaTime;
			}
		}
	}
}