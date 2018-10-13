﻿using System;
using System.ComponentModel.DataAnnotations;
using ProjectFlight.Enums;

namespace ProjectFlight.Data
{
	/// <summary>
	/// A more user friendly and descriptive version of <see cref="FlightInfoResponse"/>
	/// </summary>
	public class FlightInfo
	{
		/// <summary>
		/// Identifier broadcast by the aircraft, primary key
		/// </summary>
		[Key]
		[MaxLength(6)]
		public string Id { get; set; }

		/// <summary>
		/// Aircraft registration number
		/// </summary>
		[MaxLength(10)]
		public string RegistrationNumber { get; set; }

		/// <summary>
		/// First seen
		/// </summary>
		public DateTime FirstSeen { get; set; }

		/// <summary>
		/// Time the aircraft has been tracked for
		/// </summary>
		public TimeSpan Tracked { get; set; }

		/// <summary>
		/// Latitude over the ground
		/// </summary>
		public float Latitude { get; set; }

		/// <summary>
		/// Longitude over the ground
		/// </summary>
		public float Longitude { get; set; }

		/// <summary>
		/// Time position was last reported
		/// </summary>
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Speed in knots
		/// </summary>
		public float Speed { get; set; }

		/// <summary>
		/// Speed in kilometers per hour
		/// </summary>
		public float SpeedKm => Speed * 1.852001f;

		/// <summary>
		/// Speed type
		/// </summary>
		public ESpeedType SpeedType { get; set; }

		/// <summary>
		/// Angle clockwise in degrees
		/// </summary>
		public float Angle { get; set; }

		/// <summary>
		/// Aircraft's model
		/// </summary>
		[MaxLength(4)]
		public string Model { get; set; }

		/// <summary>
		/// Description of aircraft's model
		/// </summary>
		[MaxLength(64)]
		public string ModelDescription { get; set; }

		/// <summary>
		/// Manufacturer's name
		/// </summary>
		[MaxLength(32)]
		public string Manufacturer { get; set; }

		/// <summary>
		/// Year manufactured
		/// </summary>
		public short Year { get; set; }

		/// <summary>
		/// Name of aircraft's operator
		/// </summary>
		[MaxLength(192)]
		public string Operator { get; set; }

		/// <summary>
		/// Vertical speed in feet per minute
		/// </summary>
		public int VerticalSpeed { get; set; }

		/// <summary>
		/// Vertical speed in meters per second
		/// </summary>
		public int VerticalSpeedM => (int) (VerticalSpeed * 0.3048f);

		/// <summary>
		/// Aircraft type
		/// </summary>
		public EAircraftType Type { get; set; }

		/// <summary>
		/// Code and name of departure airport
		/// </summary>
		[MaxLength(96)]
		public string Departure { get; set; }

		/// <summary>
		/// Code and name of destination airport
		/// </summary>
		[MaxLength(96)]
		public string Destination { get; set; }

		/// <summary>
		/// If the aircraft is grounded
		/// </summary>
		public bool Grounded { get; set; }

		/// <summary>
		/// Call sign of the aircraft
		/// </summary>
		[MaxLength(8)]
		public string CallSign { get; set; }

		/// <summary>
		/// If the aircraft has a pic
		/// </summary>
		public bool HasPicture { get; set; }

		/// <summary>
		/// Number of flights recorded
		/// </summary>
		public int FlightsCount { get; set; }

		/// <summary>
		/// Default constructor used by database creation
		/// </summary>
		public FlightInfo()
		{
		}

		/// <summary>
		/// Constructs from a <see cref="FlightInfoResponse"/>
		/// </summary>
		/// <param name="info"></param>
		public FlightInfo(FlightInfoResponse info)
		{
			Id                 = info.Icao;
			RegistrationNumber = info.Reg;
			// TODO: This will probably fail
			if (DateTime.TryParse(info.Fseen, out var firstSeen))
				FirstSeen = firstSeen;
			Tracked            = TimeSpan.FromSeconds(info.Tsecs);
			Latitude           = info.Lat;
			Longitude          = info.Long;
			// TODO: This might be incorrect
			LastUpdate         = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
				.AddMilliseconds(info.PosTime);
			Speed              = info.Spd;
			SpeedType          = (ESpeedType) info.SpdTyp;
			Angle              = info.Trak;
			Model              = info.Type;
			ModelDescription   = info.Mdl;
			Manufacturer       = info.Man;
			if (short.TryParse(info.Year, out var year))
				Year = year;
			Operator           = info.Op;
			VerticalSpeed      = info.Vsi;
			Type               = (EAircraftType) info.Species;
			Departure          = info.From;
			Destination        = info.To;
			Grounded           = info.Gnd;
			CallSign           = info.Call;
			HasPicture         = info.HasPic;
			FlightsCount       = info.FlightsCount;
		}
	}
}