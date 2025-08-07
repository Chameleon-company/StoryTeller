
# Team Setup Guide – StoryTeller Project

This guide helps all team members set up their development environment for the StoryTeller game project using **Unity**, **Git**, and **GitHub**.

---

## ✅ 1. GitHub Account Setup

1. Go to [https://github.com](https://github.com)
2. Click **Sign Up**
3. Fill in your details and verify your email
4. Add your GitHub username into this form [GITHUB Usernames](https://deakin365.sharepoint.com/:x:/r/sites/Chameleon2/Shared%20Documents/The%20StoryTeller/GITHUB%20Usernames.xlsx?d=w5edb10fee0fa46acb34b0b973c779180&csf=1&web=1&e=4NV8Xf) and wait to be addeed to The Project Repo.

---

## ✅ 2. Install Git

### Windows / Mac / Linux:
- Download Git: [https://git-scm.com/downloads](https://git-scm.com/downloads)
- Install with default settings

### Test Installation:
```bash
git --version
```

> You should see something like `git version 2.x.x`

---

## ✅ 3. (Optional) Install GitHub Desktop

If you're not comfortable with the command line:
- Download: [https://desktop.github.com/](https://desktop.github.com/)
- Log in with your GitHub account
- You can clone, commit, push, and pull with a GUI

---

## ✅ 4. Install Unity & Unity Hub

1. Download Unity Hub: [https://unity.com/download](https://unity.com/download)
2. Install and sign in with your Unity account
3. Under the **Installs** tab, install the version used in the project (confirm with the team)
4. Add necessary modules if needed (Windows Build Support, WebGL, Android, etc.)

---

## ✅ 5. Clone the GitHub Project

### Option A: GitHub Desktop
1. Open GitHub Desktop
2. Click **“Clone a repository”**
3. Paste the repo URL:  
   `https://github.com/Chameleon-company/StoryTeller.git`
4. Choose a folder to store the project
5. Click **Clone**

### Option B: Git (Command Line)
```bash
# Open Git Bash
git config --global user.name "YOUR NAME"
git config --global user.email "YOUR_EMAIL"
#Git Repo Commands
git clone https://github.com/Chameleon-company/StoryTeller.git
cd storyteller
git lfs install #for LFS support
```

---

## ✅ 6. Open the Project in Unity

1. Open Unity Hub
2. Click **“Open Project”**
3. Navigate to the cloned project folder
4. Select it and wait for Unity to load it
5. Let Unity import assets (first time may take a while)

---

## ✅ 7. Connect Unity to Visual Studio Code

1. Open Unity
2. Go to `Edit > Preferences > External Tools`
3. Under **External Script Editor**, select `Visual Studio Code`
4. If it's not listed, click **Browse...** and locate `Code.exe` manually
5. Ensure the following checkboxes are enabled:
   - Embedded packages
   - Local packages
   - Registry packages

6. In VS Code, install the **C# Dev Kit** and **Unity Tools** extensions from the Extensions Marketplace

---

## ✅ 8. Git LFS Setup (One-Time Only)

We will use Git LFS to handle large files like images, audio, and 3D models.

### Step 1: Install Git LFS
```bash
git lfs install
```

### Step 2: Verify LFS is Tracking Correct File Types
Git LFS should automatically track `.psd`, `.png`, `.wav`, `.fbx`, etc. via `.gitattributes`.

---

## ✅ 9. Branching Workflow

> Always work on your **own branch**, not directly on `master`.

### Create a new branch:
```bash
git checkout -b feature/yourname-feature-detail
```

### Push your changes:
```bash
git add .
git commit -m "Added feature X"
git push origin feature/yourname-feature-detail
```

Then go to GitHub / GitHub Desktop and open a **Pull Request** (PR) to merge it into `master`.

---

## ✅ 10. Syncing with the Team

Before starting new work:
```bash
git checkout master
git pull origin master
```

Switch back to your branch:
```bash
git checkout feature/yourname-feature-detail
git merge master
```

> 🔄 Do this regularly to avoid merge conflicts.

---

## ✅ 11. Unity Project Rules

- Don’t commit `/Library`, `/Temp`, or `*.csproj` files
- Avoid renaming folders unless necessary (can break Unity references)
- Always pull before starting new work
- Push regularly to avoid losing work

---

## ✅ 12. Troubleshooting

| Issue | Solution |
|-------|----------|
| Project not opening in Unity | Make sure you're using the correct Unity version |
| Merge conflicts | Talk to the teammate involved and resolve conflicts manually |
| Git LFS errors | Run `git lfs install` again, or check if file types are tracked |
| Assets missing | Check if `.gitignore` mistakenly excludes them or they weren't committed |

---

## 📞 Contact & Support

If you get stuck, reach out on the main StoryTeller Teams Group Chat.

- **GitHub Admin**: Danny Nguyen
- **GitHub/Unity Setup Support**: Karan Sahota
- **Unity Support**: TBA


*Guide created by Karan Sahota (https://github.com/ksxo)*
