using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;
using SS.Scenario;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSStickHandleScenario : XScenario {
        public class SlideEdgeBottomScene : SSScene {
            //singleton pattern
            private static SlideEdgeBottomScene mSingleton = null;
            public static SlideEdgeBottomScene getSingleton() {
                Debug.Assert(SlideEdgeBottomScene.mSingleton != null);
                return SlideEdgeBottomScene.mSingleton;
            }
            public static SlideEdgeBottomScene createSingleton(XScenario scenario) {
                Debug.Assert(SlideEdgeBottomScene.mSingleton == null);
                SlideEdgeBottomScene.mSingleton = new SlideEdgeBottomScene(scenario);
                return SlideEdgeBottomScene.mSingleton;
            }

            private SlideEdgeBottomScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr SSMgr= ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSCameraPerson cam = ss.getGridCameraPerson();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                    ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {}

            public override void handleEraserDown(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToSlideEdgeBottom.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(), null);
                }
            }

            public override void wrapUp() {}

            //util function
        }
    }
}