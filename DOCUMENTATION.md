# C&C Rules Editor - Complete Documentation

## Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
3. [Project Structure](#project-structure)
4. [Core Components](#core-components)
5. [Configuration System](#configuration-system)
6. [Features](#features)
7. [Technical Details](#technical-details)
8. [Development Setup](#development-setup)
9. [Building and Deployment](#building-and-deployment)
10. [Extending the Editor](#extending-the-editor)
11. [API Reference](#api-reference)
12. [Troubleshooting](#troubleshooting)

---

## Project Overview

### What is C&C Rules Editor?

The C&C Rules Editor is a comprehensive Windows desktop application designed for modding and editing Command & Conquer games, specifically:
- **Tiberian Sun** (and Firestorm expansion)
- **Red Alert 2** (and Yuri's Revenge expansion)
- **Custom mods** based on these games

**Version:** 2.6.60  
**Homepage:** https://ruleseditor.denizesen.ch  
**License:** GNU General Public License v3.0  
**Author:** Deniz 'otherdeniz' Esen  
**Target Framework:** .NET 6.0 Windows  

### Key Capabilities

- **Visual INI Editing**: Edit rules.ini, art.ini, and ai.ini files with a user-friendly graphical interface
- **Game Asset Integration**: Load and display cameos, animations, and sounds directly from game MIX files
- **Entity Management**: Create, copy, and modify units, buildings, infantry, aircraft, weapons, and warheads
- **Advanced Mod Support**: Support for Ares, Phobos, and Vinifera engine extensions
- **Balance Analysis**: Built-in balancing tools to analyze and compare unit statistics
- **Snippet System**: Pre-configured templates for quick unit creation
- **Multi-Game Support**: Seamlessly switch between different C&C games and mods
- **Auto-Update**: Integrated update mechanism via GitHub releases

---

## Architecture

### Solution Structure

The solution consists of three main projects:

```
Deniz.TiberiumSunEditor.sln
├── Deniz.TiberiumSunEditor.Gui (Main Application)
├── Deniz.CCAudioPlayerCore (Audio Playback Library)
└── Deniz.Updater (Auto-Update Utility)
```

### Technology Stack

**Core Technologies:**
- .NET 6.0 Windows Forms
- C# 10 with nullable reference types enabled
- Unsafe code blocks for performance-critical operations

**Major Dependencies:**
- **Infragistics WinForms 23.2** - Professional UI components (commercial license required)
  - DataSource, Editors, ListView, TabbedMdi, TabControl, Toolbars, WinGrid
- **Newtonsoft.Json 13.0.3** - JSON serialization
- **Magick.NET-Q8-AnyCPU 13.10.0** - Image processing
- **AnimatedGif 1.0.5** - GIF animation support
- **Octokit 13.0.1** - GitHub API integration for updates
- **CSCore & FFmpeg** - Audio decoding and playback
- **RestSharp 111.4.1** - HTTP client for updater

---

## Project Structure

### Deniz.TiberiumSunEditor.Gui

Main application project containing all UI and business logic.

```
Deniz.TiberiumSunEditor.Gui/
├── Controls/              # User controls for different editing views
│   ├── AiEditMainControl.cs
│   ├── ArtEditMainControl.cs
│   ├── RulesEditMainControl.cs
│   ├── EntitiesListControl.cs
│   ├── EntityEdit/        # Entity-specific edit controls
│   ├── ColorControls/     # Color picker controls
│   └── ...
├── Dialogs/               # Modal dialog forms
│   ├── AboutForm.cs
│   ├── AddUnitForm.cs
│   ├── BalancingToolForm.cs
│   ├── MixBrowserForm.cs
│   ├── SettingsForm.cs
│   └── ...
├── Model/                 # Data models and business logic
│   ├── GameEntityModel.cs
│   ├── RulesRootModel.cs
│   ├── ArtRootModel.cs
│   ├── AiRootModel.cs
│   ├── Extensions/        # Extension methods
│   └── Interface/         # Model interfaces
├── Utils/                 # Utility classes
│   ├── CncParser/         # C&C file format parsers
│   │   ├── MixFile.cs     # MIX archive reader
│   │   ├── ShpFile.cs     # SHP image format
│   │   ├── AudFile.cs     # AUD audio format
│   │   ├── CsfFile.cs     # CSF string table
│   │   └── PalFile.cs     # PAL palette format
│   ├── Datastructure/     # Game definition system
│   ├── Files/             # File management
│   ├── UserSettings/      # User preferences
│   └── Extensions/        # General extensions
├── Resources/             # Game configuration files
│   ├── Games.json         # Game definitions
│   ├── Datastructure.json # Value definitions
│   ├── DatastructureAres.json
│   ├── DatastructurePhobos.json
│   ├── DatastructureVinifera.json
│   ├── Aistructure.json   # AI definitions
│   ├── Artstructure.json  # Art definitions
│   ├── TSrules.ini        # Default Tiberian Sun rules
│   ├── RA2rules.ini       # Default Red Alert 2 rules
│   ├── Logos/             # Game logos
│   └── ...
├── Bitmaps/               # Default cameo images
├── Snippets/              # Pre-configured unit templates
├── OpenRa/                # OpenRA MIX file support
├── CSPlugins/             # Audio codec plugins
├── MainForm.cs            # Main application window
└── Program.cs             # Application entry point
```

### Deniz.CCAudioPlayerCore

Audio playback library for playing C&C audio files.

```
Deniz.CCAudioPlayerCore/
├── AudioPlayerService.cs  # Main audio service
├── FfmpegDecoderEx.cs     # FFmpeg decoder wrapper
├── StupidStream.cs        # Custom stream implementation
├── CSAudioPlayer/         # Audio player components
├── AudioCDReaderLib/      # CD audio support
└── WinformsVisualization/ # Audio visualization
```

### Deniz.Updater

Standalone updater application for downloading and installing updates.

```
Deniz.Updater/
├── Program.cs             # Updater entry point
├── UpdateForm.cs          # Update UI
├── ConfigurationManager.cs # Configuration handling
└── appsettings.json       # Updater settings
```

---

## Core Components

### 1. Main Application (MainForm.cs)

The main window orchestrates the entire application:

**Key Responsibilities:**
- File operations (open, save, save as)
- Menu and toolbar management
- Tab management for different file types (Rules, Art, AI)
- Theme management (light/dark mode)
- Recent files tracking
- Game selection and configuration
- Update checking

**Important Methods:**
- `OpenFile(string filePath)` - Opens and parses INI files
- `LoadRulesFile()` - Loads rules.ini into the editor
- `LoadArtFile()` - Loads art.ini into the editor
- `LoadAiFile()` - Loads ai.ini into the editor
- `ButtonSaveInGame()` - Saves directly to game directory

### 2. Game Repository (CCGameRepository.cs)

Singleton class managing game assets and resources:

**Features:**
- MIX file loading and caching
- Cameo image extraction and rendering
- Animation (SHP) loading and playback
- Audio file management
- Palette handling (cameo.pal, anim.pal, unittem.pal)
- Art.ini and Sound.ini integration

**Key Methods:**
- `Initialise(GameDefinition)` - Loads game assets
- `GetCameo(string entityKey)` - Retrieves unit cameo
- `GetAnimation(string animKey)` - Loads SHP animation
- `PlaySound(string soundKey)` - Plays audio file

### 3. File Parsers (Utils/CncParser/)

Custom parsers for C&C file formats:

#### MixFile.cs
- Reads MIX archive files (encrypted and unencrypted)
- Supports both TS and RA2 formats
- CRC-based file lookup
- Blowfish decryption for encrypted archives

#### ShpFile.cs
- Parses SHP image format
- Supports both TS and RA2 variants
- Frame-by-frame rendering
- Palette-based color conversion

#### AudFile.cs
- Decodes AUD audio format
- IMA ADPCM decompression
- Converts to PCM for playback

#### CsfFile.cs
- Reads CSF string table format
- Unicode string support
- Used for localized text

#### PalFile.cs
- Loads 256-color palettes
- RGB color conversion

### 4. Data Models

#### GameEntityModel
Represents any game entity (unit, building, weapon, etc.):
- Properties: Key, Name, Image, Values
- Methods: Clone(), GetValue(), SetValue()
- Events: ValueChanged, ImageChanged

#### RulesRootModel
Root model for rules.ini files:
- Entity collections (Buildings, Infantry, Vehicles, Aircraft)
- Common values (General settings)
- Weapons and Warheads
- SuperWeapons
- Sides (factions)

#### ArtRootModel
Root model for art.ini files:
- Visual properties for entities
- Animation definitions
- Cameo references

#### AiRootModel
Root model for ai.ini files:
- AI scripts
- Task forces
- Triggers
- Team types

### 5. Configuration System

#### Games.json
Defines supported games and mods:

```json
{
  "GameKey": "TiberianSun",
  "Logo": "ts_logo.png",
  "NewMenuLabel": "Tiberian Sun",
  "ResourcesDefaultIniFile": "TSrules.ini",
  "IniNameMatchDetection": "Tiberian Sun",
  "SaveAsFilename": "rules.ini",
  "GameExecutable": "game.exe",
  "SnippetsFolder": "TiberianSun",
  "BitmapsFolders": "TiberianSun",
  "MixFiles": "cache.mix,conquer.mix,local.mix",
  "UseAres": false,
  "UsePhobos": false,
  "UseSectionInheritance": false
}
```

**Key Properties:**
- `GameKey` - Unique identifier (referenced in code)
- `ResourcesDefaultIniFile` - Default rules.ini template
- `IniNameMatchDetection` - Auto-detection string
- `SaveAsFilename` - Output filename
- `MixFiles` - Comma-separated list of MIX files to load
- `UseAres/UsePhobos/UseVinifera` - Engine extension support

#### Datastructure.json
Defines all editable values and their properties:

```json
{
  "AllUnits": [
    {
      "Key": "Cost",
      "Description": "The cost in credits to build this unit",
      "Default": "1000",
      "ValueType": "Integer"
    },
    {
      "Key": "Primary",
      "Description": "Primary weapon",
      "LookupType": "Weapons"
    }
  ],
  "CommonGeneral": [
    {
      "Key": "BuildSpeed",
      "Category": "Production",
      "Section": "General",
      "ValueType": "Float"
    }
  ],
  "ValueTypes": {
    "Integer": "0,1,2,3,4,5,10,20,50,100,200,500,1000",
    "Boolean": "yes,no",
    "Percentage": "0%,25%,50%,75%,100%,125%,150%,200%"
  }
}
```

**Value Definition Properties:**
- `Key` - INI key name
- `Description` - User-friendly description
- `Default` - Default value for new entities
- `ValueType` - Predefined value list
- `ValueList` - Custom comma-separated values
- `LookupType` - Reference to entity type
- `MultipleValues` - Allow multiple selections
- `GameKeyFilter` - Restrict to specific games
- `Category` - Grouping in UI
- `Section` - INI section name

---

## Features

### 1. Entity Editing

**Supported Entity Types:**
- Buildings (structures, defenses)
- Infantry (soldiers, heroes)
- Vehicles (tanks, harvesters)
- Aircraft (planes, helicopters)
- Weapons (projectiles, effects)
- Warheads (damage types)
- SuperWeapons (special abilities)
- Sides (factions)

**Editing Capabilities:**
- Visual property grid with categorization
- Dropdown lookups for references
- Multi-value selection
- Color pickers
- Favorite values marking
- Value comparison between entities
- Bulk editing via snippets

### 2. Visual Asset Integration

**Cameo Display:**
- Automatic extraction from MIX files
- Fallback to default bitmaps
- Brightness adjustment
- Overlay support (PNG over BMP)

**Animation Preview:**
- SHP file playback
- Frame-by-frame viewing
- Palette-based rendering
- Infantry sequence animations

**Audio Playback:**
- Direct playback from MIX files
- AUD format support
- Sound.ini integration
- Preview before applying

### 3. Advanced Features

#### Balance Analysis Tool
- Compare unit statistics side-by-side
- Calculate cost-effectiveness ratios
- Analyze damage output vs. health
- Export balance reports

#### Snippet System
- Pre-configured unit templates
- Quick insertion of common units
- Customizable snippet library
- Organized by category

#### MIX Browser
- Explore MIX file contents
- Extract individual files
- Preview images and sounds
- Search functionality

#### File Comparison
- Compare two rules.ini files
- Highlight differences
- Merge changes
- Track modifications

#### Section Inheritance
- Support for base class inheritance
- Automatic value propagation
- Override tracking

### 4. Mod Support

#### Ares Engine Extension
- Additional unit properties
- Extended weapon types
- Custom warhead effects
- Loaded via DatastructureAres.json

#### Phobos Engine Extension
- Advanced AI features
- New unit abilities
- Enhanced effects
- Loaded via DatastructurePhobos.json

#### Vinifera Engine Extension
- Tiberian Sun enhancements
- Additional features
- Loaded via DatastructureVinifera.json

### 5. User Experience

**Theme Support:**
- Light and dark themes
- Customizable via Themes.json
- Persistent user preference

**Favorites System:**
- Mark frequently used units
- Mark important values
- Quick filter by favorites

**Search and Filter:**
- Real-time entity search
- Filter by type
- Filter by parent class
- Search in values

**Recent Files:**
- Track recently opened files
- Quick access menu
- Persistent across sessions

---

## Technical Details

### File Format Support

#### INI Files
- Standard INI format with sections and key-value pairs
- Comment preservation
- Multi-line value support
- Section inheritance (optional)

**Example:**
```ini
[SMECH]
Name=Wolverine
Cost=500
Primary=20mm
Strength=200
Category=AFV
; This is a comment
```

#### MIX Files
- Proprietary archive format
- CRC-based file lookup
- Optional Blowfish encryption
- Supports nested MIX files

**Supported MIX Files:**
- cache.mix, conquer.mix, local.mix (TS)
- ra2.mix, ra2md.mix, language.mix (RA2)
- Custom mod MIX files

#### SHP Files
- Sprite sheet format
- Frame-based animation
- Palette-indexed colors
- Compression support

#### AUD Files
- Audio format
- IMA ADPCM compression
- 16-bit PCM output
- Variable sample rates

### Performance Optimizations

**Caching Strategy:**
- Cameo images cached in memory
- Animations cached with disposal
- MIX file index cached
- Lazy loading of game assets

**Async Operations:**
- Background animation loading
- Async audio playback
- Non-blocking file operations

**Memory Management:**
- Proper disposal of image resources
- Stream cleanup
- Cache size limits

### DPI Awareness

The application is DPI-aware for high-resolution displays:
```csharp
SetProcessDPIAware(); // Called on startup
```

### Thread Safety

- UI operations on synchronization context
- Thread-safe audio playback
- Async/await patterns for I/O

---

## Development Setup

### Prerequisites

1. **Visual Studio 2022** (or later)
   - .NET 6.0 SDK
   - Windows Forms development workload

2. **Infragistics WinForms 23.2**
   - Commercial license required for official builds
   - Trial version available for testing
   - Install via NuGet Package Manager

3. **Git** for version control

### Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/otherdeniz/CnC_RulesEditor.git
   cd CnC_RulesEditor
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Install Infragistics:**
   - Download from Infragistics website
   - Install NuGet packages:
     - Infragistics.WinForms.DataSource
     - Infragistics.WinForms.Editors
     - Infragistics.WinForms.ListView
     - Infragistics.WinForms.TabbedMdi
     - Infragistics.WinForms.TabControl
     - Infragistics.WinForms.Toolbars
     - Infragistics.WinForms.WinGrid

4. **Build the solution:**
   ```bash
   dotnet build Deniz.TiberiumSunEditor.sln --configuration Release
   ```

5. **Run the application:**
   ```bash
   dotnet run --project Deniz.TiberiumSunEditor.Gui
   ```

### Project Configuration

**Target Framework:** net6.0-windows  
**Language Version:** C# 10  
**Nullable:** Enabled  
**Unsafe Blocks:** Enabled (for performance)  
**Application Icon:** SUN.ICO  

### Build Configurations

**Debug:**
- Full debug symbols
- No optimizations
- Detailed logging

**Release:**
- Optimizations enabled
- Trimmed output
- Post-build content copy

### Post-Build Events

The `CopyContent.bat` script copies required resources:
- Bitmaps folder
- Resources folder
- Snippets folder
- CSPlugins folder
- FFmpeg libraries

---

## Building and Deployment

### Release Build

1. **Update version number:**
   - Edit `AssemblyInfo.cs`
   - Update `AssemblyVersion`, `AssemblyFileVersion`, `AssemblyInformationalVersion`

2. **Build release:**
   ```bash
   dotnet build --configuration Release
   ```

3. **Output location:**
   ```
   Deniz.TiberiumSunEditor.Gui/bin/Release/net6.0-windows/
   ```

### Distribution Package

**Required files:**
- Deniz.TiberiumSunEditor.Gui.exe
- Deniz.CCAudioPlayerCore.dll
- Deniz.Updater.exe
- All Infragistics DLLs
- Newtonsoft.Json.dll
- Magick.NET DLLs
- CSCore and FFmpeg DLLs
- Bitmaps/ folder
- Resources/ folder
- Snippets/ folder
- CSPlugins/ folder
- COPYING (license file)

### Auto-Update System

The application uses GitHub Releases for updates:

1. **Check for updates:**
   - Uses Octokit to query GitHub API
   - Compares current version with latest release

2. **Download update:**
   - Downloads release ZIP file
   - Launches Deniz.Updater.exe

3. **Update process:**
   - Updater extracts ZIP
   - Replaces application files
   - Restarts application

**Update configuration:**
- Repository: otherdeniz/CnC_RulesEditor
- Release asset: RulesEditor.zip

---

## Extending the Editor

### Adding a New Game

1. **Create default rules.ini:**
   - Save to `Resources/YourGameRules.ini`
   - Include all default values and comments

2. **Add game definition to Games.json:**
   ```json
   {
     "GameKey": "YourGame",
     "Logo": "yourgame_logo.png",
     "NewMenuLabel": "Your Game Name",
     "ResourcesDefaultIniFile": "YourGameRules.ini",
     "IniNameMatchDetection": "Your Game",
     "SaveAsFilename": "rules.ini",
     "GameExecutable": "yourgame.exe",
     "SnippetsFolder": "YourGame",
     "BitmapsFolders": "YourGame"
   }
   ```

3. **Create folders:**
   - `Bitmaps/YourGame/` - Default cameos
   - `Snippets/YourGame/` - Unit templates

4. **Add logo:**
   - Save 24x24 PNG to `Resources/Logos/yourgame_logo.png`

### Adding New Value Definitions

1. **Edit Datastructure.json:**
   ```json
   {
     "AllUnits": [
       {
         "Key": "YourNewValue",
         "Description": "Description of the value",
         "Default": "default_value",
         "ValueType": "Integer"
       }
     ]
   }
   ```

2. **Define value type (if custom):**
   ```json
   {
     "ValueTypes": {
       "YourCustomType": "value1,value2,value3"
     }
   }
   ```

3. **Game-specific values:**
   ```json
   {
     "Key": "GameSpecificValue",
     "GameKeyFilter": "TiberianSun,RA2"
   }
   ```

### Creating Custom Controls

1. **Inherit from UserControl:**
   ```csharp
   public partial class MyCustomControl : UserControl
   {
       public MyCustomControl()
       {
           InitializeComponent();
       }
   }
   ```

2. **Implement data binding:**
   ```csharp
   public void LoadEntity(GameEntityModel entity)
   {
       // Bind entity to controls
   }
   ```

3. **Add to main form:**
   - Reference in MainForm.cs
   - Add to tab control

### Adding File Format Support

1. **Create parser class:**
   ```csharp
   public class YourFileFormat
   {
       public static YourFileFormat Load(byte[] data)
       {
           // Parse binary data
       }
   }
   ```

2. **Add to CCFileManager:**
   - Register file extension
   - Implement loading logic

3. **Integrate with UI:**
   - Add preview control
   - Add to MIX browser

---

## API Reference

### Key Classes

#### CCGameRepository

```csharp
public class CCGameRepository
{
    // Singleton instance
    public static CCGameRepository Instance { get; }
    
    // Initialize with game definition
    public void Initialise(GameDefinition gameDefinition, string? mixFiles = null)
    
    // Get cameo image for entity
    public Image? GetCameo(string entityKey, GameEntityModel? entity = null)
    
    // Get animation
    public ImageTools.AnimatedGifImage? GetAnimation(string animKey)
    
    // Play sound
    public void PlaySound(string soundKey)
    
    // Check if game assets are loaded
    public bool IsLoaded { get; }
}
```

#### GameEntityModel

```csharp
public class GameEntityModel
{
    // Entity key (INI section name)
    public string Key { get; set; }
    
    // Display name
    public string Name { get; set; }
    
    // Entity values
    public List<EntityValueModel> Values { get; }
    
    // Get value by key
    public string? GetValue(string key)
    
    // Set value
    public void SetValue(string key, string value)
    
    // Clone entity
    public GameEntityModel Clone()
    
    // Events
    public event EventHandler<GameEntityEventArgs>? ValueChanged;
    public event EventHandler? ImageChanged;
}
```

#### IniFile

```csharp
public class IniFile
{
    // Load from file
    public static IniFile Load(string filePath)
    
    // Load from bytes
    public static IniFile Load(byte[] data)
    
    // Get section
    public IniSection? GetSection(string sectionName)
    
    // Get value
    public string? GetValue(string section, string key)
    
    // Set value
    public void SetValue(string section, string key, string value)
    
    // Save to file
    public void SaveAs(string filePath)
}
```

#### MixFile

```csharp
public class MixFile
{
    // Load MIX file
    public static MixFile Load(string filePath)
    
    // Get file by name
    public byte[]? GetFile(string fileName)
    
    // Get file by CRC
    public byte[]? GetFile(uint crc)
    
    // List all files
    public List<string> GetFileNames()
}
```

### Extension Methods

#### String Extensions

```csharp
public static class StringExtensions
{
    // Parse boolean from yes/no
    public static bool ParseBool(this string value)
    
    // Parse integer with default
    public static int ParseInt(this string value, int defaultValue = 0)
    
    // Parse float with default
    public static float ParseFloat(this string value, float defaultValue = 0f)
    
    // Split comma-separated values
    public static List<string> SplitValues(this string value)
}
```

---

## Troubleshooting

### Common Issues

#### 1. Infragistics License Error

**Problem:** "Infragistics license not found" error on build.

**Solution:**
- Install Infragistics WinForms 23.2
- Register license key
- For testing, use trial version

#### 2. MIX Files Not Loading

**Problem:** Cameos and sounds not appearing.

**Solution:**
- Verify game path is correct
- Check MixFiles setting in Games.json
- Ensure MIX files exist in game directory
- Check file permissions

#### 3. Audio Playback Fails

**Problem:** Sounds don't play or crash.

**Solution:**
- Verify FFmpeg DLLs are present in CSPlugins folder
- Check audio device is available
- Ensure AUD files are valid format

#### 4. High DPI Display Issues

**Problem:** UI elements appear blurry or incorrectly sized.

**Solution:**
- Application is DPI-aware by default
- Check Windows display scaling settings
- Verify SetProcessDPIAware() is called

#### 5. File Save Fails

**Problem:** Cannot save rules.ini to game directory.

**Solution:**
- Check write permissions
- Run as administrator if needed
- Verify game path is correct
- Check disk space

### Debug Logging

Enable detailed logging by:
1. Build in Debug configuration
2. Check Output window in Visual Studio
3. Use Debug.WriteLine() for custom logging

### Performance Issues

**Slow loading:**
- Reduce number of MIX files loaded
- Clear animation cache
- Disable auto-preview

**High memory usage:**
- Close unused tabs
- Clear cameo cache
- Restart application

---

## Contributing

### Code Style

- Follow C# naming conventions
- Use nullable reference types
- Add XML documentation comments
- Keep methods focused and small

### Pull Request Process

1. Fork the repository
2. Create feature branch
3. Make changes with clear commits
4. Test thoroughly
5. Submit pull request with description

### Reporting Issues

Include:
- Application version
- Operating system
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable

---

## License

This program is free software licensed under the GNU General Public License v3.0.

**Copyright (C) Deniz 'otherdeniz' Esen 2024**

This program uses code from:
- **World-Altering Editor** by Rami 'Rampastring' Pasanen
- **OpenRA** project (Copyright 2007-2011 The OpenRA Developers)

Both are licensed under GPL v3.0.

---

## Credits

**Developer:** Deniz 'otherdeniz' Esen  
**GitHub:** https://github.com/otherdeniz  
**Forum:** https://forums.cncnet.org/topic/12869-tiberian-sun-rules-editor-version-2024/  

**Special Thanks:**
- CnCNet community
- Rampastring for World-Altering Editor code
- OpenRA team for MIX file parsing code
- Infragistics for UI components

---

## Appendix

### File Locations

**Application Data:**
```
%AppData%\TiberiumSunEditor\
├── UserSettings.json      # User preferences
├── RecentFiles.json       # Recent file history
└── CustomGames.json       # Custom mod definitions
```

**Installation Directory:**
```
RulesEditor/
├── Deniz.TiberiumSunEditor.Gui.exe
├── Bitmaps/               # Default cameos
├── Resources/             # Game definitions
├── Snippets/              # Unit templates
└── CSPlugins/             # Audio codecs
```

### Keyboard Shortcuts

- **Ctrl+O** - Open file
- **Ctrl+S** - Save file
- **Ctrl+Shift+S** - Save as
- **Ctrl+N** - New file
- **Ctrl+F** - Search
- **F5** - Refresh
- **Alt+F4** - Exit

### Supported File Extensions

- **.ini** - Rules, Art, AI, Sound files
- **.mix** - MIX archives
- **.shp** - SHP images
- **.aud** - AUD audio
- **.csf** - CSF string tables
- **.pal** - PAL palettes

---

**Document Version:** 1.0  
**Last Updated:** 2024  
**Application Version:** 2.6.60
