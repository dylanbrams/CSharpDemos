using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWithIDAndColor : MonoBehaviour
{
    public int id;
    private Rigidbody myRigidBody;
    private Material myMaterial;
    private MeshRenderer myMeshRenderer;

    private void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        myMaterial = myMeshRenderer.material;
    }

    public void changeColor(Color colorIn)
    {
        myMaterial.color = colorIn;
    }

    public void makeAJump(float jumpAmount)
    {
        myRigidBody.velocity += Vector3.up * jumpAmount; 
    }
    
}
