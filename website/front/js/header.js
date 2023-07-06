document.addEventListener('DOMContentLoaded', () => {
    const home = document.querySelector('#home');
    const nav = document.querySelector('nav');
    window.addEventListener('scroll', () => {
        let scrollTop = window.scrollY;
        let homeCenter = home.offsetHeight /2;
        if (scrollTop >= homeCenter) {
            nav.classList.add('scroll');
            
            //home.style.marginTop = `${nav.offsetHeight}px`;
            

        } else {
            nav.classList.remove('scroll');
            //home.style.marginTop = `0px`;
            gotHalf = false;
            cinter = 0;
        }
    });

});
