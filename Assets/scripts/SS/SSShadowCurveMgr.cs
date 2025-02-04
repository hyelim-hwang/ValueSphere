using System.Collections.Generic;
using UnityEngine;
using SS.AppObject;

namespace SS {
    public class SSShadowCurveMgr {
        //constants
        public static readonly float PT_CURVE_WIDTH = 20f;
        public static readonly Color PT_CURVE_COLOR =
            new Color(0.8f, 0.8f, 0.8f, 0.8f);

        //fields
        private SSApp mSS = null;
        private SSAppPolyline2D mCurShadowCurve = null;
        public SSAppPolyline2D getCurShadowCurve() {
            return this.mCurShadowCurve;
        }
        public void setCurShadowCurve(SSAppPolyline2D ptCurve2D) {
            this.mCurShadowCurve = ptCurve2D;
        }

        private List<SSAppPolyline2D> mShadowCurves = null;
        public List<SSAppPolyline2D> getShadowCurves() {
            return this.mShadowCurves;
        }
        private Color mCurColor = SSShadowCurveMgr.PT_CURVE_COLOR;
        public Color getCurColor() {
            return this.mCurColor;
        }
        public void setCurColor(Color color) {
            this.mCurColor = color;
        }
        private float mStrokeWidth = PT_CURVE_WIDTH;
        public float getStrokeWidth() {
            return this.mStrokeWidth;
        }
        public void setStrokeWidth(float width) {
            this.mStrokeWidth = width;
        }

        //constructor
        public SSShadowCurveMgr(SSApp ss) {
            this.mSS = ss;
            this.mShadowCurves = new List<SSAppPolyline2D>();
        }

        //methods
    }
}