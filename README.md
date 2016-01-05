# Photogram
An ASP.NET based portal system for photographers, artists.

## Implemented features
- Image uploading
- Projects
  - The projects can be sorted in any permutations.
  - The contents of the projects can be also sorted.
- Multilingual contents
  - automatic language selection based on browser-data and manual options
    - now English and Hungarian available
- About and contact sections on home page

## Planned features
- Audio/video uploading
- YouTube, Vimeo, tumblr content linking to projects
- Customer accounts with galleries. The contents can be selected and reviewed before purchase.


## Changelog

### 0.6
- Bugfixes
- Indicators added to carousel, bigger arrows to better visibility.
- Minor changes on details page.

### 0.5
Azure App Insights tracking code added to public layout.

### 0.4.1
Background images are cropped to match their container elements' size on the server to get an image that exactly sized to the visible space.

### 0.4
Lazy image loading implemented with bLazy.js lib. I modified bLazy to support the server's dynamic image resizing feature.

### 0.3.2
The items on project details page ordered by include position.

### 0.3.1.1
Admin pages also modified according to new image resizing mechanism.

### 0.3.1
Image resizing redesigned. ImageResizer plugin now used as a HttpModule. Jpg downsizing is working now.

### 0.3
Downsized images displayed instead of the originals, the only little hitch is that somehow the pics are converting to png, so their size is more than the originals.

### 0.2.1
Language attribute added to the layout pages.

### 0.1.0
First published version.

### 0.2
Manual language selection is available.



