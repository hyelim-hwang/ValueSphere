using SS.AppObject;
using UnityEngine;
using System;

namespace SS.File {
    [Serializable]
    public class SSSerializableValueSphere {
        // fields
        public float radius = float.NaN;
        public SSSerializableColor color = null;
        public SSSerializableEquator equator = null;
        public SSSerializableAppPolyline3D pole = null;
        public SSSerializableVector3 pos = null;
        public SSSerializableQuaternion rot = null;

        // constructor
        public SSSerializableValueSphere(SSValueSphere vs) {
            this.color = new SSSerializableColor(vs.getColor());
            this.radius = vs.getRadius();
            this.equator = new SSSerializableEquator(vs.getEquator());
            this.pole = new SSSerializableAppPolyline3D(vs.getPole());
            this.pos = new SSSerializableVector3(vs.getSphere().transform.
                position);
            this.rot = new SSSerializableQuaternion(vs.getRot());
        }

        // methods
        public SSValueSphere toValueSphere() {
            Vector3 pos = this.pos.toVector3();
            Quaternion rot = this.rot.toQuaternion();
            float radius = this.radius;
            Color color = this.color.toColor();
            return new SSValueSphere("ValueSphere", radius, color);
        }
    }
}
