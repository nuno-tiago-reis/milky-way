using UnityEngine;

using System.Collections.Generic;

public class RaceRecord {

	public int currentStanding
	{ get; set ; }

	public int currentLap
	{ get; set ; }
	
	public int currentCheckpoint
	{ get; set ; }
	
	public RaceRecord() {

		this.currentStanding = 0;
		this.currentLap = 0;
		this.currentCheckpoint = 0;
	}
}