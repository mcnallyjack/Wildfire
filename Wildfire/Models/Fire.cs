﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wildfire.Models
{
    public class Fire
    {
        public string FireID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Time { get; set; }
        public string WindDirection { get; set; }
        public string Description { get; set; }
        public string PlaceName { get; set; }
        public string ResolvedDescription { get; set; }
        public string DeviceID { get; set; }

    }
}
