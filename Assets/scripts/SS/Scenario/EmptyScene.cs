using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;

namespace SS.Scenario
{
    public partial class SSDefaultScenario : XScenario
    {
        public class EmptyScene : SSScene
        {
            //singleton pattern
            private static EmptyScene mSingleton = null;
            public static EmptyScene getSingleton()
            {
                Debug.Assert(EmptyScene.mSingleton != null);
                return EmptyScene.mSingleton;
            }
            public static EmptyScene createSingleton(XScenario scenario)
            {
                Debug.Assert(EmptyScene.mSingleton == null);
                EmptyScene.mSingleton = new EmptyScene(scenario);
                return EmptyScene.mSingleton;
            }

            private EmptyScene(XScenario scenario) : base(scenario)
            {

            }

            //event handling methods
            public override void handleKeyDown(Key kc)
            {
            }
            public override void handleKeyUp(Key kc)
            {
            }
            public override void handlePenUp(Vector2 pt)
            {
            }
            public override void handlePenDrag(Vector2 pt)
            {
            }
            public override void handlePenDown(Vector2 pt)
            {
            }
            public override void getReady()
            {
            }
            public override void wrapUp()
            {
            }

            public override void handleEraserDown(Vector2 pt)
            {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt)
            {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt)
            {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {
                throw new System.NotImplementedException();
            }

            public override void handleTouchDrag() {
                throw new System.NotImplementedException();
            }

            public override void handleTouchUp() {
                throw new System.NotImplementedException();
            }
        }
    }
}