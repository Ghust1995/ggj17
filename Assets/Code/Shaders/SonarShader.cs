using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SonarShader : MonoBehaviour {

    [Serializable]
    public class SonarPoint
    {
        public Vector4 Position;
        public float Radius;
    }

    public List<SonarPoint> Points;

    public Material material;

    public float WaveSpeed;

    public float MaxRadius;

    private void Awake()
    {
        material.SetVectorArray("_Points", new Vector4[100]);
        material.SetFloatArray("_Radius", new float[100]);
    }

    // Use this for initialization
    void Start () {
        if(Points.Count > 0)
        {
            material.SetInt("_Points_Length", Points.Count);
            material.SetVectorArray("_Points", Points.Select((p) => p.Position).ToList());
            material.SetFloatArray("_Radius", Points.Select((p) => p.Radius).ToList());
        }        
    }

    // Update is called once per frame
    void Update()
    {
        Points = Points.Select((p) =>
        {
            if (p.Radius == 0) return p;
            p.Radius += Time.deltaTime * WaveSpeed;
            return p;
        }
        ).ToList();

        Points = Points.Select((p) =>
        {
            p.Radius = p.Radius > MaxRadius ? 0 : p.Radius;
            return p;
        }).ToList();

        material.SetInt("_Points_Length", Points.Count);
        if (Points.Count > 0)
        {
            material.SetVectorArray("_Points", Points.Select((p) => p.Position).ToList());
            material.SetFloatArray("_Radius", Points.Select((p) => p.Radius).ToList());
        }
    }

    public void SpawnSonar(Vector3 position)
    {
        if (Points.Count > 99)
        {
            Points.RemoveAt(0);
        }
        Points.Add(new SonarPoint()
        {
            Position = position,
            Radius = 0.2f,
        });
    }
}
