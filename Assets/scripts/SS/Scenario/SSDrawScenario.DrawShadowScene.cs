using UnityEngine;
using X;
using SS.Cmd;
using UnityEngine.InputSystem;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSDrawScenario : XScenario {
        public class DrawShadowScene : SSScene {
            //singleton pattern
            private static DrawShadowScene mSingleton = null;
            public static DrawShadowScene getSingleton() {
                Debug.Assert(DrawShadowScene.mSingleton != null);
                return DrawShadowScene.mSingleton;
            }
            public static DrawShadowScene createSingleton(XScenario scenario) {
                Debug.Assert(DrawShadowScene.mSingleton == null);
                DrawShadowScene.mSingleton = new DrawShadowScene(scenario);
                return DrawShadowScene.mSingleton;
            }

            private DrawShadowScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToCreateCurShadowCurve.execute(ss);
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {}

            public override void handlePenDrag(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToUpdateCurShadowCurve.execute(ss);
            }

            public override void handlePenUp(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToAddCurShadowCurveToShadowCurve.execute(ss);
                XCmdToChangeScene.execute(ss, this.mReturnScene, null);
            }

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
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDrag() {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchUp() {
                //throw new System.NotImplementedException();
            }

            public override void wrapUp() {}
        }
    }
}