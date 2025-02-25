using SS.AppObject;
using SSAppObject;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
        //camera
        private SSPerspCameraPerson mPerspCameraPerson = null;
        public SSPerspCameraPerson getPerspCameraPerson () {
            return this.mPerspCameraPerson;
        }
        private SSOrthoCameraPerson mOrthoCameraPerson = null;
        public SSOrthoCameraPerson getOrthoCameraPerson() {
            return this.mOrthoCameraPerson;
        }
        private SSGridCameraPerson mGridCameraPerson = null;
        public SSGridCameraPerson getGridCameraPerson() {
            return this.mGridCameraPerson;
        }
        private SSCropCameraPerson mCropCameraPerson = null;
        public SSCropCameraPerson getCropCameraPerson() {
            return this.mCropCameraPerson;
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
        //valueSphere
        private SSValueSphereMgr mValueSphereMgr = null;
        public SSValueSphereMgr getValueSphereMgr() {
            return this.mValueSphereMgr;
        }
        private SSValueStrokeMgr mValueStrokeMgr = null;
        public SSValueStrokeMgr getValueStrokeMgr() {
            return this.mValueStrokeMgr;
        }
        //ShadowStick
        private SSPerspectiveCubeMgr mPerspectiveCubeMgr = null;
        public SSPerspectiveCubeMgr getSSPerspectiveCubeMgr() {
            return this.mPerspectiveCubeMgr;
        }
        public void setSSPerspectiveCubeMgr(SSPerspectiveCubeMgr cubeMgr) {
            this.mPerspectiveCubeMgr = cubeMgr;
        }
        private SSShadowStickMgr mShadowStickMgr = null;
        public SSShadowStickMgr getShadowStickMgr() {
            return this.mShadowStickMgr;
        }
        private SSShadowTraceMgr mShadowTraceMgr = null;
        public SSShadowTraceMgr getShadowTraceMgr() {
            return this.mShadowTraceMgr;
        }
        private SSSnapshotMgr mSnapshotMgr = null;
        public SSSnapshotMgr getSnapshotMgr() {
            return this.mSnapshotMgr;
        }
        private SSShadowCurveMgr mShadowCurveMgr = null;
        public SSShadowCurveMgr getShadowCurveMgr() {
            return this.mShadowCurveMgr;
        }

        //eventsources
        private SSKeyEventSource mKeyEventSource = null;
        private SSPenEventSource mPenEventSource = null;
        private SSTouchEventSource mTouchEventSource = null;
        private SSEventListener mEventListener = null;

        //underlay
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
        private SSImage2D mCylinderUnderlay = null;
        public SSImage2D getCylinderUnderlay() {
            return this.mCylinderUnderlay;
        }
        private SSImage2D mCylinderFloatingUnderlay = null;
        public SSImage2D getCylinderFloatingUnderlay() {
            return this.mCylinderFloatingUnderlay;
        }
        private SSImage2D mFloatingCubeUnderlay = null;
        public SSImage2D getFloatingCubeUnderlay() {
            return this.mFloatingCubeUnderlay;
        }
        private SSImage2D mCylinderLayUnderlay = null;
        public SSImage2D getCylinderLayUnderlay() {
            return this.mCylinderLayUnderlay;
        }
        private SSImage2D mCompositionUnderlay = null;
        public SSImage2D getComposition() {
            return this.mCompositionUnderlay;
        }
        private SSImage2D mCubeFloorUnderlay = null;
        public SSImage2D getCubeFloor() {
            return this.mCubeFloorUnderlay;
        }
        private SSImage2D mConeFloorUnderlay = null;
        public SSImage2D getConeFloor() {
            return this.mConeFloorUnderlay;
        }
        private SSImage2D mConeFloatingUnderlay = null;
        public SSImage2D getConeFloating() {
            return this.mConeFloatingUnderlay;
        }
        private SSImage2D mConeCubeBigUnderlay= null;
        public SSImage2D getConeCubeBig() {
            return this.mConeCubeBigUnderlay;
        }
        private SSImage2D mCubeTowerUnderlay= null;
        public SSImage2D getCubeTower() {
            return this.mCubeTowerUnderlay;
        }
        private SSImage2D mAirplaneUnderlay= null;
        public SSImage2D getAirplane() {
            return this.mAirplaneUnderlay;
        }

        //rendertexture
        public RenderTexture renderTexture = null;
        public SSAppTexturedRect3D mCropTexture = null;

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
            this.mOrthoCameraPerson.getCamera().clearFlags =
                CameraClearFlags.Depth;
            this.mGridCameraPerson = new SSGridCameraPerson();
            // this.mCropCameraPerson = new SSCropCameraPerson(this);
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
            Vector2 screenSize2 =
                new Vector2(Screen.width / 1.5f, Screen.height);
            Vector2 screenSize3 =
                new Vector2(Screen.width / 1.2f, Screen.height / 1.2f);
            Vector2 screenSize4 = new Vector2(837f, 814f);
            Vector2 screenSize5 = new Vector2(682f, 925f);
            Vector2 screenSize6 = new Vector2(1358f / 2f, 1793f / 2f);
            Vector2 screenSize7 = new Vector2(1620, 1471f);
            Vector2 screenSize8 = new Vector2(2923, 1744);
            Vector2 screenSize9 = new Vector2(372, 346);
            Vector2 screenSize10 = new Vector2(246, 407);
            Vector2 screenSize11 = new Vector2(265, 364);
            Vector2 screenSize12 = new Vector2(1020, 1344);
            Vector2 screenSize13 = new Vector2(1113, 697);

            this.mHairdryerUnderlay = new SSImage2D("Underlay", "hairdryer",
                screenSize3, screenSize / 2.0f - new Vector2(20.0f, 0));
            this.mHairdryerUnderlay.getGameObject().SetActive(false);

            this.mRobotUnderlay = new SSImage2D("Underlay", "robot",
                screenSize2, screenSize / 2f);
            this.mRobotUnderlay.getGameObject().SetActive(false);

            this.mBuildingUnderlay = new SSImage2D("Underlay", "building",
                screenSize, screenSize / 2f);
            this.mBuildingUnderlay.getGameObject().SetActive(false);

            this.mCylinderUnderlay = new SSImage2D("Underlay", "cylinder",
                screenSize4, new Vector3(1627f, 585f, 0f));
            this.mCylinderUnderlay.getGameObject().SetActive(false);

            this.mCylinderFloatingUnderlay =
                new SSImage2D("Underlay", "cylinder_floating",
                screenSize5, new Vector3(1627f, 585f, 0f));
            this.mCylinderFloatingUnderlay.getGameObject().SetActive(false);

            this.mFloatingCubeUnderlay =
                new SSImage2D("Underlay", "foating_cube",
                screenSize6 / 2, new Vector3(1627f / 2, 585f, 0f));
            this.mFloatingCubeUnderlay.getGameObject().SetActive(false);

            this.mCylinderLayUnderlay = new SSImage2D("Underlay", "cylinder_lay",
                screenSize7, new Vector3(1627f, 585f, 0f));
            this.mCylinderLayUnderlay.getGameObject().SetActive(false);

            this.mCompositionUnderlay = new SSImage2D("Underlay", "composition",
                screenSize8, new Vector3(1627f, 585f, 0f));
            this.mCompositionUnderlay.getGameObject().SetActive(false);

            this.mCubeFloorUnderlay = new SSImage2D("Underlay", "cube_floor",
                screenSize9, new Vector3(1627f, 585f, 0f));
            this.mCubeFloorUnderlay.getGameObject().SetActive(false);

            this.mConeFloorUnderlay = new SSImage2D("Underlay", "cone_floor",
                screenSize9, new Vector3(1627f, 585f, 0f));
            this.mConeFloorUnderlay.getGameObject().SetActive(false);

            this.mConeFloatingUnderlay = new SSImage2D("Underlay", "cone_floating",
                screenSize10, new Vector3(1627f, 585f, 0f));
            this.mConeFloatingUnderlay.getGameObject().SetActive(false);

            this.mConeCubeBigUnderlay = new SSImage2D("Underlay", "conCube_big",
                screenSize11, new Vector3(1627f, 585f, 0f));
            this.mConeCubeBigUnderlay.getGameObject().SetActive(false);

            this.mCubeTowerUnderlay = new SSImage2D("Underlay", "cubeTower",
                screenSize12, new Vector3(1627f, 585f, 0f));
            this.mCubeTowerUnderlay.getGameObject().SetActive(false);

            this.mAirplaneUnderlay = new SSImage2D("Underlay", "airplane",
                screenSize13 , new Vector3(1627f, 1500f, 0f) / 2);
            this.mAirplaneUnderlay.getGameObject().SetActive(true);

            //perspectiveCube
            this.mPerspectiveCubeMgr = new SSPerspectiveCubeMgr(this);
            this.mPerspectiveCubeMgr.makeGridTransparent();

            //ShadowStick
            this.mShadowStickMgr = new SSShadowStickMgr(this);
            this.mShadowTraceMgr = new SSShadowTraceMgr(this);
            this.mShadowCurveMgr = new SSShadowCurveMgr(this);

            // //crop camera
            // // SSAppTexturedRect3D 생성
            // this.mCropTexture = new SSAppTexturedRect3D("TexturedRect", 3f, 2f);
            // renderTexture = new RenderTexture(1920 * 4, 1080 * 4, 32);
            // renderTexture.Create();
            // // 텍스처 설정
            // this.mCropCameraPerson.getCamera().targetTexture = renderTexture;
            // this.mCropTexture.setTexture(renderTexture);
            // // 텍스처 크롭 비율 설정 (50% 영역만 표시)
            // this.mCropTexture.setTextureCropRatio(new Vector2(1f, 1f));
            // // 텍스처 오프셋 설정 (텍스처의 왼쪽 위를 기준으로 표시)
            // this.mCropTexture.setTextureOffset(Vector2.zero);

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

