using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace AddOns.UI.Implementation
{
    /// <summary>
    /// This class contains methods for displaying messages
    /// to the user, offering various options for the user.
    /// </summary>
    public class UIService
    {
        /// <summary>
        /// Simple class for wrapping an action 
        /// into an Invoke method.
        /// </summary>
        private class ActionWrapper
        {
            private Action _userAction;

            public ActionWrapper(Action userAction)
            {
                _userAction = userAction;
            }

            public void Invoke(IUICommand command)
            {
                _userAction();
            }
        }

        /// <summary>
        /// Present message; user can click on a Button 
        /// with text "Undo", which will undo the action 
        /// leading up to presentation of the message.
        /// </summary>
        /// <param name="message">
        /// Message to user
        /// </param>
        /// <param name="undo">
        /// Undo action, defined by the caller
        /// </param>
        public static async Task PresentMessageWithUndo(string message, Action undo)
        {
            await PresentMessageSingleAction(message, "Undo", new ActionWrapper(undo));
        }

        /// <summary>
        /// Present message; user can only click on a Button 
        /// with the given text, which will close the dialog 
        /// without any further action.
        /// </summary>
        /// <param name="message">
        /// Message to user
        /// </param>
        /// <param name="commandText">
        /// Text on Button control
        /// </param>
        public static async Task PresentMessageNoAction(string message, string commandText)
        {
            await PresentMessageSingleAction(message, commandText, new ActionWrapper(() => { }));
        }

        /// <summary>
        /// Present message; user can click on a Button 
        /// with text "OK", which will invoke the action 
        /// defined by the caller.
        /// </summary>
        /// <param name="message">
        /// Message to user
        /// </param>
        /// <param name="userAction">
        /// Action, defined by the caller
        /// </param>
        public static async Task PresentMessageSingleAction(string message, Action userAction)
        {
            await PresentMessageSingleAction(message, "OK", new ActionWrapper(userAction));
        }

        /// <summary>
        /// Present message; user can click on a Button 
        /// with the given text, which will invoke the 
        /// action defined by the caller, or can click 
        /// "Cancel", which will close the dialog without 
        /// any actions.
        /// </summary>
        /// <param name="message">
        /// Message to user
        /// </param>
        /// <param name="commandText">
        /// Text on Button control
        /// </param>
        /// <param name="userAction">
        /// Action, defined by the caller
        /// </param>
        public static async Task PresentMessageSingleActionCancel(string message, string commandText, ICommand userAction)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand(commandText, userAction.Execute));
            messageDialog.Commands.Add(new UICommand("Cancel", command => { }));
            messageDialog.DefaultCommandIndex = 1;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private static async Task PresentMessageSingleAction(string message, string commandText, ActionWrapper aw)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand(commandText, aw.Invoke));
            await messageDialog.ShowAsync();
        }
    }
}