using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSperspectiveDecideScenario : XScenario {
        public class ScaleCubeScene : SSScene {
            //singleton pattern
            private static ScaleCubeScene mSingleton = null;
            public static ScaleCubeScene getSingleton() {
                Debug.Assert(ScaleCubeScene.mSingleton != null);
                return ScaleCubeScene.mSingleton;
            }
            public static ScaleCubeScene createSingleton(XScenario scenario) {
                Debug.Assert(ScaleCubeScene.mSingleton == null);
                ScaleCubeScene.mSingleton = new ScaleCubeScene(scenario);
                return ScaleCubeScene.mSingleton;
            }

            private ScaleCubeScene(XScenario scenario) : base(scenario) {}

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
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToScaleCube.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSSperspectiveDecideScenario.ScaleCubeReadyScene.
                        getSingleton(), null);
                }
            }

            public override void wrapUp() {
            }
        }
    }
}