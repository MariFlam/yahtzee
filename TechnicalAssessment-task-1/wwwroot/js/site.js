// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

const uri = '/api/Yahtzee'
var rollNumber = 0;

var dicehand = new Array;


function saveDice(el, i) {
    el.remove();
    var div = document.getElementById("on-hand")
    div.appendChild(getImg(i));

    dicehand.push(i);
}

function ping() {
    fetch(uri)
        .then(response => response.text())
        .then(data => console.log(data));
}

function roll() {
    
    rollNumber++;
    fetch(uri + "/roll/" + (5 - dicehand.length))
        .then(response => response.json())
        .then(data => displayResults(data, rollNumber));
}

function getResult() {
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dicehand)
    })
        .then(response => response.json())
        .then(data => {
            displayScore(data)
        })
}

function displayResults(roll, i) {
    if (i === 1) {
        var div = document.getElementById("first-roll")
        roll.forEach((i => {
            div.appendChild(getImg(i));
        }));
    }
    else if (i === 2) {
        var div = document.getElementById("second-roll")
        roll.forEach((i => {
            div.appendChild(getImg(i));
        }));
    }
    else if (i === 3) {
        var div = document.getElementById("third-roll")
        roll.forEach((i => {
            div.appendChild(getImg(i));
        }));
    }
}

function displayScore(i) {
    var span = document.createElement("span");
    span.textContent = i;

    document.getElementById("score").appendChild(span);
}

function getImg(i) {
    var img = document.createElement("img");
    img.setAttribute("roll-nr", i);
    img.className = "dice";
    img.height = 60;
    img.width = 60;
    switch (i) {
        case 1:
            img.src = "../imgs/1024px-Dice-1.svg.png";
            break;
        case 2:
            img.src = "../imgs/800px-Dice-2.svg.png";
            break;
        case 3:
            img.src = "../imgs/800px-Dice-3.svg.png";
            break;
        case 4:
            img.src = "../imgs/800px-Dice-4.svg.png";
            break;
        case 5:
            img.src = "../imgs/800px-Dice-5.svg.png";
            break;
        case 6:
            img.src = "../imgs/Dice-6a.svg.png";
            break;
    }
    img.setAttribute("onclick", "saveDice(this," + i + ")");
    return img;

}