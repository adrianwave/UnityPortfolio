using UnityEngine;
using System.Collections.Generic;

public enum textureType {
	air, grass, rock
}

public class Chunk : MonoBehaviour {
	// This first list contains every vertex of the mesh that we are going to render
	private List<Vector3> newVertices = new List<Vector3>();
	// The triangles tell Unity how to build each section of the mesh joining the vertices
   	private List<int> newTriangles = new List<int>();
	// The UV list is unimportant right now but it tells Unity how the texture is aligned on each polygon
	private List<Vector2> newUV = new List<Vector2>();
	// A mesh is made up of the vertices, triangles and UVs we are going to define, after we make them up we'll save them as this mesh
	private Mesh mesh;
	private MeshCollider chunkCollider;
	private float textureWidth = 0.083f;
	private int faceCount;
	private int chunkSize; 
	private int chunkX;
	private int chunkY;
	private int chunkZ;
	private World world;
	private GameObject worldGO;
	private bool isUpdate = false;

	//Textures
	private Vector2 tGrassTop = new Vector2(1,11);
	private Vector2 tGrassSide = new Vector2(0,10);
	private Vector2 tRock = new Vector2(7,7);

	public int ChunkSize {
		get {
			return chunkSize;
		}
		set {
			chunkSize = value;
		}
	}

	public int ChunkX {
		get {
			return chunkX;
		}
		set {
			chunkX = value;
		}
	}

	public int ChunkY {
		get {
			return chunkY;
		}
		set {
			chunkY = value;
		}
	}

	public int ChunkZ {
		get {
			return chunkZ;
		}
		set {
			chunkZ = value;
		}
	}

	public GameObject WorldGO {
		get {
			return worldGO;
		} 
		set {
			worldGO = value;
		}
	}

	public bool IsUpdate {
		get {
			return isUpdate;
		}
		set {
			isUpdate = value;
		}
	}
	
	// Use this for initialization
	void Start () {
		world = WorldGO.GetComponent("World") as World;
		mesh = GetComponent<MeshFilter> ().mesh;
		chunkCollider = GetComponent<MeshCollider>();
		GenerateMesh();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(IsUpdate){
			GenerateMesh();
			IsUpdate=false;
 		 }
	}

	public void GenerateMesh(){
		for (int x = 0; x < chunkSize; x++){
			for (int y = 0; y < chunkSize; y++){
				for (int z = 0; z < chunkSize; z++){
					//This code will run for every block in the chunk
					if(Block(x, y, z) != textureType.air.GetHashCode()){
						//If the block is solid
						if(Block(x, y + 1, z) == textureType.air.GetHashCode()){
							//Block above is air
							CubeTop(x, y, z,Block(x, y, z));
						}
						if(Block(x, y - 1, z) == textureType.air.GetHashCode()){
							//Block below is air
							CubeBot(x, y, z, Block(x, y, z));	
						}
						if(Block(x + 1, y, z) == textureType.air.GetHashCode()){
							//Block east is air
							CubeEast(x, y, z, Block(x,y,z));	
						}
						if(Block(x - 1, y, z) == textureType.air.GetHashCode()){
							//Block west is air
							CubeWest(x, y, z, Block(x, y, z));	
						}
						if(Block(x, y, z + 1) == textureType.air.GetHashCode()){
							//Block north is air
							CubeNorth(x, y, z, Block(x, y, z));	
						}
						if(Block(x, y, z - 1) == textureType.air.GetHashCode()){
							//Block south is air
							CubeSouth(x, y, z, Block(x, y, z));
						}
					}
				}
			}
		}
		UpdateMesh ();
 	}

	 void UpdateMesh() {
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.triangles = newTriangles.ToArray();
		;
		mesh.RecalculateNormals ();
		
		chunkCollider.sharedMesh = null;
		chunkCollider.sharedMesh = mesh;
		
		newVertices.Clear();
		newUV.Clear();
		newTriangles.Clear();
		
		faceCount=0;
	}

