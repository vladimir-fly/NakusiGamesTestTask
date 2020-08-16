using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NGTT.Helpers
{
    public static class BarrierGenerator
    {
        public static Transform Generate(Bounds areaBounds)
        {
            var barriersRoot = new GameObject("Barriers");
            var barrierNumber = Random.Range(Defaults.MinBarrierNumber, Defaults.MaxBarrierNumber);

            for (var i = 0; i < barrierNumber; i++)
            {
                var barrier = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var pos = 
                    new Vector3(
                        Random.Range(areaBounds.min.x, areaBounds.max.x), 
                        1, Random.Range(areaBounds.min.z, areaBounds.max.z));
                
                barrier.transform.position = pos;
                barrier.transform.localScale = new Vector3(Random.Range(2,5), Random.Range(2, 5), 1);
                barrier.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180),0);
                barrier.GetComponent<MeshRenderer>().material.color = Color.green;
                
                barrier.transform.parent = barriersRoot.transform;
                barrier.name = $"Barrier{i}";
            }

            return barriersRoot.transform;
        }
    }
}