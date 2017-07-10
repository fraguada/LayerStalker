using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LayerStalker
{
    public class SerializeableLayer
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public int LayerIndex { get; set; }
        public string LayerColor { get; set; }

        public SerializeableLayer() { }
    }
}
