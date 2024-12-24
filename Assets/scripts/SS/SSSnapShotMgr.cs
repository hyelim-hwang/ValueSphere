using System.Collections.Generic;
using System.Linq;
using SS.AppObject;
using SS.File;

namespace SS {
    public class SSSnapshotMgr {
        // fields
        private SSApp mSS;
        private SSSnapshot mCurSnapshot;
        public SSSnapshot getCurSnapshot() {
            return this.mCurSnapshot;
        }

        // constructor
        public SSSnapshotMgr(SSApp ss) {
            this.mSS = ss;
            this.mCurSnapshot = new SSSnapshot(
                new List<SSSerializableValueStroke>());
        }

        //methods
        private bool areSnapshotsSame(SSSnapshot s1, SSSnapshot s2) {
            foreach (SSSerializableValueStroke sVs in
                s1.getSerializableValueStrokes()) {

                if (!s2.containsValueStroke(sVs)) {
                    return false;
                }
            }
            foreach (SSSerializableValueStroke sVs in
                s2.getSerializableValueStrokes()) {

                if (!s1.containsValueStroke(sVs)) {
                    return false;
                }
            }
            return true;
        }

        public bool takeSnapshot() {
            // make a new snapshot
            List<SSSerializableValueStroke> sStandingCards =
                new List<SSSerializableValueStroke>();
            foreach (SSValueStroke sc in this.mSS.getValueStrokeMgr().
                getValueStrokes()) {

                sStandingCards.Add(new SSSerializableValueStroke(sc));
            }
            SSSnapshot nextSnapshot = new SSSnapshot(sStandingCards);

            // register this snapshot only if it is any different from
            // the previous snapshot
            if (!this.areSnapshotsSame(this.mCurSnapshot, nextSnapshot)) {
                this.mCurSnapshot.setNextSnapshot(nextSnapshot);
                nextSnapshot.setPrevSnapshot(this.mCurSnapshot);
                this.mCurSnapshot = nextSnapshot;
                return true;
            } else {
                return false;
            }
        }

        private void applyDiff(SSSnapshot fromSnapshot, SSSnapshot toSnapshot) {
            // if "from snapshot" has cards that are not in "to snapshot",
            // remove them
            foreach (SSSerializableValueStroke sVs in fromSnapshot.
                getSerializableValueStrokes()) {

                if (!toSnapshot.containsValueStroke(sVs)) {
                    SSValueStroke sc = this.mSS.getValueStrokeMgr().findById(
                        sVs.id);
                    this.mSS.getValueStrokeMgr().getValueStrokes().Remove(sc);
                    sc.destroyGameObject();
                }
            }
            // if "to snapshot" has cards that are not in "from snapshot",
            // add them
            foreach (SSSerializableValueStroke sVs in toSnapshot.
                getSerializableValueStrokes()) {

                if (!fromSnapshot.containsValueStroke(sVs)) {
                    this.mSS.getValueStrokeMgr().getValueStrokes().Add(sVs.
                        toValueStroke());
                }
            }
        }

        public bool undo() {
            // if next snapshot exists
            // go from current snapshot to next snapshot
            if (this.mCurSnapshot.getPrevSnapshot() == null) {
                return false;
            }

            this.applyDiff(this.mCurSnapshot, this.mCurSnapshot.
                getPrevSnapshot());
            this.mCurSnapshot = this.mCurSnapshot.getPrevSnapshot();
            return true;
        }

        public bool redo() {
            // if next snapshot exists
            // go from current snapshot to next snapshot
            if (this.mCurSnapshot.getNextSnapshot() == null) {
                return false;
            }

            this.applyDiff(this.mCurSnapshot, this.mCurSnapshot.
                getNextSnapshot());
            this.mCurSnapshot = this.mCurSnapshot.getNextSnapshot();
            return true;
        }
    }
}
