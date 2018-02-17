// System
using System.Linq;
using System.Collections.Generic;

// MilkyWay
using MilkyWay.Objects.Projectiles;

namespace MilkyWay.Managers
{
	/// <summary>
	/// Manages the projectiles
	/// </summary>
	public sealed class ProjectileManager : SingletonManager<ProjectileManager>, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The name of the projectiles container game-object.
		/// </summary>
		public const string ProjectileContainerName = "Projectiles";
		#endregion

		#region [Attributes]
		/// <summary>
		/// The list of projectile controllers <see cref="ProjectileController"/>.
		/// </summary>
		public List<ProjectileController> ProjectileList { get; private set; }
		/// <summary>
		/// The list of added projectile controllers <see cref="ProjectileController"/>.
		/// </summary>
		public List<ProjectileController> AddedProjectileList { get; private set; }
		/// <summary>
		/// The list of removed projectile controllers <see cref="ProjectileController"/>.
		/// </summary>
		public List<ProjectileController> RemovedProjectileList { get; private set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Find the projectiles in the arena/track
			this.ProjectileList = GameManager.Instance.GetComponentsInChildren<ProjectileController>().ToList();
			this.AddedProjectileList = new List<ProjectileController>();
			this.RemovedProjectileList = new List<ProjectileController>();

			// Initialize the projectiles
			foreach(ProjectileController projectile in this.ProjectileList)
			{
				projectile.transform.parent = this.transform;
				projectile.ObjectCreate();
			}
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Clean the projectile list
			this.ProjectileList.AddRange(this.AddedProjectileList);
			this.ProjectileList.RemoveAll(projectile => projectile == null);
			this.ProjectileList.RemoveAll(projectile => this.RemovedProjectileList.Contains(projectile));

			// Clean the temporary lists
			this.AddedProjectileList.Clear();
			this.RemovedProjectileList.Clear();

			// Update the projectiles
			foreach(ProjectileController projectile in this.ProjectileList)
				projectile.ObjectUpdate();
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			// Clean the projectile list
			this.ProjectileList.RemoveAll(projectile => projectile == null);
			this.ProjectileList.RemoveAll(projectile => this.RemovedProjectileList.Contains(projectile));

			// Clean the temporary lists
			this.AddedProjectileList.Clear();
			this.RemovedProjectileList.Clear();

			// Destroy the projectiles
			foreach(ProjectileController projectile in this.ProjectileList)
				projectile.ObjectDestroy();

			Destroy(this.gameObject);
		}

		/// <summary>
		/// Adds the projectile to the list.
		/// </summary>
		/// 
		/// <param name="projectile">The projectile.</param>
		public void AddProjectile(ProjectileController projectile)
		{
			if (this.ProjectileList.Contains(projectile) == false && this.AddedProjectileList.Contains(projectile) == false)
			{
				// Update the list
				this.AddedProjectileList.Add(projectile);
				// Update the parent
				projectile.transform.parent = this.transform;
			}
		}

		/// <summary>
		/// Removes the projectile from the list.
		/// </summary>
		/// <param name="projectile">The projectile.</param>
		public void RemoveProjectile(ProjectileController projectile)
		{
			if (this.ProjectileList.Contains(projectile) && this.RemovedProjectileList.Contains(projectile) == false)
			{
				// Update the list
				this.RemovedProjectileList.Add(projectile);
				// Update the parent
				projectile.transform.parent = null;
			}
		}
		#endregion
	}
}