using UnityEngine;
using System.Collections;


public class Wheel : MonoBehaviour
{

    [SerializeField]public bool isDrive = false;
    [SerializeField] ParticleSystem particle;
   
    private bool inited = false;
    public bool HasContact = false;

    public Transform visualWheel;
   // private float ForwardSlip;

    public WheelCollider collider;

    public void Awake()
    {
        collider = GetComponent<WheelCollider>();
        collider.steerAngle = 90;
    }

    public void Start()
    {
        //base.Start();
        
    }

    public void Update()
    {
       // base.Update();
        updateParticles();
    }

    
    private void updateParticles()
    {
        if (!particle)
            return;
        
        if (!HasContact)
        {
            if (!particle.isStopped)
                particle.Stop();
            return;
        } 
        else if (particle.isStopped)
        {
            particle.Play();
        }
        

        /*if (ForwardSlip >= 0)
        {
          //  particle.transform.localRotation = Quaternion.Euler(-45f, -180f, 0f);
            float coef = Mathf.Min(Mathf.Abs(ForwardSlip), 2);
            particle.emissionRate = 20*(coef / * + forceMagnitude* /);
            particle.startSpeed = 5*coef;
        }
        else
        {
           // particle.transform.localRotation = Quaternion.Euler(-45f, 360f, 0f);
            float coef = Mathf.Min(Mathf.Abs(ForwardSlip), 2);
            particle.emissionRate = 20*(coef / * + forceMagnitude* /);
            particle.startSpeed = 5*coef;
        }*/
    }

    public void FixedUpdate()
    {

       

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }


}