	void CubeTop(int x, int y, int z, byte block) {
		newVertices.Add(new Vector3 (x,  y,  z + 1));
		newVertices.Add(new Vector3 (x + 1, y,  z + 1));
		newVertices.Add(new Vector3 (x + 1, y,  z ));
		newVertices.Add(new Vector3 (x,  y,  z ));
		
		Vector2 texturePos = new Vector2(0,0);
   
		if(Block(x,y,z) == textureType.rock.GetHashCode()){
			texturePos = tRock;
		} else if(Block(x,y,z) == textureType.grass.GetHashCode()){
			texturePos= tGrassTop;
		}
		Cube(texturePos);
	}

	void CubeNorth(int x, int y, int z, byte block) {
		newVertices.Add(new Vector3 (x + 1, y-1, z + 1));
		newVertices.Add(new Vector3 (x + 1, y, z + 1));
		newVertices.Add(new Vector3 (x, y, z + 1));
		newVertices.Add(new Vector3 (x, y-1, z + 1));
		
		Vector2 texturePos = setSideTextures(x, y, z);
		Cube(texturePos);
	}

	void CubeEast(int x, int y, int z, byte block) {
		newVertices.Add(new Vector3 (x + 1, y - 1, z));
		newVertices.Add(new Vector3 (x + 1, y, z));
		newVertices.Add(new Vector3 (x + 1, y, z + 1));
		newVertices.Add(new Vector3 (x + 1, y - 1, z + 1));
		
		Vector2 texturePos;
		
		texturePos = setSideTextures(x, y, z);
		Cube(texturePos);
	}

	void CubeSouth(int x, int y, int z, byte block) {
		newVertices.Add(new Vector3 (x, y - 1, z));
		newVertices.Add(new Vector3 (x, y, z));
		newVertices.Add(new Vector3 (x + 1, y, z));
		newVertices.Add(new Vector3 (x + 1, y - 1, z));
		
		Vector2 texturePos = setSideTextures(x, y, z);
		Cube(texturePos);
	}

	void CubeWest (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3 (x, y- 1, z + 1));
		newVertices.Add(new Vector3 (x, y, z + 1));
		newVertices.Add(new Vector3 (x, y, z));
		newVertices.Add(new Vector3 (x, y - 1, z));
		
		Vector2 texturePos = setSideTextures(x, y, z);
		Cube(texturePos);
	}

	void CubeBot(int x, int y, int z, byte block) {
		newVertices.Add(new Vector3 (x,  y-1,  z ));
		newVertices.Add(new Vector3 (x + 1, y-1,  z ));
		newVertices.Add(new Vector3 (x + 1, y-1,  z + 1));
		newVertices.Add(new Vector3 (x,  y-1,  z + 1));
		
		Vector2 texturePos = setSideTextures(x, y, z);
		Cube(texturePos);
	}

	public Vector2 setSideTextures(int x, int y, int z) {
		Vector2 texturePos = new Vector2(0, 0);
		if(Block(x,y,z) == textureType.rock.GetHashCode()){
			texturePos = tRock;
		} else if(Block(x,y,z)== textureType.grass.GetHashCode()){
			texturePos= tGrassSide;
		}
		return texturePos;
	}

	void Cube (Vector2 texturePos) {
		newTriangles.Add(faceCount * 4  ); //1
		newTriangles.Add(faceCount * 4 + 1 ); //2
		newTriangles.Add(faceCount * 4 + 2 ); //3
		newTriangles.Add(faceCount * 4  ); //1
		newTriangles.Add(faceCount * 4 + 2 ); //3
		newTriangles.Add(faceCount * 4 + 3 ); //4
		
		newUV.Add(new Vector2 (textureWidth * texturePos.x + textureWidth, textureWidth * texturePos.y));
		newUV.Add(new Vector2 (textureWidth * texturePos.x + textureWidth, textureWidth * texturePos.y + textureWidth));
		newUV.Add(new Vector2 (textureWidth * texturePos.x, textureWidth * texturePos.y + textureWidth));
		newUV.Add(new Vector2 (textureWidth * texturePos.x, textureWidth * texturePos.y));
		
		faceCount++; // Add this line
	}

	byte Block(int x, int y, int z){
 		return world.Block( x + ChunkX, y + ChunkY, z + ChunkZ);
	}	
}
