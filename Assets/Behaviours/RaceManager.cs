using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public Vector3 finishLinePosition = new Vector3(46, 0.5f, -47);
    public Vector3 finishLineTriggerSize = new Vector3(12, 4, 1);

    private bool raceOver;
    private Text resultText;

    private void Start()
    {
        CreateUI();
        CreateFinishTrigger();
    }

    private void CreateUI()
    {
        var canvas = new GameObject("RaceCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.gameObject.AddComponent<CanvasScaler>();
        canvas.gameObject.AddComponent<GraphicRaycaster>();

        var textObj = new GameObject("ResultText");
        textObj.transform.SetParent(canvas.transform, false);

        resultText = textObj.AddComponent<Text>();
        resultText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        resultText.fontSize = 72;
        resultText.alignment = TextAnchor.MiddleCenter;
        resultText.enabled = false;

        var rect = textObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private void CreateFinishTrigger()
    {
        var triggerObj = new GameObject("FinishTrigger");
        triggerObj.transform.position = finishLinePosition;

        var col = triggerObj.AddComponent<BoxCollider>();
        col.size = finishLineTriggerSize;
        col.isTrigger = true;

        triggerObj.AddComponent<FinishTrigger>().manager = this;
    }

    public void OnFinishCrossed(GameObject crosser)
    {
        if (raceOver) return;
        raceOver = true;

        bool isPlayer = crosser.name == "Car";
        resultText.text = isPlayer ? "You Win!" : "You Lose!";
        resultText.color = isPlayer ? Color.green : Color.red;
        resultText.enabled = true;
    }
}
