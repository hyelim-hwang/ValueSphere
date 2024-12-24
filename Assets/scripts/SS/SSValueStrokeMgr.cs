using System.Collections.Generic;
using UnityEngine;
using SS.AppObject;

namespace SS {
    public class SSValueStrokeMgr {
        //constants
        public static readonly float PT_CURVE_WIDTH = 30f;
        public static readonly Color PT_CURVE_COLOR = new Color(0, 0, 0, 0.5f);

        //fields
        private SSApp mSS = null;
        private SSValueStroke mCurValueStroke = null;
        public SSValueStroke getCurValueStroke() {
            return this.mCurValueStroke;
        }
        public void setCurValueStroke(SSValueStroke ptCurve2D) {
            this.mCurValueStroke = ptCurve2D;
        }

        private List<SSValueStroke> mValueStrokes = null;
        public List<SSValueStroke> getValueStrokes() {
            return this.mValueStrokes;
        }
        private Color mCurColor = SSValueStrokeMgr.PT_CURVE_COLOR;
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
        public SSValueStrokeMgr(SSApp ss) {
            this.mSS = ss;
            this.mValueStrokes = new List<SSValueStroke>();
        }

        //methods
        public SSValueStroke findById(string id) {
            foreach (SSValueStroke sc in this.mValueStrokes) {
                if (sc.getId() == id) {
                    return sc;
                }
            }
            return null;
        }
        public void updateValueCoordinate() {
            SSValueSphereMgr valueSphereMgr = this.mSS.getValueSphereMgr();
            Camera cam = ((SSApp)this.mSS).getPerspCameraPerson().getCamera();
            //warning: sphere center is currently world coordinate!
            Vector3 curSphereCenter = this.mSS.getValueSphereMgr().
                getValueSphere().getSphere().transform.position;
            foreach (SSValueStroke vs in this.mValueStrokes) {
                Vector3 prevSphereCenter = vs.getSphereCenter();
                Vector2 prevValueCoordinate = vs.getValueCoordinate();
                Vector3 prevWorldPt = cam.ScreenToWorldPoint(
                    new Vector3(prevValueCoordinate.x,
                    prevValueCoordinate.y, 2.0f));
                prevWorldPt.z = 2f;
                Vector3 coordinateDir =
                    (prevWorldPt - prevSphereCenter).normalized;
                float prevSphereRadius = vs.getSphereRadius();
                float curSphereRadius =
                    valueSphereMgr.getValueSphere().getRadius() / 2f;
                float coordinateRadiusRatio = ((prevWorldPt
                    - prevSphereCenter).magnitude) / prevSphereRadius;
                Vector3 newWorldPt = curSphereCenter +
                    coordinateDir * curSphereRadius * coordinateRadiusRatio;
                Vector2 newValueCoordinate = this.mSS.getPerspCameraPerson().
                getCamera().WorldToScreenPoint(newWorldPt);
                vs.setValueCoordinate(newValueCoordinate);
                //update cur pos and rad to each strokes.
                //need to put screen center back to world pt.
                vs.setSphereCenter(curSphereCenter);
                vs.setSphereRadius(curSphereRadius);
            }
        }

        public void updateValueCoordinateForOpeningFile() {
            Camera cam = ((SSApp)this.mSS).getPerspCameraPerson().getCamera();
            //warning: sphere center is currently world coordinate!
            Vector3 curSphereCenter = this.mSS.getValueSphereMgr().
                getValueSphere().getSphere().transform.position;
            foreach (SSValueStroke vs in this.mValueStrokes) {
                Vector3 prevSphereCenter = vs.getSphereCenter();
                Vector2 prevValueCoordinate = vs.getValueCoordinate();
                Vector3 prevVSWorldPt = cam.
                    ScreenToWorldPoint(
                    new Vector3(prevValueCoordinate.x,
                    prevValueCoordinate.y, 2.0f));
                prevVSWorldPt.z = 2f;
                Vector3 coordinateDir =
                    (prevVSWorldPt - prevSphereCenter).normalized;
                float prevSphereRadius = vs.getSphereRadius();
                float curSphereRadius =
                    this.mSS.getValueSphereMgr().getValueSphere().getRadius() /
                    2f;
                float coordinateRadiusRatio = ((prevVSWorldPt
                    - prevSphereCenter).magnitude) / prevSphereRadius;
                Vector3 newWorldPt = curSphereCenter +
                    coordinateDir * curSphereRadius * coordinateRadiusRatio;
                Vector2 newValueCoordinate = this.mSS.getPerspCameraPerson().
                getCamera().WorldToScreenPoint(newWorldPt);
                vs.setValueCoordinate(newValueCoordinate);
                //update cur pos and rad to each strokes.
                //need to put screen center back to world pt.
                vs.setSphereCenter(curSphereCenter);
                vs.setSphereRadius(curSphereRadius);
            }
        }
    }
}