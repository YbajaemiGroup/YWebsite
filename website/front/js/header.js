document.addEventListener('DOMContentLoaded', () => {
    const home = document.querySelector('#home');
    const nav = document.querySelector('nav');
    var gotHalf = false;
    var cinter = 0;
    window.addEventListener('scroll', () => {
        let scrollTop = window.scrollY;
        let homeCenter = home.offsetHeight /2;
        let homeCenterVisible = home.offsetHeight /4;
        
        if (scrollTop >= homeCenter) {
            gotHalf = true;
            cinter++;
            elem.classList.add('fixed');
            movement(cinter, gotHalf);
            
            //home.style.marginTop = `${nav.offsetHeight}px`;
            

        } else {
            nav.classList.remove('fixed');
            home.style.marginTop = `0px`;
            gotHalf = false;
            cinter = 0;
        }
    });

});

animate({
    duration: 1000,
    timing(timeFraction) {
      return timeFraction;
    },
    draw(progress) {
      elem.style.width = progress * 100 + '%';
    }
  });

function movement(cinter, gotHalf) {
    if (gotHalf == true && cinter == 1) {
        var elem = document.querySelector('nav');
        var pos = -52;
        var id = setInterval(frame, .2);
        
        function frame() {
            if (pos == 0) {
                clearInterval(id);
                
            } else {
                pos++;
                elem.style.top = pos + 'px';
            }
        }
        
    }
}