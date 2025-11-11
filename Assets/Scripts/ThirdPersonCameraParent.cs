using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonCameraParent : MonoBehaviour
{
    public Vector2 sensitivityVector = Vector2.one;

    public Transform playerTransform;

    public Vector2 rotationVector = Vector2.zero;

    public Material transparentMaterial;

    public Camera playerCamera;
    public Transform nearCameraLimit;
    public Transform farCameraLimit;
    public float zoomInterpolateCoeefficient = 0.1f;

    private Material[] previousMaterials;
    private float zoomRatio = 0.0f;



    private List<RaycastInfo> RaycastInfoArray = new List<RaycastInfo>();

    private float raycastFrameCounter = 0.0f;

    // Start is called before the first frame update

    private void Start()
    {
        ZoomCamera(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        //print("Frame rate is " + 1.0f / Time.deltaTime);
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityVector.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityVector.y;

        Vector2 mouseVector = new Vector2(mouseX, mouseY);

        rotationVector.y += mouseVector.x;

        rotationVector.x -= mouseVector.y;

        rotationVector.x = Mathf.Clamp(rotationVector.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);
        transform.position = playerTransform.position;
        //playerTransform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);//Quaternion.Euler(rot, rotationVector.y, 0);

        if(raycastFrameCounter > Time.deltaTime * 10.0f)
        {
            raycastFrameCounter = 0.0f;
            RaycastAndSetTransparent();
        }
        

        if(Input.mouseScrollDelta != Vector2.zero)
        {
            ZoomCamera(Input.mouseScrollDelta);
        }
        raycastFrameCounter += Time.deltaTime;
    }
    public void ZoomCamera(Vector2 new_vector)
    {
        zoomRatio += new_vector.y * zoomInterpolateCoeefficient;
        zoomRatio = Mathf.Clamp(zoomRatio, 0.0f, 1.0f);
        playerCamera.transform.position = Vector3.Lerp(farCameraLimit.transform.position, nearCameraLimit.transform.position, zoomRatio);
        //print("PlayerScrolled: " + new_vector);
    }

    public void RaycastAndSetTransparent()
    {

        RaycastHit[] all_hits;
        
        all_hits = Physics.RaycastAll(transform.position, (playerCamera.transform.position - transform.position).normalized, Vector3.Distance(transform.position, playerCamera.transform.position));

        List<GameObject> objectsFromRaycastHit = new List<GameObject>();

        //Array.Clear(RaycastInfoArray, 0, RaycastInfoArray.Length);

        
        for (int i = 0; i < all_hits.Length; i++)
        {
                //print("Raycasting");
                //previousMaterial = ray_cast_struct.collider.GetComponent<Material>();
            if (all_hits[i].collider.GetComponent<MeshRenderer>())
            {
                objectsFromRaycastHit.Add(all_hits[i].collider.gameObject);

                RaycastInfo stored_info = new RaycastInfo(all_hits[i].collider.gameObject);
                if (!isAlreadyInRaycastInfoList(stored_info))
                {


                    //if (!isAlreadyInRaycastInfoList(stored_info))
                    //{
                    //print("appending" + stored_info.thisGameObject + " raycastlist");
                    RaycastInfoArray.Add(stored_info);
                    //print(RaycastInfoArray);
                    stored_info.thisGameObject.GetComponent<MeshRenderer>().material = transparentMaterial;

                    //}

                }
            }
                //if(!isAlreadyInRaycastInfoList(i.collider.gameObject))
            
                
        }

        
        
        if (objectsFromRaycastHit.Count != RaycastInfoArray.Count)
        {
            /*
            foreach (RaycastInfo i in RaycastInfoArray)
            {

                if (!objectsFromRaycastHit.Contains<GameObject>(i.thisGameObject))
                {
                    //print("Setting material back for " + i.thisGameObject);
                    i.thisMeshRenderer.material = i.thisMaterial;
                }

            }
            */
            //print("Clearing array");

            foreach (RaycastInfo i in RaycastInfoArray)
            {
                i.thisMeshRenderer.material = i.thisMaterial;
            }

            RaycastInfoArray = new List<RaycastInfo>();
            
                
        }
        /*
        if(all_hits.Length != RaycastInfoArray.Length)
        {
            foreach (RaycastInfo i in RaycastInfoArray)
            {
                i.thisMeshRenderer.material = i.thisMaterial;
            }
            
        }
        */
        //Array.Clear(RaycastInfoArray, 0, RaycastInfoArray.Length);

    }

    private bool isAlreadyInRaycastInfoList(RaycastInfo object_to_check)
    {



        for (int i = 0; i < RaycastInfoArray.Count; i++)
        {
            //print("Looping");
            if (object_to_check.thisGameObject.Equals(RaycastInfoArray[i].thisGameObject))
            {
                //print("Same object found in raycastinfolist");
                return true;
            }
        }
        //print(object_to_check.thisGameObject + " is not in the raycastinfolist");
        return false;
    }
}

public struct RaycastInfo
{
    public RaycastInfo(GameObject gameObject)
    {
        thisGameObject = gameObject;
        thisMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        thisMaterial = thisMeshRenderer.material;
    }

    public MeshRenderer thisMeshRenderer { get; }
    public Material thisMaterial{ get; }
    public GameObject thisGameObject { get; }

}
