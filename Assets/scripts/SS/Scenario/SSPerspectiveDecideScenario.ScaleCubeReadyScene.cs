using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSperspectiveDecideScenario : XScenario {
        public class ScaleCubeReadyScene : SSScene {
            //fields
            private string mCubeComponent = null;
            //singleton pattern
            private static ScaleCubeReadyScene mSingleton = null;
            public static ScaleCubeReadyScene getSingleton() {
                Debug.Assert(ScaleCubeReadyScene.mSingleton != null);
                return ScaleCubeReadyScene.mSingleton;
            }
            public static ScaleCubeReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ScaleCubeReadyScene.mSingleton == null);
                ScaleCubeReadyScene.mSingleton = new ScaleCubeReadyScene(scenario);
                return ScaleCubeReadyScene.mSingleton;
            }

            private ScaleCubeReadyScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                        ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }

                //make cube visible and hide grid again.
                SSPerspectiveCubeMgr cubeMgr = ss.getSSPerspectiveCubeMgr();

                //check what cube component user is controlling.
                Vector2 touchedScreenPt =
                    ss.getTouchMarkMgr().getLastDownTouchMark().getLastPt();
                this.mCubeComponent = cubeMgr.cubeCollideChecker(touchedScreenPt);
                if (this.mCubeComponent == "POV") {
                    XCmdToChangeScene.execute(ss,
                        SSSperspectiveDecideScenario.ChangePOVScene.
                        getSingleton(), null);
                } else if (this.mCubeComponent == "Rotation") {
                    XCmdToChangeScene.execute(ss,
                        SSSperspectiveDecideScenario.ChangeRotationScene.
                        getSingleton(), null);
                } else if (this.mCubeComponent == "FOV") {
                    XCmdToChangeScene.execute(ss,
                        SSSperspectiveDecideScenario.ChangeFOVScene.
                        getSingleton(), null);
                } else {
                    XCmdToChangeScene.execute(ss,
                        SSSperspectiveDecideScenario.ChangePositionScene.
                        getSingleton(), null);
                }
            }
            public override void handleKeyDown(Key kc) {
            }
            public override void handleKeyUp(Key kc) {

            }
            public override void handlePenUp(Vector2 pt) {
            }
            public override void handlePenDrag(Vector2 pt) {
            }
            public override void handlePenDown(Vector2 pt) {
            }

            public override void handleEraserDown(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {
            }

            public override void handleTouchDrag() {

            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                SSPerspectiveCubeMgr cubeMgr = ss.getSSPerspectiveCubeMgr();
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(), null);
                }
            }

            public override void wrapUp() {
            }
        }
    }
}