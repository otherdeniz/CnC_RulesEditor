# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

C&C Rules Editor - a Windows desktop application (C# / .NET 6.0 WinForms) for editing INI files and maps for Command & Conquer games (Tiberian Sun, Red Alert 2, and related mods). Current version: 2.7.67.

## Build Commands

```bash
# Build entire solution
dotnet build Deniz.TiberiumSunEditor.sln

# Build release
dotnet build Deniz.TiberiumSunEditor.sln --configuration Release

# Build specific project
dotnet build Deniz.TiberiumSunEditor.Gui/Deniz.TiberiumSunEditor.Gui.csproj
```

No test projects exist in this solution. The project has no automated test suite.

## Solution Structure

Three projects in `Deniz.TiberiumSunEditor.sln`:

- **Deniz.TiberiumSunEditor.Gui** - Main WinForms application (depends on Deniz.Updater)
- **Deniz.CCAudioPlayerCore** - Audio playback for C&C game audio formats
- **Deniz.Updater** - Auto-update utility

## Architecture

### Application Layers

```
Presentation (MainForm, Controls/, Dialogs/)
    -> Business Logic (Model/, Repositories, Services)
        -> Data Access (Utils/Files/, Utils/CncParser/)
            -> Infrastructure (Utils/, Extensions)
```

### Entry Point and Core Flow

`Program.cs` -> `MainForm` (central hub, ~1500 lines) manages three main editing tabs:

1. **RulesEditMainControl** - Game rules (units, buildings, aircraft, infantry)
2. **ArtEditMainControl** - Art/graphics configuration
3. **AiEditMainControl** - AI scripts and task forces

Each editing control uses `EntitiesListControl` (entity list with filtering) and `UnitEditControl` (property grid for editing values).

### Key Singletons

- **CCGameRepository** - Manages game assets: MIX file loading, cameo image caching, animation loading, audio playback
- **ResourcesRepository** - Manages bundled resource files: game definitions, datastructure definitions, value definitions

### Data Model

- **GameEntityModel** - Represents a single game entity (unit, building, etc.) with key-value pairs from INI sections
- **EntityValueModel** - A single editable value within an entity, typed via Datastructure.json definitions
- **RulesRootModel / ArtRootModel / AiRootModel** - Root models for each file type containing entity collections

### Game File Parsers (Utils/CncParser/)

Parsers for C&C binary formats: **MixFile** (game archives, CRC-based lookup), **ShpFile** (sprites, RLE-encoded), **AudFile** (audio, IMA ADPCM), **PalFile** (color palettes), **CsfFile** (localization strings).

### Configuration System (Resources/)

- **Games.json** - Defines supported games (TiberianSun, Firestorm, DTA, RA2, RA2YR, RA2MO) with their MIX file paths, default INI files, and bitmap folders
- **Datastructure.json** - All INI key definitions with types, defaults, descriptions, and dropdown value lists
- **Artstructure.json / Aistructure.json** - Structure definitions for art and AI files
- Default INI files per game (e.g., `DTArules.ini`, `RA2rules.ini`)

The editor is fully data-driven: editing Games.json and Datastructure.json customizes game support without code changes.

## Key Dependencies

- **Infragistics WinForms 23.2** (commercial, trial available) - UltraGrid, UltraTabControl, UltraToolbarsManager, UltraListView, Editors
- **Magick.NET-Q8-AnyCPU** - Image processing
- **ICSharpCode.TextEditorEx** - Text editor control
- **Newtonsoft.Json** - JSON parsing
- **Octokit** - GitHub API for auto-updates
- **CSCore / FFmpeg** - Audio playback (FFmpeg binaries in CSPlugins/)

## Conventions

- One class per file; suffix naming: `*Model`, `*Control`, `*Form`, `*Repository`, `*Manager`, `*Service`
- Private fields: `_camelCase`; interfaces: `I` prefix
- Event-driven UI updates via `EventHandler<GameEntityEventArgs>`
- `AllowUnsafeBlocks` is enabled for performance-critical image processing
- User settings stored at `%AppData%\TiberiumSunEditor` (legacy path, cannot change)
- Cameo bitmaps in `Bitmaps/` folder as `.bmp`; overlay PNGs drawn onto `_empty.bmp`

## Important Notes

- Requires Windows (WinForms, net6.0-windows target)
- Infragistics requires a developer license for official releases; trial NuGet packages work for building/testing
- `GenerateAssemblyInfo` is false; version info is in `Properties/AssemblyInfo.cs`
