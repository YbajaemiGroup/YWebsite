document.addEventListener('DOMContentLoaded', () => {
    const link = 'http://localhost:12345/api/'

    var xhr = new XMLHttpRequest();
    var xhrLinks = new XMLHttpRequest();
    var xhrImages = new XMLHttpRequest();
    var xhrBracket = new XMLHttpRequest();
    var xhrPlayer = new XMLHttpRequest();
    xhr.open('GET', link + 'players.get', false);
    xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
    xhr.send();
    let playerArray = JSON.parse(xhr.responseText);

    if ('content' in document.createElement('template')) {
        const cardTemplate = document.querySelector('#player-card');
        const cardPlace = document.querySelector('.players-grid');

        for (let i = 0; i < playerArray['data'].length; i++) {
            const cardClone = cardTemplate.content.cloneNode(true);
            const card = cardClone.querySelector('.player-card');
            const textHeader = card.querySelector('.card-text-header');
            const textDesc = card.querySelector('.card-text');
            const imgPlayer = card.querySelector('.card-img-border');

            xhrImages.open('GET', link + 'images.get?image_name=' + playerArray['data'][i]['image_name'] + '&image_type=players', false);
            xhrImages.setRequestHeader('Access-Control-Allow-Origin', '*');
            xhrImages.send();
            textHeader.textContent = playerArray['data'][i]['nickname'];
            textDesc.textContent = playerArray['data'][i]['description'];

            if (xhrImages.responseText.includes('error')) {
                imgPlayer.src = 'images/placeholder.png';
            }
            else {
                imgPlayer.src = xhrImages.responseURL;
            }
            
            
            xhrLinks.open('GET', link + 'links.get?player_id=' + playerArray['data'][i]['id'], false);
            xhrLinks.setRequestHeader('Access-Control-Allow-Origin', '*');
            xhrLinks.send();
            let linksPlayer = JSON.parse(xhrLinks.responseText);
            
            for (let j = 0; j < linksPlayer['data'].length; j++) {
                var linka = linksPlayer['data'][j]['url'];
                let imgElement;
                
                if (linka.includes('vk.com')) {
                    imgElement = card.querySelector('#img-vk');
                } 
                else if (linka.includes('twitch.tv')) {
                    imgElement = card.querySelector('#img-twitch');
                } 
                else if (linka.includes('steamcommunity.com')) {
                    imgElement = card.querySelector('#img-steam');
                } 
                else if (linka.includes('youtube.com') || linka.includes('youtu.be')) {
                    imgElement = card.querySelector('#img-youtube');
                }

                if (imgElement) {
                    imgElement.style.display = 'inline';
                    imgElement.href = linka;
                }
            }

            cardPlace.appendChild(cardClone);
        }
    } else {
        console.log('Error loading data..');
    }

    var players1group = [];
    var players2group = [];
    var players3group = [];
    var players4group = [];
    for (let i = 0; i < playerArray['data'].length; i++) {
        
        if (playerArray['data'][i]['group_number'] == '1') {
            players1group.push(playerArray['data'][i]);
        }
        if (playerArray['data'][i]['group_number'] == '2') {
            players2group.push(playerArray['data'][i]);
        }
        if (playerArray['data'][i]['group_number'] == '3') {
            players3group.push(playerArray['data'][i]);
        }
        if (playerArray['data'][i]['group_number'] == '4') {
            players4group.push(playerArray['data'][i]);
        }
    }

    for (let i = 0; i <= 4; i++) {
        if (players1group[i] != undefined) {
            let groupPlayer = document.querySelector('#group-1-player-' + (i+1));
            let textPlayer = groupPlayer.querySelector('[data-th="Player"]');
            let textWin = groupPlayer.querySelector("[data-th='Win']");
            let textLose = groupPlayer.querySelector("[data-th='Lose']");
            let textScore = groupPlayer.querySelector("[data-th='Score']");

            textPlayer.textContent = players1group[i]['nickname'];
            textWin.textContent = players1group[i]['won'];
            textLose.textContent = players1group[i]['lose'];
            textScore.textContent = players1group[i]['points'];
        }
        

        if (players2group[i] != undefined) {
            groupPlayer = document.querySelector('#group-2-player-' + (i+1));
            textPlayer = groupPlayer.querySelector("[data-th='Player']");
            textWin = groupPlayer.querySelector("[data-th='Win']");
            textLose = groupPlayer.querySelector("[data-th='Lose']");
            textScore = groupPlayer.querySelector("[data-th='Score']");

            textPlayer.textContent = players2group[i]['nickname'];
            textWin.textContent = players2group[i]['won'];
            textLose.textContent = players2group[i]['lose'];
            textScore.textContent = players2group[i]['points'];
        }

        if (players3group[i] != undefined) {
            groupPlayer = document.querySelector('#group-3-player-' + (i+1));
            textPlayer = groupPlayer.querySelector("[data-th='Player']");
            textWin = groupPlayer.querySelector("[data-th='Win']");
            textLose = groupPlayer.querySelector("[data-th='Lose']");
            textScore = groupPlayer.querySelector("[data-th='Score']");

            textPlayer.textContent = players3group[i]['nickname'];
            textWin.textContent = players3group[i]['won'];
            textLose.textContent = players3group[i]['lose'];
            textScore.textContent = players3group[i]['points'];
        }

        if (players4group[i] != undefined) {
            groupPlayer = document.querySelector('#group-4-player-' + (i+1));
            textPlayer = groupPlayer.querySelector("[data-th='Player']");
            textWin = groupPlayer.querySelector("[data-th='Win']");
            textLose = groupPlayer.querySelector("[data-th='Lose']");
            textScore = groupPlayer.querySelector("[data-th='Score']");

            textPlayer.textContent = players4group[i]['nickname'];
            textWin.textContent = players4group[i]['won'];
            textLose.textContent = players4group[i]['lose'];
            textScore.textContent = players4group[i]['points'];
        }
    }

    xhrBracket.open('GET', link + 'bracket.updates.get', false);
    xhrBracket.setRequestHeader('Access-Control-Allow-Origin', '*');
    xhrBracket.send();
    let brackets = JSON.parse(xhrBracket.responseText);

    for (let i = 0; i < brackets['data'].length; i++) {
        for (let j = 0; j < brackets['data'][i]['games'].length; j++) {
            if (brackets['data'][i]['games'][j]['is_upper'] == true) {
                let tournamentRound = document.querySelector('#round'+ (brackets['data'][i]['round_number']) +'-upper-' + (brackets['data'][i]['games'][j]['row']));
                let textPlayer_1 = tournamentRound.querySelector('#player-pos-1');
                let textPlayer_2 = tournamentRound.querySelector('#player-pos-2');
                if (brackets['data'][i]['games'][j]['player1'] != null) {
                    xhrPlayer.open('GET', link + 'players.get?player_id=' + brackets['data'][i]['games'][j]['player1'], false);
                    xhrPlayer.setRequestHeader('Access-Control-Allow-Origin', '*');
                    xhrPlayer.send();
                    let textOfPlayer = JSON.parse(xhrPlayer.responseText);

                    textPlayer_1.textContent = textOfPlayer['data']['nickname'];
                }
                
                if (brackets['data'][i]['games'][j]['player2'] != null) {
                    xhrPlayer.open('GET', link + 'players.get?player_id=' + brackets['data'][i]['games'][j]['player2'], false);
                    xhrPlayer.setRequestHeader('Access-Control-Allow-Origin', '*');
                    xhrPlayer.send();
                    textOfPlayer = JSON.parse(xhrPlayer.responseText);

                    textPlayer_2.textContent = textOfPlayer['data']['nickname'];
                }
                
            }
        }
    }

    for (let i = 0; i < brackets['data'].length; i++) { 
        for (let j = 0; j < brackets['data'][i]['games'].length; j++) {
            if (brackets['data'][i]['games'][j]['is_upper'] == false) {
                let tournamentRound = document.querySelector('#round'+ (brackets['data'][i]['round_number']) +'-' + (brackets['data'][i]['games'][j]['row']));
                let textPlayer_1 = tournamentRound.querySelector('#player-pos-1');
                let textPlayer_2 = tournamentRound.querySelector('#player-pos-2');
                if (brackets['data'][i]['games'][j]['player1'] != null) {
                    xhrPlayer.open('GET', link + 'players.get?player_id=' + brackets['data'][i]['games'][j]['player1'], false);
                    xhrPlayer.setRequestHeader('Access-Control-Allow-Origin', '*');
                    xhrPlayer.send();
                    let textOfPlayer = JSON.parse(xhrPlayer.responseText);
                    console.log(textOfPlayer['data']['nickname']);
                    textPlayer_1.textContent = textOfPlayer['data']['nickname'];
                }
                
                if (brackets['data'][i]['games'][j]['player2'] != null) {
                    xhrPlayer.open('GET', link + 'players.get?player_id=' + brackets['data'][i]['games'][j]['player2'], false);
                    xhrPlayer.setRequestHeader('Access-Control-Allow-Origin', '*');
                    xhrPlayer.send();
                    textOfPlayer = JSON.parse(xhrPlayer.responseText);
                    console.log(textOfPlayer['data']['nickname']);
                    textPlayer_2.textContent = textOfPlayer['data']['nickname'];
                }
                textPlayer_1 = '';
                textPlayer_2 = '';
            }
        }
    }
});