﻿using System;
using Utilities;

namespace EddiEvents
{
    [PublicAPI]
    public class ClearedSaveEvent : Event
    {
        public const string NAME = "Cleared save";
        public const string DESCRIPTION = "Triggered when you clear your save";
        public const string SAMPLE = "{\"timestamp\":\"2016-06-10T14:32:03Z\",\"event\":\"ClearSavedGame\",\"FID\":\"F0000000\",\"Name\":\"HRC1\"}";

        [PublicAPI("The name of the player whose save has been cleared")]
        public string name { get; private set; }

        // Not intended to be user facing
        
        public string frontierID { get; private set; }

        public ClearedSaveEvent(DateTime timestamp, string name, string frontierID) : base(timestamp, NAME)
        {
            this.name = name;
            this.frontierID = frontierID;
        }
    }
}
