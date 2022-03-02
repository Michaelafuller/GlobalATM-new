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

