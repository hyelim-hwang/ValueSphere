using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Geom;

namespace SS.AppObject
{
    public class SSAppCircle2D : SSAppGeom2D
    {
        //constants
        public static readonly int NUM_SIDE = 64;

        //fields
        public void setRadius(float radius)
        {
            SSCircle2D circle = (SSCircle2D)this.mGeom;
            this.mGeom =
            new SSCircle2D(radius, circle.getPos(), circle.getRot());
            this.refreshAtGeomChange();
        }
        public float getRadius() {
            SSCircle2D circle = (SSCircle2D)this.mGeom;
            return circle.getRadius();
        }
        private Color mColor = Color.red;
        public void setColor(Color color)
        {
            this.mColor = color;
            this.refreshRenderer();
        }

        //constructor
        public SSAppCircle2D(string name, float radius, Color color) :
        base($"{name}/Circle2D")
        {
            this.mGeom =
            new SSCircle2D(radius, Vector2.zero, Quaternion.identity);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        //methods
        protected override void addComponents()
        {
            this.mGameObject.AddComponent<CircleCollider2D>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshFilter>();
        }

        protected override void refreshCollider()
        {
            SSCircle2D circle = (SSCircle2D)this.mGeom;
            CircleCollider2D cc =
            this.mGameObject.GetComponent<CircleCollider2D>();
            cc.radius = circle.getRadius();
        }

        protected override void refreshRenderer()
        {
            SSCircle2D circle = (SSCircle2D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = circle.calcMesh(SSAppCircle3D.NUM_SIDE);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }
    }
}