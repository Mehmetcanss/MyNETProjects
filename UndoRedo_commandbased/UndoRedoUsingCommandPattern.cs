using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UndoRedo_CommandPattern
{

    #region Commands

    interface ICommand
    {
        void Execute();
        void UnExecute();
    }
    class MoveCommand : ICommand
    {
        private Thickness _ChangeOfMargin;
        private FrameworkElement _UiElement;

        public MoveCommand(Thickness margin, FrameworkElement uiElement)
        {
            _ChangeOfMargin = margin;
            _UiElement = uiElement;
        }

        #region ICommand Members

        public void Execute()
        {

            _UiElement.Margin = new Thickness(_UiElement.Margin.Left + _ChangeOfMargin.Left, _UiElement.Margin.Top + _ChangeOfMargin.Top, _UiElement.Margin.Right + _ChangeOfMargin.Right, _UiElement.Margin.Bottom + _ChangeOfMargin.Bottom);
        }

        public void UnExecute()
        {
            _UiElement.Margin = new Thickness(_UiElement.Margin.Left - _ChangeOfMargin.Left, _UiElement.Margin.Top - _ChangeOfMargin.Top, _UiElement.Margin.Right - _ChangeOfMargin.Right, _UiElement.Margin.Bottom - _ChangeOfMargin.Bottom);
        }

        #endregion
    }
    class ResizeCommand : ICommand
    {
        private Thickness _ChangeOfMargin;
        private double _ChangeofWidth;
        private double _Changeofheight;
        private FrameworkElement _UiElement;

        public ResizeCommand(Thickness margin, double width, double height, FrameworkElement uiElement)
        {

            _ChangeOfMargin = margin;
            _ChangeofWidth = width;
            _Changeofheight = height;
            _UiElement = uiElement;

        }

        #region ICommand Members

        public void Execute()
        {
            _UiElement.Height = _UiElement.Height + _Changeofheight;
            _UiElement.Width = _UiElement.Width + _ChangeofWidth;
            _UiElement.Margin = new Thickness(_UiElement.Margin.Left + _ChangeOfMargin.Left, _UiElement.Margin.Top + _ChangeOfMargin.Top, _UiElement.Margin.Right + _ChangeOfMargin.Right, _UiElement.Margin.Bottom + _ChangeOfMargin.Bottom);

        }

        public void UnExecute()
        {
            _UiElement.Height = _UiElement.Height - _Changeofheight;
            _UiElement.Width = _UiElement.Width - _ChangeofWidth;
            _UiElement.Margin = new Thickness(_UiElement.Margin.Left - _ChangeOfMargin.Left, _UiElement.Margin.Top - _ChangeOfMargin.Top, _UiElement.Margin.Right - _ChangeOfMargin.Right, _UiElement.Margin.Bottom - _ChangeOfMargin.Bottom);
        }

        #endregion
    }
    class InsertCommand : ICommand
    {

        private FrameworkElement _UiElement;
        private Canvas _Container;

        public InsertCommand(FrameworkElement uiElement, Canvas container)
        {
            _UiElement = uiElement;
            _Container = container;
        }

        #region ICommand Members

        public void Execute()
        {
            if (!_Container.Children.Contains(_UiElement))
            {
                _Container.Children.Add(_UiElement);
            }
        }

        public void UnExecute()
        {
            _Container.Children.Remove(_UiElement);
        }

        #endregion
    }
    class DeleteCommand : ICommand
    {

        private FrameworkElement _UiElement;
        private Canvas _Container;

        public DeleteCommand(FrameworkElement uiElement, Canvas container)
        {
            _UiElement = uiElement;
            _Container = container;
        }

        #region ICommand Members

        public void Execute()
        {
            _Container.Children.Remove(_UiElement);
        }

        public void UnExecute()
        {
            _Container.Children.Add(_UiElement);
        }

        #endregion
    }

    #endregion

    #region UndoRedo
    public class UnDoRedo
    {

        private Stack<ICommand> _Undocommands = new Stack<ICommand>();
        private Stack<ICommand> _Redocommands = new Stack<ICommand>();

        public event EventHandler EnableDisableUndoRedoFeature;


        private Canvas _Container;

        public Canvas Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        public void Redo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Redocommands.Count != 0)
                {
                    ICommand command = _Redocommands.Pop();
                    command.Execute();
                    _Undocommands.Push(command);
                }

            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Undocommands.Count != 0)
                {
                    ICommand command = _Undocommands.Pop();
                    command.UnExecute();
                    _Redocommands.Push(command);
                }

            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        #region UndoHelperFunctions

        public void InsertInUnDoRedoForInsert(FrameworkElement ApbOrDevice)
        {
            ICommand cmd = new InsertCommand(ApbOrDevice, Container);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void InsertInUnDoRedoForDelete(FrameworkElement ApbOrDevice)
        {
            ICommand cmd = new DeleteCommand(ApbOrDevice, Container);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void InsertInUnDoRedoForMove(Point margin, FrameworkElement UIelement)
        {
            ICommand cmd = new MoveCommand(new Thickness(margin.X, margin.Y, 0, 0), UIelement);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void InsertInUnDoRedoForResize(Point margin, double width, double height, FrameworkElement UIelement)
        {
            ICommand cmd = new ResizeCommand(new Thickness(margin.X, margin.Y, 0, 0), width, height, UIelement);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        #endregion

        public  bool IsUndoPossible()
        {
            if (_Undocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }             
        }

        public bool IsRedoPossible()
        {

            if (_Redocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }
    }
    #endregion
}
