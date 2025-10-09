# C&C Rules Editor - Quick Reference Guide

## Quick Start

### First Time Setup

1. **Download and extract** the Rules Editor
2. **Launch** `Deniz.TiberiumSunEditor.Gui.exe`
3. **Select your game:**
   - Go to `Games` menu
   - Choose your C&C game (Tiberian Sun, Red Alert 2, etc.)
   - Browse to your game installation folder
4. **Start editing:**
   - Click `File → New` to create a new rules file
   - Or `File → Open` to edit an existing rules.ini

### Opening Files

**New File:**
- `File → New → [Game Name]`
- Creates a fresh rules.ini based on game defaults

**Open Existing:**
- `File → Open` (Ctrl+O)
- Select rules.ini, art.ini, or ai.ini
- Editor auto-detects game type

**Recent Files:**
- `File → Recent Files`
- Quick access to previously opened files

---

## Main Interface

### Layout

```
┌─────────────────────────────────────────────────────┐
│  Menu Bar: File | Edit | Games | Tools | Help       │
├─────────────────────────────────────────────────────┤
│  Toolbar: [Open] [Save] [New] [Search] [Filter]    │
├──────────────┬──────────────────────────────────────┤
│              │                                      │
│  Entity List │  Entity Editor                       │
│              │                                      │
│  - Buildings │  Name: [Wolverine]                   │
│  - Infantry  │  Cost: [500]                         │
│  - Vehicles  │  Primary: [20mm]                     │
│  - Aircraft  │  Strength: [200]                     │
│  - Weapons   │  ...                                 │
│              │                                      │
│  [Search]    │  [Cameo Image]                       │
│  [☆ Favs]    │  [Play Animation] [Play Sound]       │
│              │                                      │
└──────────────┴──────────────────────────────────────┘
```

### Tabs

- **Rules** - Main game rules (units, buildings, weapons)
- **Art** - Visual properties (animations, cameos)
- **AI** - Artificial intelligence settings
- **Common** - General game settings

---

## Editing Entities

### Selecting an Entity

1. **Browse the list** on the left
2. **Click** on any unit/building/weapon
3. **Properties appear** on the right

### Modifying Values

**Text Fields:**
- Click in the field
- Type new value
- Press Enter or Tab

**Dropdowns:**
- Click dropdown arrow
- Select from list
- Or type to search

**Lookups (References):**
- Click `[...]` button
- Select from entity list
- Or type entity name

**Colors:**
- Click color box
- Use color picker
- Or enter RGB values

### Common Properties

**All Units:**
- `Name` - Display name
- `Cost` - Build cost in credits
- `Strength` - Hit points
- `Armor` - Armor type
- `Primary` - Primary weapon
- `Secondary` - Secondary weapon
- `Speed` - Movement speed
- `Owner` - Which sides can build it

**Buildings:**
- `Power` - Power generation/consumption
- `BuildCat` - Build category
- `Adjacent` - Adjacency bonus
- `Factory` - Production type

**Weapons:**
- `Damage` - Base damage
- `ROF` - Rate of fire
- `Range` - Attack range
- `Projectile` - Projectile type
- `Warhead` - Warhead type

**Warheads:**
- `Verses` - Armor effectiveness
- `InfDeath` - Infantry death animation
- `Spread` - Splash damage radius

---

## Creating New Units

### Method 1: Add Empty

1. Click `Edit → Add Empty Unit`
2. Select unit type (Building, Infantry, Vehicle, Aircraft)
3. Enter unique ID (e.g., "MYUNIT")
4. Enter display name
5. Click OK
6. Edit properties manually

### Method 2: Copy Existing

1. Select a unit to copy
2. Click `Edit → Create Copy`
3. Enter new ID
4. Enter new name
5. Click OK
6. Modify as needed

### Method 3: Insert Snippet

1. Click `Edit → Insert Snippet`
2. Browse snippet categories
3. Select pre-configured unit
4. Enter unique ID
5. Click OK
6. Customize values

### Best Practices

- **Use descriptive IDs** (e.g., "HTANK" not "UNIT1")
- **Keep IDs short** (max 24 characters)
- **No spaces in IDs** (use underscores if needed)
- **Start with letter** (not number)
- **Check for conflicts** (ID must be unique)

---

## Working with Weapons

### Creating a Weapon

1. Go to **Weapons** tab
2. Click `Add Empty Unit`
3. Enter weapon ID (e.g., "MyGun")
4. Set properties:
   - `Damage` - How much damage
   - `ROF` - How fast it fires (lower = faster)
   - `Range` - How far it shoots
   - `Projectile` - What it looks like
   - `Warhead` - What damage type
   - `Speed` - Projectile speed

### Creating a Warhead

1. Go to **Warheads** tab
2. Click `Add Empty Unit`
3. Enter warhead ID (e.g., "MyWarhead")
4. Set properties:
   - `Verses` - Damage vs armor types (comma-separated percentages)
   - `Spread` - Splash damage radius
   - `Wall` - Can destroy walls
   - `Wood` - Can destroy trees

