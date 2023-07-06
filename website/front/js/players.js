
document.addEventListener('DOMContentLoaded', () => {
    const link = 'http://localhost:8181/api/'


    var xhr = new XMLHttpRequest();
       xhr.open('GET', link + 'players.get', false);
       xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
       xhr.send();
       let playerArray = xhr.responseText;


    if ('content' in document.createElement('template')) {
       
       console.log(playerArray)
       const cardPlace = document.querySelector('.players-grid');
       const card = document.querySelector('#player-card');
       
       for (let i = 0; i < playerArray.length; i++) {
           const textHeader = card.content.querySelector('.card-text-header');
           const textDesc = card.content.querySelector('.card-text');
           const imgPlayer = card.content.querySelector('.card-img-border');
           textHeader.textContent = playerArray[i]['nickname'];
           imgPlayer.src = xhr.open('GET', link + 'images.get?image_name=' + playerArray[i]['image_name'] + '&image_type=players');
           textDesc.textContent = playerArray[i]['description'];
           
           let div = card.content.cloneNode(true);
           
           cardPlace.append(div);
       }
    } else {
       console.log('Error loading data..')
    }
    const players1group = [];
    const players2group = [];
    const players3group = [];
    const players4group = [];
    for (let i = 0; i < playerArray.length; i++) {
       if (playerArray[i]['group_number'] == 1) {
           players1group.append(playerArray[i]);
       }
       if (playerArray[i]['group_number'] == 2) {
           players2group.append(playerArray[i]);
       }
       if (playerArray[i]['group_number'] == 3) {
           players3group.append(playerArray[i]);
       }
       if (playerArray[i]['group_number'] == 4) {
           players4group.append(playerArray[i]);
       }
    }

    for (let i = 1; i <= 4; i++) {
        let groupPlayer = document.querySelector('#group-' + '1' + '-player-' + i);
        let textPlayer = groupPlayer.querySelector("[data-th='Player']");
        let textWin = groupPlayer.querySelector("[data-th='Win']");
        let textLose = groupPlayer.querySelector("[data-th='Lose']");
        let textScore = groupPlayer.querySelector("[data-th='Score']");

        textPlayer.textContent = 'ABOBA';
        textPlayer.textContent = players1group[i]['nickname'];
        textWin.textContent = players1group[i]['won'];
        textLose.textContent = players1group[i]['lose'];
        textScore.textContent = players1group[i]['points'];


        groupPlayer = document.querySelector('#group-' + '2' + '-player-' + i);
        textPlayer = groupPlayer.querySelector("[data-th='Player']");
        textWin = groupPlayer.querySelector("[data-th='Win']");
        textLose = groupPlayer.querySelector("[data-th='Lose']");
        textScore = groupPlayer.querySelector("[data-th='Score']");

        textPlayer.textContent = 'BBOBA';
        textPlayer.textContent = players2group[i]['nickname'];
        textWin.textContent = players2group[i]['won'];
        textLose.textContent = players2group[i]['lose'];
        textScore.textContent = players2group[i]['points'];


        groupPlayer = document.querySelector('#group-' + '3' + '-player-' + i);
        textPlayer = groupPlayer.querySelector("[data-th='Player']");
        textWin = groupPlayer.querySelector("[data-th='Win']");
        textLose = groupPlayer.querySelector("[data-th='Lose']");
        textScore = groupPlayer.querySelector("[data-th='Score']");

        textPlayer.textContent = 'CBOBA';
        textPlayer.textContent = players3group[i]['nickname'];
        textWin.textContent = players3group[i]['won'];
        textLose.textContent = players3group[i]['lose'];
        textScore.textContent = players3group[i]['points'];


        groupPlayer = document.querySelector('#group-' + '4' + '-player-' + i);
        textPlayer = groupPlayer.querySelector("[data-th='Player']");
        textWin = groupPlayer.querySelector("[data-th='Win']");
        textLose = groupPlayer.querySelector("[data-th='Lose']");
        textScore = groupPlayer.querySelector("[data-th='Score']");

        textPlayer.textContent = 'GBOBA';
        textPlayer.textContent = players4group[i]['nickname'];
        textWin.textContent = players4group[i]['won'];
        textLose.textContent = players4group[i]['lose'];
        textScore.textContent = players4group[i]['points'];
    }
    

});