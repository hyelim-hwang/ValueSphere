using System;
using System.Collections.Generic;
using SS.AppObject;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace SS {
    public class SSPerspectiveCube : SSAppNoGeom3D {
        //fields
        private GameObject mCube = null;
        public GameObject getCube() {
            return this.mCube;
        }
        public void setCube(GameObject cube) {
            this.mCube = cube;
        }
        private SSGrid mGrid = null;
        public SSGrid getGrid() {
            return this.mGrid;
        }
        public void setGrid(SSGrid grid) {
            this.mGrid = grid;
        }
        private GameObject mPOVController = null;
        private GameObject mFOVController = null;
        private SSAppPolyline3D mRotationController = null;


        //constructor
        public SSPerspectiveCube() : base("Perspective Cube") {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.setCube(cube);
            SSGrid grid = new SSGrid();
            grid.setRot(SSGrid.GRID_ROTATION);
            grid.setScale(SSGrid.GRID_SCALE);
            this.setGrid(grid);
            this.mCube.transform.SetParent(this.mGameObject.transform);
            this.addChild(this.mGrid);
            foreach (Transform child in this.getGameObject().transform) {
                child.GameObject().layer = 3;
            }
            Vector3 vertex1 = new Vector3(0.5f, 0.5f, 0.5f);
            Vector3 vertex2 = new Vector3(-0.5f, +0.5f, +0.5f);
            Vector3 vertex3 = new Vector3(-0.5f, +0.5f, -0.5f);
            Vector3 vertex4 = new Vector3(0.5f, 0.5f, -0.5f);
            Vector3 vertex5 = new Vector3(0.5f, -0.5f, 0.5f);
            Vector3 vertex6 = new Vector3(-0.5f, -0.5f, 0.5f);
            Vector3 vertex7 = new Vector3(-0.5f, -0.5f, -0.5f);
            Vector3 vertex8 = new Vector3(0.5f, -0.5f, -0.5f);
            Vector3 xInfinityOf7 = new Vector3(200f, -0.5f, -0.5f);
            Vector3 xInfinityOf6 = new Vector3(200f, -0.5f, 0.5f);
            Vector3 zInfinityOf7 = new Vector3(-0.5f, -0.5f, 200f);
            Vector3 zInfinityOf8 = new Vector3(0.5f, -0.5f,  200f);

            //draw the outlines of the cube.
            drawEdgeByVertices(vertex1, vertex2);
            drawEdgeByVertices(vertex1, vertex4);
            drawEdgeByVertices(vertex1, vertex5);
            drawEdgeByVertices(vertex2, vertex3);
            drawEdgeByVertices(vertex2, vertex6);
            drawEdgeByVertices(vertex3, vertex4);
            // drawEdgeByVertices(vertex3, vertex7);
             this.mRotationController =
                drawControllerEdgeByVertices(vertex3, vertex7, Color.yellow);
            drawEdgeByVertices(vertex4, vertex8);
            drawEdgeByVertices(vertex5, vertex6);
            drawEdgeByVertices(vertex5, vertex8);
            drawEdgeByVertices(vertex6, vertex7);
            drawEdgeByVertices(vertex7, vertex8);

            //draw the vertices of the cube.
            createVertexSphere(vertex1);
            createVertexSphere(vertex2);
            this.mPOVController =
                createControllerSphere(vertex3, Color.red, "POV");
            createVertexSphere(vertex4);
            createVertexSphere(vertex5);
            createVertexSphere(vertex6);
            this.mFOVController =
                createControllerSphere(vertex7, Color.green, "FOV");
            createVertexSphere(vertex8);

            //draw the vanishing line of the cube.
            drawVanishingLine(vertex7, xInfinityOf7);
            drawVanishingLine(vertex7, zInfinityOf7);
            drawVanishingLine(vertex6, xInfinityOf6);
            drawVanishingLine(vertex8, zInfinityOf8);

            //make cube transparent.
            changeRenderMode(
                this.getCube().GetComponent<Renderer>().material,
                BlendMode.Transparent);
            Color color =
                this.getCube().GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            this.getCube().GetComponent<Renderer>().material.color = color;
        }

         //util functions
        public enum BlendMode {
            Opaque = 0,
            Cutout,
            Fade,
            Transparent
        }

        private void drawEdgeByVertices(Vector3 vertex1, Vector3 vertex2) {
            List<Vector3> pts = new List<Vector3>();
            pts.Add(vertex1);
            pts.Add(vertex2);
            SSAppPolyline3D edge = new SSAppPolyline3D("cubeEdge1", pts,
                SSPerspectiveCubeMgr.CUBE_EDGE_WIDTH,
                SSPerspectiveCubeMgr.CUBE_EDGE_COLOR);
            this.addChild(edge);
            edge.getGameObject().layer = 3;
        }

        private SSAppPolyline3D drawControllerEdgeByVertices(Vector3 vertex1,
            Vector3 vertex2, Color color) {
            List<Vector3> pts = new List<Vector3>();
            pts.Add(vertex1);
            pts.Add(vertex2);
            SSAppPolyline3D edge = new SSAppPolyline3D("Rotation Controller", pts,
                SSPerspectiveCubeMgr.CUBE_EDGE_WIDTH, color);
            this.addChild(edge);
            edge.getGameObject().layer = 3;
            return edge;
        }

        private void drawVanishingLine(Vector3 vertex1, Vector3 vertex2) {
            List<Vector3> pts = new List<Vector3>();
            pts.Add(vertex1);
            pts.Add(vertex2);
            SSAppPolyline3D vanishingLine =
                new SSAppPolyline3D("vanishingLine", pts,
                SSPerspectiveCubeMgr.VANISHING_LINE_WIDTH,
                SSPerspectiveCubeMgr.VANISHING_LINE_COLOR);
            this.addChild(vanishingLine);
            vanishingLine.getGameObject().layer = 3;
        }

        private void createVertexSphere(Vector3 pt) {
            GameObject vertexSphere = GameObject.CreatePrimitive(
                PrimitiveType.Sphere);
            vertexSphere.name = "vertexSphere";
            vertexSphere.transform.position = pt;
            vertexSphere.transform.localScale = 0.15f * Vector3.one;
            vertexSphere.GetComponent<MeshRenderer>().material =
                new Material(Shader.Find("UI/Unlit/Transparent"));
            vertexSphere.GetComponent<MeshRenderer>().material.color =
                SSPerspectiveCubeMgr.CUBE_EDGE_COLOR;
            vertexSphere.transform.SetParent(this.mGameObject.transform);
            vertexSphere.layer = 3;
        }
        private GameObject createControllerSphere(Vector3 pt, Color color, string name) {
            GameObject vertexSphere = GameObject.CreatePrimitive(
                PrimitiveType.Sphere);
            vertexSphere.name = "name";
            vertexSphere.transform.position = pt;
            vertexSphere.transform.localScale = 0.15f * Vector3.one;
            vertexSphere.GetComponent<MeshRenderer>().material =
                new Material(Shader.Find("UI/Unlit/Transparent"));
            vertexSphere.GetComponent<MeshRenderer>().material.color = color;
            vertexSphere.transform.SetParent(this.mGameObject.transform);
            vertexSphere.layer = 3;
            return vertexSphere;
        }


        public static void changeRenderMode(Material standardShaderMaterial,
            BlendMode blendMode) {
            switch (blendMode) {
                case BlendMode.Opaque:
                    standardShaderMaterial.SetFloat("_Mode", 0.0f);
                    standardShaderMaterial.SetOverrideTag(
                        "RenderType", "Opaque");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = -1;
                    break;
                case BlendMode.Cutout:
                    standardShaderMaterial.SetFloat("_Mode", 1.0f);
                    standardShaderMaterial.SetOverrideTag("RenderType",
                        "Opaque");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 2450;
                    break;
                case BlendMode.Fade:
                    standardShaderMaterial.SetFloat("_Mode", 2.0f);
                    standardShaderMaterial.SetOverrideTag("RenderType",
                        "Transparent");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.
                        OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
                case BlendMode.Transparent:
                    standardShaderMaterial.SetFloat("_Mode", 3.0f);
                    standardShaderMaterial.SetOverrideTag("RenderType",
                        "Transparent");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.
                        OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.EnableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
            }
        }
    }
}


