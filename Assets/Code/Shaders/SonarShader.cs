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

    public Vector4 Position;
    public float Speed;

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
	void Update () {
        Points = Points.Select((p) => {
            p.Radius += Time.deltaTime * WaveSpeed;
            return p;
        }
        ).ToList();            

        var velocity = Speed * Time.deltaTime * new Vector4(Input.GetAxisRaw("Horizontal"), 0,  Input.GetAxisRaw("Vertical"), 0);
        Position += velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Points.Add(new SonarPoint()
            {
                Position = Position,
                Radius = 0.2f,
            });            
        }

        Points.RemoveAll((p) => p.Radius > MaxRadius);

        material.SetInt("_Points_Length", Points.Count);
        if(Points.Count > 0)
        {
            material.SetVectorArray("_Points", Points.Select((p) => p.Position).ToList());
            material.SetFloatArray("_Radius", Points.Select((p) => p.Radius).ToList());
        }        
    }
}
