using System.Collections.Generic;
using UnityEngine;

namespace SS {
    public class SSPenMarkMgr {
        //constants
        public static readonly int MAX_NUM_PEN_MARKS = 10;

        //fields
        private List<SSPenMark> mPenMarks = null;
        public List<SSPenMark> getPenMarks() {
            return (this.mPenMarks);
        }

        //constructors
        public SSPenMarkMgr() {
            this.mPenMarks = new List<SSPenMark>();
        }

        //methods
        public void addPenMark(SSPenMark penMark) {
            this.mPenMarks.Add(penMark);
            if (this.mPenMarks.Count > SSPenMarkMgr.MAX_NUM_PEN_MARKS) {
                this.mPenMarks.RemoveAt(0);
            }
            Debug.Assert(
                this.mPenMarks.Count <= SSPenMarkMgr.MAX_NUM_PEN_MARKS);
        }

        public SSPenMark getLastPenMark() {
            int size = this.mPenMarks.Count;
            if(size == 0) {
                return null;
            } else {
                return (this.mPenMarks[size - 1]);
            }
        }

        public SSPenMark getRecentPenMark(int i) {
            int size = this.mPenMarks.Count;
            int index = size - 1 - i;
            if (index < 0 || index >= size) {
                return null;
            } else {
                return (this.mPenMarks[index]);
            }
        }

        public bool handlePenDown(Vector2 pt) {
            SSPenMark penMark = new SSPenMark(pt);
            this.addPenMark(penMark);
            return true;
        }

        public bool handlePenDrag(Vector2 pt) {
            SSPenMark penMark = this.getLastPenMark();
            if (penMark != null && penMark.addPt(pt)) {
                return true;
            } else {
                return false;
            }
        }

        public bool handlePenUp(Vector2 pt) {
            return true;
        }

        public bool handleEraserUp(Vector2 pt) {
            return true;
        }

        public bool handleEraserDrag(Vector2 pt) {
            return true;
        }

        public bool handleEraserDown(Vector2 pt) {
            return true;
        }
    }
}
