using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip sand;
    public AudioClip wood;
    public AudioClip stone;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;
    
    // Get tag of surface
    public void Footstep()
    {
        print("stepping!");
        if(Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range))
        {
            print(hit.collider.tag);
            if (hit.collider.CompareTag("matSand"))
            {
                PlayFootstepSoundL(sand);
            }
            if (hit.collider.CompareTag("matWood"))
            {
                PlayFootstepSoundL(wood);
            }
            if (hit.collider.CompareTag("matStone"))
            {
                PlayFootstepSoundL(stone);
            }
        }
    }
    public void FootstepStop()
    {
        AudioSource.Stop();
    }
    // Play footstep sound
    void PlayFootstepSoundL(AudioClip clip)
    {
        AudioSource.pitch = Random.Range(0.8f, 1f);
        AudioSource.PlayOneShot(clip);
    }
    // Get floor tag
    public string FloorTag()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range))
        {
            return hit.collider.tag;
        }
        else { return null; }
    }
    // Draw debug raycast
    private void Update()
    {
        Debug.DrawRay(RayStart.position, RayStart.transform.up * range * -1, Color.green);
    }
}
