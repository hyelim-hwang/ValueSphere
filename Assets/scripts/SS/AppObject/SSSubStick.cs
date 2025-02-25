using UnityEngine;
using SS.Geom;
using SS.AppObject;
using System.Collections.Generic;
using System.Linq;
using SS;
using UnityEngine.Rendering;

namespace SSAppObject {
    public class SSSubStick : SSAppObject3D {
        // constants

        // fields

        //rotation
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }

        //plane
        private Plane mPlane;
        public Plane getPlane() {
            return this.mPlane;
        }
        public void setPlane(Plane plane) {
            this.mPlane = plane;
        }

        //light direction
        private Vector3 mLightVector;
        public Vector3 getLightVector() {
            return this.mLightVector;
        }
        public void setLightDirection(Vector3 dir) {
            this.mLightVector = dir;
            this.mShadowTop = calculateShadowTop();
            this.mShadowBottom = calculateShadowBottom();
            updateWidgetWithChangedPoints();
            //update the shadowTrace too.
            if (this.mShadowTrace != null) {
                this.mShadowTrace.setTraceTop(this.getShadowTop());
                this.mShadowTrace.setTraceBottom(this.getShadowBottom());
            }
        }

        //special points
        private Vector3 mEdgeTop;
        public Vector3 getEdgeTop() {
            return this.mEdgeTop;
        }
        public void setEdgeTop(Vector3 top) {
            this.mEdgeTop = top;
            this.mShadowTop = this.calculateShadowTop();
        }
        private Vector3 mEdgeBottom;
        public Vector3 getEdgeBottom() {
            return this.mEdgeBottom;
        }
        public void setEdgeBottom(Vector3 bot) {
            this.mEdgeBottom = bot;
            this.mShadowBottom = this.calculateShadowBottom();
        }
        public Vector3 mBaseOfStick;
        public Vector3 getBaseOfStick() {
            return this.mBaseOfStick;
        }
        public void setBaseOfStick(Vector3 stickBase) {
            this.mBaseOfStick = stickBase;
            this.mGameObject.transform.position = stickBase;
            // this.updateSpecialPointsByBaseOfStick();
        }
        private Vector3 mShadowTop;
        public Vector3 getShadowTop() {
            return this.mShadowTop;
        }
        public void setShadowTop(Vector3 top) {
            this.mShadowTop = top;
        }

        private Vector3 mShadowBottom;
        public Vector3 getShadowBottom() {
            return this.mShadowBottom;
        }
        public void setShadowBottom(Vector3 bot) {
            this.mShadowBottom = bot;
        }
        private SSShadowTrace mShadowTrace;
        public SSShadowTrace getShadowTrace() {
            return this.mShadowTrace;
        }
        public void setShadowTrace(SSShadowTrace shadowTrace) {
            this.mShadowTrace = shadowTrace;
        }

        //face
        private SSAppTrapezoid3D mFace;
        public SSAppTrapezoid3D getFace() {
            return this.mFace;
        }
        public void setFace(SSAppTrapezoid3D face) {
            this.mFace = face;
        }

        //sticks
        private SSAppPolyline3D mVerticalStick = null;
        public SSAppPolyline3D getVerticalStick() {
            return this.mVerticalStick;
        }
        private SSAppPolyline3D mTopLightDirection = null;
        public SSAppPolyline3D getTopLightDirection() {
            return this.mTopLightDirection;
        }
        private SSAppPolyline3D mShadowDirection = null;
        public SSAppPolyline3D getShadowDirection() {
            return this.mShadowDirection;
        }
        private SSAppPolyline3D mBottomLightDirection = null;
        public SSAppPolyline3D getBottomLightDirection() {
            return this.mBottomLightDirection;
        }

