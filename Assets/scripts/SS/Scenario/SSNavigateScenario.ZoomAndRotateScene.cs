using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSNavigateScenario {
        public class ZoomAndRotateScene : SSScene {
            // singleton pattern
            private static ZoomAndRotateScene mSingleton = null;
            public static ZoomAndRotateScene getSingleton() {
                Debug.Assert(ZoomAndRotateScene.mSingleton != null);
                return ZoomAndRotateScene.mSingleton;
            }
            public static ZoomAndRotateScene createSingleton(
                XScenario scenario) {
                Debug.Assert(ZoomAndRotateScene.mSingleton == null);
                ZoomAndRotateScene.mSingleton =
                new ZoomAndRotateScene(scenario);
                return ZoomAndRotateScene.mSingleton;
            }

            private ZoomAndRotateScene(XScenario scenario) : base(scenario) {}

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

            public override void handleTouchDown() {}

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                //zoom and rotate here.
                SSCmdToZoomAndRotate.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(ss,
                        SSNavigateScenario.RotateReadyScene.getSingleton(),
                        null);
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