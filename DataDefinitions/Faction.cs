﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace EddiDataDefinitions
{
    public class Faction
    {
        /// <summary> The faction's name </summary>
        public string name { get; set; }

        /// <summary> The faction's EDDB ID </summary>
        public long? EDDBID { get; set; }

        /// <summary> The faction's EDSM ID </summary>
        public long? EDSMID { get; set; }

        /// <summary> The faction's allegiance (localized name) </summary>
        [Obsolete("Please use Superpower instead")]
        public string allegiance => (Allegiance ?? Superpower.None).localizedName;
        /// <summary> The faction's allegiance </summary>
        public Superpower Allegiance { get; set; } = Superpower.None;

        /// <summary> The faction's government (localized name) </summary>
        [Obsolete("Please use Government instead")]
        public string government => (Government ?? Government.None).localizedName;
        /// <summary> The faction's government </summary>
        public Government Government { get; set; } = Government.None;

        /// <summary> The faction's current overall state (this may differ from the state in a particular system)</summary>
        [Obsolete("Please use State instead")]
        public string state => (State ?? State.None).localizedName;
        public State State
        {
            get
            {
                if (_State != null)
                {
                    // Return _State if it has been set
                    return _State;
                }
                else if (Influences.Count > 0)
                {
                    // Return the most common state
                    return Influences.GroupBy
                        (i => i.systemState).OrderByDescending
                        (grp => grp.Count()).Select(grp => grp.Key).First();
                }
                else
                {
                    return State.None;
                }
            }
            set
            {
                _State = value;
            }
        }
        private State _State;

        /// <summary> The faction's home system EDDBID </summary>
        public long? homeSystemEddbId { get; set; }

        /// <summary> Whether the faction is a player faction </summary>
        public bool isplayer { get; set; }

        /// <summary> Where this faction exerts influence </summary>
        public List<FactionInfluence> Influences { get; set; } = new List<FactionInfluence>();

        /// <summary> The last time the information present changed </summary> 
        public long? updatedat => Dates.fromDateTimeStringToSeconds(updatedAt.ToString());
        public DateTime updatedAt { get; set; }
    }

    public class FactionInfluence
    {
        /// <summary> The system in which the faction is found </summary>
        public string systemName { get; set; }

        /// <summary> The EDDB ID of the system in which the faction is found </summary>
        public long? eddbSystemId { get; set; }

        /// <summary> The faction's current inflence in the system (as a fraction of 1) </summary>
        public decimal? influence { get; set; }

        /// <summary> The faction's current system state </summary>
        public State systemState { get; set; }
    }
}
