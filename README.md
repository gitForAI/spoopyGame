# spoopyGame
Spoopy Monster Game

What is "Spoopy Monster Game"?

Spoopy Monster Game is the product of hours of experimentation and refinement. Designed to showcase pathfinding and behavior AI, this self-running game implements the A* pathfinding algorithm in combination with hierarchical finite state machines. In this state-of-the-art horror simulator, our team pits a human against a monster. The human’s goal is to find the key and exit the house, while the monster’s aim is to chase and capture the human. The project was created by a team of undergraduates enrolled in American University's computer science program, specifically in a course focused around artificial intelligence.

Build instructions:
All classes, objects and code needed to build the program are contained within this repository. To build and run the game, clone the repository to a folder on your computer, open unity, and select the folder through Unity’s “Open Project” explorer. Once the game is loaded in the Unity session, you may need to double click on “The Game” object in the lower drawer to get the game to run.

Sample Console output:

The human starts at position (0,0). The path should take the human to position (3,4). The console 
output shows that the A* algorithm that we implement generates a reasonable path to the goal by 
showing the x and y coordinates at each step of the path. The human ends at position (3,4) 
graphically (but this is also printed since this is difficult to eyeball).

Human start: x = 0, y = 0

PATH:

x = 0, y = 0

x = 0, y = 1

x = 0, y = 2

x = 0, y = 3

x = 0, y = 4

x = 1, y = 4

x = 2, y = 4

x = 3, y = 4

Human final: x = 3, y = 4
