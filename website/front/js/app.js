document.addEventListener('DOMContentLoaded', () => {
    const sections = document.querySelectorAll("section");
    const bubble = document.querySelector(".bubble");
    const border_color = "rgba(127, 255, 0, 1)";

    const options = {
        threshold: 0.6
    };

    let observer = new IntersectionObserver(navCheck, options);
    
    function navCheck(entries) {
        try {
            entries.forEach(entry => {
                const className = entry.target.className;
                console.log(className);
                const activeAnchor = document.querySelector(`[data-page=${className}]`);
                // const gradientIndex = entry.target.getAttribute('data-index');
                const coords = activeAnchor.getBoundingClientRect();
                const directions = {
                    height: coords.height,
                    width: coords.width,
                    top: coords.top,
                    left: coords.left
                };
                if(entry.isIntersecting) {
                    bubble.style.setProperty('left', `${directions.left}px`);
                    bubble.style.setProperty('top', `${directions.top}px`);
                    bubble.style.setProperty('width', `${directions.width}px`);
                    bubble.style.setProperty('height', `${directions.height}px`);
                    bubble.style.borderColor = border_color;
                
                }
            });
        }
        catch (err) {
        }

    }

    sections.forEach(section => {
        observer.observe(section);
    });
});