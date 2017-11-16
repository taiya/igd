using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class CustomLBSkin : MonoBehaviour {

	public Transform[] bones;
	Matrix4x4[] inverseBindPoses;

	MeshFilter meshFilter;

	Vector3[] baseVerts;
	Vector3[] baseNormals;

	void Start () {
		
		meshFilter = GetComponent<MeshFilter> ();
		meshFilter.mesh.MarkDynamic ();

		baseVerts = meshFilter.mesh.vertices;
		baseNormals = meshFilter.mesh.normals;

		inverseBindPoses = new Matrix4x4[bones.Length];
		for (var i = 0;i < bones.Length;i++) {

			// TODO: -------------------------
			// Calculate the matrix used to express a vertex position in a bone's local space
			
			inverseBindPoses [i] = Matrix4x4.identity; // <--- Change this

			// -------------------------------

		}

	}

	void Update () {

		var mesh = meshFilter.mesh;

		var newVerts = new Vector3[baseVerts.Length];
		var newNormals = new Vector3[baseVerts.Length];

		var weights = mesh.boneWeights;

		var boneMatrices = new Matrix4x4[bones.Length];
		for (int i = 0; i < bones.Length; i++) {

			// TODO: -------------------------
			// Calculate the matrix used to apply a bone's transformation to a vertex

			boneMatrices [i] = Matrix4x4.identity; // <--- Change this

			// -------------------------------

		}

		for (int i = 0;i < mesh.vertexCount;i++) {

			// TODO: -------------------------
			// Calculate the displaced position of each vertex
			
			newVerts [i] = baseVerts [i]; // <--- Change this

			newNormals [i] = baseNormals [i]; // <--- Change this

			// -------------------------------

			newNormals [i].Normalize ();

		}

		mesh.vertices = newVerts;
		mesh.normals = newNormals;
		mesh.UploadMeshData (false);
		
	}
}
