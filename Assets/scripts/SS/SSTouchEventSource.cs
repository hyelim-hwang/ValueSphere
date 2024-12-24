using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

namespace SS {
    public class SSTouchEventSource {
        // fields
        private SSEventListener mEventListener = null;
        public void setEventListener(SSEventListener eventListener) {
            this.mEventListener = eventListener;
        }
        private List<SSTouchPacket> mPrevTouches = null;
        private List<SSTouchPacket> mCurTouches = null;

        // constructor
        public SSTouchEventSource() {
            this.mPrevTouches = new List<SSTouchPacket>();
            this.mCurTouches = new List<SSTouchPacket>();
            TouchSimulation.Disable();
        }

        // WARNING!!!
        // don't use TouchControl's phase information (e.g. Began, Moved, Ended)
        // because they are VERY unreliable and sometimes do not fire properly
        public void update() {
            // detect pen input
            Vector2 penPt = SSUtil.VECTOR2_NAN;
            if (Pen.current.tip.isPressed || Pen.current.eraser.isPressed) {
                penPt = Pen.current.position.ReadValue();
            }

            // detect touch changes
            this.mCurTouches.Clear();
            foreach (TouchControl tc in Touchscreen.current.touches) {
                if (tc.press.isPressed) {
                    string id = tc.touchId.ReadValue().ToString();
                    Vector2 pt = tc.position.ReadValue();
                    // Unity also registers pen input as touch input
                    // also, TouchSimulation.Disable() doesn't work
                    // so, manually verify that touch is not pen
                    if (pt != penPt) {
                        SSTouchPacket touch = new SSTouchPacket(id, pt);
                        this.mCurTouches.Add(touch);
                    }
                }
            }

            // touch down
            foreach (SSTouchPacket curTouch in this.mCurTouches) {
                bool prevTouchExists = false;
                foreach (SSTouchPacket prevTouch in this.mPrevTouches) {
                    if (prevTouch.getId() == curTouch.getId()) {
                        prevTouchExists = true;
                    }
                }
                if (!prevTouchExists) {
                    this.mEventListener.touchDown(curTouch);
                }
            }

            // touch dragged
            List<SSTouchPacket> draggedTouches = new List<SSTouchPacket>();
            foreach (SSTouchPacket curTouch in this.mCurTouches) {
                foreach (SSTouchPacket prevTouch in this.mPrevTouches) {
                    if (prevTouch.getId() == curTouch.getId() &&
                        prevTouch.getPt() != curTouch.getPt()) {
                        draggedTouches.Add(curTouch);
                    }
                }
            }
            if (draggedTouches.Count > 0) {
                this.mEventListener.touchDragged(draggedTouches);
            }

            // touch up
            foreach (SSTouchPacket prevTouch in this.mPrevTouches) {
                bool curTouchExists = false;
                foreach (SSTouchPacket curTouch in this.mCurTouches) {
                    if (prevTouch.getId() == curTouch.getId()) {
                        curTouchExists = true;
                    }
                }

                if (!curTouchExists) {
                    this.mEventListener.touchUp(prevTouch);
                }
            }

            // update the previous data
            this.mPrevTouches.Clear();
            foreach (SSTouchPacket touch in this.mCurTouches) {
                this.mPrevTouches.Add(touch);
            }
        }
    }
}
