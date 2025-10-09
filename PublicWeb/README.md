# C&C Rules Editor - Static Website

This folder contains a complete static HTML website for the C&C Rules Editor, cloned from https://ruleseditor.denizesen.ch/ and updated with content from all documentation files.

## Structure

```
PublicWeb/
├── index.html          # Homepage with project overview
├── features.html       # Detailed features page
├── tutorial.html       # Beginner tutorial
├── download.html       # Download and installation guide
├── css/
│   └── style.css      # Main stylesheet with responsive design
├── images/            # Screenshots and images
│   └── README.md      # Instructions for adding images
└── README.md          # This file
```

## Features

- **Responsive Design**: Works on desktop, tablet, and mobile devices
- **Modern UI**: Clean, professional design matching the original website
- **Complete Content**: All information from documentation files integrated
- **SEO Optimized**: Proper meta tags and semantic HTML
- **Fast Loading**: Minimal dependencies, pure HTML/CSS

## Pages

### 1. Homepage (index.html)
- Hero section with call-to-action buttons
- Project overview and key capabilities
- Feature highlights grid
- User testimonials
- Technical details
- Footer with links and resources

### 2. Features (features.html)
- Comprehensive feature list from DOCUMENTATION.md
- Entity editing capabilities
- Visual asset integration
- Advanced features (Balance Tool, Snippets, MIX Browser)
- Mod support (Ares, Phobos, Vinifera)
- User experience features
- File format support

### 3. Tutorial (tutorial.html)
- Step-by-step beginner guide from QUICK_REFERENCE.md
- Getting started instructions
- File structure basics
- Creating and editing units
- Working with weapons and warheads
- Saving and testing
- Tips and tricks

### 4. Download (download.html)
- Latest release information
- System requirements
- Installation instructions
- What's included
- Supported games
- Auto-update feature
- Getting help and support
- License information

## Content Sources

All content has been extracted and integrated from the following documentation files:

- **README.md** - Project overview and basic information
- **DOCUMENTATION.md** - Comprehensive technical documentation and features
- **QUICK_REFERENCE.md** - Quick start guide and tutorials
- **DEVELOPER_GUIDE.md** - Development information
- **CONFIGURATION_REFERENCE.md** - Configuration details
- **ARCHITECTURE_DIAGRAMS.md** - Architecture information

## Deployment

### Local Testing

1. Open `index.html` in a web browser
2. Navigate through the pages using the menu
3. All pages are fully functional without a web server

### Web Hosting

Upload all files to your web server maintaining the folder structure:

**Static Hosting (Recommended):**
- GitHub Pages
- Netlify
- Vercel
- AWS S3 + CloudFront
- Azure Static Web Apps

**Traditional Hosting:**
- Any web server (Apache, Nginx, IIS)
- Simply upload the PublicWeb folder contents to your web root

### GitHub Pages Deployment

1. Push the PublicWeb folder to your GitHub repository
2. Go to Settings → Pages
3. Select the branch and `/PublicWeb` folder
4. Save and wait for deployment
5. Your site will be available at `https://yourusername.github.io/repository-name/`

## Customization

### Colors and Styling

Edit `css/style.css` to customize:
- Color scheme (CSS variables at the top)
- Fonts and typography
- Layout and spacing
- Responsive breakpoints

### Content

Edit the HTML files directly to:
- Update text and descriptions
- Add or remove sections
- Modify links and URLs
- Change images

### Images

Add screenshots to the `images/` folder:
1. Copy screenshots from the main project's `Screenshots` folder
2. Or download from GitHub repository
3. Update image paths in HTML if needed

## Browser Support

- Chrome/Edge (latest)
- Firefox (latest)
- Safari (latest)
- Opera (latest)
- Mobile browsers (iOS Safari, Chrome Mobile)

## Performance

- No external dependencies
- Pure HTML/CSS (no JavaScript required)
- Optimized for fast loading
- Mobile-friendly responsive design
- Semantic HTML for accessibility

## License

This website content is part of the C&C Rules Editor project and is licensed under GNU General Public License v3.0.

## Credits

- **Original Website**: https://ruleseditor.denizesen.ch/
- **Author**: Deniz 'otherdeniz' Esen ([WH]Otherdeniz)
- **Project**: https://github.com/otherdeniz/CnC_RulesEditor

## Notes

- The website is fully functional and ready to deploy
- Screenshots should be added to the `images/` folder for complete functionality
- All links to external resources (GitHub, Discord, CNCNet) are included
- The design closely matches the original website while being fully static
- Content is comprehensive and includes all information from documentation files
