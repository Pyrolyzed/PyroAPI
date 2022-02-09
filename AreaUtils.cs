using System.Collections.Generic;
using System.Linq;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI
{
    public class AreaUtils
    {
        /*
         * Get all creatures in an area from origin with radius radius.
         */
        public static List<Creature> GetCreaturesInRadius(Vector3 origin, float radius, bool allowPlayer = false) => Physics.OverlapSphere(origin, radius).Where(collider => collider.GetComponent<Creature>())
                .Where(collider => collider.GetComponent<Player>() == allowPlayer)
                .Select(collider => collider.GetComponent<Creature>()).ToList();

            /*
             * Get every GameObject in an area from origin with radius radius.
             */
        public static List<GameObject> GetObjectsInRadius(Vector3 origin, float radius, bool allowPlayer = false) =>
            Physics.OverlapSphere(origin, radius)
                .Where(collider => collider.gameObject.GetComponent<Player>() == allowPlayer)
                .Select(collider => collider.gameObject).ToList();

        /*
         * Gets the closest creature to a point
         */
        public static Creature GetClosestCreature(Vector3 origin) => Creature.allActive
            .Where(creature => !creature.isKilled && !creature.isPlayer).OrderBy(creature =>
                Vector3.Distance(origin, creature.ragdoll.GetPart(RagdollPart.Type.Head).transform.position))
            .FirstOrDefault();
        
        /*
         * Not sure why you would want this, but here
         */
        public static Creature GetFurthestCreature(Vector3 origin) => Creature.allActive
            .Where(creature => !creature.isKilled && !creature.isPlayer).OrderByDescending(creature =>
                Vector3.Distance(origin, creature.ragdoll.GetPart(RagdollPart.Type.Head).transform.position))
            .FirstOrDefault();

    }
}