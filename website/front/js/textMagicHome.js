document.addEventListener('DOMContentLoaded', () => {
    document.getElementById("auroYB").style.transition = "0.2s";
    window.onscroll = function() {scrollFunction()};

    function scrollFunction() {
        
      if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {

        document.getElementById("auroYB").style.fontSize = "10vw";
        
      } else {

        document.getElementById("auroYB").style.fontSize = "12vw";
      }
    }
});