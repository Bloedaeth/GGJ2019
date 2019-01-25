using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGrowth : MonoBehaviour
{
    //Scale is what is applied to the transform
    const float minScale = 0.1f; //flaceholder
    const float maxScale = 10; //placeholder
    float scale;
    int tier;
    readonly float[] tierThresholds = { 0, 1000, 3000, 10000 }; //TODO placeholder values
    readonly float[] armor = { 0, 0, 15, 50 }; //Flat reduction in damage taken for each tier //TODO placeholder values
    readonly float[] biteDamage = { 5, 50, 500, 5000 };
    Health health;
    Rigidbody body;
    float baseMass;

    //Growth is a meter from 0 to 10000 that increases as you c o n s u m e
    const float maxGrowth = 10000f;

    float growth;
    public float Growth {
        get => 
            growth;
        set
        {
            growth = value;
            UpdateScale();
        }

    }

	void Start()
	{
		health = GetComponent<Health>();
		body = GetComponent<Rigidbody>();
		baseMass = body.mass;
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
        body.mass = ((baseMass * maxScale - baseMass*minScale) * scale) + baseMass;
        UpdateTier();
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
    }
}
