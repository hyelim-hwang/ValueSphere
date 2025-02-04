using System.Collections.Generic;
using UnityEngine;
using SS.AppObject;
using SSAppObject;
using Unity.VisualScripting;

namespace SS {
    public class SSShadowTraceMgr {
        //constants
        public static readonly Color TRACE_DEFAULT_COLOR = Color.blue;
        public static readonly float TRACE_DEFAULT_WIDTH = 0.02f;

        //fields
        private SSApp mSS = null;
        private SSShadowTrace mCurShadowTrace= null;
        public SSShadowTrace getCurShadowTrace() {
            return this.mCurShadowTrace;
        }
        public void setCurShadowTrace(SSShadowTrace trace) {
            this.mCurShadowTrace = trace;
        }

        private List<SSShadowTrace> mShadowTraces = null;
        public List<SSShadowTrace> getShadowTraces() {
            return this.mShadowTraces;
        }
        private List<SSAppTrapezoid3D> mShadowFaces = null;
        public List<SSAppTrapezoid3D> getShadowFaces() {
            return this.mShadowFaces;
        }
        private Color mCurColor = SSShadowTraceMgr.TRACE_DEFAULT_COLOR;
        public Color getCurColor() {
            return this.mCurColor;
        }
        public void setCurColor(Color color) {
            this.mCurColor = color;
        }
        private float mStrokeWidth = TRACE_DEFAULT_WIDTH;
        public float getStrokeWidth() {
            return this.mStrokeWidth;
        }
        public void setStrokeWidth(float width) {
            this.mStrokeWidth = width;
        }

        //constructor
        public SSShadowTraceMgr(SSApp ss) {
            this.mSS = ss;
            this.mShadowTraces = new List<SSShadowTrace>();
            this.mShadowFaces = new List<SSAppTrapezoid3D>();
        }

        //methods
        public void updateShadowTracesByChangedLightDirection() {
            foreach (SSShadowTrace trace in this.mShadowTraces) {
                trace.refreshShadowTraceByChangedPoints();
            }
        }

        //calculate traceTop & traceBottom's world coordinate with the position
        //of the BaseOfStick.
        public Vector3 transformLocalToWorldByBOS(Vector3 local) {
            Vector3 baseOfStick =
                this.mSS.getShadowStickMgr().getShadowStick().getBaseOfStick();
            return local + baseOfStick;
        }

        public void createShadowFacesByShadowTraces() {
            this.mShadowFaces.Clear();
            SSShadowStickMgr SSMgr = this.mSS.getShadowStickMgr();
            List<SSShadowTrace> traces = this.getShadowTraces();
            int numberOfFaces = this.mShadowTraces.Count - 1;
            for (int i = 0; i < numberOfFaces; i++) {
                Vector3 trace1Top = traces[i].getTraceTop();
                Vector3 trace2Top = traces[i + 1].getTraceTop();
                Vector3 trace2Bottom = traces[i + 1].getTraceBottom();
                Vector3 trace1Bottom = traces[i].getTraceBottom();
                SSAppTrapezoid3D face = new SSAppTrapezoid3D("shadowFace",
                    trace1Top, trace2Top, trace1Bottom, trace2Bottom,
                    Color.blue);
                face.getGameObject().layer = 3;
                this.mShadowFaces.Add(face);
            }
        }
    }
}