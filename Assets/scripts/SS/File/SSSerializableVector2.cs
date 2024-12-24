using System;
using UnityEngine;

namespace SS.File {
    [Serializable]
    public class SSSerializableVector2 {
        // fields
        public float x;
        public float y;

        // constructor
        public SSSerializableVector2(Vector2 v) {
            this.x = v.x;
            this.y = v.y;
        }

        // methods
        public Vector3 toVector2() {
            return new Vector2(this.x, this.y);
        }
    }
}