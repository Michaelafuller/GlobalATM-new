// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function hideCardOrAccount(obj)
{
    let accountNum = document.getElementById("AccountNum");
    let cardNum = document.getElementById("CardNum");
    
    if(obj.value == "Checking")
    {
        accountNum.style.display = "none";
        cardNum.style.display = "block";
    }
    if(obj.value == "Saving")
    {
        cardNum.style.display = "none";
        accountNum.style.display = "block";
    }
}

function HideDivfaveColor()
{
    let faveColor = document.getElementById("faveColor");
    let breakFast = document.getElementById("breakFast");

        faveColor.style.display = "none"
        breakFast.style.display = "block"
}

function HideDivbreakFast()
{
    let avgSpeedSwallow = document.getElementById("avgSpeedSwallow");
    let breakFast = document.getElementById("breakFast");


        breakFast.style.display = "none"
        avgSpeedSwallow.style.display = "block"

}

function HideDivavgSpeedSwallow()
{
    let avgSpeedSwallow = document.getElementById("avgSpeedSwallow");
    let bestKPop = document.getElementById("bestKPop");


        avgSpeedSwallow.style.display = "none"
        bestKPop.style.display = "block"

}
