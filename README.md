<h1 align="center">DRIVING SIMULATION GAME</h1>

## Project Overview

This project is a standalone driving simulation that showcases core elements of vehicle movement, physics, camera functionality, and a heads-up display (HUD). The game allows players to control a car with realistic physics, demonstrating acceleration, braking, and steering. It includes camera tracking and a dynamic speedometer HUD for an immersive driving experience.

Play here: [Driving Simulator on Unity Play](https://play.unity.com/en/games/9ad14b50-d56d-46ae-ae4d-5eea644f2288/driving-simulator)

---

## Game Features

- **Realistic Car Physics**: Drive a vehicle with accurate acceleration, braking, and handling mechanics.
- **Camera Follow System**: A smooth-follow camera that tracks the car’s movement and rotation.
- **HUD Elements**: Includes a speedometer and engine audio that adjusts based on the car’s speed.

---

## Script Descriptions


1. **CarController.cs**  
   This script is responsible for all aspects of vehicle control and movement. It includes:
   - **Motor and Brake Forces**: Applies forces for acceleration and braking based on player input.
   - **Steering Mechanics**: Adjusts the car’s wheels for realistic turning.
   - **Deceleration**: Gradually slows the car when no input is provided.
   - **Wheel Transform Updates**: Synchronizes visual wheel positions with physics-based wheel colliders.

2. **CarHUDController.cs**  
   This script controls the HUD elements and engine audio based on the car’s speed. It includes:
   - **Speedometer**: Displays the car’s speed with a rotating needle and speed text.
   - **Engine Audio**: Adjusts the audio pitch to match the vehicle’s speed.
     
3. **CameraFollow.cs**  
   This script manages the camera's position and rotation to follow the car. It smoothly translates the camera based on a configurable offset and rotates it to keep the car in view.
---

## About RoadWise Adventures

This project is a part of *RoadWise Adventures*, a larger educational driving simulation game. Developed to promote road safety and driving awareness, *RoadWise Adventures* offers a comprehensive learning experience by allowing players to explore diffrent driving environments, encounter different road signs, and practice safe driving techniques.

In the complete game, players are educated on essential traffic rules, encouraged to follow road safety guidelines, and gain experience identifying and responding to real-world traffic signals. This simulation demo, focused primarily on vehicle dynamics, HUD elements, and camera functionality, serves as a foundational component of the *RoadWise Adventures* project, providing the core driving mechanics.

*RoadWise Adventures* is divided into two main tracks that represent two different driving environments, each providing the user with information related to common traffic signs encountered in that environment.

1. **First Track**: This environment simulates a city setting, where players will learn about urban traffic signs and rules.
2. **Second Track**: This environment represents a highway, focusing on the traffic signs and regulations specific to highway driving.

## Video Demonstration
Watch the video demonstration of *RoadWise Adventures* [here](https://drive.google.com/file/d/1ehOVcT6NwC3WCGqtkFxkCFb0wssDco7d/view?usp=drive_link).



