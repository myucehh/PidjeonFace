using UnityEngine;
using System.Collections;

public class MouseOver : MonoBehaviour {

    public bool vibrate = false;
    bool vibrating = false;
    //ComboManager comboManager;
    public int id;
    public bool playParticleEffect = false;
    public ParticleSystem sparks;

    public bool playSpriteEffect = false;
    public bool spriteFollowObject = false;
    public GameObject sprite;

    bool alternate = true;
    float timer = 0;

    Color originalColour;
    Material thisMaterial;
    public bool fade = false;
    public Color fadeColour;
    Color targetColor;
    public bool hasReflective = false;
    Color targetReflective;
    Color originalReflective;

    public float timeToRun = 1;
    public float fadeRate = 0.02f;
    float fadeTimer = 0;
    public bool setStartColour = false;
    public Color startColour;

    public bool combo = false;
	Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        thisMaterial = this.GetComponent<Renderer>().material;
        originalColour = thisMaterial.color;
        if (hasReflective)
            originalReflective = thisMaterial.GetColor("_ReflectColor");

        if (setStartColour)
        {
            thisMaterial.color = startColour;
            if (hasReflective)
                thisMaterial.SetColor("_ReflectColor", startColour);
        }
       /* if (combo)
            comboManager = GameObject.Find("ComboManager").GetComponent<ComboManager>();
        if (playParticleEffect)
        {
            sparks.emissionRate = 0;
        }*/
    }

    void Update()
    {
        if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            Color newColour = thisMaterial.color;
            if (targetColor.r > newColour.r)
                newColour.r += fadeRate;
            else if (targetColor.r < newColour.r)
                newColour.r -= fadeRate;

            if (targetColor.g > newColour.g)
                newColour.g += fadeRate;
            else if (targetColor.g < newColour.g)
                newColour.g -= fadeRate;

            if (targetColor.b > newColour.b)
                newColour.b += fadeRate;
            else if (targetColor.b < newColour.b)
                newColour.b -= fadeRate;

            thisMaterial.color = newColour;
        
            if (hasReflective)
            {
                Color newReflective = thisMaterial.GetColor("_ReflectColor");
                if (targetReflective.r > newReflective.r)
                    newReflective.r += fadeRate;
                else if (targetReflective.r < newReflective.r)
                    newReflective.r -= fadeRate;

                if (targetReflective.g > newReflective.g)
                    newReflective.g += fadeRate;
                else if (targetReflective.g < newReflective.g)
                    newReflective.g -= fadeRate;

                if (targetReflective.b > newReflective.b)
                    newReflective.b += fadeRate;
                else if (targetReflective.b < newReflective.b)
                    newReflective.b -= fadeRate;
        
                thisMaterial.SetColor("_ReflectColor", newReflective);
            }
        }

        if (vibrating)
        {
            if (alternate)
            {
                alternate = !alternate;
                transform.Rotate(0, 0, 100 * Time.deltaTime);
            }
            else
            {
                alternate = !alternate;
                transform.Rotate(0, 0, -100 * Time.deltaTime);
            }
		}
			else
			{
				if(transform.rotation != originalRotation)
				{
					transform.rotation = originalRotation;
				}
			}
        

        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            vibrating = false;
        }
    }

    void OnMouseEnter()
    {
        PlayEffect();
    }

    public void PlayEffect()
    {
        /*if (combo)
        {
            comboManager.AddCombo(id);
        }*/

        timer = 5;

        if (fade)
        {
            targetColor = fadeColour;
            targetReflective = fadeColour;
            fadeTimer = timeToRun;
        }

        if (vibrate)
        {
            vibrating = true;
        }

        GetComponent<AudioSource>().Play();
        if (playSpriteEffect)
        {
            GameObject newSprite = (GameObject)Instantiate(sprite, gameObject.transform.position, new Quaternion(0, 180, 0, 0));
            if (spriteFollowObject)
            {
                newSprite.transform.SetParent(this.transform);
            }
        }
    }

    public void EndEffect()
    {
        targetColor = originalColour;
        targetReflective = originalReflective;
        fadeTimer = timeToRun;

        vibrating = false;
        if (playParticleEffect)
        {
            sparks.emissionRate = 0;
        }
    }

    void OnMouseExit()
    {
        EndEffect();
    }

    void OnMouseOver()
    {
        if (playParticleEffect)
        {
            sparks.transform.parent = gameObject.transform;
            sparks.transform.position = gameObject.transform.position;
            sparks.emissionRate = 300;
        }
    }
}