### Assigning to Unit

1. Select your unit
2. Find `Primary` or `Secondary` property
3. Click dropdown
4. Select your weapon
5. Save

---

## Balancing Tools

### Balance Analyzer

1. Click `Tools → Balance Tool`
2. Select units to compare
3. View statistics:
   - Cost efficiency
   - Damage per second
   - Health to cost ratio
   - Speed comparison
4. Export report

### Value Comparison

1. Select first unit
2. Click `Tools → Compare Values`
3. Select second unit
4. View side-by-side comparison
5. Identify differences

---

## Favorites System

### Marking Favorites

**Favorite Units:**
- Right-click unit in list
- Select "Add to Favorites"
- Star icon appears

**Favorite Values:**
- Right-click property name
- Select "Add to Favorites"
- Property highlighted

### Using Favorites

**Filter by Favorites:**
- Click ☆ button in toolbar
- Only favorite units shown

**Show Favorite Values:**
- Click "Show Only Favorites" in value grid
- Only marked properties shown

---

## Search and Filter

### Searching Units

1. Type in search box (top of entity list)
2. Results filter in real-time
3. Searches:
   - Unit ID
   - Display name
   - Description

### Advanced Filtering

**By Type:**
- Select tab (Buildings, Infantry, etc.)

**By Parent:**
- Use "Filter by Parent" dropdown
- Shows only units inheriting from selected parent

**By Side:**
- Filter by faction/side
- Shows only units buildable by that side

---

## Saving Your Work

### Save to File

**Save:**
- `File → Save` (Ctrl+S)
- Overwrites current file

**Save As:**
- `File → Save As` (Ctrl+Shift+S)
- Choose new location/name

### Save to Game

**Direct Save:**
- `File → Save in Game`
- Saves directly to game folder
- Automatically uses correct filename
- Game will use modified rules

**Manual Save:**
- Save to any location
- Copy rules.ini to game folder manually

### Multiplayer

To play modified games online:
1. Save your rules.ini
2. Share file with all players
3. All players copy to game folder
4. Everyone must have identical rules.ini

---

## Game Assets

### Viewing Cameos

- Select any unit
- Cameo appears in editor (if available)
- Loaded from game's MIX files

### Playing Animations

1. Select unit
2. Click `[Play Animation]` button
3. Animation plays in popup window
4. Shows unit's SHP file

### Playing Sounds

1. Select unit
2. Click `[Play Sound]` button
3. Sound plays from game files
4. Uses Sound.ini references

### MIX Browser

1. Click `Tools → MIX Browser`
2. Browse game archives
3. Preview files:
   - Images (SHP, PCX)
   - Sounds (AUD, WAV)
   - Text (INI, TXT)
4. Extract files if needed

---

## Themes

### Switching Themes

1. Click `View → Theme`
2. Select:
   - Light (default)
   - Dark
   - Custom themes

### Theme Persistence

- Selected theme saved automatically
- Applies on next launch

---

## Keyboard Shortcuts

### File Operations
- `Ctrl+N` - New file
- `Ctrl+O` - Open file
- `Ctrl+S` - Save file
- `Ctrl+Shift+S` - Save as

### Editing
- `Ctrl+C` - Copy entity
- `Ctrl+V` - Paste entity
- `Ctrl+X` - Cut entity
- `Delete` - Delete entity
- `Ctrl+Z` - Undo (if supported)

### Navigation
- `Ctrl+F` - Search
- `F5` - Refresh
- `Tab` - Next field
- `Shift+Tab` - Previous field

### View
- `Ctrl+1` - Rules tab
- `Ctrl+2` - Art tab
- `Ctrl+3` - AI tab
- `Ctrl+4` - Common tab

---

## Tips and Tricks

### Productivity Tips

1. **Use Snippets** for common units
2. **Mark Favorites** for quick access
3. **Use Search** instead of scrolling
4. **Copy Similar Units** instead of starting from scratch
5. **Save Often** to prevent data loss

### Modding Tips

1. **Test Incrementally** - Make small changes and test
2. **Keep Backups** - Save original rules.ini
3. **Document Changes** - Use comments in INI
4. **Balance Carefully** - Use balance tool
5. **Check Dependencies** - Ensure referenced entities exist

### Performance Tips

1. **Close Unused Tabs** to save memory
2. **Clear Cache** if experiencing slowdowns
3. **Limit MIX Files** if loading is slow
4. **Disable Animations** if not needed

---

## Common Tasks

### Making a Unit Stronger

1. Select unit
2. Increase `Strength` (health)
3. Increase `Armor` (damage resistance)
4. Increase weapon `Damage`
5. Decrease weapon `ROF` (faster firing)
6. Save and test

### Making a Unit Faster

1. Select unit
2. Increase `Speed` value
3. For aircraft, increase `PitchSpeed` and `PitchAngle`
4. Save and test

### Making a Unit Cheaper

