using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGrowth : MonoBehaviour
{
    //Scale is what is applied to the transform
    Vector3 minScale;
    Vector3 maxScale; 
    Vector3 scale;
    int tier;
    readonly float[] tierThresholds = { 0, 1500, 3000, 10000 }; //TODO placeholder values
    readonly float[] armor = { 0, 0, 15, 50 }; //Flat reduction in damage taken for each tier //TODO placeholder values
    readonly float[] biteDamage = { 50, 100, 200, 400 };
    Health health;
    Rigidbody body;
    Bite bite;
    float baseMass;

    //Growth is a meter from 0 to 10000 that increases as you c o n s u m e
    const float maxGrowth = 10000f;

    [SerializeField] float growth;
    public float Growth {
        get => 
            growth;
        set
        {
            growth = value;
            UpdateScale();
        }

    }



	//Returns growth level as a percentage of the max
	float GrowthPercentage()
    {
        return growth / maxGrowth;
    }

    //Applies growth to actual transform.scale
    void UpdateScale()
    {
        scale = (maxScale - minScale) * GrowthPercentage() + minScale;
        body.mass = ((baseMass * maxScale.y - baseMass*minScale.y) * scale.y) + baseMass;
        transform.localScale = scale;
        UpdateTier(); //TODO do this when nesting
    }

    public void Grow(float amount)
    {
        Growth += amount;
    }

    void UpdateTier()
    {
        foreach (float threshold in tierThresholds)
        {
            int i = 0;
            if (growth > threshold)
            {
                tier = i;                
            }
            i++;            
        }
        health.armor = armor[tier];
        bite.biteDamage = biteDamage[tier];
        //FindObjectOfType<CameraScaler>().StartScaleCam(); //Commented out for debugging other shit
    }
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody>();
        bite = GetComponent<Bite>();
        baseMass = body.mass;
        minScale = transform.localScale;
        maxScale = new Vector3(minScale.x * 10, minScale.y * 10, minScale.z * 10);
        UpdateScale();
    }
}
