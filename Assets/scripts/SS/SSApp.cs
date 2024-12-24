using SS.AppObject;
using UnityEngine;
using X;

namespace SS {
    public class SSApp : XApp {
        //fields
        [SerializeField]
        private GameObject mCanvas;
        public GameObject getCanvas() {
            return this.mCanvas;
        }
        private Vector3 mLightDirection = Vector3.up;
        public Vector3 getLightDirection() {
            return this.mLightDirection;
        }
        private XLogMgr mLogMgr = null;
        public override XLogMgr getLogMgr() {
            return this.mLogMgr;
        }
        private XScenarioMgr mScenarioMgr = null;
        public override XScenarioMgr getScenarioMgr() {
            return this.mScenarioMgr;
        }
        private SSPenMarkMgr mPenMarkMgr = null;
        public SSPenMarkMgr getPenMarkMgr() {
            return this.mPenMarkMgr;
        }
        private SSPerspCameraPerson mPerspCameraPerson = null;
        public SSPerspCameraPerson getPerspCameraPerson () {
            return this.mPerspCameraPerson;
        }
        private SSOrthoCameraPerson mOrthoCameraPerson = null;
        public SSOrthoCameraPerson getOrthoCameraPerson() {
            return this.mOrthoCameraPerson;
        }
        private SSCursorMgr mCursorMgr = null;
        public SSCursorMgr getCursorMgr() {
            return this.mCursorMgr;
        }
        private SSTouchMarkMgr mTouchMarkMgr = null;
        public SSTouchMarkMgr getTouchMarkMgr() {
            return this.mTouchMarkMgr;
        }
        private SSLightSourceMgr mLightSourceMgr = null;
        public SSLightSourceMgr getLightSourceMgr() {
            return this.mLightSourceMgr;
        }
        private SSValueSphereMgr mValueSphereMgr = null;
        public SSValueSphereMgr getValueSphereMgr() {
            return this.mValueSphereMgr;
        }
        private SSValueStrokeMgr mValueStrokeMgr = null;
        public SSValueStrokeMgr getValueStrokeMgr() {
            return this.mValueStrokeMgr;
        }
        private SSSnapshotMgr mSnapshotMgr = null;
        public SSSnapshotMgr getSnapshotMgr() {
            return this.mSnapshotMgr;
        }

        private SSKeyEventSource mKeyEventSource = null;
        private SSPenEventSource mPenEventSource = null;
        private SSTouchEventSource mTouchEventSource = null;
        private SSEventListener mEventListener = null;

        private SSImage2D mHairdryerUnderlay = null;
        public SSImage2D getHairdryerUnderlay() {
            return this.mHairdryerUnderlay;
        }
        private SSImage2D mRobotUnderlay = null;
        public SSImage2D getRobotUnderlay() {
            return this.mRobotUnderlay;
        }
        private SSImage2D mBuildingUnderlay = null;
        public SSImage2D getBuildingUnderlay() {
            return this.mBuildingUnderlay;
        }

        private void configureUnity() {
            // necessary for manually refreshing collider physics
            Physics2D.simulationMode = SimulationMode2D.Script; // Unity 2020
            // Physics2D.autoSimulation = false; // Unity 2019
            // enable multi-threading for faster physics performance
            Physics2D.jobOptions = new PhysicsJobOptions2D {
                useMultithreading = true
            };
            // for disabling all the unncessary graphics options
            QualitySettings.SetQualityLevel(0);
            // for maximum refresh rate of pen and touch input
            Application.targetFrameRate = -1; // does not work if VR is connected
            // the only graphics quality setting that should be important
            QualitySettings.antiAliasing = 0;
            // QualitySettings.antiAliasing = 4;
            // enable collision with both sides of the mesh
            Physics.queriesHitBackfaces = true;
        }

        private void Start() {
            this.configureUnity();
            //unity physics options
            Debug.Log("Hello, world!");
            //event
            this.mEventListener = new SSEventListener(this);
            this.mKeyEventSource = new SSKeyEventSource();
            this.mPenEventSource = new SSPenEventSource();
            this.mTouchEventSource = new SSTouchEventSource();
            //camera
            this.mPerspCameraPerson = new SSPerspCameraPerson();
            Camera.main.useOcclusionCulling = false;
            this.mOrthoCameraPerson = new SSOrthoCameraPerson();
            //penmark & touchmark
            this.mPenMarkMgr = new SSPenMarkMgr();
            this.mTouchMarkMgr = new SSTouchMarkMgr();
            //log
            this.mLogMgr = new XLogMgr();
            this.mLogMgr.setPrintOn(true);
            //ptCurve
            this.mValueStrokeMgr = new SSValueStrokeMgr(this);
            //ui objects
            this.mCursorMgr = new SSCursorMgr(this);
            this.mValueSphereMgr = new SSValueSphereMgr(this);
            //scenario
            this.mScenarioMgr = new SSScenarioMgr(this);
            //set event listener
            this.mKeyEventSource.setEventListener(this.mEventListener);
            this.mPenEventSource.setEventListener(this.mEventListener);
            this.mTouchEventSource.setEventListener(this.mEventListener);
            //light condition
            this.mLightSourceMgr = new SSLightSourceMgr(this);
            // underlay
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 screenSize2 = new Vector2(Screen.width / 1.5f, Screen.height);
            Vector2 screenSize3 = new Vector2(Screen.width / 1.2f, Screen.height/1.2f);
            this.mHairdryerUnderlay = new SSImage2D("Underlay", "hairdryer",
                screenSize3, screenSize / 2.0f - new Vector2(20.0f, 0));
            this.mHairdryerUnderlay.getGameObject().SetActive(true);
            this.mRobotUnderlay = new SSImage2D("Underlay", "robot",
                screenSize2, screenSize / 2f);
            this.mRobotUnderlay.getGameObject().SetActive(false);
            this.mBuildingUnderlay = new SSImage2D("Underlay", "building",
                screenSize, screenSize / 2f);
            this.mBuildingUnderlay.getGameObject().SetActive(false);
            //undo & redo
            this.mSnapshotMgr = new SSSnapshotMgr(this);
        }

        private void Update() {
            this.mOrthoCameraPerson.update();
            this.mKeyEventSource.update();
            this.mPenEventSource.update();
            this.mTouchEventSource.update();

        }
    }
}