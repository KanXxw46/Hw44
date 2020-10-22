using System;

namespace Hw444
{
    internal class ConfirmationWindow
    {
        private MainWindow mainWindow;
        private object confirmationCode;

        public ConfirmationWindow(MainWindow mainWindow, object confirmationCode)
        {
            this.mainWindow = mainWindow;
            this.confirmationCode = confirmationCode;
        }

        internal bool? ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}