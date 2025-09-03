# 🐶 Dog-n-Bone Mini Game

A tiny 2D Unity game where you guide a dog through a labyrinth to reach a bone without touching the walls.  
Built with **Unity 2022.3.44f1 (LTS)**.

---

## 🎮 Gameplay
- The dog follows the **mouse cursor**.
- **Lose Condition:** Touching a wall → Game Over.
- **Win Condition:** Reaching the bone → Victory!


---

## 🛠️ Features
- **2D Physics:** BoxCollider2D (walls), Rigidbody2D & Collider2D (dog), Trigger collider (bone).
- **UI Panels:**
  - Win Panel
  - Game Over Panel
  - Countdown (3,2,1) before the game starts
- **Menu Scene:**
  - Start Game
  - Settings (volume slider)
  - Quit Game

---

## 🎵 Audio
- Background ambience loops quietly.
- Goal sound plays once when winning.
- Lose sound plays once when touching a wall.
- Settings menu includes a volume slider that saves your preference.

---

## ▶️ How to Play
1. Clone or download the project.
2. Open in **Unity 2022.3.44f1 (LTS)**.
3. Set the **MainMenu** scene as the startup scene.
4. Press **Play** in the editor or build the game.
5. Move your mouse to guide the dog:
   - Avoid walls.
   - Reach the bone to win!

---

## 📂 Project Structure
```
Assets/
 ├── Scenes/
 │    ├── MainMenu.unity
 │    └── Game.unity
 ├── Scripts/
 │    ├── GameManager.cs
 │    ├── PlayerController.cs
 │    └── MenuUI.cs
 ├── Audio/
 │    ├── ambience.mp3
 │    ├── win.wav
 │    └── lose.wav
 └── UI/
      ├── WinPanel
      ├── LosePanel
      └── CountdownText
```

---

## 🧑‍💻 Scripts
- **GameManager.cs** → Handles win/lose logic, audio, and UI panel control.
- **PlayerController.cs** → Moves the dog with the mouse and detects collisions.
- **MenuUI.cs** → Controls main menu buttons and settings (volume, quit, etc).

---

## 🚀 Build
- Go to **File → Build Settings**.
- Add both scenes (`MainMenu` and `Game`) in the build order.
- Build and run on your platform (Windows, macOS, Linux).

---

## 🤖 Known Issues
- **UI scaling:** Depending on screen resolution, UI text (e.g., countdown) may appear too small or off-center.
- **Fast mouse moves:** If the dog moves extremely quickly, it may visually overlap walls before collision registers (a limitation of using `MovePosition` without interpolation).
- **Quit button:** Has no effect in the web host.

--- 

## ⌛ What is Skipped
- No multiple levels or level progression.
- No animations for the dog or bone.
- No mobile/touch controls (mouse only).
- No save/load system except for audio volume.
- No pause menu during gameplay.
- No VFX/UI transitions
- No Timer/best time 

---

## 📜 License
This project is for learning/demo purposes.  
Feel free to fork and modify.
