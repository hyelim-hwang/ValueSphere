using System;
using SSAppObject;

namespace SS.File {
    [Serializable]
    public class SSSerializableEquator{

        // fields
        private float mWidth = float.NaN;
        private SSSerializableColor mColor = null;
        private SSSerializableAppPolyline3D mDiameterLine1 = null;
        private SSSerializableAppPolyline3D mDiameterLine2 = null;
        private SSSerializableValueSphere mValueSphere = null;

        // constructor
        public SSSerializableEquator(SSEquator equator) {
            this.mWidth = equator.getWidth();
            this.mColor = new SSSerializableColor(equator.getColor());
            this.mDiameterLine1 =
            new SSSerializableAppPolyline3D(equator.getDiameterLine1());
            this.mDiameterLine2 =
            new SSSerializableAppPolyline3D(equator.getDiameterLine2());
        }

        // methods
        public SSEquator toEquator() {
            return new SSEquator("eq", this.mWidth,
            this.mColor.toColor(), this.mValueSphere.toValueSphere());
        }
    }
}