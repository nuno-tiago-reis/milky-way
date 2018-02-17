// Unity
using UnityEngine;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// The available joystick modes.
	/// </summary>
	public enum JoystickMode
	{
		Playstation4
	};

	/// <summary>
	/// Implements a spaceships joystick controller.
	/// </summary>
	/// 
	/// <seealso cref="IObjectController" />
	public sealed class JoystickController : IObjectController
	{
		#region [Attributes]
		/// <summary>
		/// The joystick mode.
		/// </summary>
		public JoystickMode JoystickMode;

		/// <summary>
		/// The triangle keys corresponding key-code.
		/// </summary>
		public KeyCode Triangle;
		/// <summary>
		/// The circle keys corresponding key-code.
		/// </summary>
		public KeyCode Circle;
		/// <summary>
		/// The cross keys corresponding key-code.
		/// </summary>
		public KeyCode Cross;
		/// <summary>
		/// The square keys corresponding key-code.
		/// </summary>
		public KeyCode Square;

		/// <summary>
		/// The L1 keys corresponding key-code.
		/// </summary>
		public KeyCode L1;
		/// <summary>
		/// The L2 keys corresponding key-code.
		/// </summary>
		public KeyCode L2;
		/// <summary>
		/// The L3 keys corresponding key-code.
		/// </summary>
		public KeyCode L3;
		/// <summary>
		/// The R1 keys corresponding key-code.
		/// </summary>
		public KeyCode R1;
		/// <summary>
		/// The R2 keys corresponding key-code.
		/// </summary>
		public KeyCode R2;
		/// <summary>
		/// The R3 keys corresponding key-code.
		/// </summary>
		public KeyCode R3;
		
		/// <summary>
		/// The start keys corresponding key-code.
		/// </summary>
		public KeyCode Start;
		/// <summary>
		/// The select keys corresponding key-code.
		/// </summary>
		public KeyCode Select;
		/// <summary>
		/// The playstation keys corresponding key-code.
		/// </summary>
		public KeyCode Playstation;

		/// <summary>
		/// The name of the vertical axis.
		/// </summary>
		public string VerticalAxis;
		/// <summary>
		/// The name of the horizontal axis.
		/// </summary>
		public string HorizontalAxis;

		/// <summary>
		/// The joysticks spaceship.
		/// </summary>
		public SpaceshipController Spaceship;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Map the keys according to the spaceships ID
			switch(this.Spaceship.ID)
			{
				case 1: 
					if(this.JoystickMode == JoystickMode.Playstation4)
					{
						this.Triangle = KeyCode.Joystick1Button0;
						this.Circle = KeyCode.Joystick1Button1;
						this.Cross = KeyCode.Joystick1Button2;
						this.Square = KeyCode.Joystick1Button3;

						this.L1 = KeyCode.Joystick1Button4;
						this.L2 = KeyCode.Joystick1Button6;
						this.L3 = KeyCode.Joystick1Button9;
						this.R1 = KeyCode.Joystick1Button5;
						this.R2 = KeyCode.Joystick1Button7;
						this.R3 = KeyCode.Joystick1Button10;

						this.Start = KeyCode.Joystick1Button11;
						this.Select = KeyCode.Joystick1Button9;
						this.Playstation = KeyCode.Joystick1Button12;
					}
					break;

				case 2:
					if(this.JoystickMode == JoystickMode.Playstation4)
					{
						this.Triangle = KeyCode.Joystick2Button0;
						this.Circle = KeyCode.Joystick2Button1;
						this.Cross = KeyCode.Joystick2Button2;
						this.Square = KeyCode.Joystick2Button3;

						this.L1 = KeyCode.Joystick2Button4;
						this.L2 = KeyCode.Joystick2Button6;
						this.L3 = KeyCode.Joystick2Button9;
						this.R1 = KeyCode.Joystick2Button5;
						this.R2 = KeyCode.Joystick2Button7;
						this.R3 = KeyCode.Joystick2Button10;

						this.Start = KeyCode.Joystick2Button11;
						this.Select = KeyCode.Joystick2Button9;
						this.Playstation = KeyCode.Joystick2Button12;
					}
					break;
			}

			this.VerticalAxis = "Vertical Axis Joystick " + this.Spaceship.ID;
			this.HorizontalAxis = "Horizontal Axis Joystick " + this.Spaceship.ID;
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			// Nothing to do here.
		}
		#endregion
	}
}