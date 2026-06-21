# Game Dev Workshop — Computer Setup Guide

Welcome! Before the workshop, each computer needs a few free programs installed. This guide walks you through everything, step by step. No prior experience needed.

Set aside about **45–60 minutes** — Unity in particular is a large download, so start early and make sure you have a good internet connection.

> **Tip:** Do the installs in the order listed below. Unity is the biggest download, so kick it off first and let it run while you install the smaller tools.

---

## What you'll be installing

| # | Software | What it's for | Approx. download |
|---|----------|---------------|------------------|
| 1 | **Unity Hub** | Manages Unity versions and projects | ~150 MB |
| 2 | **Unity Editor 6000.3.9f1** | The actual game engine we build in | ~5–8 GB |
| 3 | **GitHub Desktop** | Downloads and saves our project code (Git made easy) | ~150 MB |
| 4 | **Visual Studio 2022 (Community)** | View and edit the game's code files | ~2–4 GB |
| 5 | **Claude Code (Desktop app)** | AI assistant that helps write code | ~200 MB |

**Before you start, you'll need:**
- A computer running **Windows 10 or 11**.
- At least **20 GB of free disk space**.
- A **GitHub account** (free) — sign up at [github.com](https://github.com/join).
- A **Unity account** (free) — you'll be prompted to create one when you open Unity Hub.
- A **Claude account** — sign in when prompted in the Claude app.

---

## 1. Unity Hub

Unity Hub is the launcher that keeps track of which version of Unity you have and which projects you're working on.

1. Go to **[unity.com/download](https://unity.com/download)**.
2. Click **Download for Windows**.
3. Run the downloaded installer and follow the prompts (keep all the default options).
4. Open **Unity Hub** once it finishes installing.
5. When asked, **sign in or create a free Unity account**. A personal/free licence is all we need.

---

## 2. Unity Editor — version 6000.3.9f1

> ⚠️ **The version matters.** This workshop uses **Unity 6000.3.9f1** exactly. A different version may open the project incorrectly or show errors. Please install this specific version.

The easiest way to get the exact version:

1. Open this link in your browser: **[Unity 6000.3.9f1 download (Unity Hub link)](unityhub://6000.3.9f1)**
   - This should pop open Unity Hub and offer to install version 6000.3.9f1. Click **Install**.
2. **If that link doesn't work**, install it manually inside Unity Hub:
   - Open Unity Hub → click **Installs** on the left → click **Install Editor**.
   - Look under the **Archive** tab, or visit the **[Unity Download Archive](https://unity.com/releases/editor/archive)** and find **6000.3.9f1**, then click its **Unity Hub** button.
3. When asked which **modules/components** to add, make sure these are ticked:
   - **Microsoft Visual Studio** *(optional — we install it separately in step 4; you can untick if it's there)*
   - **Windows Build Support (IL2CPP)**
   - **Documentation** (optional but handy).
4. Click **Continue / Install** and wait. **This is the big download** — it can take 20–40 minutes depending on your connection.

---

## 3. GitHub Desktop

This is how we'll download the workshop project and save (commit) our changes without typing Git commands.

### Install and sign in

1. Go to **[desktop.github.com](https://desktop.github.com)**.
2. Click **Download** and run the installer.
3. Open **GitHub Desktop** and **sign in with your GitHub account** when prompted.
4. Finish the short setup (it may ask for your name and email — the defaults are fine).

### Clone (download) the workshop project

"Cloning" makes your own copy of the project on this computer.

1. In GitHub Desktop, go to **File → Clone repository** (or click **Clone a repository from the Internet…** on the welcome screen).
2. Click the **URL** tab.
3. Paste this address into the box:
   ```
   https://github.com/kiml42/Game-Dev-Workshop.git
   ```
4. Choose where to save it on your computer (the default **Local path** is fine — make a note of it, you'll need it in Unity).
5. Click **Clone** and wait for the download to finish.

Once it's done, the project files are on your computer and ready to open in Unity (see the test in the checklist below).

---

## 4. Visual Studio 2022 (Community Edition)

Visual Studio lets us open, read, and edit the game's code files (C# scripts).

> **Note:** This is **Visual Studio**, *not* "Visual Studio Code" — they're different programs. We want the full **Visual Studio 2022 Community** (it's free).

1. Go to **[visualstudio.microsoft.com/downloads](https://visualstudio.microsoft.com/downloads/)**.
2. Under **Visual Studio 2022 Community**, click **Free download**.
3. Run the installer. When it shows the list of **Workloads**, tick:
   - ✅ **Game development with Unity**
   - ✅ **.NET desktop development**
4. Click **Install** and wait for it to finish (this is another large download).

---

## 5. Claude Code (Desktop app)

Claude Code is the AI assistant that helps us write and understand code during the workshop.

1. Go to **[claude.com/claude-code](https://claude.com/claude-code)** (or **[claude.ai/download](https://claude.ai/download)** for the desktop app).
2. Download and install the **desktop app** for your operating system.
3. Open the app and **sign in with your Claude account**.
4. When you first use it, point it at the workshop project folder (we'll do this together).

---

## ✅ Grown-up helper checklist — confirm everything works

A parent or other adult helper should run through these checks **before the workshop day** to make sure the computer is ready. Each step should "just work" — if one doesn't, see the troubleshooting notes below.

Tick each box once confirmed:

- [ ] **1. Unity Hub opens** and you are **signed in** (your name/initials show in the top corner).
- [ ] **2. Unity 6000.3.9f1 is installed** — in Unity Hub, click **Installs**; you should see **6000.3.9f1** in the list. *(The version number must match exactly.)*
- [ ] **3. GitHub Desktop opens** and shows you are **signed in** to a GitHub account (File → Options → Accounts).
- [ ] **4. Visual Studio 2022 opens** — launch it once and let it finish any first-time setup. You don't need to create a project; just confirm it opens to the start screen.
- [ ] **5. Claude Code app opens** and you are **signed in** to a Claude account.
- [ ] **6. Disk space** — confirm there's still at least **5 GB free** after everything is installed.
- [ ] **7. Test the full chain (optional but recommended):**
  - In GitHub Desktop, clone the workshop project as described in step 3 above (URL: `https://github.com/kiml42/Game-Dev-Workshop.git`).
  - Open Unity Hub → **Open** → select the cloned project folder (the **Local path** you chose when cloning).
  - Unity should open the project with **no red errors** in the bottom bar. The first open may take several minutes while Unity prepares the project — that's normal.
  - Press the **Play** button at the top of Unity. If a small scene appears and runs, everything is working! 🎉

### Troubleshooting

| Problem | What to try |
|---------|-------------|
| Unity Hub won't install the editor version | Use the **[Unity Download Archive](https://unity.com/releases/editor/archive)** and click the **Unity Hub** button next to **6000.3.9f1**. |
| Unity shows the wrong version when opening the project | Install the exact version **6000.3.9f1** (step 2). Don't let Unity "upgrade" the project to another version. |
| Can't sign in to GitHub / Unity / Claude | Double-check the email and password; use the "forgot password" link if needed. A free account is sufficient for all three. |
| Visual Studio install is huge / slow | It's normal — make sure only the two workloads in step 4 are ticked to keep it as small as possible. |
| Project opens with red errors | Confirm the Unity version is exactly **6000.3.9f1**, then close and reopen the project. If errors remain, note them down to show the instructor. |
| Not enough disk space | Free up space, or check with the instructor — the Unity install alone needs several GB. |

---

## Quick reference — all the download links

- Unity Hub & Editor: **[unity.com/download](https://unity.com/download)** · Archive: **[unity.com/releases/editor/archive](https://unity.com/releases/editor/archive)**
- GitHub Desktop: **[desktop.github.com](https://desktop.github.com)**
- Visual Studio 2022 Community: **[visualstudio.microsoft.com/downloads](https://visualstudio.microsoft.com/downloads/)**
- Claude Code: **[claude.com/claude-code](https://claude.com/claude-code)**

**Required Unity version: `6000.3.9f1`** — please double-check this one!

See you at the workshop! 🎮
