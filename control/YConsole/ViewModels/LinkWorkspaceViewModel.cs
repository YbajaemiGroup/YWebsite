using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YApi;
using YApiModel.Models;
using YConsole.Utillities;

namespace YConsole.ViewModels
{
    public class LinkWorkspaceViewModel : ViewModelBase, IDataLoadable
    {
        #region Bindings

        private ObservableCollection<Player> players = new();

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        private Player? chosenPlayer;

        public Player? ChosenPlayer
        {
            get => chosenPlayer;
            set
            {
                chosenPlayer = value;
                OnPropertyChanged(nameof(ChosenPlayer));
                UpdateLinksOfChosenPlayer();
            }
        }

        private List<Link> links = new();

        private ObservableCollection<Link> _linksOfChosenPlayer = new();

        public ObservableCollection<Link> LinksOfChosenPlayer
        {
            get => _linksOfChosenPlayer;
            set
            {
                _linksOfChosenPlayer = value;
                OnPropertyChanged(nameof(LinksOfChosenPlayer));
            }
        }

        private Link? selectedLink;

        public Link? SelectedLink
        {
            get { return selectedLink; }
            set 
            { 
                selectedLink = value;
                OnPropertyChanged(nameof(SelectedLink));
            }
        }

        #endregion

        public RelayCommand SaveButton { get; private set; }
        public RelayCommand CreateButton { get; private set; }
        public RelayCommand DeleteDialogButton { get; private set; }

        private readonly YApiInteractor _apiInteractor;

        public LinkWorkspaceViewModel(YApiInteractor apiInteractor) 
        { 
            _apiInteractor = apiInteractor;
            SaveButton = new(OnSaveButtonClick);
            CreateButton = new(OnCreateButtonClick);
            DeleteDialogButton = new(OnDeleteDialogButtonClick);
        }

        private void OnCreateButtonClick(object? ignorable)
        {
            if (ChosenPlayer?.Id == null)
            {
                MessageBox.Show("Выберите игрока!");
                return;
            }
            links.Add(new(string.Empty, string.Empty, playerId: ChosenPlayer.Id));
            UpdateLinksOfChosenPlayer();
            OnPropertyChanged(nameof(LinksOfChosenPlayer));
        }

        private async void OnSaveButtonClick(object? ignorable) 
        {
            if (ChosenPlayer?.Id == null)
            {
                MessageBox.Show("Выберите игрока!");
                return;
            }

<<<<<<< HEAD
            var chosenList = dbLinks.Where(l => l.PlayerId == ChosenPlayer.Id);

            /*for (int i = 0; i < dbLinks.Count; i++)
=======
            var linksDeleting = new List<Task>();
            foreach (var link in LinksOfChosenPlayer)
>>>>>>> 4f9e59af71c4ee1b113b4af05a220553f1ed1fe3
            {
                if (link.Id.HasValue)
                {
                    linksDeleting.Add(_apiInteractor.DeleteLinkAsync(link.Id.Value));
                }
<<<<<<< HEAD
            }*/

            foreach (var link in dbLinks.Where(l => l.PlayerId == ChosenPlayer.Id))
            {
                if (link.Id == null)
                {
                    continue;
                }
                await _apiInteractor.DeleteLinkAsync(link.Id ?? 0);
            }


            _ = _apiInteractor.PostLinksAsync(chosenList.ToList());

            dbLinks = await _apiInteractor.GetAllLinksAsync();
=======
            }
            await Task.WhenAll(linksDeleting);

            await _apiInteractor.PostLinksAsync(LinksOfChosenPlayer.ToList());

            links = await _apiInteractor.GetAllLinksAsync();
>>>>>>> 4f9e59af71c4ee1b113b4af05a220553f1ed1fe3
        }

        private void OnDeleteDialogButtonClick(object? ignorable) 
        {  
            throw new NotImplementedException(); 
        }

        public void LoadData()
        {
            try
            {
                Players = new(_apiInteractor.GetAllPlayersAsync().Result);
                links = _apiInteractor.GetAllLinksAsync().Result;
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка при загрузке ссылок и игроков.");
                Players = new();
            }
        }

        public async Task LoadDataAsync()
        {
            try
            {
                Players = new(await _apiInteractor.GetAllPlayersAsync());
                links = await _apiInteractor.GetAllLinksAsync();
                OnPropertyChanged(nameof(LinksOfChosenPlayer));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке ссылок и игроков." + ex.ToString());
                Players = new();
            }
        }

        private void UpdateLinksOfChosenPlayer()
        {
            _linksOfChosenPlayer = new(links.Where(l => l.PlayerId == chosenPlayer?.Id));
            OnPropertyChanged(nameof(LinksOfChosenPlayer));
        }
    }
}
