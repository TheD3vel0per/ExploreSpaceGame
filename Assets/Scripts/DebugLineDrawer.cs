using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class DebugLineDrawer : MonoBehaviour {
    private Material defaultMat;
    private int lastFrame;
    private Material material;

    [SerializeField] [HideInInspector] private List<Path> paths;

    public Shader shader;
    public Vector3 testParams;
    public float thickness = 1;

    private void Init() {
        if (paths == null || Time.frameCount != lastFrame) {
            lastFrame = Time.frameCount;
            paths = new List<Path>();
        }
    }

    public void DrawPath(Vector3[] points, Color colour) {
        Init();
        var polyLine = new Path {points = points, colour = colour};
        paths.Add(polyLine);
    }

    private void DrawDefault(RenderTexture src, RenderTexture dest) {
        if (defaultMat == null) defaultMat = new Material(Shader.Find("Unlit/Texture"));
        Graphics.Blit(src, dest, defaultMat);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if (paths == null || paths.Count == 0) {
            DrawDefault(src, dest);
            return;
        }

        if (material == null || material.shader != shader) material = new Material(shader);
        var cam = Camera.current;

        var tempTextures = new List<RenderTexture>();

        for (var pathIndex = 0; pathIndex < paths.Count; pathIndex++) {
            var path = paths[pathIndex];
            var points2D = new Vector2[path.points.Length];

            for (var i = 0; i < points2D.Length; i++) points2D[i] = cam.WorldToViewportPoint(path.points[i]);

            var buffer = new ComputeBuffer(points2D.Length, sizeof(float) * 2);
            buffer.SetData(points2D);
            material.SetBuffer("points", buffer);
            material.SetInt("numPoints", points2D.Length);
            material.SetFloat("thickness", thickness / 1000f);
            material.SetVector("params", testParams);
            material.SetColor("colour", path.colour);

            var isFinalPass = pathIndex == paths.Count - 1;

            var currentDest = dest;
            if (!isFinalPass) {
                currentDest = RenderTexture.GetTemporary(src.width, src.height);
                currentDest.name = "Temp texture " + pathIndex;
                tempTextures.Add(currentDest);
            }

            Graphics.Blit(src, currentDest, material);
            src = currentDest;
            buffer.Release();
        }

        foreach (var temp in tempTextures) temp.Release();
    }

    [Serializable]
    public class Path {
        public Color colour;
        public Vector3[] points;
    }
}