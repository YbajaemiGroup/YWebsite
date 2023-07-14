using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YApi;
using YApiModel.Models;
using YConsole.Utillities;

namespace YConsole.ViewModels
{
    public class LinkWorkspaceViewModel : ViewModelBase, IDataLoadable
    {

        private const string DEFAULT_VALUE = "??";

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
                _saved = true;
                Id = chosenPlayer?.Id ?? 0;
                Nickname = chosenPlayer?.NickName ?? DEFAULT_VALUE;
                OnPropertyChanged(nameof(Links));
            }
        }

        public int Id
        {
            get => ChosenPlayer?.Id ?? 0;
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Id)
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Nickname
        {
            get => ChosenPlayer?.NickName ?? "??";
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.NickName)
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.NickName = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }

        private List<Link> dbLinks = new List<Link>();

        private ObservableCollection<Link> links = new();

        public ObservableCollection<Link> Links
        {
            get 
            { 
               return ChosenPlayer?.Id == null ? new(dbLinks) : new(dbLinks.Where(l => l.PlayerId == ChosenPlayer.Id));     
            }
        }

        private Link? selectedLink;

        public Link? SelectedLink
        {
            get { return selectedLink; }
            set 
            { 
                selectedLink = value; 
                PlayerId = selectedLink?.PlayerId ?? 0;
                LinkUrl = selectedLink?.LinkUrl ?? string.Empty;
                DescriptionOfUrl = selectedLink?.Description ?? string.Empty;
            }
        }
        public int PlayerId
        {
            get => SelectedLink?.PlayerId ?? 0;
            set
            {
                SelectedLink.Id = value;
                OnPropertyChanged(nameof(PlayerId));
            }
        }
        public string LinkUrl 
        { 
            get => SelectedLink?.LinkUrl ?? string.Empty; 
            set
            {
                SelectedLink.LinkUrl = value;
                OnPropertyChanged(nameof(LinkUrl));
            } 
        }
        public string DescriptionOfUrl
        {
            get => selectedLink?.Description ?? string.Empty;
            set
            {
                SelectedLink.Description = value; 
                OnPropertyChanged(nameof(DescriptionOfUrl));
            }
        }


        public string Status { get => _saved ? "Сохранено" : "Не сохранено"; }
        #endregion

        public RelayCommand SaveButton { get; private set; }
        public RelayCommand CreateButton { get; private set; }
        public RelayCommand DeleteDialogButton { get; private set; }

        
        private bool _saved = true;
        private readonly YApiInteractor _apiInteractor;
        private readonly IWindowService _windowService;

        public LinkWorkspaceViewModel(YApiInteractor apiInteractor, IWindowService windowService) 
        { 
            _apiInteractor = apiInteractor;
            _windowService = windowService;
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

            Link createdLinkInstance = new Link("", "", null, ChosenPlayer.Id);
            dbLinks.Add(createdLinkInstance);

            OnPropertyChanged(nameof(Links));

        }
        private async void OnSaveButtonClick(object? ignorable) 
        {
            if (ChosenPlayer?.Id == null)
            {
                MessageBox.Show("Выберите игрока!");
                return;
            }

            var chosenList = dbLinks.Where(l => l.PlayerId == ChosenPlayer.Id);

            for (int i = 0; i < dbLinks.Count; i++)
            {
                if (dbLinks[i].Id == null)
                {
                    continue;
                }

                if (dbLinks[i].PlayerId == ChosenPlayer.Id)
                {
                   await _apiInteractor.DeleteLinkAsync(dbLinks[i].Id ?? 0);
                }
            }

            /*foreach (var link in dbLinks.Where(l => l.PlayerId == ChosenPlayer.Id)) 
            {
                if (link.Id == null)
                {
                    continue;
                }
                await _apiInteractor.DeleteLinkAsync(link.Id ?? 0);

                Console.WriteLine("Ссылка с ID: " + link.Id);
            }*/


            _ = _apiInteractor.PostLinksAsync(chosenList.ToList());

            dbLinks = await _apiInteractor.GetAllLinksAsync();
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
                
                dbLinks = _apiInteractor.GetAllLinksAsync().Result;
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
                
                dbLinks = await _apiInteractor.GetAllLinksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке ссылок и игроков." + ex.ToString());
                Players = new();
            }
        }

    }
}
