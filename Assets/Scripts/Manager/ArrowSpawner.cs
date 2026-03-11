using UnityEngine;
using UnityEngine.UI;

public class ArrowSpawner : MonoBehaviour
{
    public static ArrowSpawner instance;

    public RectTransform arrowHandler;   // גרור את ה-Canvas לכאן
    public GameObject[] arrowPrefabs;          // תמונת הכדור
    public bool canSpawn = false;
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

    public void StartSpawn()
    {
        canSpawn = true;
        SpawnBall();
    }

    public void SpawnBall()
    {
        if (!canSpawn) { return; }
        int randomArrow = Random.Range(0, 4);
        GameObject arrowInstance = Instantiate(arrowPrefabs[randomArrow], arrowHandler.transform);

        Image img = arrowInstance.GetComponent<Image>();

        int randomColor = Random.Range(0, 4);

        img.color = colors[randomColor].color;
        arrowInstance.GetComponent<ArrowScript>().arrowColorID = colors[randomColor].colorID;
        RectTransform rect = arrowInstance.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 300);

        arrowInstance.GetComponent<ArrowSizeReduction>().duration = GameManager.instance.mainSizeReductionduration;

    }
}