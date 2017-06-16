using UnityEngine;
using System.Collections;


public class Wheel : MonoBehaviour
{

    [SerializeField]public bool isDrive = false;
    [SerializeField] ParticleSystem particle;
   
    private bool inited = false;
    

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
        collider.steerAngle = 90;
    }

    void OnEnable()
    {
        collider.steerAngle = 90;
    }

    public void Update()
    {
        // base.Update();
       // collider.steerAngle = 90;
        updateParticles();
    }

    
    private void updateParticles()
    {
        if (!particle)
            return;
        
        if (!collider.isGrounded)
        {
            if (!particle.isStopped)
                particle.Stop();
            return;
        } 
        else if (particle.isStopped)
        {
            particle.Play();
        }
        WheelHit hit;
        collider.GetGroundHit(out hit);
        ParticleSystem.EmissionModule emission = particle.emission;
        ParticleSystem.MainModule main = particle.main;
        float coef = hit.forwardSlip < 0 ? Mathf.Abs(hit.forwardSlip) : 0;
       /* if (hit.forwardSlip >= 0)
        {*/
          //  particle.transform.localRotation = Quaternion.Euler(-45f, -180f, 0f);
        float coefrot = (collider.rpm / 100);
        coefrot = coefrot > 7 ? 7 : coefrot;
            emission.rateOverTime = 20 * (collider.radius+ collider.rpm/70) * hit.forwardSlip;
            main.startSpeed =  new ParticleSystem.MinMaxCurve(2, coefrot); ;
       // }
       /* else
        {
           // particle.transform.localRotation = Quaternion.Euler(-45f, 360f, 0f);
           // float coef = Mathf.Min(Mathf.Abs(hit.forwardSlip), 2);
            emission.rateOverTime = 30*(coef  + collider.radius ) * hit.forwardSlip;
            main.startSpeed = 5*coef;
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
