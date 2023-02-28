# VRHacks2023
## Description
An AR/VR game that spawns enemies and walls around you to shoot. This was a hackathon solution for VR_Hacks 2023 from Georgian College.

## Device
Oculus Quest 2

## Tools/Frameworks
Passthrough VR, Unity, C#

## Features
- Enemies using the public model "Unity-chan" with various animations (Walking/Running/Shooting/Idle)
- Enemies spawn in areas not visible to the user and hidden behind objects
- Pistols to pick up and shoot with
- Shooting and movement sound effects
- Path-finding algorithm via NavMesh for enemies to find and shoot you

## Goal
To create an AR FPS game where your environment is your map. With Quest 2's passthrough VR mode, we aimed to take advantage of Quest 2's spatial mapping ability to model 
the user's room and spawn enemies behind pillars to shoot at.

Our vision for this FPS game is to bring Nerf-like games into the digital world where players can physically move around and play FPS games simultaneously. We were inspired
by the fun that we had as children when playing Nerf games and felt disappointed that teenagers are becoming more and more attached to computer games where they sit and play.

## Issues
- Quest 2 does not provide software for their spatial mapping hardware
- Lack of time to implement a different method of mapping the area (i.e, user scanning objects to make a map)
- Hitboxes and projectile collision detection are not 100% accurate
- Restricted to ~24 hours of development


