using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    public GameObject headField;
    public GameObject upperBodyField;
    public GameObject lowerBodyField;

    // public Sprite player;
    public GameObject player;

    public Sprite[] headSprites;
    public Sprite[] upperBodySprites;
    public Sprite[] lowerBodySprites;

    public RuntimeAnimatorController[] animators;

    private int headIndex = 0;
    private int bodyIndex = 0;
    private int legsIndex = 0;

    void Awake()
    {
        headField.GetComponent<Image>().sprite = headSprites[headIndex];
        upperBodyField.GetComponent<Image>().sprite = upperBodySprites[bodyIndex];
        lowerBodyField.GetComponent<Image>().sprite = lowerBodySprites[legsIndex];
    }

    public void IterateOverHeads(Button button)
    {
        if (button.name == "HeadLeft")
        {
            if (headIndex == 0)
            {
                headIndex = headSprites.Length - 1;
            }
            else
            {
                headIndex--;
            }
        }

        if (button.name == "HeadRight")
        {
            if (headIndex == headSprites.Length - 1)
            {
                headIndex = 0;
            }
            else
            {
                headIndex++;
            }
        }

        headField.GetComponent<Image>().sprite = headSprites[headIndex];
    }

    public void IterateOverChests(Button button)
    {
        if (button.name == "ChestLeft")
        {
            if (bodyIndex == 0)
            {
                bodyIndex = upperBodySprites.Length - 1;
            }
            else
            {
                bodyIndex--;
            }
        }

        if (button.name == "ChestRight")
        {
            if (bodyIndex == upperBodySprites.Length - 1)
            {
                bodyIndex = 0;
            }
            else
            {
                bodyIndex++;
            }
        }

        upperBodyField.GetComponent<Image>().sprite = upperBodySprites[bodyIndex];
    }

    public void IterateOverLegs(Button button)
    {
        if (button.name == "LegsLeft")
        {
            if (legsIndex == 0)
            {
                legsIndex = lowerBodySprites.Length - 1;
            }
            else
            {
                legsIndex--;
            }
        }

        if (button.name == "LegsRight")
        {
            if (legsIndex == lowerBodySprites.Length - 1)
            {
                legsIndex = 0;
            }
            else
            {
                legsIndex++;
            }
        }

        lowerBodyField.GetComponent<Image>().sprite = lowerBodySprites[legsIndex];
    }


    /** Have a Player prefab with animators for each Part and set it here */
    public void SetupPlayer()
    {
        Transform head = player.transform.GetChild(0);
        Transform body = head.transform.GetChild(0);
        Transform legs = body.transform.GetChild(0);

        head.GetComponent<SpriteRenderer>().sprite = headSprites[headIndex];
        body.GetComponent<SpriteRenderer>().sprite = upperBodySprites[bodyIndex];
        legs.GetComponent<SpriteRenderer>().sprite = lowerBodySprites[legsIndex];

        string headName = head.GetComponent<SpriteRenderer>().sprite.name.Split('_')[0];
        string bodyName = body.GetComponent<SpriteRenderer>().sprite.name.Split('_')[0];
        string legsName = legs.GetComponent<SpriteRenderer>().sprite.name.Split('_')[0];

        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i].name == headName)
            {
                head.GetComponent<Animator>().runtimeAnimatorController = animators[i];
            }

            if (animators[i].name == bodyName)
            {
                body.GetComponent<Animator>().runtimeAnimatorController = animators[i];
            }

            if (animators[i].name == legsName)
            {
                legs.GetComponent<Animator>().runtimeAnimatorController = animators[i];
            }
        }
    }

    /** Tryout to merge sprites into one */
    public void createPlayer()
    {      
        int headWidth, headHeight;
        int chestWidth, chestHeight;
        int legsWidth, legsHeight;

        Sprite head_sprite = headSprites[headIndex];
        Sprite chest_sprite = upperBodySprites[bodyIndex];
        Sprite leg_sprite = lowerBodySprites[legsIndex];
        
        headWidth = head_sprite.texture.width;
        headHeight = head_sprite.texture.height;
        
        chestWidth = chest_sprite.texture.width;
        chestHeight = chest_sprite.texture.height;
        
        legsWidth = leg_sprite.texture.width;
        legsHeight = leg_sprite.texture.height;
        
        Color[] headPixels = head_sprite.texture.GetPixels();
        Color[] chestPixels = chest_sprite.texture.GetPixels();
        Color[] legPixels = leg_sprite.texture.GetPixels();
        
        
        for (int x = 0; x < chestWidth; x++)
        {
            for (int y = 0; y < chestHeight; y++)
            {
            }
        }
        for (int x = 0; x < legsWidth; x++)
        {
            for (int y = 0; y < legsHeight; y++)
            {
            }
        }
    }
}