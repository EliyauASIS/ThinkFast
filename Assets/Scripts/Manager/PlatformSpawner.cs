using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    public GameObject[] platforms;
    public ColorEntry[] colors;

    [System.Serializable]
    public class ColorEntry
    {
        public Color color;
        public int colorID;
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetRandomUniqueColors();
    }

    public void SetRandomUniqueColors()
    {
        if (colors.Length < platforms.Length)
        {
            Debug.LogError("אין מספיק צבעים בשביל לתת צבע שונה לכל פלטפורמה");
            return;
        }

        List<int> colorIndexes = new List<int>();

        for (int i = 0; i < colors.Length; i++)
        {
            colorIndexes.Add(i);
        }

        for (int i = 0; i < colorIndexes.Count; i++)
        {
            int randomIndex = Random.Range(i, colorIndexes.Count);
            int temp = colorIndexes[i];
            colorIndexes[i] = colorIndexes[randomIndex];
            colorIndexes[randomIndex] = temp;
        }

        for (int i = 0; i < platforms.Length; i++)
        {
            GameObject platform = platforms[i];
            Image platformImage = platform.GetComponent<Image>();
            PlatformScript platformScript = platform.GetComponent<PlatformScript>();

            int colorIndex = colorIndexes[i];

            platformImage.color = colors[colorIndex].color;
            platformScript.platformColorID = colors[colorIndex].colorID;
        }
    }
}