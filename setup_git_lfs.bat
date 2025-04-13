@echo off
echo Initializing Git LFS...

:: Check if git is installed
where git >nul 2>&1
IF %ERRORLEVEL% NEQ 0 (
    echo Git is not installed or not in PATH.
    pause
    exit /b 1
)

:: Initialize Git LFS
git lfs install

:: Track common Unity asset types
git lfs track "*.fbx"
git lfs track "*.png"
git lfs track "*.jpg"
git lfs track "*.tga"
git lfs track "*.wav"
git lfs track "*.mp3"
git lfs track "*.mp4"
git lfs track "*.psd"
git lfs track "*.anim"
git lfs track "*.prefab"
git lfs track "*.unity"

:: Commit the .gitattributes file (only works if not yet committed)
git add .gitattributes

echo Git LFS has been initialized and asset types tracked.
echo You should now commit the updated .gitattributes file if it's your first time.
pause
