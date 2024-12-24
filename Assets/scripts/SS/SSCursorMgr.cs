using System.Collections.Generic;
using UnityEngine;

namespace SS {
    public class SSCursorMgr {
        // constants
        public static readonly float PEN_RADIUS = 8f; // in px
        public static readonly float TOUCH_RADIUS = 20f; // in px

        // fields
        private SSCursor2D mPenCursor = null;
        public SSCursor2D getPenCursor() {
            return this.mPenCursor;
        }
        private List<SSCursor2D> mTouchCursors = null;
        public List<SSCursor2D> getTouchCursors() {
            return this.mTouchCursors;
        }

        // constructor
        public SSCursorMgr(SSApp ss) {
            // pen cursors
            this.mPenCursor = new SSCursor2D(ss, SSUtil.createId(),
                "PenCursor", SSCursorMgr.PEN_RADIUS, Vector2.zero);
            this.mPenCursor.getGameObject().SetActive(false);

            // touch cursors
            this.mTouchCursors = new List<SSCursor2D>();
        }

        // public methods
        public SSCursor2D findTouchCursor(SSTouchMark tm) {
            if (tm != null) {
                foreach (SSCursor2D tc in this.mTouchCursors) {
                    if (tm.getId() == tc.getId()) {
                        return tc;
                    }
                }
            }
            return null;
        }

        public SSCursor2D findTouchCursor(SSTouchPacket tp) {
            foreach (SSCursor2D tc in this.mTouchCursors) {
                if (tp.getId() == tc.getId()) {
                    return tc;
                }
            }
            return null;
        }
    }
}
