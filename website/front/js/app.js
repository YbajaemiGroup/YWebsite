const sections = document.querySelectorAll("section");
const bubble = document.querySelector(".bubble");
const gradients = [
    "linear-gradient(to right top, #3494E6, #EC6EAD)",
    "linear-gradient(to right top, #DCE35B, #45B649)",
    "linear-gradient(to right top, #c0c0aa, #1cefff)",
    "linear-gradient(to right top, #DBE6F6, #C5796D)"
];

const options = {
    threshold: 0.7
};

let observer = new IntersectionObserver(navCheck, options);

function navCheck(entries) {
    entries.forEach(entry => {
        const className = entry.target.className;
        const activeAnchor = document.querySelector(`[data-page=${className}]`);
        const gradientIndex = entry.target.getAttribute('data-index');
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
            bubble.style.background = gradients[gradientIndex];
        }
    });
}

sections.forEach(section => {
    observer.observe(section);
})