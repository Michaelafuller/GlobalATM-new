const currencyOne = document.getElementById("Currency1");
const currencyTwo = document.getElementById("Currency2");
const amountone = document.getElementById("amount1");
const amounttwo = document.getElementById("amount2");
const rate1 = document.getElementById("rate");
// const swap = document.getElementById("select1");

function calculate(){
    const currency_one = currencyOne.value;
    const currency_two = currencyTwo.value;

    fetch(`https://v6.exchangerate-api.com/v6/71aa557ba4fb44bd02d5192f/latest/${currency_one}`)
    .then((res) => res.json())
    .then((data) => {
        const rate = data.conversion_rates[currency_two];
        rate1.innerText = `1 ${currency_one} = ${rate} ${currency_two}`;
        amounttwo.value =(amountone.value * rate).toFixed(2);
    });
}

currencyOne.addEventListener('change', calculate);
amountone.addEventListener('input', calculate);
currencyTwo.addEventListener('change', calculate);
amounttwo.addEventListener('input', calculate);