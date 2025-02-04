using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Geom;
using SS.Scenario;
using SSAppObject;
using UnityEngine.UIElements;

namespace SS.AppObject {
    public class SSShadowTrace : SSAppNoGeom3D {
        //constants

        //fields
        private Vector3 mTraceTop = Vector3.zero;
        public Vector3 getTraceTop() {
            return this.mTraceTop;
        }
        public void setTraceTop(Vector3 top) {
            this.mTraceTop = top;
            this.refreshShadowTraceByChangedPoints();
        }
        private Vector3 mTraceBottom = Vector3.zero;
        public Vector3 getTraceBottom() {
            return this.mTraceBottom;
        }
        public void setTraceBottom(Vector3 bot) {
            this.mTraceTop = bot;
            this.refreshShadowTraceByChangedPoints();
        }
        private SSAppPolyline3D mTraceLine = null;
        public Vector3 getTraceLine() {
            return this.mTraceBottom;
        }
        public void setTraceLine(SSAppPolyline3D line) {
            this.mTraceLine = line;
        }

        //constructor
        public SSShadowTrace(string name, Vector3 traceTop,
            Vector3 traceBottom) : base($"{name}/shadowTrace") {
            List<Vector3> pts = new List<Vector3>();
            pts.Add(traceTop);
            pts.Add(traceBottom);
            this.mTraceTop = traceTop;
            this.mTraceBottom = traceBottom;
            this.mTraceLine = new SSAppPolyline3D("traceLine", pts,
                SSShadowTraceMgr.TRACE_DEFAULT_WIDTH,
                SSShadowTraceMgr.TRACE_DEFAULT_COLOR);
            this.mTraceLine.getGameObject().layer = 3;

        }

        //methods
        public void refreshShadowTraceByChangedPoints() {
                List<Vector3> pts = new List<Vector3>();
                pts.Add(this.mTraceTop);
                pts.Add(this.mTraceBottom);
                this.mTraceLine.setPts(pts);
        }
    }
}