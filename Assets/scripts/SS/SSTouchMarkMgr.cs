using System.Collections.Generic;
using UnityEngine;

namespace SS {
    public class SSTouchMarkMgr {
        // constants
        private static readonly int MAX_NUM_UP_TOUCH_MARKS = 100;

        //fields
        private List<SSTouchMark> mDownTouchMarks = null;
        public List<SSTouchMark> getDownTouchMarks() {
            return this.mDownTouchMarks;
        }
        private List<SSTouchMark> mDraggedTouchMarks = null;
        public List<SSTouchMark> getDraggedTouchMarks() {
            return this.mDraggedTouchMarks;
        }
        private List<SSTouchMark> mUpTouchMarks = null;
        public List<SSTouchMark> getUpTouchMarks() {
            return this.mUpTouchMarks;
        }
        private bool mWasTouchDownJustNow = false;
        public bool wasTouchDownJustNow() {
            return this.mWasTouchDownJustNow;
        }
        public void setTouchDownWasJustNow(bool b) {
            this.mWasTouchDownJustNow = b;
        }
        private bool mWasTouchDragJustNow = false;
        public bool wasTouchDragJustNow() {
            return this.mWasTouchDragJustNow;
        }
        public void setTouchDragWasJustNow(bool b) {
            this.mWasTouchDragJustNow = b;
        }
        private bool mWasTouchUpJustNow = false;
        public bool wasTouchUpJustNow() {
            return this.mWasTouchUpJustNow;
        }
        public void setTouchUpWasJustNow(bool b) {
            this.mWasTouchUpJustNow = b;
        }

        //constructor
        public SSTouchMarkMgr() {
            this.mDownTouchMarks = new List<SSTouchMark>();
            this.mDraggedTouchMarks = new List<SSTouchMark>();
            this.mUpTouchMarks = new List<SSTouchMark>();
        }

        public void addUpTouchMark(SSTouchMark touchMark) {
            this.mUpTouchMarks.Add(touchMark);
            if (this.mUpTouchMarks.Count > SSTouchMarkMgr.
                MAX_NUM_UP_TOUCH_MARKS) {
                this.mUpTouchMarks.RemoveAt(0);
                Debug.Assert(this.mUpTouchMarks.Count <= SSTouchMarkMgr.
                    MAX_NUM_UP_TOUCH_MARKS);
            }
        }

        public SSTouchMark getFirstDownTouchMark() {
            return this.getEarliestDownTouchMark(0);
        }

        public SSTouchMark getSecondDownTouchMark() {
            return this.getEarliestDownTouchMark(1);
        }

        public SSTouchMark getLastDownTouchMark() {
            return this.getLatestDownTouchMark(0);
        }

        public SSTouchMark getEarliestDownTouchMark(int i) {
            int size = this.mDownTouchMarks.Count;
            int index = i;
            if (index < 0 || index >= size) {
                return null;
            } else {
                return this.mDownTouchMarks[index];
            }
        }

        public SSTouchMark getLatestDownTouchMark(int i) {
            int size = this.mDownTouchMarks.Count;
            int index = size - 1 - i;
            if (index < 0 || index >= size) {
                return null;
            } else {
                return this.mDownTouchMarks[index];
            }
        }

        public SSTouchMark getLastUpTouchMark() {
            return this.getLatestUpTouchMark(0);
        }

        public SSTouchMark getLatestUpTouchMark(int i) {
            int size = this.mUpTouchMarks.Count;
            int index = size - 1 - i;
            if (index < 0 || index >= size) {
                return null;
            } else {
                return this.mUpTouchMarks[index];
            }
        }

        public bool touchDown(SSTouchPacket tp) {
            foreach (SSTouchMark tm in this.mDownTouchMarks) {
                if (tp.getId() == tm.getId()) {
                    return false;
                }
            }
            SSTouchMark touchMark = new SSTouchMark(tp);
            this.mDownTouchMarks.Add(touchMark);
            return true;
        }

        public bool touchDrag(List<SSTouchPacket> tps) {
            this.mDraggedTouchMarks.Clear();
            foreach (SSTouchPacket tp in tps) {
                foreach (SSTouchMark tm in this.mDownTouchMarks) {
                    if (tp.getId() == tm.getId() && tm.addPt(tp.getPt())) {
                        this.mDraggedTouchMarks.Add(tm);
                    }
                }
            }

            bool isAnyTouchDragged = this.mDraggedTouchMarks.Count > 0;
            return isAnyTouchDragged;
        }

        public bool touchUp(SSTouchPacket tp) {
            SSTouchMark upTouchMark = null;
            foreach (SSTouchMark tm in this.mDownTouchMarks) {
                if (tp.getId() == tm.getId()) {
                    upTouchMark = tm;
                }
            }

            if (upTouchMark != null) {
                this.mDownTouchMarks.Remove(upTouchMark);
                this.addUpTouchMark(upTouchMark);
                return true;
            } else {
                return false;
            }
        }
    }
}
