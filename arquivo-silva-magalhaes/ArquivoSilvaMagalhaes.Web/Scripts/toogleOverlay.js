//Função para colocar as fotos em foreground e o resto do site em background com transparencia
function toggleOverlay() {
    var overlay = document.getElementById('overlay');
    var specialBox = document.getElementById('specialBox');
    overlay.style.opacity = .8;
    if (overlay.style.display == "block") {
        overlay.style.display = "none";
        specialBox.style.display = "none";
    } else {
        overlay.style.display = "block";
        specialBox.style.display = "block";
    }
}