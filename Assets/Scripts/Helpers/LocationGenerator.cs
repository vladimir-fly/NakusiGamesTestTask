using UnityEngine;

namespace NGTT.Helpers
{
    public static class LocationGenerator
    {
        public static GameObject Generate(Vector3 planeScale = default)
        {
            var groundPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            var localScale = planeScale != default ? planeScale : Defaults.LocationPlaneScale;
            groundPlane.transform.localScale = localScale;
            var rb = groundPlane.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            var renderer = groundPlane.GetComponent<MeshRenderer>();
            var bounds = renderer.bounds;
            
            var barriersTransform = BarrierGenerator.Generate(bounds);
            barriersTransform.parent = groundPlane.transform;
            
            // creating walls
            var wallsTransform = new GameObject("Walls").transform;
            wallsTransform.parent = groundPlane.transform;
            wallsTransform.localScale = Vector3.one;
            
            var frontWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
            frontWall.transform.parent = wallsTransform;
            frontWall.name = nameof(frontWall);
            frontWall.transform.position = new Vector3(0, 5, -bounds.extents.z);
            frontWall.transform.rotation = Quaternion.Euler(90, 0, 0);
            frontWall.transform.localScale = Vector3.one * localScale.y;
            frontWall.AddComponent<Rigidbody>().isKinematic = true;

            var backWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
            backWall.transform.parent = wallsTransform;
            backWall.name = nameof(backWall);
            backWall.transform.position = new Vector3(0, 5, bounds.extents.z);
            backWall.transform.rotation = Quaternion.Euler(-90, 0, 0);
            backWall.transform.localScale = Vector3.one * localScale.y;
            backWall.AddComponent<Rigidbody>().isKinematic = true;

            var leftWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
            leftWall.transform.parent = wallsTransform;
            leftWall.name = nameof(leftWall);
            leftWall.transform.position = new Vector3(-bounds.extents.x, 5, 0);
            leftWall.transform.rotation = Quaternion.Euler(-90, -90, 0);
            leftWall.transform.localScale = Vector3.one * localScale.y;
            leftWall.AddComponent<Rigidbody>().isKinematic = true;
            
            var rightWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
            rightWall.transform.parent = wallsTransform;
            rightWall.name = nameof(rightWall);
            rightWall.transform.position = new Vector3(bounds.extents.x, 5, 0);
            rightWall.transform.rotation = Quaternion.Euler(-90, 90, 0);
            rightWall.transform.localScale = Vector3.one * localScale.y;
            rightWall.AddComponent<Rigidbody>().isKinematic = true;
            
            return groundPlane;
        }
    }
}