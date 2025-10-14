# Unity Rogue-lite: Infernal Ascension

## 🚀 Overview

**Infernal Ascension** is a dynamic, procedurally generated Rogue-lite game developed in **Unity**. The game emphasizes immersive exploration, survival, strategic decision-making, resource management, and skillful combat across a series of increasingly difficult levels.

The project demonstrates proficiency in building complex core mechanics, including a dynamic item and combat system, and ensuring high replayability through the use of procedural content generation. The ultimate goal is to complete all five distinct levels, demanding continuous player improvement and adaptation.

## ✨ Core Mechanics and Features

* **Dynamic Procedural Generation:** Implementation of algorithms to dynamically generate levels, enemy placements, and loot distribution, ensuring a unique and unpredictable experience on every playthrough.
* **Rogue-lite Progression:** A structured progression system across five distinct levels, where each level is unlocked sequentially and presents specific, escalating challenges and unique enemy types.
* **Strategic Resource Management:** Players must strategically manage health, ammunition, and other critical resources to navigate hazardous environments and survive powerful enemy encounters.
* **Skillful Combat System:** Features a robust weapon and combat system (platformer/shooter style) requiring precision and tactical positioning.
* **Machinations Diagram Integration:** The game’s core economy (resource flow, health, and difficulty scaling) was designed and validated using a **Machinations** diagram, ensuring a balanced and engaging difficulty curve.
* **Modular Systems:** Includes dedicated, modular systems for Health/HUD, Enemy AI/Behavior, Camera control, and a comprehensive Death/Respawn mechanic.
* **Portal Mechanics:** Implemented a system of portals and keys for level transitions and secret zone access.

## 🛠️ Technical Stack

* **Engine:** Unity Engine (202X.X)
* **Language:** C#
* **Design Tools:** Machinations (for system design and balance validation)

## 💡 Technical Implementation Highlights

### 1. Procedural Generation Architecture
The procedural generation logic, implemented in C#, manages several aspects simultaneously:
* **Level Layout:** Generating unique physical geometry and pathing.
* **Enemy Spawning:** Determining the number and type of enemies based on the current difficulty level.
* **Loot Placement:** Distributing strategic resources and items to reward exploration.

### 2. Difficulty and Progression Control
* The game is structured with 5 distinct levels, where the challenge (enemy stats, quantity, and environmental hazards) is rigorously scaled based on the Machinations analysis to maintain a consistent learning curve.
* Each level introduces novel mechanics or enemy behavior patterns, forcing players to adapt their strategies.

### 3. Combat and Physics Implementation
* The weapon system integrates raycasting and collision detection for accurate shooter mechanics.
* Player movement and environmental interaction adhere to Unity's 2D/3D physics standards (depending on the project's dimension) to provide responsive platforming controls.

## ▶️ How to Run the Project

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/MahdisSep/Unity-RogueLite-Procedural-Ascension.git]
    cd Unity-RogueLite-Procedural-Ascension
    ```
2.  **Open in Unity:** Launch Unity Hub and add the project folder. Ensure you have the correct Unity editor version installed (The version used for development is recommended).
3.  **Load the Scene:** Open the main game scene (e.g., `Assets/Scenes/GameLevel.unity`).
4.  **Run:** Press the Play button in the Unity Editor.

## 👥 Author

* [Mahdissep]
* *Based on the documentation, this project was authored by Mahdis Sepahvand.*
