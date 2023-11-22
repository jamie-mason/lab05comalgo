using UnityEngine;

namespace DitzelGames.FastIK
{
    class SampleProcedualAnimation :  MonoBehaviour
    {
        public Transform LookTarget;
        public Transform LHandTarget;
        public Transform RHandTarget;
        public Transform HandPole;
        public Transform Attraction;

        public void LateUpdate()
        {
           
            

           

            //hand and look
            var normDist = Mathf.Clamp((Vector3.Distance(LookTarget.position, Attraction.position) - 0.3f) / 1f, 0, 1);
            LHandTarget.rotation = Quaternion.Lerp(Quaternion.Euler(90, 0, 0), LHandTarget.rotation, normDist);
            LHandTarget.position = Vector3.Lerp(Attraction.position, LHandTarget.position, normDist);
            HandPole.position = Vector3.Lerp(LHandTarget.position + Vector3.down * 2, LHandTarget.position + Vector3.forward * 2f, normDist);
            LookTarget.position = Vector3.Lerp(Attraction.position, LookTarget.position, normDist);


        }

    }
}
