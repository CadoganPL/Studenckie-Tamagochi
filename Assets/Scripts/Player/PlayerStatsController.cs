using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
	#region Stats - Properties. They cannot go under 0.0 by design
	private float _knowledge;
	public float Knowledge
	{
		get
		{
			return _knowledge;
		}

		set
		{
			this._knowledge = (value < 0) ? (0.0F) : (value);
		}
	}

	private float _mass;
	public float Mass
	{
		get
		{
			return _mass;
		}

		set
		{
			this._mass = (value < 0) ? (0.0F) : (value);
		}
	}

	private float _social;
	public float Social
	{
		get
		{
			return _social;
		}

		set
		{
			this._social = (value < 0) ? (0.0F) : (value);
		}
	}

	private float _energy;
	public float Energy
	{
		get
		{
			return _energy;
		}

		set
		{
			this._energy = (value < 0) ? (0.0F) : (value);
		}
	}
	#endregion

	#region Stats Multipliers - Properties. They are used to calculate gain/loss of certain statistic per second
	public float KnowledgeMultiplier { get; set; }
	public float MassMultiplier { get; set; }
	public float SocialMultiplier { get; set; }
	public float EnergyMultiplier { get; set; }
	#endregion

	#region Stats - Starting Values
	public static readonly float StartingKnowledge = 40.0F;
	public static readonly float StartingMass = 50.0F;
	public static readonly float StartingSocial = 30.0F;
	public static readonly float StartingEnergy = 60.0F;
	#endregion

	#region Stats Multipliers - Starting Values
	public static readonly float StartingKnowledgeMultiplier = -0.05F;
	public static readonly float StartingMassMultiplier = -0.003F;
	public static readonly float StartingSocialMultiplier = -0.018F;
	public static readonly float StartingEnergyMultiplier = -0.023F;
	#endregion

	#region Other Properties
	/// <summary>
	/// This property sets automatic stats update per frame using MonoBehaviour.Update()
	/// </summary>
	public bool RealTimeUpdateEnabled { get; set; }
	#endregion

	#region Public Methods
	/// <summary>
	/// Update stats manually
	/// </summary>
	/// <param name="time">Time passed in seconds</param>
	public void UpdateStats(float time)
	{
		Knowledge	+= time * KnowledgeMultiplier;
		Mass		+= time * MassMultiplier;
		Social		+= time * SocialMultiplier;
		Energy		+= time * EnergyMultiplier;
	}

	/// <summary>
	/// Restore stats to their starting positions as defined in PlayerStatsController.cs
	/// </summary>
	public void RestoreStartingValues()
	{
		Knowledge = StartingKnowledge;
		Mass = StartingMass;
		Social = StartingSocial;
		Energy = StartingEnergy;

		KnowledgeMultiplier = StartingKnowledgeMultiplier;
		MassMultiplier = StartingMassMultiplier;
		SocialMultiplier = StartingSocialMultiplier;
		EnergyMultiplier = StartingEnergyMultiplier;
	}
	#endregion

	void Start()
	{
		RestoreStartingValues();
		RealTimeUpdateEnabled = true;
	}

	void Update()
	{
		if(RealTimeUpdateEnabled == true)
		{
			UpdateStats(Time.deltaTime);
		}
	}
}
