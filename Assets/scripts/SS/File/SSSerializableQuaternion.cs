using System;
using UnityEngine;

namespace SS.File {
    [Serializable]
    public class SSSerializableQuaternion {
        // fields
        public float x;
        public float y;
        public float z;
        public float w;

        // constructor
        public SSSerializableQuaternion(Quaternion q) {
            this.x = q.x;
            this.y = q.y;
            this.z = q.z;
            this.w = q.w;
        }

        // methods
        public Quaternion toQuaternion() {
            return new Quaternion(this.x, this.y, this.z, this.w);
        }
    }
}
