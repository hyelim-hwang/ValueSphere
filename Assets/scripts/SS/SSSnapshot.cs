using System.Collections.Generic;
using SS.File;

namespace SS {
    public class SSSnapshot {
        // fields
        private SSSnapshot mPrevSnapshot = null;
        public SSSnapshot getPrevSnapshot() {
            return this.mPrevSnapshot;
        }
        public void setPrevSnapshot(SSSnapshot prevSnapshot) {
            this.mPrevSnapshot = prevSnapshot;
        }
        private SSSnapshot mNextSnapshot = null;
        public SSSnapshot getNextSnapshot() {
            return this.mNextSnapshot;
        }
        public void setNextSnapshot(SSSnapshot nextSnapshot) {
            this.mNextSnapshot = nextSnapshot;
        }
        private List<SSSerializableValueStroke> mSerializableValueStrokes;
        public List<SSSerializableValueStroke> getSerializableValueStrokes() {
            return this.mSerializableValueStrokes;
        }

        // constructor
        public SSSnapshot(List<SSSerializableValueStroke> valueStrokes) {
            this.mSerializableValueStrokes = valueStrokes;
        }

        //methods
        private bool areValueStrokesSame(SSSerializableValueStroke sVs1,
            SSSerializableValueStroke sVs2) {
            return sVs1.id == sVs2.id && sVs1.width == sVs2.width &&
                sVs1.color == sVs2.color && sVs1.pts ==
                sVs2.pts && sVs1.valueCoordinate.toVector2() ==
                sVs2.valueCoordinate.toVector2();
        }

        public bool containsValueStroke(
            SSSerializableValueStroke valueStroke) {

            foreach (SSSerializableValueStroke sVs in
                this.mSerializableValueStrokes) {
                if (this.areValueStrokesSame(valueStroke, sVs)) {
                    return true;
                }
            }
            return false;
        }
    }
}
