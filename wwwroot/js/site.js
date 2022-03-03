// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function hideCardOrAccount(obj) {
    let accountNum = document.getElementById("AccountNum");
    let cardNum = document.getElementById("CardNum");

    if (obj.value == "Checking") {
        accountNum.style.display = "none";
        cardNum.style.display = "block";
    }
    if (obj.value == "Saving") {
        cardNum.style.display = "none";
        accountNum.style.display = "block";
    }
}

function gameTimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = 0;
        }

        if (timer === 0) {

            $('#timeoutModal').modal('show');
        }

    }, 1000);
}

window.onload = function () {
    var testseconds = 60,
        display = document.querySelector('#countdown');
    gameTimer(testseconds, display);
};


