using UnityEngine;

using System.Collections.Generic;

public class RaceRecord {

	public int id
	{ get; set; }

	public int currentStanding
	{ get; set ; }

	public int currentLap
	{ get; set ; }
	public int currentCheckpoint
	{ get; set ; }

	public float bestLapTime
	{ get; set ; }
	public float currentLapTime
	{ get; set ; }
	public float totalLapTime
	{ get; set ; }
	
	public RaceRecord(int id) {

		this.id = id;
		
		this.currentStanding = 0;
		
		this.currentLap = 0;
		this.currentCheckpoint = 0;
		
		this.bestLapTime = 0.0f;
		this.currentLapTime = 0.0f;
		this.totalLapTime = 0.0f;
	}
}