# Game Dev Workshop

A Unity starting point for a game development workshop — designed for children learning to program with the help of AI tools.

## What you'll build

In this workshop you'll modify a simple 3D scene containing a very basic car and a cube, and use AI to help you add new features, for example making the car move, changing colours, adding obstacles, but you can take this in any direction you like!

---

## Setup (do this before the workshop)

### 1. Install Unity Hub
Download from [unity.com/download](https://unity.com/download) and create a free Unity account.

### 2. Install Unity 6
Open Unity Hub → **Installs** → **Install Editor** → choose version **6000.3.9f1**.  
(If that exact version isn't listed, install the closest Unity 6 LTS release.)

### 3. Open the project
In Unity Hub → **Projects** → **Open** → select this folder.  
Unity will import the project the first time, which can take a few minutes.

### 4. Open the scene
In the **Project** window, navigate to `Assets/Scenes` and double-click **SampleScene**.  
Press the **Play ▶** button to see the car in action.

---

## Project layout

| Folder | What's in it |
|---|---|
| `Assets/Scenes/` | The main scene (`SampleScene.unity`) |
| `Assets/Behaviours/` | C# scripts you'll write and modify |
| `Assets/Prefabs/Car/` | The car and wheel objects |
| `Assets/Materials/` | Coloured materials (Black, Blue, Green, Red, Yellow) |
| `Assets/EditModeTests/` | Unit tests that run in the editor |
| `Assets/PlayModeTests/` | Tests that run while the game is playing |

---

## Running tests

Open **Window → General → Test Runner**, then click **Run All**.  
Both Edit Mode and Play Mode tests should pass before you start making changes.

---

## Using AI in this workshop

You can ask an AI assistant (such as Claude) to help you write and understand code.  
Good things to ask:

- *"What does this line of code do?"*
- *"How do I make a GameObject move forward in Unity?"*
- *"Can you add a score counter to this script?"*
- *"Why is my script not working? Here's the error message: …"*

Paste error messages directly into the chat — AI is great at explaining them.
