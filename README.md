# 🎮 Unity Object Template Generator with JSON 📜

![Unity JSON Template Generator](https://github.com/Asbaq/Unity_Object-_Template_Generator_With_JSON/assets/62818241/2391bd0a-7de1-4a9f-b0c7-5fe564dd71cb)

## 📌 Introduction
The **Unity Object Template Generator with JSON** is a script that dynamically creates UI elements based on JSON data. This script helps in generating UI templates efficiently without hardcoding UI elements manually. It reads a JSON file, extracts data, and displays it on a dynamically created UI panel.

## 🔥 Features
- 📂 Reads JSON files to populate UI elements.
- 🎨 Dynamically creates UI elements (Canvas, Panels, Text, Images).
- 📌 Uses **TextMeshPro** for rich text rendering.
- 🖼️ Loads images and displays them in the UI.
- 🎭 Easily configurable for different data structures.
- 🚀 Automates UI creation in Unity.

---

## 🏗️ How It Works
The script consists of two main classes:
1. **Template Class** 📝 (Holds the JSON data structure.)
2. **Trip Class** 🚀 (Handles UI creation and JSON parsing.)

### 📌 Template Class
The `Template` class represents the structure of the data stored in the JSON file.
```csharp
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
```

---

### 🚀 Trip Class (Main Functionality)
The `Trip` class is responsible for handling the UI creation and retrieving data from JSON.

#### ✅ Start Method
- Creates a **Canvas** dynamically when the scene starts.
```csharp
void Start() {
    canvas = CreateCanvas();
}
```

#### 🔘 Button Click Handler
- Calls the function to retrieve JSON data when a button is clicked.
```csharp
public void ButtonClick() {
    JsonReteive();
}
```

#### 📜 Retrieving JSON Data
- Reads a JSON file and parses it to populate UI elements.
```csharp
void JsonReteive() {
    Template template = new Template(Name, Data, price, des);
    string manualValues = File.ReadAllText(Application.dataPath + "/Template.json");
    SetData(JsonUtility.FromJson<Template>(manualValues));
    Debug.Log(JsonConvert.SerializeObject(template));
    BG();
}
```

#### 🎨 Creating UI Elements
- **Create Canvas:** Generates a UI canvas dynamically.
```csharp
RectTransform CreateCanvas() {
    CreateEventSystem();
    GameObject obj = new GameObject("Canvas");
    obj.AddComponent<Canvas>();
    obj.AddComponent<CanvasScaler>();
    obj.AddComponent<GraphicRaycaster>();
    return obj.GetComponent<RectTransform>();
}
```

- **Create Panel:** Generates a panel to hold UI elements.
```csharp
void CreatePanel(RectTransform myTransform) {
    RectTransform holderTransform = ChildInHierarchy("Panel", myTransform);
    holderTransform.gameObject.AddComponent<Image>();
    holderTransform.GetComponent<Image>().color = Color.grey;
    AddImage(holderTransform);
    AddText(holderTransform);
}
```

- **Create Text:** Dynamically generates a text element.
```csharp
void CreateText(string name, RectTransform parent, float posX, float posY, float posZ, float width, float height, string text,FontStyles styles, int Size, Color color, TextAlignmentOptions alignmentOption) {
    GameObject obj = new GameObject(name);
    obj.AddComponent<TextMeshProUGUI>();
    obj.GetComponent<TextMeshProUGUI>().text = text;
    obj.GetComponent<TextMeshProUGUI>().fontSize = Size;
    obj.GetComponent<TextMeshProUGUI>().color = color;
}
```

---

## 📁 JSON File Structure
The JSON file should be formatted as follows:
```json
{
    "Name": "Warrior Sword",
    "Data": 10,
    "price": "1500 Gold",
    "des": "A powerful sword with mystical abilities."
}
```

---

## 🎯 Conclusion
The **Unity Object Template Generator with JSON** is a powerful script that automates UI generation. It eliminates the need for manual UI creation and makes the process dynamic and reusable. 🚀💡

