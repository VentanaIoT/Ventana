using UnityEngine;
using System.Collections;

public class BuildMesh : MonoBehaviour {

    public Vector3 VertLeftTopFront = new Vector3(-1, 1, 1);
    public Vector3 VertRightTopFront = new Vector3(1, 1, 1);
    public Vector3 VertLeftBottomFront = new Vector3(-1, -1, 1);
    public Vector3 VertRightBottomFront = new Vector3(1, -1, 1);
    public Vector3 VertRightTopBack = new Vector3(1, 1, -1);
    public Vector3 VertRightBottomBack = new Vector3(1, -1, -1);
    public Vector3 VertLeftTopBack = new Vector3(-1, 1, -1);
    public Vector3 VertLeftBottomBack = new Vector3(-1, -1, -1);


    // Use this for initialization
    void Start() {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        //Vertices//
        Vector3[] vertices = new Vector3[]
        {
            //front face//
            VertLeftTopFront, //left top front , 0
            VertRightTopFront, //right top front , 1 
            VertLeftBottomFront, //left bottom front, 2  
            VertRightBottomFront, //right bottom front, 3

            //back face//
            VertRightTopBack, //right top back, 4
            VertLeftTopBack, // left top back, 5
            VertRightBottomBack, //right bottom back, 6
            VertLeftBottomBack, //left bttom back, 7

            //left face//
            VertLeftTopBack,   //left top back, 8
            VertLeftTopFront,  //left top front, 9
            VertLeftBottomBack,  //left bottom back, 10
            VertLeftBottomFront, //left bttom front, 11

            //right face//
            VertRightTopFront,  //right top front 12
            VertRightTopBack,   //right top back 13
            VertRightBottomFront,   //right bottom front 14
            VertRightBottomBack,    //right bottom back. 15

            //top face//
            VertLeftTopBack,  //left top back 16
            VertRightTopBack,   //right top back 17
            VertLeftTopFront,   //left top front 18
            VertRightTopFront,    //right top front. 19

            //bottom face//
            VertLeftBottomFront, //left bottom front, 20
            VertRightBottomFront,  //right bottom front, 21
            VertLeftBottomBack,  //left botton back, 22
            VertRightBottomBack   //right bottom back,23



        };

        //Triangles// 3 points clockwise determines which side is visible.
        int[] triangles = new int[]
        {
            //front face//
            //first triangle//
            0,2,3,
            3,1,0,

            //back face triangle//
            4,6,7,
            7,5,4,

            //left face triangle//
            8,10,11,
            11,9,8,

            //right face triangle//
            12,14,15,
            15,13,12,

            //top face triangle//
            16,18,19,
            19,17,16,

            //bottom face triangle//
            20,22,23,
            23,21,20

        };

        //UV's for textures??//
        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)

        };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        ;
        mesh.RecalculateNormals();
        MeshCollider collider = gameObject.GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
