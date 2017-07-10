using System.ComponentModel;
using NotifyPropertyChangedBase;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace Minesweeper
{
    public class BoardViewModel : INotifyPropertyChanged
    {

        #region Private Variables

        private int _numberOfRows;
        private int _numberOfColumns;
        private int _numberOfMines;
        private static BoardViewModel instance;
        private bool _isPopulated;

        public static BoardViewModel Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new BoardViewModel(16, 30, 99);
                }
                return instance;
            }
        }

        #endregion

        #region Public Properties

        public ObservableCollection<List<ElementViewModel>> Elements { get; set; }
        public int NumberOfRows { get { return _numberOfRows; } set { _numberOfRows = value; } }
        public int NumberOfColumns { get { return _numberOfColumns; } set { _numberOfColumns = value; } }
        public int NumberOfMines { get { return _numberOfMines; } set { _numberOfMines = value; } }
        public bool IsPopulated { get { return _isPopulated; } set { _isPopulated = value; } }

        #endregion

        #region Public Commands

        public ICommand NewGameCommand { get; set; }

        #endregion

        #region Constructor

        private BoardViewModel(int numberOfRows, int numberOfColumns, int numberOfMines)
        {
            this.NewGameCommand = new RelayCommand(NewGame);

            _numberOfRows = numberOfRows;
            _numberOfColumns = numberOfColumns;
            _numberOfMines = numberOfMines;
            CreateEmptyBoard(_numberOfRows, _numberOfColumns);

            _isPopulated = false;
        }

        #endregion

        #region Helper Methods

        public void NewGame()
        {
            Elements.Clear();
            CreateEmptyBoard(_numberOfRows, _numberOfColumns);
            PropertyChanged(BoardViewModel.Instance, new PropertyChangedEventArgs("Elements"));

            _isPopulated = false;          
        }

        public void CreateEmptyBoard(int numberOfRows, int numberOfColumns)
        {
            Elements = new ObservableCollection<List<ElementViewModel>>();
            for (int i = 0; i < numberOfRows; i++)
            {
                Elements.Add(new List<ElementViewModel>());

                for (int j = 0; j < numberOfColumns; j++)
                {
                    Elements[i].Add(new ElementViewModel(new Element(i, j, ElementState.Unknown)));
                }
            }
        }

        public void CreateMines(int totalMines, int row, int column)
        {
            int i = 0;
            int j = 0;
            while (totalMines != 0)
            {
                if (j >= Elements[i].Count)
                {
                    i++;
                    j = 0;
                }
                Elements[i][j].IsMine = true;
                j++;
                totalMines--;
            }

            for (int k = _numberOfRows*_numberOfColumns - 1; k > 0; k--)
            {
                int i0 = k % _numberOfRows;
                int i1 = k / _numberOfRows;

                var rand = new Random();
                int randomNumber = rand.Next(k + 1);
                int j0 = randomNumber % _numberOfRows;
                int j1 = randomNumber / _numberOfRows;

                bool temp = Elements[i0][i1].IsMine;
                Elements[i0][i1].IsMine = Elements[j0][j1].IsMine;
                Elements[j0][j1].IsMine = temp;
            }

            for (int k = _numberOfRows * _numberOfColumns - 1; k > 0; k--)
            {
                int i0 = k % _numberOfRows;
                int i1 = k / _numberOfRows;

                var rand = new Random();
                int randomNumber = rand.Next(k + 1);
                int j0 = randomNumber % _numberOfRows;
                int j1 = randomNumber / _numberOfRows;

                bool temp = Elements[i0][i1].IsMine;
                Elements[i0][i1].IsMine = Elements[j0][j1].IsMine;
                Elements[j0][j1].IsMine = temp;
            }

            while (Elements[row][column].IsMine)
            {
                var rand = new Random();
                int randomNumber = rand.Next(_numberOfRows * _numberOfColumns - 1);
                int j0 = randomNumber % _numberOfRows;
                int j1 = randomNumber / _numberOfRows;

                bool temp = Elements[row][column].IsMine;
                Elements[row][column].IsMine = Elements[j0][j1].IsMine;
                Elements[j0][j1].IsMine = temp;
            }
            _isPopulated = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
