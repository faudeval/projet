#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// Menu affiché lors du lancement
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization
        /// <summary>
        /// Remplissage du menu
        /// </summary>
        public MainMenuScreen()
            : base("Menu Principal")
        {
            // Création des entrées de menu
            MenuEntry playGameMenuEntry = new MenuEntry("Jouer");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Quitter");

            // Ajout des pointeurs de fonction qui seront effectuées lors de la sélection d'une entrée de menu (validation)
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Ajout des entrées de menu au menu
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }
        #endregion

        #region Handle Input
        /// <summary>
        /// Gestionnaire d'évènement lors de la sélection de l'entrée de menu Jouer
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            IsMouseVisible(false);
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        /// <summary>
        /// Gestionnaire d'évènement lors de la sélection de l'entrée de menu Options
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Gestionnaire d'évènement lors de la sélection de l'entrée de menu Quitter :
        /// Demande à l'utilisateur si il est sûr (géré avec un MessageBoxScreen)
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Êtes vous sûr de vouloir quitter?";


            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
            if (!IsMouseVisible())
            {
                IsMouseVisible(true);
                confirmExitMessageBox.Accepted += HideMouse;
                confirmExitMessageBox.Cancelled += HideMouse;
            }

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
