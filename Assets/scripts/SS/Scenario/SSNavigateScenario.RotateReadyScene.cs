using UnityEngine;
using X;
using UnityEngine.InputSystem;

namespace SS.Scenario {
    public partial class SSNavigateScenario {
        public class RotateReadyScene : SSScene {
            // singleton pattern
            private static RotateReadyScene mSingleton = null;
            public static RotateReadyScene getSingleton() {
                Debug.Assert(RotateReadyScene.mSingleton != null);
                return RotateReadyScene.mSingleton;
            }
            public static RotateReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(RotateReadyScene.mSingleton == null);
                RotateReadyScene.mSingleton = new RotateReadyScene(scenario);
                return RotateReadyScene.mSingleton;
            }
            private RotateReadyScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSNavigateScenario scenario = SSNavigateScenario.getSingleton();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                        ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {}

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {}

            public override void handleEraserDown(Vector2 pt) {}

            public override void handleEraserDrag(Vector2 pt) {}

            public override void handleEraserUp(Vector2 pt) {}

            public override void handleTouchDown() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                //go to zoom and rotate scene.
                XCmdToChangeScene.execute(ss,
                        SSNavigateScenario.ZoomAndRotateScene.getSingleton(),
                        this.mReturnScene);
            }

            public override void handleTouchDrag() {}

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(), null);
            }

            public override void wrapUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSNavigateScenario scenario =
                    SSNavigateScenario.getSingleton();
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                }
            }
        }
    }
}