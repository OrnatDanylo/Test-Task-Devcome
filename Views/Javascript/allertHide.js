
document.addEventListener('DOMContentLoaded', function () {
    var closeButton = document.getElementById("closeButton");
    var modal = document.getElementById("myModal");

    closeButton.addEventListener('click', function () {
        modal.style.display = "none";
    });
});
