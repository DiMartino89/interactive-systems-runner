using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    public GameObject headField;
    public GameObject upperBodyField;
    public GameObject lowerBodyField;
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
		
		// Components for PlayerController
		
		// Use "#if Unity_Editor" for the Build as Editor-References are not accessible in Build
		// Whole Look/Head
		Transform pLook = player.transform.Find("editedLook");
		#if UNITY_EDITOR
		Object prefab1 = EditorUtility.CreateEmptyPrefab("Assets/Resources/editedAppearance/" + pLook.gameObject.name + ".prefab");
		PrefabUtility.ReplacePrefab(pLook.gameObject, prefab1, ReplacePrefabOptions.ConnectToPrefab);
		#endif
		// UpperBody
		Transform upperBody = pLook.Find("UpperBody");
		#if UNITY_EDITOR
		Object prefab2 = EditorUtility.CreateEmptyPrefab("Assets/Resources/editedAppearance/" + upperBody.gameObject.name + ".prefab");
		PrefabUtility.ReplacePrefab(upperBody.gameObject, prefab2, ReplacePrefabOptions.ConnectToPrefab);
		#endif
		// LowerBody
		Transform lowerBody = pLook.Find("UpperBody").transform.Find("LowerBody");
		#if UNITY_EDITOR
		Object prefab3 = EditorUtility.CreateEmptyPrefab("Assets/Resources/editedAppearance/" + lowerBody.gameObject.name + ".prefab");
		PrefabUtility.ReplacePrefab(lowerBody.gameObject, prefab3, ReplacePrefabOptions.ConnectToPrefab);
		#endif
    }
}