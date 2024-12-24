using System;
using SS.Geom;

namespace SS.File{
    [Serializable]
    public class SSSerializableCircle3D {
        //field
        public float mRadius = float.NaN;
        public SSSerializableVector3 mPos = null;
        public SSSerializableQuaternion mRot = null;

        //constructor
        public SSSerializableCircle3D (SSCircle3D circle)
        {
            this.mRadius = circle.getRadius();
            this.mPos = new SSSerializableVector3(circle.getPos());
            this.mRot = new SSSerializableQuaternion(circle.getRot());
        }

        //methods
        public SSCircle3D toCircle3D() {
            return new SSCircle3D(this.mRadius, this.mPos.toVector3(),
            this.mRot.toQuaternion());
        }
    }
}