using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2f;
    
    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;
    public float meshDestroyDelay = 3f;
    public Transform positionToSpawn;

    [Header("Shader Related")]
    public Material mat;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

        void Start()
    {
        
    }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine (ActivateTrail(activeTime));
        }

    }
        IEnumerator ActivateTrail (float timeActive) 
    {
        while(timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for(int i=0; i<skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);
 
                mf.mesh = mesh;
                mr.material = mat;

                Destroy (gObj, meshDestroyDelay);

            }
            yield return new WaitForSeconds (meshRefreshRate);
        }
        isTrailActive = false;
    }


}
