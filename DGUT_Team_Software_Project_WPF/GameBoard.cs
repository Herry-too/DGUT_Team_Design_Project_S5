﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class GameBoard
    {
        public Piece.Players player { get; set; } = Piece.Players.red;//current player
        public Piece[,] pieces { get; set; }//all pieces
        public bool gameStatus { get; set; } = true;//game status now
        public int selectedX { get; set; } = -1;//if selected a pieces, it's row
        public int selectedY { get; set; } = -1;//it's column
        public int[] redGeneralPiece { get; set; }//red General Piece
        public int[] blackGeneralPiece { get; set; }//black general piece

        public int movehistory = 0;
        public GameBoard()
        {
            pieces = new Piece[10, 9];                      //Create a new gameboard, include 10*9 places to hold piece
            //Set Red Player default pieces
            pieces[0, 0] = new CarPiece(Piece.Players.red, 0, 0);
            pieces[0, 1] = new HorsePiece(Piece.Players.red, 0, 1);
            pieces[0, 2] = new ElephantPiece(Piece.Players.red, 0, 2);
            pieces[0, 3] = new AdvisorPiece(Piece.Players.red, 0, 3);
            pieces[0, 4] = new GeneralPiece(Piece.Players.red, 0, 4);
            pieces[0, 5] = new AdvisorPiece(Piece.Players.red, 0, 5);
            pieces[0, 6] = new ElephantPiece(Piece.Players.red, 0, 6);
            pieces[0, 7] = new HorsePiece(Piece.Players.red, 0, 7);
            pieces[0, 8] = new CarPiece(Piece.Players.red, 0, 8);
            pieces[2, 1] = new CannonPiece(Piece.Players.red, 2, 1);
            pieces[2, 7] = new CannonPiece(Piece.Players.red, 2, 7);
            pieces[3, 0] = new SoldierPiece(Piece.Players.red, 3, 0);
            pieces[3, 2] = new SoldierPiece(Piece.Players.red, 3, 2);
            pieces[3, 4] = new SoldierPiece(Piece.Players.red, 3, 4);
            pieces[3, 6] = new SoldierPiece(Piece.Players.red, 3, 6);
            pieces[3, 8] = new SoldierPiece(Piece.Players.red, 3, 8);
            //Same as Black Player
            pieces[9, 0] = new CarPiece(Piece.Players.black, 9, 0);
            pieces[9, 1] = new HorsePiece(Piece.Players.black, 9, 1);
            pieces[9, 2] = new ElephantPiece(Piece.Players.black, 9, 2);
            pieces[9, 3] = new AdvisorPiece(Piece.Players.black, 9, 3);
            pieces[9, 4] = new GeneralPiece(Piece.Players.black, 9, 4);
            pieces[9, 5] = new AdvisorPiece(Piece.Players.black, 9, 5);
            pieces[9, 6] = new ElephantPiece(Piece.Players.black, 9, 6);
            pieces[9, 7] = new HorsePiece(Piece.Players.black, 9, 7);
            pieces[9, 8] = new CarPiece(Piece.Players.black, 9, 8);
            pieces[7, 1] = new CannonPiece(Piece.Players.black, 7, 1);
            pieces[7, 7] = new CannonPiece(Piece.Players.black, 7, 7);
            pieces[6, 0] = new SoldierPiece(Piece.Players.black, 6, 0);
            pieces[6, 2] = new SoldierPiece(Piece.Players.black, 6, 2);
            pieces[6, 4] = new SoldierPiece(Piece.Players.black, 6, 4);
            pieces[6, 6] = new SoldierPiece(Piece.Players.black, 6, 6);
            pieces[6, 8] = new SoldierPiece(Piece.Players.black, 6, 8);

            redGeneralPiece = new int[2]{ 0,4};
            blackGeneralPiece = new int[2]{ 9,4};
        }
        public string toJson()
        {
            var toJson = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return toJson;
        }

        public string outputFENFile(GameBoard gameboard)
        {
            //Record the current game
            string fen = "";
            //get current gameboard
            Piece[,] pieces = gameboard.getPieces();
            //record number of space
            int number_of_space = 0;
            //traverse the whole gameboard (can be update)
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (pieces[i, j] != null)
                    {
                        if (number_of_space != 0)
                        {
                            fen += "" + number_of_space;
                        }
                        fen += pieces[i, j].GetPieceName();
                        number_of_space = 0;
                    }
                    else
                    {
                        number_of_space++;
                    }
                    if (j == 8)
                    {
                        if (number_of_space != 0)
                        {
                            fen += "" + number_of_space;
                            number_of_space = 0;
                        }
                        //if it's the final pieces, stop recording "/"
                        if (i == 9) break;
                        fen += "/";
                    }
                }
            }
            //if current player is red, output r, else output b
            if(gameboard.getPlayer()==0) fen += " b - - 0 " + movehistory;
            else fen += " r - - 0 " + movehistory;
            return fen;
        }

        public Piece[,] getPieces()
        {
            return pieces;  //return all pieces,string.
        }

        public Piece.Players getPlayer()
        {
            return player;  //return current player, string.
        }
        public string getPieceName(int x,int y)
        {
            if(pieces[x,y] == null)
            {
                return "";  //if there is no piece at this place
            }
            else
            {
                return pieces[x, y].getPieceWords();//else return the Name of the Piece
            }
        }

        public Piece.Players getPiecePlayer(int x,int y)
        {
            return pieces[x, y].getPlayer();    //get a piece's player
        }

        public void SwitchPlayer()  //Switch player
        {
            if(player == Piece.Players.red)
            {
                player = Piece.Players.black;
            }
            else
            {
                player = Piece.Players.red;
            }
        }

        int[] strInputToIntArrayInput(String strInput)
        {
            int[] returnArray = new int[2] { -1,-1};
            if (strInput.Length != 2)
                return new int[] { -1,-1};  //if there is a invaild input,return {-1,-1}
            for (int i = 0; i < strInput.Length; i++)
            {
                if (strInput[i] > 96 && strInput[i] < 106)
                    returnArray[1] = strInput[i] - 97;  //if there is a character between alphabet 'a' to 'i', change it to int and set it as the posY/intY
                if (strInput[i] > 47 && strInput[i] < 58)
                    returnArray[0] = strInput[i] - 48;// same
            }
            return returnArray;
        }
        public bool boolSelectPiece(String strInput)
        {
            int[] intArray = strInputToIntArrayInput(strInput);
            int posX = intArray[0];
            if (intArray[0] == -1 || intArray[1] == -1)
                return false;// if invaild number, return false
            int posY = intArray[1];
            if (pieces[posX,posY] == null)
            {
                return false;//if there is no pieces at this place,could not select nothing,return false
            }
            if (pieces[posX, posY].getPlayer() == player)//make sure only current player could select own pieces
            {
                selectedX = posX;
                selectedY = posY;
                return true;
            }
            return false;
        }

        public bool boolMovePiece(String strInput)
        {
            int posX, posY;
            int[] intArray = strInputToIntArrayInput(strInput);
            if (intArray[0] == -1 || intArray[1] == -1)
                return false;//same as boolSelectPiece
            posX = intArray[0];
            posY = intArray[1];
            if (posX == selectedX && posY == selectedY) //cancel the select
            {
                SwitchPlayer();
                selectedX = -1;
                selectedY = -1;
                return true;
            }
            if (calculateValidMoves(posX, posY))//check if it could move
            {
                if(pieces[posX, posY] != null && (pieces[posX,posY].getPieceWords() == "將"|| pieces[posX, posY].getPieceWords() == "帥"))
                {
                    gameStatus = false;
                }
                pieces[posX, posY] = pieces[selectedX, selectedY];//coverage the pieces
                pieces[posX, posY].setCurrentPosition(posX, posY);
                pieces[selectedX, selectedY] = null;//remove old pieces
                selectedX = selectedY = -1;// remove selected record.
                if ((pieces[posX, posY].getPieceWords() == "將" || pieces[posX, posY].getPieceWords() == "帥"))
                {
                    switch (player)
                    {
                        case Piece.Players.red:
                            redGeneralPiece[0] = posX;
                            redGeneralPiece[1] = posY;
                            break;
                        case Piece.Players.black:
                            blackGeneralPiece[0] = posX;
                            blackGeneralPiece[1] = posY;
                            break;
                    }
                }
                //增加当前回合数
                movehistory++;
                return true;
            }
            return false;
        }
        public bool boolMovePiece(int posX, int posY)
        {
            if (posX == selectedX && posY == selectedY) //cancel the select
            {
                SwitchPlayer();
                selectedX = -1;
                selectedY = -1;
                return true;
            }
            if (calculateValidMoves(posX, posY))//check if it could move
            {
                if (pieces[posX, posY] != null && (pieces[posX, posY].getPieceWords() == "將" || pieces[posX, posY].getPieceWords() == "帥"))
                {
                    gameStatus = false;
                }
                pieces[posX, posY] = pieces[selectedX, selectedY];//coverage the pieces
                pieces[posX, posY].setCurrentPosition(posX, posY);
                pieces[selectedX, selectedY] = null;//remove old pieces
                selectedX = selectedY = -1;// remove selected record.
                if ((pieces[posX, posY].getPieceWords() == "將" || pieces[posX, posY].getPieceWords() == "帥"))
                {
                    switch (player)
                    {
                        case Piece.Players.red:
                            redGeneralPiece[0] = posX;
                            redGeneralPiece[1] = posY;
                            break;
                        case Piece.Players.black:
                            blackGeneralPiece[0] = posX;
                            blackGeneralPiece[1] = posY;
                            break;
                    }
                }
                return true;
            }
            return false;
        }
        public bool calculateValidMoves(int posX, int posY)
        {
            if (posX < 0 || posX > 9)
            {
                return false;//if the point is out of board
            }
            if (posY < 0 || posY > 8)
            {
                return false;//same
            }
            if(player != pieces[selectedX, selectedY].getPlayer())
            {
                return false;//current player is the owner of the piece
            }
            if (pieces[posX, posY] != null && player == pieces[posX, posY].getPlayer())
                return false;//dont eat own pieces
            return pieces[selectedX, selectedY].ValidMoves(posX, posY, this);//let pieces check it's rule
        }


        public int getSelectedX()
        {
            return selectedX;
        }
        public int getSelectedY()
        {
            return selectedY;
        }

        public bool ifDeliveredCheck()
        {
            int genX, genY;
            if(player == Piece.Players.red)
            {
                genX = redGeneralPiece[0];
                genY = redGeneralPiece[1];
            }
            else
            {
                genX = blackGeneralPiece[0];
                genY = blackGeneralPiece[1];
            }
            foreach (Piece piece in pieces)
            {
                if (piece == null || piece.getPlayer() == player)
                {
                    continue;
                }
                if (piece.ValidMoves(genX, genY, this))
                {
                    return true;
                }
            }
            return false;

        }
        public bool getGameStatus()
        {
            return gameStatus;
        }
    }
}
