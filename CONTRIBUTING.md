
# 🤝 Contributing to the StoryTeller Project

Welcome, and thank you for taking the time to contribute to the StoryTeller project!  
This guide will help you understand how to work with the team smoothly and keep our codebase clean and stable.

---

## 📁 Repository Structure

Before making changes, take a moment to explore the project structure.  
Most work happens inside the `Assets/` folder in Unity.

---

## 🔀 Branching Strategy

- `master` – production-ready, stable builds only
- `dev` – integration branch (optional)
- `feature/yourname-feature` – for new features
- `bugfix/yourname-fix` – for bug fixes

### Example:
```bash
git checkout -b feature/john-new-dialog-system
```

---

## 💬 Commit Message Guidelines

Follow this format:

```
[type]: short description
```

**Types**:
- `feat`: new feature
- `fix`: bug fix
- `docs`: documentation update
- `style`: formatting, white-space only
- `refactor`: code restructuring
- `chore`: maintenance

**Example**:
```bash
git commit -m "feat: added scoring system to end screen"
```

---

## 🔁 Before You Push

1. Pull latest from `master`:
   ```bash
   git checkout master
   git pull origin master
   ```
2. Merge into your branch:
   ```bash
   git checkout feature/yourname-feature
   git merge master
   ```
3. Resolve any conflicts
4. Run and test the game in Unity
5. Commit and push your branch

---

## 📦 Making a Pull Request (PR)

1. Push your branch:
   ```bash
   git push origin feature/yourname-feature
   ```
2. Go to GitHub and click **“Compare & Pull Request”**
3. Describe your changes clearly
4. Assign Danny Nguyen to review
5. Wait for approval before merging

---

## 📐 Unity Guidelines

- Don’t commit `Library/`, `Temp/`, `Build/`, or `.csproj` files
- Avoid renaming folders or moving assets unless necessary
- Use prefabs and reusable scripts where possible
- Keep scene changes minimal and documented

---

## 📸 Assets & File Naming

- Use lowercase, hyphen-separated names (e.g., `master-menu-bg.png`)
- Organize assets into folders (`Art/`, `Audio/`, `Scripts/`, etc.)
- Large assets like `.psd` or `.wav` must be tracked with Git LFS

---

## 🙌 Need Help?

If you’re stuck:
- Ask on StoryTeller Teams Chat
- Contact the Team Lead
- Check the [CONTRIBUTING.md](CONTRIBUTING.md) and [Setup Guide.md](SETUP.md)


*Guide created by Karan Sahota (https://github.com/ksxo)*