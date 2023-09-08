using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.EventSystems;
using System.IO;

public class Template {
    public string Name;
    public int Data;
    public string price;
    public string des;

    public Template(string Name, int Data, string price, string des) {
        this.Name = Name;
        this.Data = Data;
        this.price = price;
        this.des = des;
    }
}

public class Trip : MonoBehaviour {

    RectTransform canvas;
    [SerializeField] Texture2D Image;
    [SerializeField] string Name;
    [SerializeField] int Data;
    [SerializeField] string price;
    [SerializeField] string des;

    void Start() {
        canvas = CreateCanvas();
    }

    // On Clicking Button
    public void ButtonClick() {
        JsonReteive();
    }

    // Retieving Data
    void JsonReteive() {
        Template template = new Template(Name, Data, price, des);

        // reading data from json file
        string manualValues = File.ReadAllText(Application.dataPath + "/Template.json");
        SetData(JsonUtility.FromJson<Template>(manualValues));

        // Showing json data
        template = JsonUtility.FromJson<Template>(manualValues);
        Debug.Log(JsonConvert.SerializeObject(template));
        BG();
    }

    // Calling Creating Panel func
    void BG() {
        CreatePanel(canvas);
    }

    // Set the json data
    void SetData(Template adTemplate) {
        Name = adTemplate.Name;
        Data = adTemplate.Data;
        price = adTemplate.price;
        des = adTemplate.des;
    }

    // Creating Canvas
    RectTransform CreateCanvas() {
        CreateEventSystem();

        GameObject obj = new GameObject("Canvas");

        obj.AddComponent<RectTransform>();
        obj.AddComponent<Canvas>();
        obj.AddComponent<CanvasScaler>();
        obj.AddComponent<GraphicRaycaster>();

        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.planeDistance = 80;

        CanvasScaler canvasScaler = obj.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1080, 1920);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 0.5f;
        canvasScaler.referencePixelsPerUnit = 100;

        GraphicRaycaster graphicRaycaster = obj.GetComponent<GraphicRaycaster>();
        graphicRaycaster.ignoreReversedGraphics = true;

        return obj.GetComponent<RectTransform>();
    }

    // Creating Event System
    void CreateEventSystem() {
        if (EventSystem.current != null)
        {
            return;
        }
        GameObject obj = new GameObject("Event System");

        obj.AddComponent<EventSystem>();
        obj.AddComponent<StandaloneInputModule>();
    }

    // Creating a Panel
    void CreatePanel(RectTransform myTransform) {
        RectTransform holderTransform = ChildInHierarchy("Panel", myTransform);

        // Set position and size Panel
        holderTransform.localPosition = Vector3.zero;
        holderTransform.sizeDelta = new Vector2(1900, 900);

        holderTransform.gameObject.AddComponent<Image>();
        holderTransform.GetComponent<Image>().color = Color.grey ;

        AddImage(holderTransform);
        AddText(holderTransform);
    }

    // Adding an ImageParent and Image
    void AddImage(RectTransform myTransform) {
        RectTransform childTransform = ChildInHierarchy("ImageParent", myTransform);

        // Set position and size of ImageParent
        childTransform.localPosition = new Vector3(0, 100, 0);
        childTransform.sizeDelta = new Vector2(40, 20);
        // Set position and size of Image
        CreateImage("Image", childTransform, 0, -30, 0, 1800, 700, Image);
    }

    // Adding a TextParent and Text
    void AddText(RectTransform myTransform) {
        RectTransform childTransform = ChildInHierarchy("TextParent", myTransform);

        // Set position and size of TextParent
        childTransform.localPosition = new Vector3(0, -300, 0);
        childTransform.sizeDelta = new Vector2(500, 200);
        // Set position and size of Text
        CreateText("Text", childTransform, 0, -55, 0, 670, 50, des, FontStyles.Bold, 55, Color.green, TextAlignmentOptions.Left);
    }

    // To Add Child in a Hierarchy
    RectTransform ChildInHierarchy(string name, RectTransform myTransform) {
        GameObject obj = new GameObject(name);

        // Add RectTransform
        obj.AddComponent<RectTransform>();
        RectTransform rect = obj.GetComponent<RectTransform>();

        // Local Local & Set Parent
        RectTransform localRect = rect;
        rect.SetParent(myTransform);

        // Set Transform
        rect.localPosition = new Vector3(localRect.localPosition.x, localRect.localPosition.x, localRect.localPosition.x);
        rect.localRotation = new Quaternion(localRect.localRotation.x, localRect.localRotation.y, localRect.localRotation.z, localRect.localRotation.w);
        rect.localScale = Vector3.one;

        return rect;
    }

    // To Create Image 
    void CreateImage(string name, RectTransform parent, float posX, float posY, float posZ, float width, float height, Texture2D imagetexture) {
        GameObject obj = new GameObject(name);
        
        // Add RectTransform
        obj.AddComponent<RectTransform>();

        // Set Parent
        obj.GetComponent<RectTransform>().SetParent(parent);

        // Set Transform
        obj.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, posZ);
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        obj.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        // Attach Image
        obj.AddComponent<RawImage>();

        // Set Image
        obj.GetComponent<RawImage>().texture = imagetexture;
    }

    // To Create Text
    void CreateText(string name, RectTransform parent, float posX, float posY, float posZ, float width, float height, string text,FontStyles styles, int Size, Color color, TextAlignmentOptions alignmentOption) {
        GameObject obj = new GameObject(name);

        // Add RectTransform
        obj.AddComponent<RectTransform>();

        // Set Parent
        obj.GetComponent<RectTransform>().SetParent(parent);

        // Set Transform
        obj.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, posZ);
        obj.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);


        // Add TextMeshProUGUI
        obj.AddComponent<TextMeshProUGUI>();

        // Set Text Elements
        obj.GetComponent<TextMeshProUGUI>().text = text;
        obj.GetComponent<TextMeshProUGUI>().fontStyle = styles;
        obj.GetComponent<TextMeshProUGUI>().fontSize = Size;
        obj.GetComponent<TextMeshProUGUI>().color = color;
        obj.GetComponent<TextMeshProUGUI>().alignment = alignmentOption;
    }

}
