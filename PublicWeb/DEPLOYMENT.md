# Quick Deployment Guide

## Option 1: Copy Screenshots First (Recommended)

1. Run the batch script to copy screenshots:
   ```
   cd PublicWeb
   copy-screenshots.bat
   ```

2. Open `index.html` in your browser to preview the site

## Option 2: Manual Screenshot Copy

1. Copy all PNG files from `Screenshots/` folder to `PublicWeb/images/`
2. Open `index.html` in your browser

## Option 3: Deploy Without Screenshots

The website will work without screenshots, but image links will be broken.
You can add screenshots later.

## Deploy to GitHub Pages

1. Commit the PublicWeb folder to your repository:
   ```bash
   git add PublicWeb/
   git commit -m "Add static website"
   git push
   ```

2. Go to your repository on GitHub
3. Click Settings → Pages
4. Select your branch and `/PublicWeb` folder
5. Click Save
6. Your site will be live at: `https://yourusername.github.io/repository-name/`

## Deploy to Netlify

1. Drag and drop the `PublicWeb` folder to Netlify
2. Or connect your GitHub repository
3. Set build directory to `PublicWeb`
4. Deploy!

## Deploy to Any Web Server

1. Upload all files from `PublicWeb/` to your web server
2. Maintain the folder structure
3. Set `index.html` as the default document
4. Done!

## Local Testing

Simply open `PublicWeb/index.html` in any web browser.
No web server required for local testing.

## Troubleshooting

**Images not showing:**
- Run `copy-screenshots.bat` to copy images
- Or manually copy PNG files from `Screenshots/` to `PublicWeb/images/`

**Styles not loading:**
- Check that `css/style.css` exists
- Verify file paths are correct
- Clear browser cache

**Links not working:**
- All internal links use relative paths
- External links require internet connection
- Check that all HTML files are in the same directory

## Next Steps

1. Copy screenshots using the provided batch script
2. Test the website locally by opening index.html
3. Customize colors and content if needed
4. Deploy to your preferred hosting platform
5. Share the URL with your community!
