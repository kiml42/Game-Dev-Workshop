# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A Unity 6 (6000.3.9f1) workshop template for teaching game development fundamentals. The project provides a minimal starting point with a sample scene, car prefab, materials, and a test framework already configured.

## Working with Unity Projects

This is a Unity project — there is no CLI build command. All building, running, and testing happens inside the Unity Editor.

- **Open project:** Launch Unity Hub, open this folder with Unity 6000.3.9f1
- **Run the game:** Open `Assets/Scenes/SampleScene.unity`, press Play in the Editor
- **Build:** File → Build Settings → Build

## Running Tests

Tests are split into two assemblies with different contexts:

- **Edit Mode Tests** (`Assets/EditModeTests/`) — run without entering Play Mode; use for pure logic/unit tests
- **Play Mode Tests** (`Assets/PlayModeTests/`) — run in Play Mode; use for scene, physics, and integration tests

Access the Test Runner via: **Window → General → Test Runner**

Both use NUnit and Unity's `[UnityTest]` coroutine-based test pattern.

## Code Architecture

All game code lives under `Assets/`:

- **`Behaviours/`** — MonoBehaviour scripts attached to GameObjects. `ExampleBehaviour.cs` is the starting template.
- **`Prefabs/Car/`** — Reusable car and wheel GameObjects. Prefabs are the primary way to instantiate entities into scenes.
- **`Scenes/`** — Unity scene files. `SampleScene.unity` contains the example setup (colored plane + car prefab).
- **`Materials/`** — Simple colored materials (Black, Blue, Green, Red, Yellow) used to visually distinguish objects.
- **`InputSystem_Actions.inputactions`** — New Unity Input System asset; controls are defined here, not in code.

### Key patterns

- Game logic goes in MonoBehaviours under `Behaviours/`. Scripts must be attached to a GameObject in a scene or prefab to run.
- The Input System uses action-based input (not the legacy `Input.GetKey`). Read input via the generated C# class from `InputSystem_Actions.inputactions` or via `InputAction` references.
- Edit Mode tests can test static methods and ScriptableObjects without a scene. Play Mode tests should be used when a scene, physics, or `MonoBehaviour` lifecycle is needed.
