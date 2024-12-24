using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Geom;

namespace SS.AppObject
{
    public class SSAppCircle3D : SSAppGeom3D
    {
        //constants
        public static readonly int NUM_SIDE = 64;

        //fields
        public void setRadius (float radius)
        {
            SSCircle3D circle = (SSCircle3D)this.mGeom;
            this.mGeom =
            new SSCircle3D(radius, circle.getPos(), circle.getRot());
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red;
        public void setColor(Color color)
        {
            this.mColor = color;
            this.refreshRenderer();
        }

        //constructor
        public SSAppCircle3D(string name, float radius, Color color) :
        base($"{name}/Circle3D")
        {
            this.mGeom =
            new SSCircle3D(radius, Vector3.zero, Quaternion.identity);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        //methods
        protected override void addComponents()
        {
            this.mGameObject.AddComponent<MeshCollider>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshFilter>();
        }

        protected override void refreshCollider()
        {
            MeshCollider mc = this.mGameObject.GetComponent<MeshCollider>();
            mc.sharedMesh = this.mGameObject.GetComponent<MeshFilter>().mesh;
        }

        protected override void refreshRenderer()
        {
            SSCircle3D circle = (SSCircle3D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = circle.calcMesh(SSAppCircle3D.NUM_SIDE);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }
    }
}