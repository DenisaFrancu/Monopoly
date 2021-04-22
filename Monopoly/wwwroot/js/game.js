"use strict";

//create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/Game").build();
//get current players
var players = new Object();
var players = JSON.parse(document.getElementById('modelToJavascript').value);
var numberOfPlayers = 0;
var playersTurn = 0;
var dicesSum = 0;

connection.start().then(function () {
    connection.invoke('AddToGroup');
    players.forEach(element => {
        numberOfPlayers++;
    });
    startGame();
    connection.invoke('StartGame',playersTurn);
}).catch(function (err) {
    return console.error(err.toString());
});
    
// chatting options
document.getElementById("sendToGroupBtn").disabled = true;
connection.on("ReceiveGroupMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("ulGroupMessages").appendChild(li);
    updateScroll();
});

document.getElementById("sendToGroupBtn").addEventListener("click", function (event) {
    var message = document.getElementById("txtmessage").value;
    document.getElementById("txtmessage").value = '';
    document.getElementById("sendToGroupBtn").disabled = true;
    connection.invoke("SendMessageToGroup", message).catch(function (err) {
        return console.error(err.toString());
    });    
    event.preventDefault();
});

var input = document.getElementById("txtmessage");
input.addEventListener("keyup", function(event) {
if (event.key  === 'Enter') {
    event.preventDefault();
    document.getElementById("sendToGroupBtn").click();
}
});

function updateScroll(){
    var element = document.getElementById("messagesList");
    element.scrollTop = element.scrollHeight;
}

//game functionality
connection.on("PlayGame", function (turn) {
    playersTurn=turn;
    document.getElementById("rollDiceBtn").hidden = false;
    document.getElementById("rollDiceBtn").disabled = false;
    document.getElementById("endTurnBtn").hidden = false;
    //here it's players turn

    //end turn here
    //nextPlayer();
});

connection.on("DisplayRollDices", function(dice1, dice2, pawn){
    $("#dice1").attr("src", dice1);
    $("#dice2").attr("src", dice2);
    dicesSum = parseInt(dice1.charAt(18)) + parseInt(dice2.charAt(18));
    movePawn(pawn, 0, dicesSum*8);
});

document.getElementById("rollDiceBtn").addEventListener("click", function (event) {
    $.ajax({
        url: 'RollDice',
        type: 'GET',
        contentType: 'application/json',
        success: function(data) {
            connection.invoke("RollDices", data[0], data[1]);
        }
        }
    );
    
    document.getElementById("rollDiceBtn").disabled = true;
    event.preventDefault();
});

document.getElementById("endTurnBtn").addEventListener("click", function (event) {
    startGame();
    nextPlayer();
    event.preventDefault();
});

function startGame(){
    document.getElementById("rollDiceBtn").hidden = true;
    document.getElementById("endTurnBtn").hidden = true;
    document.getElementById("bankruptcyBtn").hidden = true;
    document.getElementById("leaveGameBtn").hidden = false;
}

function nextPlayer(){
    playersTurn++;
    if(playersTurn>=numberOfPlayers){
        playersTurn=0;
    }
    connection.invoke('StartGame',playersTurn);
}

function movePawn(pawn, i, numberOfMoves) {
    var actualWidth = document.getElementById(pawn).style.marginLeft.slice(0,-2);
    var actualHeight = document.getElementById(pawn).style.marginTop.slice(0,-2);
    setTimeout(function() {
        i++;
        findMovementDirection(actualHeight, actualWidth);        
        if (i < numberOfMoves) {
            movePawn(pawn, i, numberOfMoves);
        }
    }, 100);
}

function findMovementDirection(actualHeight, actualWidth){
    if((actualWidth == 89 && actualHeight == 90) || (actualWidth == 5 && actualHeight == 90) || (actualWidth == 5 && actualHeight == 6) ||
        (actualWidth == 89 && actualHeight == 6) || (actualWidth == 89 && actualHeight == 87) || (actualWidth == 8 && actualHeight == 90) ||
        (actualWidth == 5 && actualHeight == 9) || (actualWidth == 86 && actualHeight == 6)){
            numberOfMoves+=2;
        }
        if(actualHeight == 90 && actualWidth == 5){
            rotate90(pawn);
        }
        if(actualHeight == 6 && actualWidth == 5){
            rotate0(pawn);
        }
        if(actualHeight == 6 && actualWidth == 89){
            rotate270(pawn);
        }
        if(actualHeight == 90 && actualWidth == 89){
            rotate0(pawn);
        }
        if(actualHeight == 90 && actualWidth <= 89 && actualWidth > 5){
            moveLeft(pawn);
        }else if(actualWidth == 5 && actualHeight <= 90 && actualHeight >6){
            moveUp(pawn);
        }else if(actualHeight == 6 && actualWidth >= 5 && actualWidth < 89){
            moveRight(pawn);
        }else if(actualWidth == 89 && actualHeight >= 6 && actualHeight < 90){
            moveDown(pawn);
        }
}

function rotate90(pawn){
    document.getElementById(pawn).style.transform = "rotate(90deg)";
}

function rotate270(pawn){
    document.getElementById(pawn).style.transform = "rotate(270deg)";
}

function rotate0(pawn){
    document.getElementById(pawn).style.transform = "rotate(0deg)";
}

function moveLeft(pawn){
    var actualWidth = parseInt(document.getElementById(pawn).style.marginLeft.slice(0,-2));
    var newWidth = actualWidth-1;
    newWidth = newWidth + "vh";
    document.getElementById(pawn).style.marginLeft = newWidth;
}

function moveUp(pawn){
    var actualHeight = parseInt(document.getElementById(pawn).style.marginTop.slice(0,-2));
    var newHeight = actualHeight-1;
    newHeight = newHeight + "vh";
    document.getElementById(pawn).style.marginTop = newHeight;
}

function moveRight(pawn){
    var actualWidth = parseInt(document.getElementById(pawn).style.marginLeft.slice(0,-2));
    var newWidth = actualWidth+1;
    newWidth = newWidth + "vh";
    document.getElementById(pawn).style.marginLeft = newWidth;
}

function moveDown(pawn){
    var actualHeight = parseInt(document.getElementById(pawn).style.marginTop.slice(0,-2));
    var newHeight = actualHeight+1;
    newHeight = newHeight + "vh";
    document.getElementById(pawn).style.marginTop = newHeight;
}