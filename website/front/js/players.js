
document.addEventListener('DOMContentLoaded', () => {
    const link = 'http://localhost:12345/api/'


    var xhr = new XMLHttpRequest();
    xhr.open('GET', link + 'players.get', false);
    xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
    xhr.send();
    let playerArray = JSON.parse(xhr.responseText);

    if ('content' in document.createElement('template')) {

       const cardPlace = document.querySelector('.players-grid');
       const card = document.querySelector('#player-card');
       
       for (let i = 0; i < playerArray['data'].length; i++) {
           const textHeader = card.content.querySelector('.card-text-header');
           const textDesc = card.content.querySelector('.card-text');
           const imgPlayer = card.content.querySelector('.card-img-border');
           textHeader.textContent = playerArray['data'][i]['nickname'];
           //imgPlayer.src = xhr.open('GET', link + 'images.get?image_name=' + playerArray['data'][i]['image_name'] + '&image_type=players');
           //xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
           //xhr.send();
           textDesc.textContent = playerArray['data'][i]['description'];
           
           let div = card.content.cloneNode(true);
           
           cardPlace.append(div);
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
    

});