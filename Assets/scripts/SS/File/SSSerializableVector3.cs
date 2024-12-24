using System;
using UnityEngine;

namespace SS.File {
    [Serializable]
    public class SSSerializableVector3 {
        // fields
        public float x;
        public float y;
        public float z;

        // constructor
        public SSSerializableVector3(Vector3 v) {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        // methods
        public Vector3 toVector3() {
            return new Vector3(this.x, this.y, this.z);
        }
    }
}