1. Select unit
2. Decrease `Cost`
3. Optionally decrease `BuildTime`
4. Save and test

### Adding a Unit to a Side

1. Select unit
2. Find `Owner` property
3. Add side name to list (comma-separated)
4. Example: `GDI,Nod,Mutants`
5. Save

### Creating a Super Weapon

1. Go to **SuperWeapons** tab
2. Copy existing super weapon
3. Modify properties:
   - `Type` - Effect type
   - `Action` - What it does
   - `RechargeTime` - Cooldown
   - `SidebarImage` - Icon
4. Add to side's `SuperWeapons` list
5. Save

---

## Troubleshooting

### Unit Not Appearing in Game

**Check:**
- [ ] Unit has unique ID
- [ ] Unit added to appropriate list (BuildingTypes, InfantryTypes, etc.)
- [ ] Unit has `Owner` set to your side
- [ ] Unit has `Prerequisite` requirements met
- [ ] Unit has valid `BuildCat` (for buildings)
- [ ] File saved to correct game folder

### Weapon Not Working

**Check:**
- [ ] Weapon has valid `Projectile`
- [ ] Weapon has valid `Warhead`
- [ ] Weapon `Range` is greater than 0
- [ ] Weapon `Damage` is greater than 0
- [ ] Unit has weapon assigned to `Primary` or `Secondary`

### Game Crashes on Load

**Possible Causes:**
- Invalid INI syntax
- Missing required values
- Circular references
- Invalid file references

**Solutions:**
- Validate INI file
- Check for typos
- Restore from backup
- Start with minimal changes

### Cameos Not Showing

**Check:**
- [ ] Game path is set correctly
- [ ] MIX files are loading
- [ ] Cameo exists in game files
- [ ] Art.ini has correct cameo reference

---

## Advanced Features

### Section Inheritance

Some mods support inheritance:

```ini
[BaseUnit]
Cost=500
Strength=200

[MyUnit]
; Inherits from BaseUnit
Cost=600  ; Override
; Strength=200 (inherited)
```

Enable in game settings if supported.

### Ares/Phobos Support

For mods using engine extensions:

1. Open game settings
2. Check "Use Ares" or "Use Phobos"
3. Additional properties become available
4. See extension documentation for details

### Custom Mods

To add a custom mod:

1. Click `Games → Add Custom Mod`
2. Enter mod name
3. Select base game
4. Browse to mod folder
5. Configure settings
6. Click OK

---

## File Formats

### Rules.ini

Main game rules file:
- Unit definitions
- Weapon definitions
- Warhead definitions
- General settings

### Art.ini

Visual properties:
- Cameo references
- Animation sequences
- Visual effects
- Palette settings

### AI.ini

Artificial intelligence:
- AI scripts
- Task forces
- Triggers
- Team types

### Sound.ini

Audio references:
- Sound effects
- Music tracks
- Voice lines

---

## Getting Help

### In-App Help

- `Help → About` - Version info
- `Help → Documentation` - This guide
- `Help → Check for Updates` - Update checker

### Online Resources

- **Homepage:** https://ruleseditor.denizesen.ch
- **GitHub:** https://github.com/otherdeniz/CnC_RulesEditor
- **Forum:** https://forums.cncnet.org/topic/12869-tiberian-sun-rules-editor-version-2024/

### Reporting Issues

When reporting bugs, include:
- Application version
- Operating system
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable

---

## Glossary

**Entity** - Any game object (unit, building, weapon, etc.)

**Cameo** - Small icon image representing a unit

**MIX File** - Archive file containing game assets

**SHP File** - Sprite/animation file format

**INI File** - Configuration file with sections and key-value pairs

**Lookup** - Reference to another entity

**Snippet** - Pre-configured template for quick unit creation

**Warhead** - Defines damage type and effects

**Projectile** - Visual representation of weapon fire

**ROF** - Rate of Fire (lower = faster)

**Verses** - Damage multipliers against armor types

**Prerequisite** - Required building to unlock unit

**BuildCat** - Build category for construction queue

---

## Quick Reference Tables

### Common Value Ranges

| Property | Typical Range | Notes |
|----------|---------------|-------|
| Cost | 100-5000 | Credits to build |
| Strength | 50-2000 | Hit points |
| Speed | 4-12 | Movement speed |
| ROF | 10-100 | Rate of fire (lower = faster) |
| Range | 1-20 | Attack range (cells) |
| Damage | 10-500 | Base damage |

### Armor Types

| Type | Description |
|------|-------------|
| none | No armor |
| wood | Light armor |
| light | Light vehicle armor |
| medium | Medium armor |
| heavy | Heavy armor |
| concrete | Building armor |

### Boolean Values

| Value | Meaning |
|-------|---------|
| yes | True/Enabled |
| no | False/Disabled |
| true | True/Enabled |
| false | False/Disabled |
| 1 | True/Enabled |
| 0 | False/Disabled |

---

**Document Version:** 1.0  
**Last Updated:** 2025-10-09  
**For Application Version:** 2.6.60
