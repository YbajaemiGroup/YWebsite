function scrollToTop() {
    window.scroll({
        top: 0, 
        left: 0, 
        behavior: 'smooth'
    });
};

function scrollToGroup() {
    const groupSection = document.querySelector('#groupStage');
    var rect1 = groupSection.getBoundingClientRect();
    console.log(rect1.top);
    window.scroll({
        top: rect1.top + window.scrollY, 
        left: 0, 
        behavior: 'smooth'
    });
};

function scrollToPlayoff() {
    const playoffSection = document.querySelector('#playOff');
    var rect2 = playoffSection.getBoundingClientRect();
    console.log(rect2.top);
    window.scroll({
        top: rect2.top + window.scrollY, 
        left: 0, 
        behavior: 'smooth'
    });
};

function scrollToPlayers() {
    const playersSection = document.querySelector('#players');
    var rect3 = playersSection.getBoundingClientRect();
    console.log(rect3.top);
    window.scroll({
        top: rect3.top + window.scrollY, 
        left: 0, 
        behavior: 'smooth'
    });
};

