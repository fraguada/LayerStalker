using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LayerStalker
{
    public class SerializeableLayer
    {
        [JsonProperty("id")]
        public Guid Uuid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent")]
        public Guid? ParentId { get; set; }

        public int LayerIndex { get; set; }

        public string LayerColor { get; set; }

        public SerializeableLayer() { }
    }
}
