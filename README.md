# Unbeatable-Tic-Tac-Toe-Player
A Tic-Tac-Toe playing environment with an unbeatable AI.

The purpose of this project was to refresh my knowledge of C# and Visual Studio, and to challenge myself to think of a good algorithm for an AI to play tic tac toe, without hardcoding the winning strategy like in some implementations.  The application consists of a tic tac toe board that the user can click to mark their move, and the computer will automatically play in response.  Additionally, the player can specify whether they want to play as X or O, and whether the computer starts or not.

The algorithm that I chose for the AI was minimax with alpha-beta pruning, using values of 1 for a win, 0 for a tie, and -1 for a loss for player 1, randomly choosing a move from the best options in order to feel more realistic.  Using alpha-beta pruning signficantly improves the algorithm, since there are quite a few branches that need not be considered if a loss is possible using a given move.  This algorithm results in a completely unbeatable AI, and its search tree is actually not very large so it is pretty quick.

This project was made using Visual Studio's Windows Forms.  As such, there are many project files and files for building and formatting the GUI.  However, because these were not written or used by me and are not very interesting, they were excluded from the repo.  Only the code that I wrote and some basic resources were included.
