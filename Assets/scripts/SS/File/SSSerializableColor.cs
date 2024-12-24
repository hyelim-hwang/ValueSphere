using System;
using UnityEngine;

namespace SS.File {
    [Serializable]
    public class SSSerializableColor {
        // fields
        public float r;
        public float g;
        public float b;
        public float a;

        // constructor
        public SSSerializableColor(Color c) {
            this.r = c.r;
            this.g = c.g;
            this.b = c.b;
            this.a = c.a;
        }

        // methods
        public Color toColor() {
            return new Color(this.r, this.g, this.b, this.a);
        }
    }
}
