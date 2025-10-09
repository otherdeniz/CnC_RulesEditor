# C&C Rules Editor - Developer Guide

## Table of Contents

1. [Architecture Deep Dive](#architecture-deep-dive)
2. [Code Organization](#code-organization)
3. [Data Flow](#data-flow)
4. [Key Design Patterns](#key-design-patterns)
5. [Working with Game Files](#working-with-game-files)
6. [UI Components](#ui-components)
7. [Testing Guidelines](#testing-guidelines)
8. [Performance Optimization](#performance-optimization)
9. [Common Development Tasks](#common-development-tasks)

---

## Architecture Deep Dive

### Application Layers

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│  (MainForm, Controls, Dialogs)          │
├─────────────────────────────────────────┤
│         Business Logic Layer            │
│  (Models, Repositories, Services)       │
├─────────────────────────────────────────┤
│         Data Access Layer               │
│  (File Parsers, MIX Readers)            │
├─────────────────────────────────────────┤
│         Infrastructure Layer            │
│  (Utils, Extensions, Helpers)           │
└─────────────────────────────────────────┘
```

### Component Interaction

```
MainForm
  ├─> RulesEditMainControl
  │     ├─> EntitiesListControl
  │     │     └─> GameEntityModel[]
  │     └─> UnitEditControl
  │           └─> EntityValueModel[]
  │
  ├─> ArtEditMainControl
  │     └─> Similar structure
  │
  └─> AiEditMainControl
        └─> Similar structure

CCGameRepository (Singleton)
  ├─> CCFileManager
  │     └─> MixFile[]
  ├─> BitmapRepository
  ├─> AnimationsAsyncLoader
  └─> RaAudioManager

ResourcesRepository (Singleton)
  ├─> GameDefinition[]
  ├─> DatastructureDefinition
  └─> ValueDefinition[]
```

---

## Code Organization

### Naming Conventions

**Classes:**
- Models: `*Model.cs` (e.g., `GameEntityModel.cs`)
- Controls: `*Control.cs` (e.g., `UnitEditControl.cs`)
- Forms: `*Form.cs` (e.g., `AddUnitForm.cs`)
- Repositories: `*Repository.cs` (e.g., `CCGameRepository.cs`)
- Managers: `*Manager.cs` (e.g., `CCFileManager.cs`)
- Services: `*Service.cs` (e.g., `AudioPlayerService.cs`)

**Interfaces:**
- Prefix with `I` (e.g., `IValueModel`, `ILookupValueModel`)

**Events:**
- Use EventHandler pattern
- Suffix with `EventArgs` (e.g., `GameEntityEventArgs`)

**Private Fields:**
- Prefix with underscore: `_fieldName`
- Use camelCase

**Properties:**
- PascalCase: `PropertyName`

**Methods:**
- PascalCase: `MethodName()`
- Async methods: `MethodNameAsync()`

### File Organization

**One class per file** (except nested classes)

**Related classes in same folder:**
```
Model/
  ├── GameEntityModel.cs
  ├── EntityValueModel.cs
  ├── EntityListItemModel.cs
  └── Extensions/
        └── GameEntityModelExtensions.cs
```

---

## Data Flow

### Opening a File

```
User clicks "Open"
  ↓
MainForm.ButtonOpen()
  ↓
OpenFileDialog.ShowDialog()
  ↓
MainForm.OpenFile(filePath)
  ↓
IniFile.Load(filePath)
  ↓
MainForm.ParseFileType(iniFile)
  ↓
GameTypeDetector.DetectGame(iniFile)
  ↓
MainForm.LoadRulesFile(iniFile, fileType)
  ↓
RulesRootModel.Load(iniFile, gameDefinition)
  ↓
Parse entities from INI sections
  ↓
Create GameEntityModel for each entity
  ↓
Load values from Datastructure.json
  ↓
RulesEditMainControl.LoadModel(rootModel)
  ↓
EntitiesListControl.LoadEntities(entities)
  ↓
Display in UI
```

### Saving a File

```
User clicks "Save"
  ↓
MainForm.ButtonSave()
  ↓
RulesEditMainControl.Model.File.SaveAs(path)
  ↓
IniFile.SaveAs(path)
  ↓
Write sections and values to file
  ↓
Preserve comments and formatting
  ↓
File saved to disk
```

### Loading Game Assets

```
User selects game path
  ↓
CCGameRepository.Initialise(gameDefinition)
  ↓
CCFileManager.LoadAllMixFilesInDirectory()
  ↓
For each MIX file:
  ├─> MixFile.Load(path)
  ├─> Parse MIX header
  ├─> Build file index (CRC → offset)
  └─> Cache in memory
  ↓
Load palette files (cameo.pal, anim.pal)
  ↓
Load art.ini and sound.ini
  ↓
Assets ready for use
```

### Displaying a Cameo

```
UI requests cameo for entity
  ↓
CCGameRepository.GetCameo(entityKey)
  ↓
Check cache
  ├─> If cached: return image
  └─> If not cached:
        ↓
        Get cameo filename from art.ini
        ↓
        CCFileManager.LoadFile(cameoName + ".shp")
        ↓
        ShpFile.Load(bytes)
        ↓
        Render first frame with palette
        ↓
        Apply brightness adjustment
        ↓
        Cache and return image
```

---

## Key Design Patterns

### 1. Singleton Pattern

Used for global repositories:

```csharp
public class CCGameRepository
{
    private static CCGameRepository? _instance;
    public static CCGameRepository Instance => _instance ??= new CCGameRepository();
    
    private CCGameRepository() { }
}
```

**Usage:**
```csharp
var cameo = CCGameRepository.Instance.GetCameo("SMECH");
```

### 2. Repository Pattern

Centralized data access:

```csharp
public class ResourcesRepository
{
    public List<GameDefinition> GetAllGames()
    public GameDefinition? GetGame(string gameKey)
    public DatastructureDefinition GetDatastructure(GameDefinition game)
}
```

### 3. Model-View Pattern

Separation of data and UI:

```csharp
// Model
public class GameEntityModel
{
    public string Key { get; set; }
    public List<EntityValueModel> Values { get; }
}

// View
public class UnitEditControl : UserControl
{
    public void LoadEntity(GameEntityModel entity)
    {
        // Bind model to UI controls
    }
}
```

### 4. Factory Pattern

Creating entities:

```csharp
public class AiGameEntityFactory
{
    public static GameEntityModel CreateScript(string key)
    {
        var entity = new GameEntityModel { Key = key };
        // Initialize with default values
        return entity;
    }
}
```

### 5. Observer Pattern

Event-driven updates:

```csharp
public class GameEntityModel
{
    public event EventHandler<GameEntityEventArgs>? ValueChanged;
    
    public void SetValue(string key, string value)
    {
        // Update value
        ValueChanged?.Invoke(this, new GameEntityEventArgs(key, value));
    }
}
```

### 6. Strategy Pattern

Different file type handling:

```csharp
public interface IFileTypeHandler
{
    void Load(IniFile file);
    void Save(string path);
}

public class RulesFileHandler : IFileTypeHandler { }
public class ArtFileHandler : IFileTypeHandler { }
public class AiFileHandler : IFileTypeHandler { }
```

---

## Working with Game Files

### INI File Structure

```csharp
public class IniFile
{
    private Dictionary<string, IniSection> _sections;
    
    public IniSection? GetSection(string name)
    {
        return _sections.TryGetValue(name, out var section) ? section : null;
    }
    
    public void SetValue(string section, string key, string value)
    {
        var sec = GetOrCreateSection(section);
        sec.SetValue(key, value);
    }
}

public class IniSection
{
    public string Name { get; set; }
    public Dictionary<string, string> Values { get; }
    public List<string> Comments { get; }
}
```

**Reading values:**
```csharp
var iniFile = IniFile.Load("rules.ini");
var cost = iniFile.GetValue("SMECH", "Cost"); // "500"
```

**Writing values:**
```csharp
iniFile.SetValue("SMECH", "Cost", "600");
iniFile.SaveAs("rules.ini");
```

### MIX File Format

**Header Structure:**
```
Offset | Size | Description
-------|------|------------
0x00   | 2    | Flags (0x0000 = unencrypted, 0x0001 = encrypted)
0x02   | 2    | Number of files
0x04   | 4    | Data size
0x08   | var  | File index (12 bytes per file)
```

**File Index Entry:**
```
Offset | Size | Description
-------|------|------------
0x00   | 4    | File CRC
0x04   | 4    | File offset
0x08   | 4    | File size
```

**Implementation:**
```csharp
public class MixFile
{
    private Dictionary<uint, MixEntry> _entries;
    
    public byte[]? GetFile(string fileName)
    {
        var crc = MixCRC.Calculate(fileName);
        return GetFile(crc);
    }
    
    public byte[]? GetFile(uint crc)
    {
        if (!_entries.TryGetValue(crc, out var entry))
            return null;
            
        // Read from file at offset
        return ReadBytes(entry.Offset, entry.Size);
    }
}
```

### SHP File Format

**Header:**
```
Offset | Size | Description
-------|------|------------
0x00   | 2    | Frame count
0x02   | 2    | Width
0x04   | 2    | Height
0x06   | var  | Frame offsets
```

**Frame Data:**
- Run-length encoded
- Palette-indexed colors
- Transparency support

**Rendering:**
```csharp
public class ShpFile
{
    public Bitmap RenderFrame(int frameIndex, List<Color> palette)
    {
        var frame = _frames[frameIndex];
        var bitmap = new Bitmap(Width, Height);
        
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var paletteIndex = frame.GetPixel(x, y);
                if (paletteIndex != 0) // 0 = transparent
                {
                    bitmap.SetPixel(x, y, palette[paletteIndex]);
                }
            }
        }
        
        return bitmap;
    }
}
```

### AUD File Format

**Header:**
```
Offset | Size | Description
-------|------|------------
0x00   | 2    | Sample rate
0x02   | 4    | Data size
0x06   | 4    | Output size
0x0A   | 1    | Flags
0x0B   | 1    | Compression type
```

**Decompression:**
```csharp
public class AudFile
{
    public byte[] Decompress()
    {
        if (_compressionType == 1) // IMA ADPCM
        {
            return DecompressImaAdpcm(_data);
        }
        else if (_compressionType == 99) // Uncompressed
        {
            return _data;
        }
        
        throw new NotSupportedException();
    }
}
```

---

## UI Components

### Custom Controls

#### EntitiesListControl

Displays list of entities with filtering and search:

```csharp
public partial class EntitiesListControl : UserControl
{
    public event EventHandler<GameEntityModel>? EntitySelected;
    
    public void LoadEntities(List<GameEntityModel> entities)
    {
        _entities = entities;
        RefreshList();
    }
    
    public void ApplyFilter(string searchText, bool favoritesOnly)
    {
        var filtered = _entities
            .Where(e => string.IsNullOrEmpty(searchText) || 
                       e.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .Where(e => !favoritesOnly || e.IsFavorite);
            
        DisplayEntities(filtered);
    }
}
```

#### UnitEditControl

Property grid for editing entity values:

```csharp
public partial class UnitEditControl : UserControl
{
    private GameEntityModel? _entity;
    
    public void LoadEntity(GameEntityModel entity)
    {
        _entity = entity;
        
        // Clear existing controls
        valuesPanel.Controls.Clear();
        
        // Create control for each value
        foreach (var value in entity.Values)
        {
            var control = CreateValueControl(value);
            valuesPanel.Controls.Add(control);
        }
    }
    
    private Control CreateValueControl(EntityValueModel value)
    {
        if (value.ValueList != null)
        {
            // Dropdown
            return new LookupTextControl(value);
        }
        else if (value.LookupType != null)
        {
            // Entity reference
            return new LookupSingleValueModel(value);
        }
        else
        {
            // Text box
            return new TextBox { Text = value.Value };
        }
    }
}
```

### Infragistics Components

#### UltraGrid

Used for displaying entity lists:

```csharp
private void InitializeGrid()
{
    ultraGrid.DisplayLayout.Bands[0].Columns["Key"].Hidden = true;
    ultraGrid.DisplayLayout.Bands[0].Columns["Name"].Width = 200;
    ultraGrid.DisplayLayout.Bands[0].Columns["Cost"].Width = 100;
    
    ultraGrid.InitializeRow += (s, e) =>
    {
        var entity = (GameEntityModel)e.Row.ListObject;
        e.Row.Cells["Image"].Value = entity.Image;
    };
}
```

#### UltraToolbarsManager

Menu and toolbar management:

```csharp
private void InitializeToolbars()
{
    var tool = new ButtonTool("OpenFile");
    tool.SharedProps.Caption = "Open";
    tool.SharedProps.AppearancesSmall.Appearance.Image = Resources.OpenIcon;
    
    mainToolbarsManager.Tools.Add(tool);
    mainToolbarsManager.ToolClick += ToolbarsManager_ToolClick;
}

private void ToolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
{
    switch (e.Tool.Key)
    {
        case "OpenFile":
            ButtonOpen();
            break;
        case "SaveFile":
            ButtonSave();
            break;
    }
}
```

---

## Testing Guidelines

### Unit Testing

**Test structure:**
```csharp
[TestClass]
public class IniFileTests
{
    [TestMethod]
    public void Load_ValidFile_ReturnsIniFile()
    {
        // Arrange
        var content = "[Section]\nKey=Value";
        var bytes = Encoding.UTF8.GetBytes(content);
        
        // Act
        var iniFile = IniFile.Load(bytes);
        
        // Assert
        Assert.IsNotNull(iniFile);
        Assert.AreEqual("Value", iniFile.GetValue("Section", "Key"));
    }
}
```

### Integration Testing

**Testing file operations:**
```csharp
[TestMethod]
public void SaveAndLoad_PreservesData()
{
    // Arrange
    var iniFile = new IniFile();
    iniFile.SetValue("Test", "Key", "Value");
    var tempFile = Path.GetTempFileName();
    
    // Act
    iniFile.SaveAs(tempFile);
    var loaded = IniFile.Load(tempFile);
    
    // Assert
    Assert.AreEqual("Value", loaded.GetValue("Test", "Key"));
    
    // Cleanup
    File.Delete(tempFile);
}
```

### Manual Testing Checklist

**File Operations:**
- [ ] Open rules.ini
- [ ] Open art.ini
- [ ] Open ai.ini
- [ ] Save file
- [ ] Save as new file
- [ ] Save to game directory

**Entity Operations:**
- [ ] Add new unit
- [ ] Copy existing unit
- [ ] Delete unit
- [ ] Modify values
- [ ] Insert snippet

**Game Assets:**
- [ ] Load cameos
- [ ] Play animations
- [ ] Play sounds
- [ ] Browse MIX files

**UI Features:**
- [ ] Search entities
- [ ] Filter by favorites
- [ ] Switch themes
- [ ] Compare files
- [ ] Balance tool

---

## Performance Optimization

### Caching Strategies

**Image caching:**
```csharp
private readonly Dictionary<string, Image?> _cameosCache = new();

public Image? GetCameo(string entityKey)
{
    if (_cameosCache.TryGetValue(entityKey, out var cached))
        return cached;
        
    var image = LoadCameoFromMix(entityKey);
    _cameosCache[entityKey] = image;
    return image;
}
```

**Lazy loading:**
```csharp
private List<GameEntityModel>? _entities;

public List<GameEntityModel> Entities
{
    get
    {
        if (_entities == null)
        {
            _entities = LoadEntitiesFromIni();
        }
        return _entities;
    }
}
```

### Async Operations

**Background loading:**
```csharp
public async Task<Image?> GetAnimationAsync(string animKey)
{
    return await Task.Run(() =>
    {
        var shpBytes = _fileManager.LoadFile(animKey + ".shp");
        if (shpBytes == null) return null;
        
        var shpFile = ShpFile.Load(shpBytes);
        return shpFile.RenderFrame(0, _animPaletteColors);
    });
}
```

**UI responsiveness:**
```csharp
private async void LoadEntitiesButton_Click(object sender, EventArgs e)
{
    loadingLabel.Visible = true;
    
    var entities = await Task.Run(() => LoadEntitiesFromFile());
    
    entitiesListControl.LoadEntities(entities);
    loadingLabel.Visible = false;
}
```

### Memory Management

**Dispose pattern:**
```csharp
public class AnimatedGifImage : IDisposable
{
    private List<Image> _frames = new();
    
    public void Dispose()
    {
        foreach (var frame in _frames)
        {
            frame?.Dispose();
        }
        _frames.Clear();
    }
}
```

**Cache limits:**
```csharp
private const int MaxCacheSize = 1000;

private void AddToCache(string key, Image image)
{
    if (_cache.Count >= MaxCacheSize)
    {
        // Remove oldest entry
        var oldest = _cache.First();
        oldest.Value?.Dispose();
        _cache.Remove(oldest.Key);
    }
    
    _cache[key] = image;
}
```

---

## Common Development Tasks

### Adding a New Entity Type

1. **Update Datastructure.json:**
```json
{
  "YourNewType": [
    {
      "Key": "Name",
      "Description": "Display name",
      "Default": "New Entity"
    }
  ]
}
```

2. **Add to RulesRootModel:**
```csharp
public class RulesRootModel
{
    public List<GameEntityModel> YourNewTypes { get; set; } = new();
    
    private void LoadYourNewTypes(IniFile iniFile)
    {
        var typesList = iniFile.GetValue("General", "YourNewTypes")?.Split(',');
        foreach (var key in typesList)
        {
            var entity = LoadEntity(key, "YourNewType");
            YourNewTypes.Add(entity);
        }
    }
}
```

3. **Add UI tab:**
```csharp
var tab = new UltraTab("YourNewTypes");
tab.TabPage.Controls.Add(new EntitiesListControl());
mainTabControl.Tabs.Add(tab);
```

### Adding a New Value Type

1. **Define in Datastructure.json:**
```json
{
  "ValueTypes": {
    "YourValueType": "option1,option2,option3"
  }
}
```

2. **Use in value definition:**
```json
{
  "Key": "YourValue",
  "ValueType": "YourValueType"
}
```

### Adding a New File Format Parser

1. **Create parser class:**
```csharp
public class YourFileFormat
{
    public static YourFileFormat Load(byte[] data)
    {
        using var stream = new MemoryStream(data);
        using var reader = new BinaryReader(stream);
        
        // Parse header
        var header = reader.ReadInt32();
        
        // Parse data
        var content = reader.ReadBytes((int)stream.Length - 4);
        
        return new YourFileFormat { Data = content };
    }
}
```

2. **Integrate with CCFileManager:**
```csharp
public byte[]? LoadYourFile(string fileName)
{
    var data = LoadFile(fileName);
    if (data == null) return null;
    
    var parsed = YourFileFormat.Load(data);
    return parsed.Data;
}
```

### Implementing a New Dialog

1. **Create form:**
```csharp
public partial class YourDialog : Form
{
    public string Result { get; private set; }
    
    public YourDialog()
    {
        InitializeComponent();
    }
    
    private void OkButton_Click(object sender, EventArgs e)
    {
        Result = textBox.Text;
        DialogResult = DialogResult.OK;
        Close();
    }
}
```

2. **Show dialog:**
```csharp
private void ShowYourDialog()
{
    using var dialog = new YourDialog();
    if (dialog.ShowDialog(this) == DialogResult.OK)
    {
        var result = dialog.Result;
        // Use result
    }
}
```

### Adding a New Menu Item

1. **Create tool:**
```csharp
var tool = new ButtonTool("YourAction");
tool.SharedProps.Caption = "Your Action";
tool.SharedProps.Category = "Edit";
mainToolbarsManager.Tools.Add(tool);
```

2. **Handle click:**
```csharp
private void ToolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
{
    if (e.Tool.Key == "YourAction")
    {
        YourActionMethod();
    }
}
```

---

## Best Practices

### Error Handling

**Use try-catch for I/O:**
```csharp
public IniFile? LoadFile(string path)
{
    try
    {
        return IniFile.Load(path);
    }
    catch (FileNotFoundException)
    {
        MessageBox.Show($"File not found: {path}");
        return null;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error loading file: {ex.Message}");
        return null;
    }
}
```

**Validate user input:**
```csharp
private void SaveButton_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(nameTextBox.Text))
    {
        MessageBox.Show("Name is required");
        return;
    }
    
    // Proceed with save
}
```

### Code Documentation

**XML comments:**
```csharp
/// <summary>
/// Loads a cameo image for the specified entity.
/// </summary>
/// <param name="entityKey">The entity key (INI section name)</param>
/// <returns>The cameo image, or null if not found</returns>
public Image? GetCameo(string entityKey)
{
    // Implementation
}
```

### Resource Management

**Use using statements:**
```csharp
using (var stream = File.OpenRead(path))
using (var reader = new BinaryReader(stream))
{
    // Read data
}
```

**Dispose explicitly:**
```csharp
public void Cleanup()
{
    _fileManager?.Dispose();
    _raAudioManager?.Dispose();
    
    foreach (var image in _cameosCache.Values)
    {
        image?.Dispose();
    }
    _cameosCache.Clear();
}
```

---

## Debugging Tips

### Visual Studio Debugging

**Breakpoint conditions:**
```csharp
// Break only when entity key is "SMECH"
if (entity.Key == "SMECH")
{
    Debugger.Break();
}
```

**Watch expressions:**
- `_entities.Count`
- `_cameosCache.Keys`
- `iniFile.GetValue("SMECH", "Cost")`

**Immediate window:**
```csharp
// Evaluate expressions
? entity.GetValue("Cost")
// Call methods
entity.SetValue("Cost", "1000")
```

### Logging

**Debug output:**
```csharp
Debug.WriteLine($"Loading entity: {entityKey}");
Debug.WriteLine($"Cache size: {_cameosCache.Count}");
```

**Conditional compilation:**
```csharp
#if DEBUG
    Console.WriteLine($"Debug info: {value}");
#endif
```

---

**Document Version:** 1.0  
**Last Updated:** 2025-10-09  
**For Application Version:** 2.6.60
