using UnityEngine;
using System.Collections;

[AddComponentMenu("Kinect/Kinect Clamp")]

public class BoneRotateLimit : MonoBehaviour
{

    [System.Serializable]
    public class BoneClamp
    { 
        public Transform bone;
        public float minX = 0;
        public float maxX = 360;
        public float minY = 0;
        public float maxY = 360;
        public float minZ = 0;
        public float maxZ = 360;
    }

    public BoneClamp[] boneClamps;
    private Vector3 newV3 = new Vector3(0f, 0f, 0f);
    private Rigidbody2D boneBody;
    private bool needFreeze;

    void Start()
    {
        boneBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (needFreeze)
            return;
        foreach (BoneClamp clamp in boneClamps)
        {
            clamp.minX = Mathf.Clamp(clamp.minX, 0, 360);
            clamp.minY = Mathf.Clamp(clamp.minY, 0, 360);
            clamp.minZ = Mathf.Clamp(clamp.minZ, 0, 360);
            clamp.maxX = Mathf.Clamp(clamp.maxX, 0, 360);
            clamp.maxY = Mathf.Clamp(clamp.maxY, 0, 360);
            clamp.maxZ = Mathf.Clamp(clamp.maxZ, 0, 360);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            boneBody.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (needFreeze)
            return;
        foreach (BoneClamp clamp in boneClamps)
        {
            float rotationX = clamp.bone.localEulerAngles.x;
            float rotationY = clamp.bone.localEulerAngles.y;
            float rotationZ = clamp.bone.localEulerAngles.z;

            rotationX = Mathf.Clamp(rotationX, clamp.minX, clamp.maxX);
            rotationY = Mathf.Clamp(rotationY, clamp.minY, clamp.maxY);
            rotationZ = Mathf.Clamp(rotationZ, clamp.minZ, clamp.maxZ);

            newV3.x = rotationX;
            newV3.y = rotationY;
            newV3.z = rotationZ;

            clamp.bone.localEulerAngles = newV3;
        }

        if (boneBody.velocity.magnitude > 1.5f)
            boneBody.velocity = Vector2.zero;

        if (transform.localPosition.x < -0.196f || transform.localPosition.y != -0.197f)
            transform.localPosition = new Vector3(-0.196f, 0.197f,0);

    }

}