document.addEventListener('DOMContentLoaded', () => {

    if ('content' in document.createElement('template')) {
        
        //чето как-то получить какие-то данные от бд тут надо..
        let nickArray = ['Yatorogod', 'YaToGoRot', 'devokius', 'doradura'];
        let descArray = ['Смешной', 'казах', 'test', 'test'];
        let imgArray = ['https://gas-kvas.com/uploads/posts/2023-01/1673330719_gas-kvas-com-p-anime-slyuni-risunki-55.jpg','https://damion.club/uploads/posts/2022-09/1664303733_26-damion-club-p-akhegao-art-vkontakte-37.jpg', 'https://sun9-70.userapi.com/impf/c830108/v830108146/1ba4fe/lPuN2iDziXw.jpg?size=915x922&quality=96&sign=cb1fea6e74fb191dd0b53e11c96b8781&c_uniq_tag=c5uv2f8-W5PGqkWkHN2co7SGzg_kIf_ChLB12UmA2F0&type=album', 'https://phonoteka.org/uploads/posts/2022-09/1664068292_65-phonoteka-org-p-akhegao-manga-oboi-vkontakte-69.jpg']
        const cardPlace = document.querySelector('.players-grid');
        const card = document.querySelector('#player-card');
        
        for (let i = 0; i < nickArray.length; i++) {
            const textHeader = card.content.querySelector('.card-text-header');
            const textDesc = card.content.querySelector('.card-text');
            const imgPlayer = card.content.querySelector('.card-img-border');
            textHeader.textContent = nickArray[i];

            if (imgArray.length - i <= 0) {
                imgPlayer.src = 'http://via.placeholder.com/150x150';
            } else {
                imgPlayer.src = imgArray[i];
            }

            if (descArray.length - i <= 0) {
                textDesc.textContent = 'Нет описания';
            } else {
                textDesc.textContent = descArray[i]
            }
            
            let div = card.content.cloneNode(true);
            
            cardPlace.append(div);
        }
    } else {
        console.log('govno..')
    }
});