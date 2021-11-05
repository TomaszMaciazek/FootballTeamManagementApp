export function detectBody() {
    if (window.innerWidth < 769) {
        document.body.classList.add('body-small');
    }
    else {
        document.body.classList.remove('body-small');
    }
}

export function goTop() {
    document.body.animate({scrollTop: 0}, 200);
    document.documentElement.animate({scrollTop: 0}, 200);
}