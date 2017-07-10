using System.ComponentModel;
using System.Windows.Input;
using NotifyPropertyChangedBase;

namespace Minesweeper
{
    public class ElementViewModel : INotifyPropertyChanged
    {

        #region Private Variables
        
        private Element _element;
        private ElementState _elementState;
        private bool _isMine;

        #endregion

        #region Public Properties

        public ElementState ElementState { get { return _elementState; } set { _elementState = value; } }
        public Element GetElement { get { return _element; } }
        public int NumberOfMines { get { return GetNumberOfMines(); } }
        public string Content { get { return GetContent(); } }
        public bool IsMine { get { return _isMine ; } set { _isMine = value ; } }

        #endregion

        #region Public Commands

        public ICommand LeftClickCommand { get; set; }
        public ICommand RightClickCommand { get; set; }

        #endregion

        #region Constructor

        public ElementViewModel(Element element)
        {
            this.LeftClickCommand = new RelayCommand(LeftClick);
            this.RightClickCommand = new RelayCommand(RightClick);

            _element = element;
            _elementState = _element.ElementState;
            _isMine = false;
        }

        #endregion

        #region Helper Methods


        public void AutoOtkrivanje(ElementViewModel elementToChange)
        {
            if (elementToChange._isMine == false)
            {
                elementToChange.ElementState = ElementState.Clear;
                PropertyChanged(elementToChange, new PropertyChangedEventArgs("Content"));
                PropertyChanged(elementToChange, new PropertyChangedEventArgs("ElementState"));

                if (elementToChange._isMine == false && elementToChange.NumberOfMines == 0)
                    for (int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)
                        {
                            try
                            {
                                if (elementToChange.GetElement.Row + i >= 0 && elementToChange.GetElement.Row + i < BoardViewModel.Instance.NumberOfRows && elementToChange.GetElement.Column + j >= 0 && elementToChange.GetElement.Column + j < BoardViewModel.Instance.NumberOfColumns)
                                    if (BoardViewModel.Instance.Elements[elementToChange.GetElement.Row + i][this.GetElement.Column + j]._isMine == false && elementToChange.NumberOfMines == 0 && BoardViewModel.Instance.Elements[elementToChange.GetElement.Row + i][elementToChange.GetElement.Column + j].ElementState == ElementState.Unknown)
                                        AutoOtkrivanje(BoardViewModel.Instance.Elements[elementToChange.GetElement.Row + i][elementToChange.GetElement.Column + j]);
                            }
                            catch { }//Out of bounds, ignorira se
                            
                        }


                PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                PropertyChanged(this, new PropertyChangedEventArgs("ElementState"));
            }

        }
        
        public void LeftClick()
        {
            if (BoardViewModel.Instance.IsPopulated == false)
                BoardViewModel.Instance.CreateMines(BoardViewModel.Instance.NumberOfMines, GetElement.Row, GetElement.Column);

            if (ElementState == ElementState.Unknown)
            {
                if (this._isMine == false)
                {
                    ElementState = ElementState.Clear;

                    if (this._isMine == false && this.NumberOfMines == 0)
                        for (int i = -1; i < 2; i++)
                            for (int j = -1; j < 2; j++)
                            {
                                if (this.GetElement.Row + i >= 0 && this.GetElement.Row + i < BoardViewModel.Instance.NumberOfRows && this.GetElement.Column + j >= 0 && this.GetElement.Column + j < BoardViewModel.Instance.NumberOfColumns)
                                    if (BoardViewModel.Instance.Elements[this.GetElement.Row + i][this.GetElement.Column + j]._isMine == false && BoardViewModel.Instance.Elements[this.GetElement.Row + i][this.GetElement.Column + j].NumberOfMines == 0)
                                        AutoOtkrivanje(BoardViewModel.Instance.Elements[this.GetElement.Row + i][this.GetElement.Column + j]);
                            }

                    PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ElementState"));
                }
                else
                {
                    ElementState = ElementState.DetonatedMine;
                    PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ElementState"));

                    for (int i = 0; i < BoardViewModel.Instance.NumberOfRows; i++)
                        for (int j = 0; j < BoardViewModel.Instance.NumberOfColumns; j++)
                            if (BoardViewModel.Instance.Elements[i][j].ElementState == ElementState.Unknown || BoardViewModel.Instance.Elements[i][j].ElementState == ElementState.Mine)
                            {
                                if (BoardViewModel.Instance.Elements[i][j]._isMine && BoardViewModel.Instance.Elements[i][j].ElementState == ElementState.Unknown)
                                    BoardViewModel.Instance.Elements[i][j].ElementState = ElementState.NotFoundMine;
                                else if (BoardViewModel.Instance.Elements[i][j].ElementState == ElementState.Unknown && BoardViewModel.Instance.Elements[i][j].IsMine == false)
                                    BoardViewModel.Instance.Elements[i][j].ElementState = ElementState.NotFoundClear;
                                else if (BoardViewModel.Instance.Elements[i][j].ElementState == ElementState.Mine && BoardViewModel.Instance.Elements[i][j].IsMine == false)
                                    BoardViewModel.Instance.Elements[i][j].ElementState = ElementState.FalsePositive;
                                else if (BoardViewModel.Instance.Elements[i][j].ElementState == ElementState.Mine && BoardViewModel.Instance.Elements[i][j].IsMine == true)
                                    BoardViewModel.Instance.Elements[i][j].ElementState = ElementState.TrueMine;
                                PropertyChanged(BoardViewModel.Instance.Elements[i][j], new PropertyChangedEventArgs("Content"));
                                PropertyChanged(BoardViewModel.Instance.Elements[i][j], new PropertyChangedEventArgs("ElementState"));
                            }
                }
            }        
        }

        public void RightClick()
        {
            if (ElementState == ElementState.Unknown)
            {
                ElementState = ElementState.Mine;
                PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                PropertyChanged(this, new PropertyChangedEventArgs("ElementState"));
            }
            else if (ElementState == ElementState.Mine)
            {
                ElementState = ElementState.Unknown;
                PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                PropertyChanged(this, new PropertyChangedEventArgs("ElementState"));
            }
        }

        public int GetNumberOfMines()
        {
            int numberOfMines = 0;

            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if(this.GetElement.Row + i >= 0 && this.GetElement.Row + i < BoardViewModel.Instance.NumberOfRows && this.GetElement.Column + j >= 0 && this.GetElement.Column + j < BoardViewModel.Instance.NumberOfColumns)
                        if (BoardViewModel.Instance.Elements[this.GetElement.Row + i][this.GetElement.Column + j]._isMine)
                            numberOfMines++;
                }
            return numberOfMines;
        }

        public string GetContent()
        {
            char mine = '\u2739';
            char flag = '\u2691';
            if (this.ElementState == ElementState.Clear && GetNumberOfMines() == 0)
                return " ";
            if (this.ElementState == ElementState.FalsePositive)
            {
                if (GetNumberOfMines() == 0)
                    return " ";
                return GetNumberOfMines().ToString();
            } 
            if (this.ElementState == ElementState.Clear)
                return GetNumberOfMines().ToString();
            if (this.ElementState == ElementState.Mine)
                return flag.ToString();
            if (this.ElementState == ElementState.NotFoundMine)
                return mine.ToString(); ;
            if (this.ElementState == ElementState.DetonatedMine)
                return mine.ToString();
            if (this.ElementState == ElementState.TrueMine)
                return flag.ToString(); ;
            return " ";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
