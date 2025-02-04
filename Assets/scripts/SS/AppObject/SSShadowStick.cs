// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using SS.Geom;
// using SS.Scenario;
// using SSAppObject;
// using UnityEngine.UIElements;

// namespace SS.AppObject {
//     public class SSShadowStick : SSAppGeom3D {
//         //constants

//         //fields
//         private Vector3 mPosition = Vector3.zero;
//         private Vector3 mLightPos = Vector3.zero;
//         private Quaternion mCenterAngle = Quaternion.identity;

//         //sticks
//         private SSStick mLeftStick = null;
//         public SSStick getLeftStick() {
//             return this.mLeftStick;
//         }

//         private SSStick mRightStick = null;
//         public SSStick getRightStick() {
//             return this.mRightStick;
//         }

//         //floor
//         private SSFloor mFloor = null;
//         public SSFloor getFloor() {
//             return this.mFloor;
//         }

//         //constructor
//         public SSShadowStick(string name) :
//             base($"{name}/ShadowStick") {
//             this.refreshAtGeomChange();
//         }

//         //methods
//         protected override void addComponents() {
//             this.mGameObject.AddComponent<SphereCollider>();
//             this.mGameObject.AddComponent<MeshRenderer>();
//             this.mGameObject.AddComponent<MeshFilter>();
//         }

//         protected override void refreshCollider() {
//             SphereCollider cc = this.mSphere.GetComponent<SphereCollider>();
//             cc.radius = this.mRadius;
//         }

//         protected override void refreshRenderer() {
//             MeshFilter mf = this.mSphere.GetComponent<MeshFilter>();
//             MeshRenderer mr = this.mSphere.GetComponent<MeshRenderer>();
//             Vector3 scaleChange =
//             new Vector3(this.mRadius, this.mRadius, this.mRadius);
//             this.mSphere.transform.localScale = scaleChange;
//             this.mEquator.refreshAtGeomChange();
//             this.updatePole();
//         }

//         public void setPos(Vector3 pos) {
//             this.getGameObject().transform.position = pos;
//             this.refreshAtGeomChange();
//         }
//     }
// }