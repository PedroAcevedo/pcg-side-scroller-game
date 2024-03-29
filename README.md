﻿# pcg-side-scroller-game

<img src="https://github.com/PedroAcevedo/pcg-side-scroller-game/blob/main/Recordings/movie_001.gif?raw=true" width="736.8" height="343.2" alt="pcg-game">

This repository contains the 2D Infinity Side Scroller developed for CGT 581 class, assignment 2.

## How It Works

The level is divided into chunks. Each chunk is defined by some rules based on the expected level of difficulty (easy, medium, and hard).

1.	A level chunk is composed of items and has a size of 30 cells.
2.	An item comprises a 2D Game Object, a size in X and Y position on the screen, an item type to define some constraints, and an assigned score.
3.	The item types are 1. Non-dangerous items. 2. 1x1 dangerous cell item, and 3. Area dangerous items.
4.	The players lose their life once it collides with items of type 2 and 3. Also, lose if the player falls from the screen and if they get trapped by the spikes.
5.	The player can collect cherries during the gameplay.
6.	There are 5 types of chunks to be selected. Each chunk has its score accordingly to the difficulty of the obstacles (between 0 and 25).
7.	The items score according to their dangerous type (2, 5, or 10).
8.	As an input of the algorithm, the layout of the chunk is required. The chunk is defined by the floor position in X and Y and if there are holes. The cells with holes are not available to be occupied by items. 
9.	The difficulty level has its user-defined score based on the expected item density (easy < hard).
10.	To place the items on each chunk, the scores are considered as the following problem: We want to have our wallet with fewer items that fit our money capacity. The money capacity is defined for the user-specified level of difficulty. Initially, the initial chunk will contribute to the final wallet score capacity. For example, an easy chunk contributes fewer scores (e.g., 5 o 0) than the difficult chunks (e.g., 25). The rest is divided by the score of the items, so we want to have fewer items on the chunks; the list of scores is sorted in decreasing order to select as many as possible items with higher scores, then the second higher, and so on. The algorithm continues to include items in the wallet until the capacity is filled. 
11.	There are some constraints based on the type of item. Those constraints are for item 2 that they could not be placed at a side of a hole, similar to the object of type 3. For type 3 items, the position could not be at the corners of the chunk. Also, they cannot be placed on higher cells (In which Y is higher than usual).
12.	The chunks are placed infinitely once the player reaches the middle of the chunk.
