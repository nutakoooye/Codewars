/*
Given a 2D array and a number of generations, compute n timesteps of Conway's Game of Life.

    The rules of the game are:

- Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
- Any live cell with more than three live neighbours dies, as if by overcrowding.
- Any live cell with two or three live neighbours lives on to the next generation.
- Any dead cell with exactly three live neighbours becomes a live cell.

Each cell's neighborhood is the 8 cells immediately around it (i.e. Moore Neighborhood).
 The universe is infinite in both the x and y dimensions and all cells are initially dead -
  except for those specified in the arguments. The return value should be a 2d array cropped
   around all of the living cells. (If there are no living cells, then return [[]].)

For illustration purposes, 0 and 1 will be represented as ░░ and ▓▓ blocks respectively (PHP: plain black and white squares).
 You can take advantage of the htmlize function to get a text representation of the universe, e.g.:
*/

using System.Globalization;

static int[,] GetGeneration(int[,] cells, int generation)
{
    if (generation != 0)
    {
        cells = AddEmptyEdging(cells);

        int numRows = cells.GetUpperBound(0) + 1;
        int numColumns = cells.GetUpperBound(1) + 1;

        var nextGenCells = new int[numRows, numColumns];

        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                int numNeighbours = GetNumNeighbours(cells, row, column);
            
                if (cells[row, column] == 1 && (numNeighbours >= 2 && numNeighbours <= 3))
                {
                    nextGenCells[row, column] = 1;
                }
                else if (cells[row, column] == 0 && numNeighbours == 3 )
                {
                    nextGenCells[row, column] = 1;
                }
            }
        }

        generation -= 1;
        cells = GetGeneration(nextGenCells, generation);
    }

    return TrimArray(cells);
}

static int GetNumNeighbours(int[,] cells, int row, int column)
{
    int numRows = cells.GetUpperBound(0) + 1;
    int numColumns = cells.GetUpperBound(1) + 1;
    int numNeighbours = 0;

    for (int i = row - 1; i <= row + 1; i++)
    {
        if (i >= 0 && i < numRows)
        {
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (j >= 0 && j < numColumns)
                {
                    if (i != row || j != column)
                    {
                        numNeighbours += cells[i, j];
                    }
                }
            }
        }
    }

    return numNeighbours;
}

static int[,] AddEmptyEdging(int[,] cells)
{
    int numRows = cells.GetUpperBound(0) + 1;
    int numColumns = cells.GetUpperBound(1) + 1;

    var cellsWithEdging = new int[numRows + 2, numColumns + 2];

    for (int row = 0; row < numRows; row++)
    {
        for (int column = 0; column < numColumns; column++)
        {
            cellsWithEdging[row + 1, column + 1] = cells[row, column];
        }
    }

    return cellsWithEdging;
}
static int[,] TrimArray(int[,] array)
{
    int rows = array.GetLength(0);
    int cols = array.GetLength(1);
    
    int top = 0, bottom = rows - 1, left = 0, right = cols - 1;

    while (top < rows && IsRowZero(array, top))
        top++;

    while (bottom >= 0 && IsRowZero(array, bottom))
        bottom--;

    while (left < cols && IsColumnZero(array, left, top, bottom))
        left++;

    while (right >= 0 && IsColumnZero(array, right, top, bottom))
        right--;

    if (top > bottom || left > right)
        return new int[0, 0];

    int newRows = bottom - top + 1;
    int newCols = right - left + 1;
    int[,] trimmed = new int[newRows, newCols];

    for (int i = 0; i < newRows; i++)
    {
        for (int j = 0; j < newCols; j++)
        {
            trimmed[i, j] = array[top + i, left + j];
        }
    }

    return trimmed;
}

static bool IsRowZero(int[,] array, int row)
{
    for (int j = 0; j < array.GetLength(1); j++)
    {
        if (array[row, j] != 0)
            return false;
    }
    return true;
}

static bool IsColumnZero(int[,] array, int col, int top, int bottom)
{
    for (int i = top; i <= bottom; i++)
    {
        if (array[i, col] != 0)
            return false;
    }
    return true;
}

static void Print2DArray(int[,] numbers)
{
    int rows = numbers.GetUpperBound(0) + 1;
    int columns = numbers.GetUpperBound(1) + 1;

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            Console.Write($"{numbers[i, j]} ");
        }

        Console.WriteLine();
    }
}

var testArr = new int[,]
{
    {1,1,1,0,0,0,1,0},
    {1,0,0,0,0,0,0,1},
    {0,1,0,0,0,1,1,1}
};
Print2DArray(GetGeneration(testArr,16));