        // constructor
        public SSSubStick(string name, Plane plane, Vector3 lightVector) :
            base($"{ name }/Stick") {
            this.mPlane = plane;
            this.mLightVector = lightVector;
            this.mBaseOfStick = SSShadowStickMgr.DEFAULT_POS;
            this.mEdgeTop = this.mBaseOfStick +
                Vector3.up * SSShadowStickMgr.DEFAULT_TOP_HEIGHT;
            this.mEdgeBottom = this.mBaseOfStick +
                Vector3.up * SSShadowStickMgr.DEFAULT_BOTTOM_HEIGHT;
            this.mShadowTop = calculateShadowTop();
            this.mShadowBottom = calculateShadowBottom();

            //sticks
            List<Vector3> verticalStickPts = new List<Vector3>();
            verticalStickPts.Add(this.mEdgeTop);
            verticalStickPts.Add(Vector3.zero);
            this.mVerticalStick = new SSAppPolyline3D("vertical",
                verticalStickPts, SSShadowStickMgr.DEFAULT_WIDTH,
                SSShadowStickMgr.DEFAULT_COLOR);
            this.addChild(this.mVerticalStick);

            List<Vector3> shadowDirectionPts = new List<Vector3>();
            shadowDirectionPts.Add(Vector3.zero);
            shadowDirectionPts.Add(this.mShadowTop);
            this.mShadowDirection = new SSAppPolyline3D("shadowDirection",
                shadowDirectionPts, SSShadowStickMgr.DEFAULT_WIDTH,
                SSShadowStickMgr.DEFAULT_COLOR);
            this.addChild(this.mShadowDirection);

            List<Vector3> topLightDirectionPts = new List<Vector3>();
            topLightDirectionPts.Add(this.mEdgeTop);
            topLightDirectionPts.Add(this.mShadowTop);
            this.mTopLightDirection = new SSAppPolyline3D("topLightDirection",
                topLightDirectionPts, SSShadowStickMgr.DEFAULT_WIDTH,
                SSShadowStickMgr.DEFAULT_COLOR);
            this.addChild(this.mTopLightDirection);

            List<Vector3> bottomLightDirectionPts = new List<Vector3>();
            bottomLightDirectionPts.Add(this.mEdgeBottom);
            bottomLightDirectionPts.Add(this.mShadowBottom);
            this.mBottomLightDirection = new SSAppPolyline3D(
                "bottomLightDirection",
                bottomLightDirectionPts, SSShadowStickMgr.DEFAULT_WIDTH,
                SSShadowStickMgr.DEFAULT_COLOR);
            this.addChild(this.mBottomLightDirection);

            //face
            this.mFace = new SSAppTrapezoid3D("face", this.mEdgeTop,
                this.mEdgeBottom, this.mShadowTop, this.mShadowBottom,
                Color.yellow);
            Material faceMat = this.mFace.getGameObject().
                GetComponent<MeshRenderer>().material;
            faceMat.SetFloat("_Mode", 3);
            faceMat.SetInt("_SrcBlend",
                (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            faceMat.SetInt("_DstBlend",
                (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            faceMat.SetInt("_ZWrite", 0);
            faceMat.DisableKeyword("_ALPHATEST_ON");
            faceMat.EnableKeyword("_ALPHABLEND_ON");
            faceMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            faceMat.renderQueue = 3000;
            // Alpha 값 적용
            Color color = faceMat.color;
            color.a =  0.5f;
            faceMat.color = color;

            this.addChild(this.mFace);

            //add stick and face to grid layer(3)
            this.getGameObject().layer = 3;
            foreach (Transform child in this.getGameObject().transform) {
                child.gameObject.layer = 3;
            }
        }


        // methods
        protected override void addComponents() {
            //this.mGameObject.AddComponent<LineRenderer>();
        }

        protected Vector3 calculateShadowTop() {
            Ray ray = new Ray(this.mEdgeTop, this.mLightVector);
            if (this.mPlane.Raycast(ray, out float enter)) {
                Vector3 intersectionPoint = ray.GetPoint(enter);
                Debug.Log("intersection: " + intersectionPoint);
                return intersectionPoint;
            } else {
                Debug.Log("not intersecting");
                return Vector3.negativeInfinity;
            }
        }

        protected Vector3 calculateShadowBottom() {
            Ray ray = new Ray(this.mEdgeBottom, this.mLightVector);
            if (this.mPlane.Raycast(ray, out float enter)) {
                Vector3 intersectionPoint = ray.GetPoint(enter);
                Debug.Log("intersection: " + intersectionPoint);
                return intersectionPoint;
            } else {
                Debug.Log("not intersecting");
                return Vector3.negativeInfinity;
            }
        }

        public void updateWidgetWithChangedPoints() {
            List<Vector3> verticalStickPts = new List<Vector3>();
            verticalStickPts.Add(this.mEdgeTop);
            verticalStickPts.Add(Vector3.zero);
            this.mVerticalStick.setPts(verticalStickPts);

            List<Vector3> shadowDirectionPts = new List<Vector3>();
            shadowDirectionPts.Add(Vector3.zero);
            shadowDirectionPts.Add(this.mShadowTop);
            this.mShadowDirection.setPts(shadowDirectionPts);

            List<Vector3> topLightDirectionPts = new List<Vector3>();
            topLightDirectionPts.Add(this.mEdgeTop);
            topLightDirectionPts.Add(this.mShadowTop);
            this.mTopLightDirection.setPts(topLightDirectionPts);

            List<Vector3> bottomLightDirectionPts = new List<Vector3>();
            bottomLightDirectionPts.Add(this.mEdgeBottom);
            bottomLightDirectionPts.Add(this.mShadowBottom);
            this.mBottomLightDirection.setPts(bottomLightDirectionPts);
            List<Vector3> pts = new List<Vector3>();

            pts.Add(this.mEdgeTop);
            pts.Add(this.mEdgeBottom);
            pts.Add(this.mShadowTop);
            pts.Add(this.mShadowBottom);
            this.mFace.setPts(pts);
        }

        // protected void updateSpecialPointsByBaseOfStick() {
        //     this.mEdgeTop = this.mBaseOfStick +
        //         Vector3.up * SSShadowStickMgr.DEFAULT_TOP_HEIGHT;
        //     this.mEdgeBottom = this.mBaseOfStick +
        //         Vector3.up * SSShadowStickMgr.DEFAULT_BOTTOM_HEIGHT;
        //     this.mShadowTop = calculateShadowTop();
        //     this.mShadowBottom = calculateShadowBottom();
        //     //need to update appobjects by updated special points.
        //     this.mGameObject.transform
        // }

        public void makeStickTransparent(SSSubStick stick) {
            SSAppTrapezoid3D face = stick.getFace();
            SSAppPolyline3D topLightDir = stick.getTopLightDirection();
            SSAppPolyline3D bottomLightDir = stick.getBottomLightDirection();
            SSAppPolyline3D verticalStick = stick.getVerticalStick();
            SSAppPolyline3D shadowDir = stick.getShadowDirection();
            Material faceMat = face.getGameObject().
                GetComponent<MeshRenderer>().material;
            faceMat.SetFloat("_Mode", 3);
            faceMat.SetInt("_SrcBlend",
                (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            faceMat.SetInt("_DstBlend",
                (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            faceMat.SetInt("_ZWrite", 0);
            faceMat.DisableKeyword("_ALPHATEST_ON");
            faceMat.EnableKeyword("_ALPHABLEND_ON");
            faceMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            faceMat.renderQueue = 3000;
            // Alpha 값 적용
            Color color = faceMat.color;
            color.a =  0.1f;
            faceMat.color = color;
            makeLineTransparent(topLightDir);
            makeLineTransparent(bottomLightDir);
            makeLineTransparent(verticalStick);
            makeLineTransparent(shadowDir);
        }
        public void makeLineTransparent(SSAppPolyline3D line) {
            Material faceMat = line.getGameObject().
                GetComponent<LineRenderer>().material;
            LineRenderer lineRenderer = line.getGameObject().GetComponent<LineRenderer>();
            faceMat.renderQueue = 3000;
            // Alpha 값 적용
            Color color = faceMat.color;
            color.a =  0.5f;
            Color startColor = lineRenderer.startColor;
            startColor.a = 0.5f; // Alpha 값 적용
            lineRenderer.startColor = startColor;
            Color endColor = lineRenderer.endColor;
            endColor.a = 0.5f; // Alpha 값 적용
            lineRenderer.endColor = endColor;
        }
    }
}