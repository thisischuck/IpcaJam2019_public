using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatParticleSystem : MonoBehaviour
{
    public ParticleSystem splatSystem;
    public GameObject splatPrefab;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(splatSystem, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            Vector2 pos = collisionEvents[i].intersection;
            GameObject splat = Instantiate(splatPrefab, new Vector2(pos.x, pos.y - Random.Range(0.0f, 0.5f)), Quaternion.identity);
            if (splatHolder != null)
                splat.transform.SetParent(splatHolder);

            Splat splatScript = splat.GetComponent<Splat>();
            splatScript.CreateSplat(Splat.SplatLocation.Foreground);
        }
    }
}
