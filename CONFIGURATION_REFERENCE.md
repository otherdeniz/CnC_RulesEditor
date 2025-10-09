# C&C Rules Editor - Configuration Reference

## Table of Contents

1. [Games.json Configuration](#gamesjson-configuration)
2. [Datastructure.json Configuration](#datastructurejson-configuration)
3. [Aistructure.json Configuration](#aistructurejson-configuration)
4. [Artstructure.json Configuration](#artstructurejson-configuration)
5. [Themes.json Configuration](#themesjson-configuration)
6. [User Settings](#user-settings)
7. [File Format Specifications](#file-format-specifications)

---

## Games.json Configuration

Location: `Resources/Games.json`

Defines all supported games and mods in the editor.

### Schema

```json
[
  {
    "GameKey": "string",
    "Logo": "string",
    "NewMenuLabel": "string",
    "NewMenuSeparator": boolean,
    "ResourcesDefaultIniFile": "string",
    "ResourcesDefaultArtIniFile": "string",
    "ResourcesDefaultAiIniFile": "string",
    "ResourcesDescriptionIniFile": "string",
    "IniNameMatchDetection": "string",
    "SaveAsFilename": "string",
    "SaveAsRelativeToGameFolder": "string",
    "GameExecutable": "string",
    "SnippetsFolder": "string",
    "BitmapsFolders": "string",
    "SoundIni": "string",
    "MixFiles": "string",
    "UseAres": boolean,
    "UsePhobos": boolean,
    "UseVinifera": boolean,
    "UseSectionInheritance": boolean
  }
]
```

### Property Descriptions

#### GameKey (required)
- **Type:** String
- **Description:** Unique identifier for the game
- **Usage:** Referenced in code and Datastructure.json
- **Restrictions:** Cannot change for "TiberianSun", "RA2", "RA2YR" (hardcoded)
- **Example:** `"TiberianSun"`

#### Logo (required)
- **Type:** String
- **Description:** Filename of logo image
- **Location:** `Resources/Logos/`
- **Format:** PNG, 24x24 pixels recommended
- **Example:** `"ts_logo.png"`

#### NewMenuLabel (required)
- **Type:** String
- **Description:** Display name in "New" menu
- **Example:** `"Tiberian Sun"`

#### NewMenuSeparator (optional)
- **Type:** Boolean
- **Default:** `false`
- **Description:** Show separator before this menu item
- **Example:** `true`

#### ResourcesDefaultIniFile (required)
- **Type:** String
- **Description:** Default rules.ini filename
- **Location:** `Resources/`
- **Example:** `"TSrules.ini"`

#### ResourcesDefaultArtIniFile (optional)
- **Type:** String
- **Description:** Default art.ini filename
- **Location:** `Resources/`
- **Example:** `"TSart.ini"`

#### ResourcesDefaultAiIniFile (optional)
- **Type:** String
- **Description:** Default ai.ini filename
- **Location:** `Resources/`
- **Example:** `"TSai.ini"`

#### ResourcesDescriptionIniFile (optional)
- **Type:** String
- **Description:** Alternative INI for loading descriptions/comments
- **Usage:** When main INI lacks comments
- **Example:** `"TSrules.ini"`

#### IniNameMatchDetection (required)
- **Type:** String
- **Description:** Substring to match in [General]->Name for auto-detection
- **Must:** Be unique across all games
- **Example:** `"Tiberian Sun"`

#### SaveAsFilename (required)
- **Type:** String
- **Description:** Output filename when saving
- **Example:** `"rules.ini"`

#### SaveAsRelativeToGameFolder (optional)
- **Type:** String
- **Description:** Subdirectory for saving (relative to game root)
- **Default:** Root directory if not specified
- **Example:** `"INI"`

#### GameExecutable (required)
- **Type:** String
- **Description:** Main game executable filename
- **Usage:** Path validation
- **Example:** `"game.exe"`

#### SnippetsFolder (required)
- **Type:** String
- **Description:** Folder name in `Snippets/` directory
- **Must:** Exist (can be empty)
- **Example:** `"TiberianSun"`

#### BitmapsFolders (required)
- **Type:** String
- **Description:** Folder name in `Bitmaps/` directory
- **Must:** Exist (can be empty)
- **Example:** `"TiberianSun"`

#### SoundIni (optional)
- **Type:** String
- **Default:** `"sound.ini"`
- **Description:** Sound configuration filename
- **Example:** `"soundmd.ini"`

#### MixFiles (optional)
- **Type:** String (comma-separated)
- **Description:** Specific MIX files to load (in order)
- **Default:** All MIX files if not specified
- **Order:** First file with matching entry is used
- **Example:** `"cache.mix,conquer.mix,local.mix"`

#### UseAres (optional)
- **Type:** Boolean
- **Default:** `false`
- **Description:** Enable Ares engine extension support
- **Effect:** Loads DatastructureAres.json
- **Example:** `true`

#### UsePhobos (optional)
- **Type:** Boolean
- **Default:** `false`
- **Description:** Enable Phobos engine extension support
- **Effect:** Loads DatastructurePhobos.json
- **Example:** `true`

#### UseVinifera (optional)
- **Type:** Boolean
- **Default:** `false`
- **Description:** Enable Vinifera engine extension support
- **Effect:** Loads DatastructureVinifera.json
- **Example:** `true`

#### UseSectionInheritance (optional)
- **Type:** Boolean
- **Default:** `false`
- **Description:** Enable INI section inheritance
- **Example:** `true`

### Complete Example

```json
{
  "GameKey": "TiberianSun",
  "Logo": "ts_logo.png",
  "NewMenuLabel": "Tiberian Sun",
  "NewMenuSeparator": false,
  "ResourcesDefaultIniFile": "TSrules.ini",
  "ResourcesDefaultArtIniFile": "TSart.ini",
  "ResourcesDefaultAiIniFile": "TSai.ini",
  "ResourcesDescriptionIniFile": "TSrules.ini",
  "IniNameMatchDetection": "Tiberian Sun",
  "SaveAsFilename": "rules.ini",
  "SaveAsRelativeToGameFolder": "",
  "GameExecutable": "game.exe",
  "SnippetsFolder": "TiberianSun",
  "BitmapsFolders": "TiberianSun",
  "SoundIni": "sound.ini",
  "MixFiles": "cache.mix,conquer.mix,local.mix",
  "UseAres": false,
  "UsePhobos": false,
  "UseVinifera": false,
  "UseSectionInheritance": false
}
```

---

## Datastructure.json Configuration

Location: `Resources/Datastructure.json`

Defines all editable values and their properties.

### Schema

```json
{
  "AllUnits": [ /* Value definitions */ ],
  "AllMovingUnits": [ /* Value definitions */ ],
  "InfantryUnits": [ /* Value definitions */ ],
  "DrivingVehicleUnits": [ /* Value definitions */ ],
  "AircraftUnits": [ /* Value definitions */ ],
  "BuildingUnits": [ /* Value definitions */ ],
  "Weapons": [ /* Value definitions */ ],
  "Warheads": [ /* Value definitions */ ],
  "SuperWeapons": [ /* Value definitions */ ],
  "Sides": [ /* Value definitions */ ],
  "CommonGeneral": [ /* Value definitions */ ],
  "AIGeneral": [ /* Value definitions */ ],
  "TiberiumGeneral": [ /* Value definitions */ ],
  "TiberiumValues": [ /* Value definitions */ ],
  "SuperWeaponsGeneral": [ /* Value definitions */ ],
  "ValueTypes": { /* Type definitions */ },
  "NewBuilding": { /* Template */ },
  "NewInfantry": { /* Template */ },
  "NewVehicle": { /* Template */ },
  "NewAircraft": { /* Template */ }
}
```

### Value Definition Schema

```json
{
  "Key": "string",
  "Description": "string",
  "Default": "string",
  "ValueType": "string",
  "ValueList": "string",
  "LookupType": "string",
  "MultipleValues": boolean,
  "GameKeyFilter": "string",
  "Category": "string",
  "Section": "string"
}
```

### Property Descriptions

#### Key (required)
- **Type:** String
- **Description:** INI key name
- **Example:** `"Cost"`

#### Description (required for unit values, optional for common)
- **Type:** String
- **Description:** User-friendly description
- **Example:** `"The cost in credits to build this unit"`

#### Default (optional)
- **Type:** String
- **Description:** Default value for new entities
- **Default:** Empty string if not specified
- **Example:** `"1000"`

#### ValueType (optional)
- **Type:** String
- **Description:** Reference to predefined value list in ValueTypes
- **Example:** `"Integer"`

#### ValueList (optional)
- **Type:** String (comma-separated)
- **Description:** Custom list of possible values
- **Example:** `"yes,no,auto"`

#### LookupType (optional)
- **Type:** String
- **Description:** Entity type to lookup (for references)
- **Values:** `"BuildingTypes"`, `"InfantryTypes"`, `"VehicleTypes"`, `"AircraftTypes"`, `"Weapons"`, `"Warheads"`, etc.
- **Example:** `"Weapons"`

#### MultipleValues (optional)
- **Type:** Boolean
- **Default:** `false`
- **Description:** Allow multiple comma-separated values
- **Example:** `true`

#### GameKeyFilter (optional)
- **Type:** String (comma-separated)
- **Description:** Restrict to specific games
- **Example:** `"TiberianSun,RA2"`

#### Category (common values only)
- **Type:** String
- **Description:** Group header in UI
- **Example:** `"Production"`

#### Section (common values only)
- **Type:** String
- **Default:** `"General"`
- **Description:** INI section name
- **Example:** `"AudioVisual"`

### Value Definition Examples

**Simple Value:**
```json
{
  "Key": "Cost",
  "Description": "The cost in credits to build this unit",
  "Default": "1000",
  "ValueType": "Integer"
}
```

**Lookup Value:**
```json
{
  "Key": "Primary",
  "Description": "Primary weapon",
  "LookupType": "Weapons"
}
```

**Multiple Values:**
```json
{
  "Key": "Owner",
  "Description": "Which sides can build this unit",
  "LookupType": "Sides",
  "MultipleValues": true
}
```

**Custom Value List:**
```json
{
  "Key": "Armor",
  "Description": "Armor type",
  "ValueList": "none,wood,light,medium,heavy,concrete"
}
```

**Game-Specific Value:**
```json
{
  "Key": "Insignia",
  "Description": "Insignia image",
  "GameKeyFilter": "RA2,RA2YR"
}
```

**Common Value:**
```json
{
  "Key": "BuildSpeed",
  "Category": "Production",
  "Section": "General",
  "ValueType": "Float"
}
```

### ValueTypes Definition

```json
{
  "ValueTypes": {
    "Integer": "0,1,2,3,4,5,10,20,50,100,200,500,1000",
    "Float": "0.0,0.25,0.5,0.75,1.0,1.5,2.0",
    "Boolean": "yes,no",
    "Percentage": "0%,25%,50%,75%,100%,125%,150%,200%",
    "ArmorType": "none,wood,light,medium,heavy,concrete"
  }
}
```

### Section Descriptions

#### AllUnits
Values common to all unit types (buildings, infantry, vehicles, aircraft)

#### AllMovingUnits
Values specific to mobile units (infantry, vehicles, aircraft)

#### InfantryUnits
Values specific to infantry

#### DrivingVehicleUnits
Values specific to ground vehicles

#### AircraftUnits
Values specific to aircraft

#### BuildingUnits
Values specific to buildings/structures

#### Weapons
Weapon definitions

#### Warheads
Warhead definitions

#### SuperWeapons
Super weapon definitions

#### Sides
Faction/side definitions

#### CommonGeneral
General game settings

#### AIGeneral
AI-related settings

#### TiberiumGeneral
Tiberium/ore settings

#### TiberiumValues
Individual tiberium type values

#### SuperWeaponsGeneral
Super weapon system settings

### New Entity Templates

Templates for creating new entities:

```json
{
  "NewBuilding": {
    "Name": "New Building",
    "Cost": "1000",
    "Strength": "500",
    "Armor": "concrete",
    "TechLevel": "1",
    "Sight": "5",
    "Owner": "GDI,Nod",
    "Power": "0",
    "BuildCat": "Combat"
  }
}
```

---

## Aistructure.json Configuration

Location: `Resources/Aistructure.json`

Defines AI-specific values and structures.

### Schema

```json
{
  "AIScripts": [ /* Script action definitions */ ],
  "AITaskForces": [ /* Task force value definitions */ ],
  "AITriggers": [ /* Trigger value definitions */ ],
  "AITeamTypes": [ /* Team type value definitions */ ]
}
```

### Example

```json
{
  "AIScripts": [
    {
      "Key": "0",
      "Description": "Attack..."
    },
    {
      "Key": "1",
      "Description": "Attack Waypoint..."
    }
  ],
  "AITaskForces": [
    {
      "Key": "Name",
      "Description": "Task force name"
    },
    {
      "Key": "Group",
      "Description": "Unit group (count,unit)",
      "MultipleValues": true
    }
  ]
}
```

---

## Artstructure.json Configuration

Location: `Resources/Artstructure.json`

Defines art-specific values.

### Schema

```json
{
  "AllUnits": [ /* Art value definitions */ ],
  "BuildingUnits": [ /* Building art definitions */ ],
  "Animations": [ /* Animation definitions */ ]
}
```

### Example

```json
{
  "AllUnits": [
    {
      "Key": "Image",
      "Description": "Image/SHP filename"
    },
    {
      "Key": "Cameo",
      "Description": "Cameo image"
    }
  ],
  "Animations": [
    {
      "Key": "Start",
      "Description": "Starting frame",
      "ValueType": "Integer"
    },
    {
      "Key": "End",
      "Description": "Ending frame",
      "ValueType": "Integer"
    }
  ]
}
```

---

## Themes.json Configuration

Location: `Resources/Themes.json`

Defines UI themes.

### Schema

```json
[
  {
    "Key": "string",
    "Name": "string",
    "BackColor": "string",
    "ForeColor": "string",
    "ControlBackColor": "string",
    "ControlForeColor": "string",
    "BorderColor": "string",
    "HighlightColor": "string"
  }
]
```

### Example

```json
[
  {
    "Key": "Light",
    "Name": "Light Theme",
    "BackColor": "#FFFFFF",
    "ForeColor": "#000000",
    "ControlBackColor": "#F0F0F0",
    "ControlForeColor": "#000000",
    "BorderColor": "#CCCCCC",
    "HighlightColor": "#0078D7"
  },
  {
    "Key": "Dark",
    "Name": "Dark Theme",
    "BackColor": "#1E1E1E",
    "ForeColor": "#FFFFFF",
    "ControlBackColor": "#2D2D30",
    "ControlForeColor": "#FFFFFF",
    "BorderColor": "#3F3F46",
    "HighlightColor": "#007ACC"
  }
]
```

---

## User Settings

Location: `%AppData%\TiberiumSunEditor\`

### UserSettings.json

Stores user preferences:

```json
{
  "SelectedTheme": "Dark",
  "LastGameKey": "TiberianSun",
  "ShowFavoritesOnly": false,
  "WindowState": {
    "Width": 1280,
    "Height": 720,
    "Maximized": false
  }
}
```

### RecentFiles.json

Tracks recently opened files:

```json
[
  {
    "FilePath": "C:\\Games\\TS\\rules.ini",
    "GameKey": "TiberianSun",
    "LastOpened": "2024-01-15T10:30:00"
  }
]
```

### CustomGames.json

User-defined custom mods:

```json
[
  {
    "GameKey": "MyMod",
    "BasedOn": "TiberianSun",
    "GamePath": "C:\\Games\\MyMod",
    "Name": "My Custom Mod"
  }
]
```

### FavoriteUnits.json

User's favorite entities:

```json
{
  "TiberianSun": [
    "SMECH",
    "TTNK",
    "ORCA"
  ]
}
```

### FavoriteValues.json

User's favorite properties:

```json
[
  "Cost",
  "Strength",
  "Primary",
  "Speed"
]
```

---

## File Format Specifications

### INI File Format

**Structure:**
```ini
; Comment line
[SectionName]
Key=Value
Key2=Value2

[AnotherSection]
Key=Value
```

**Rules:**
- Sections enclosed in `[brackets]`
- Key-value pairs separated by `=`
- Comments start with `;`
- Case-insensitive
- Whitespace trimmed

**Special Values:**
- Boolean: `yes`, `no`, `true`, `false`, `1`, `0`
- Lists: Comma-separated values
- References: Entity keys

### MIX File Format

**Header (Unencrypted):**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 2    | uint16 | Flags (0x0000)
0x02   | 2    | uint16 | File count
0x04   | 4    | uint32 | Data size
```

**Header (Encrypted):**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 2    | uint16 | Flags (0x0001)
0x02   | 80   | byte[] | Blowfish key
0x52   | 2    | uint16 | File count
0x54   | 4    | uint32 | Data size
```

**Index Entry:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 4    | uint32 | File CRC
0x04   | 4    | uint32 | File offset
0x08   | 4    | uint32 | File size
```

### SHP File Format

**Header:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 2    | uint16 | Frame count
0x02   | 2    | uint16 | Width
0x04   | 2    | uint16 | Height
```

**Frame Header:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 2    | uint16 | X offset
0x02   | 2    | uint16 | Y offset
0x04   | 2    | uint16 | Width
0x06   | 2    | uint16 | Height
0x08   | 1    | uint8  | Compression
0x09   | 3    | byte[] | Reserved
0x0C   | 4    | uint32 | Data offset
```

### AUD File Format

**Header:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 2    | uint16 | Sample rate
0x02   | 4    | uint32 | Data size
0x06   | 4    | uint32 | Output size
0x0A   | 1    | uint8  | Flags
0x0B   | 1    | uint8  | Compression
```

**Compression Types:**
- `1` - IMA ADPCM
- `99` - Uncompressed PCM

### PAL File Format

**Structure:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 768  | byte[] | 256 RGB colors (3 bytes each)
```

**Color Entry:**
- Byte 0: Red (0-63)
- Byte 1: Green (0-63)
- Byte 2: Blue (0-63)

**Note:** Values are 6-bit (0-63), multiply by 4 for 8-bit RGB

### CSF File Format

**Header:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 4    | char[] | Signature " FSC"
0x04   | 4    | uint32 | Version
0x08   | 4    | uint32 | Label count
0x0C   | 4    | uint32 | String count
0x10   | 4    | uint32 | Reserved
0x14   | 4    | uint32 | Language
```

**Label Entry:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 4    | char[] | Signature " LBL"
0x04   | 4    | uint32 | String count
0x08   | 4    | uint32 | Label length
0x0C   | var  | char[] | Label name
```

**String Entry:**
```
Offset | Size | Type   | Description
-------|------|--------|-------------
0x00   | 4    | char[] | Signature " RTS" or "WRTS"
0x04   | 4    | uint32 | String length
0x08   | var  | wchar[]| Unicode string
```

---

## Extension Configurations

### DatastructureAres.json

Additional values for Ares engine extension.

**Structure:** Same as Datastructure.json

**Usage:** Merged with base datastructure when UseAres=true

**Example:**
```json
{
  "AllUnits": [
    {
      "Key": "Ares.CustomValue",
      "Description": "Ares-specific property"
    }
  ]
}
```

### DatastructurePhobos.json

Additional values for Phobos engine extension.

**Structure:** Same as Datastructure.json

**Usage:** Merged with base datastructure when UsePhobos=true

### DatastructureVinifera.json

Additional values for Vinifera engine extension.

**Structure:** Same as Datastructure.json

**Usage:** Merged with base datastructure when UseVinifera=true

---

## Configuration Best Practices

### Games.json

1. **Unique GameKeys** - Never duplicate
2. **Descriptive Labels** - Clear menu names
3. **Correct Paths** - Verify all file references
4. **Test Detection** - Ensure IniNameMatchDetection is unique
5. **Order MixFiles** - Most important first

### Datastructure.json

1. **Clear Descriptions** - Help users understand values
2. **Sensible Defaults** - Provide working default values
3. **Appropriate Types** - Use correct ValueType
4. **Logical Categories** - Group related values
5. **Document Custom Values** - Add comments for complex values

### Themes.json

1. **Contrast** - Ensure readable text
2. **Consistency** - Use consistent color scheme
3. **Accessibility** - Consider color-blind users
4. **Test Both** - Verify light and dark themes

---

## Validation

### JSON Validation

All JSON files must be valid JSON format:
- Proper quotes
- Correct comma placement
- Matching brackets
- No trailing commas

**Validation Tools:**
- JSONLint.com
- Visual Studio Code JSON validation
- Online JSON validators

### Testing Changes

After modifying configuration:
1. Validate JSON syntax
2. Test in editor
3. Verify all references exist
4. Check for typos
5. Test with actual game files

---

**Document Version:** 1.0  
**Last Updated:** 2025-10-09  
**For Application Version:** 2.6.60
