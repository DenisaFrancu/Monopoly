"use strict";
//create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/Game").build();
//get current players
var players = new Object();
var players = JSON.parse(document.getElementById('modelToJavascript').value);
var numberOfPlayers = 0;
var playersTurn = 0;
var numberOfDubles = 0;
var pawn;
const timer = ms => new Promise(res => setTimeout(res, ms));
var imageClickEnabled = false;
var expectedPosition;
var clickedProperty = "";

//variables needed for proposal
var _proposedProperty = "";
var _pawnSender = "";
var _pawnReceiver = "";
var _proposedMessage = "";
var _proposedValue;

var pawnsColors = {
    blue:
    {
        background: "#5CBCBD",
        rgb: "rgb(92, 188, 189)"

    },
    red:
    {
        background: "#EB4034",
        rgb: "rgb(236, 64, 67)"
    },
    green:
    {
        background: "#6ACF5D",
        rgb: "rgb(106, 207, 93)"
    },
    yellow:
    {
        background: "#F0F75c",
        rgb: "rgb(240, 247, 92)"
    }
}

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
    numberOfDubles = 0;
    document.getElementById("proposeDeal").hidden = true;
    document.getElementById("proposeDeal").disabled = true;
    document.getElementById("proposeDealValue").style.display = "none";
    document.getElementById("rollDiceBtn").hidden = false;
    document.getElementById("rollDiceBtn").disabled = false;
    document.getElementById("endTurnBtn").hidden = false;
    document.getElementById("endTurnBtn").disabled = true;
    imageClickEnabled = true;
});

connection.on("DisplayRollDices", function(dice1, dice2, pawnSent){
    pawn = pawnSent;
    $("#dice1").attr("src", dice1);
    $("#dice2").attr("src", dice2);
    var dicesSum = parseInt(dice1.charAt(18)) + parseInt(dice2.charAt(18));

    var actualWidth = document.getElementById(pawn).style.marginLeft.slice(0,-2);
    var actualHeight = document.getElementById(pawn).style.marginTop.slice(0,-2);
    var currentPosition;
    for (const [key, value] of Object.entries(properties)) {
        if(`${value.top}` == actualHeight && `${value.left}` == actualWidth){
            currentPosition = parseInt(`${key}`.replace('p',''));
        }
    }
    
    expectedPosition = (currentPosition + dicesSum) % 40;
    expectedPosition = "p" + expectedPosition;
    for (const [key, value] of Object.entries(properties)) {
        if(`${key}` == expectedPosition){
            var expectedWidth = parseInt(`${value.left}`);
            var expectedHeight = parseInt(`${value.top}`);
            var propertyLand = `${value.name}`;
            var propertyOwned = `${value.owned}`;
            var propertyCost = `${value.cost}`;
            var propertyRent = `${value.rent}`;
        }
    }

    if(actualHeight == 90 && actualWidth == 5){
        getOutOfJail(dice1, dice2,pawn);
        //check bankrupcity
    }

    movePawnByDice(pawn, expectedHeight, expectedWidth).then(() => {
        if(dice1 == dice2){
            numberOfDubles++;
            if(numberOfDubles == 3){
                goToJail(pawn).then(() => {
                    reset();
                });
            }else if(expectedWidth == 89 && expectedHeight == 6){
                goToJail(pawn).then(() => {
                    reset();
                });
            }else{
                if(propertyOwned == "false"){
                    document.getElementById("buyProperty").hidden = false;
                    cardClick(propertyLand, getPropertyCost(propertyCost), getPropertyPath(propertyLand));
                    var pawnColor = pawn.replace('~/images/pawns/','');
                    pawnColor = pawnColor.replace('.png','');
                }else{
                    document.getElementById("buyProperty").hidden = true;
                    checkPayRent(properties[expectedPosition].name, propertyRent);
                }
                
                //check bankrupcity
                document.getElementById("rollDiceBtn").disabled = false;
                document.getElementById("endTurnBtn").disabled = true;
            }
        }else if(expectedWidth == 89 && expectedHeight == 6){
            goToJail(pawn).then(() => {
                reset();
            });
        }else{
            if(propertyOwned == "false"){
                document.getElementById("buyProperty").hidden = false;
                cardClick(propertyLand, getPropertyCost(propertyCost), getPropertyPath(propertyLand));
                var pawnColor = pawn.replace('~/images/pawns/','');
                pawnColor = pawnColor.replace('.png','');
            }else{
                document.getElementById("buyProperty").hidden = true;
                checkPayRent(properties[expectedPosition].name, propertyRent);
            }            
            //buy or pay for property
            //check bankrupcity
            reset();
        }
    });   
});

connection.on("DisplayPopup", function(propertyCost, propertyLand) {
    cardClick(propertyLand, getPropertyCost(propertyCost), getPropertyPath(propertyLand));
});

connection.on("BuysProperty", function(color) {
    properties[expectedPosition].owned = true;
    document.getElementById(properties[expectedPosition].name).style.backgroundColor = color;
    payRent(properties[expectedPosition].cost);
    //increase rent if you have more railroads/airports/neighbourhood
    //railroad land
    console.log(expectedPosition);
    increaseRailroadRent(pawn);
    //airport land
    increaseAirportRent(pawn);

});

connection.on("ViewDeal", function(proposedProperty, pawnSender, pawnReceiver, proposedMessage, proposedValue){
    _proposedProperty = proposedProperty;
    _pawnSender = pawnSender;
    _pawnReceiver = pawnReceiver;
    _proposedMessage = proposedMessage;
    _proposedValue = proposedValue;

    document.getElementById("acceptDeal").hidden = false;
    document.getElementById("declineDeal").hidden = false;
    document.getElementById("closePopup").hidden = true;
    document.getElementById("buyProperty").hidden = true;

    openProposalPopup(_proposedProperty, _proposedMessage, getPropertyPath(_proposedProperty));
});

connection.on("ViewProposalResponse", function(proposedProperty, pawnSender, pawnReceiver, proposedMessage, proposedValue){
    _proposedProperty = proposedProperty;
    _pawnSender = pawnSender;
    _pawnReceiver = pawnReceiver;
    _proposedMessage = proposedMessage;
    _proposedValue = proposedValue;

    displayProposalResponse(_proposedProperty, _proposedMessage, getPropertyPath(_proposedProperty));
});

connection.on("SwitchProperty", function(proposedProperty, pawnSender, pawnReceiver, proposedMessage, proposedValue){
    _proposedProperty = proposedProperty;
    _pawnSender = pawnSender;
    _pawnReceiver = pawnReceiver;
    _proposedMessage = proposedMessage;
    _proposedValue = proposedValue;

    var pawnColor = _pawnSender.replace('~/images/pawns/','');
    pawnColor = pawnColor.replace('.png','');
    document.getElementById(_proposedProperty).style.backgroundColor = pawnsColors[pawnColor].background;
    updateMoneyAfterProposal();
    //update railroad/airport/neighbourhood rent
    increaseAirportRent(_pawnSender);
    increaseAirportRent(_pawnReceiver);
    increaseAirportRent(_pawnSender);
    increaseAirportRent(_pawnReceiver);
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

document.getElementById("acceptDeal").addEventListener("click", function(event){
    document.getElementById("acceptDeal").hidden = true;
    document.getElementById("declineDeal").hidden = true;
    document.getElementById("closePopup").hidden = false;
    closePopup();
    var playerName = _pawnReceiver + " name";
    var name = document.getElementById(playerName).innerHTML;
    _proposedMessage = name + " accepted your offer.";

    var playerName = _pawnSender + " name";
    var receiverName = document.getElementById(playerName).innerHTML;

    connection.invoke('ShowResponse',receiverName, _proposedProperty, _pawnSender, _pawnReceiver, _proposedMessage, _proposedValue);
    connection.invoke('ChangeProperty', _proposedProperty, _pawnSender, _pawnReceiver, _proposedMessage, _proposedValue);
});

document.getElementById("declineDeal").addEventListener("click", function(event){
    document.getElementById("acceptDeal").hidden = true;
    document.getElementById("declineDeal").hidden = true;
    document.getElementById("closePopup").hidden = false;
    closePopup();

    var playerName = _pawnReceiver + " name";
    var name = document.getElementById(playerName).innerHTML;
    _proposedMessage = name + " declined your offer.";

    var playerName = _pawnSender + " name";
    var receiverName = document.getElementById(playerName).innerHTML;

    connection.invoke('ShowResponse',receiverName, _proposedProperty, _pawnSender, _pawnReceiver, _proposedMessage, _proposedValue);
});

document.getElementById("proposeDeal").addEventListener("click", function (event) {
    _pawnSender = pawn;
    _proposedValue = document.getElementById("proposeDealValue").value;
    _proposedProperty = clickedProperty;

    var backgroundProposed = document.getElementById(_proposedProperty).style.backgroundColor;
    var receiverColor;
    for (const [key, value] of Object.entries(pawnsColors)) {
        if(`${value.rgb}` == backgroundProposed){
            receiverColor = `${key}`;
        }
    }
    var playerName = pawn + " name";
    var name = document.getElementById(playerName).innerHTML;
    _proposedMessage = name + " offers you " + _proposedValue + "$ for this property.";
    _pawnReceiver = "~/images/pawns/"+ receiverColor +".png";
    var playerName = _pawnReceiver + " name";
    var receiverName = document.getElementById(playerName).innerHTML;

    connection.invoke('ProposeDeal',receiverName, _proposedProperty, _pawnSender, _pawnReceiver, _proposedMessage, _proposedValue);
    closePopup();
});

document.getElementById("buyProperty").addEventListener("click", function (event) {
    var pawnColor = pawn.replace('~/images/pawns/','');
    pawnColor = pawnColor.replace('.png','');
    var color;
    for (const [key, value] of Object.entries(pawnsColors)) {
        if(`${key}` == pawnColor){
            color = `${value.background}`;
        }
    }
    connection.invoke('PlayerBuysProperty',color);
    closePopup();
});

function reset(){
    document.getElementById("rollDiceBtn").disabled = true;
    document.getElementById("endTurnBtn").disabled = false;
    numberOfDubles = 0;
}
function startGame(){
    document.getElementById("rollDiceBtn").hidden = true;
    document.getElementById("endTurnBtn").hidden = true;
    document.getElementById("bankruptcyBtn").hidden = true;
    // document.getElementById("leaveGameBtn").hidden = false;
    document.getElementById("buyProperty").hidden = true;
    document.getElementById("buyHouse").hidden = true;
    document.getElementById("proposeDeal").hidden = true;
    document.getElementById("proposeDeal").disabled = true;
    document.getElementById("proposeDealValue").style.display = "none";
    document.getElementById("acceptDeal").hidden = true;
    document.getElementById("declineDeal").hidden = true;
    imageClickEnabled = false;
}

function nextPlayer(){
    playersTurn++;
    if(playersTurn>=numberOfPlayers){
        playersTurn=0;
    }
    connection.invoke('StartGame',playersTurn);
}

async function movePawnByDice(pawn, expectedHeight, expectedWidth){
    var actualWidth = document.getElementById(pawn).style.marginLeft.slice(0,-2);
    var actualHeight = document.getElementById(pawn).style.marginTop.slice(0,-2);
    while(actualHeight != expectedHeight || actualWidth != expectedWidth){
        await timer(100);
        findPawnMovementDirection(actualHeight, actualWidth, pawn);
        actualWidth = document.getElementById(pawn).style.marginLeft.slice(0,-2);
        actualHeight = document.getElementById(pawn).style.marginTop.slice(0,-2);
        if(actualHeight == 90 && actualWidth == 89){
            getMoneyFromStart(pawn);
        }
    }
}

async function goToJail(pawn){
    var expectedHeight = 90;
    var expectedWidth = 5;
    var actualWidth = document.getElementById(pawn).style.marginLeft.slice(0,-2);
    var actualHeight = document.getElementById(pawn).style.marginTop.slice(0,-2);
    while(actualHeight != expectedHeight || actualWidth != expectedWidth){
        await timer(10);
        findGoToJailMovementPawn(actualHeight, actualWidth, pawn);
        actualWidth = document.getElementById(pawn).style.marginLeft.slice(0,-2);
        actualHeight = document.getElementById(pawn).style.marginTop.slice(0,-2);
    }
}

function findGoToJailMovementPawn(actualHeight, actualWidth, pawn){
    if(actualWidth == 89 && actualHeight >= 6 && actualHeight < 90){
        rotate0(pawn);
    }
    if(actualHeight == 90 && actualWidth >= 5 && actualWidth <= 89){
        moveLeft(pawn);
    }else if(actualWidth == 5 && actualHeight >= 6 && actualHeight <= 90){
        moveDown(pawn);
    }else if(actualHeight == 6 && actualWidth > 5 && actualWidth <= 89){
        moveLeft(pawn);
    }else if(actualWidth == 89 && actualHeight > 6 && actualHeight <= 90){
        moveUp(pawn);
    }
}

function findPawnMovementDirection(actualHeight, actualWidth, pawn){
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

function getOutOfJail(dice1,dice2, pawn){
    var moneyElement = pawn + " money";
    var currentMoney = document.getElementById(moneyElement).innerHTML;
    var updateMoney = parseInt(currentMoney.slice(0,-1));
    if(dice1 == dice2){
        updateMoney = updateMoney - 20;
    }else{
        updateMoney = updateMoney - 50;
    }
    document.getElementById(moneyElement).innerHTML = (updateMoney + "$").toString();
}

function payRent(rent){
    var moneyElement = pawn + " money";
    var currentMoney = document.getElementById(moneyElement).innerHTML;
    var updateMoney = parseInt(currentMoney.slice(0,-1));
    updateMoney = updateMoney - rent;
    document.getElementById(moneyElement).innerHTML = (updateMoney + "$").toString();
}

function increaseRent(rent, player){
    var moneyElement = player + " money";
    var currentMoney = document.getElementById(moneyElement).innerHTML;
    var updateMoney = parseInt(currentMoney.slice(0,-1));
    updateMoney = updateMoney + parseInt(rent);
    document.getElementById(moneyElement).innerHTML = (updateMoney + "$").toString();
}

function getMoneyFromStart(pawn){
    var moneyElement = pawn + " money";
    var currentMoney = document.getElementById(moneyElement).innerHTML;
    var updateMoney = parseInt(currentMoney.slice(0,-1));
    updateMoney = updateMoney + 200;
    document.getElementById(moneyElement).innerHTML = (updateMoney + "$").toString();
}

function cardClick(name, subtitle, picturePath){
    if(imageClickEnabled){
        checkProposalAvailable(name);
        $('#modalTitle').html(name);
        $('#modalSubtitle').html(subtitle);
        $('#modalImage').attr('src', picturePath);
        clickedProperty = name;
        openPopup();
    }
}

function openProposalPopup(name, subtitle, picturePath){
    $('#modalTitle').html(name);
    $('#modalSubtitle').html(subtitle);
    $('#modalImage').attr('src', picturePath);
    clickedProperty = name;
    openPopup();
}

function displayProposalResponse(name, subtitle, picturePath){
    document.getElementById("acceptDeal").hidden = true;
    document.getElementById("declineDeal").hidden = true;
    document.getElementById("closePopup").hidden = false;
    $('#modalTitle').html(name);
    $('#modalSubtitle').html(subtitle);
    $('#modalImage').attr('src', picturePath);
    openPopup();
}

function checkProposalAvailable(propertyClicked){
    var pawnColor = pawn.replace('~/images/pawns/','');
    pawnColor = pawnColor.replace('.png','')
    var color;
    for (const [key, value] of Object.entries(pawnsColors)) {
        if(`${key}` == pawnColor){
            color = `${value.rgb}`;
        }
    }
    if(document.getElementById(propertyClicked).style.backgroundColor != color && document.getElementById(propertyClicked).style.backgroundColor != ""){
        document.getElementById("proposeDeal").hidden = false;
        document.getElementById("proposeDealValue").style.display = "block";
    }
}

function checkPayRent(property, rent){
    var pawnColor = pawn.replace('~/images/pawns/','');
    pawnColor = pawnColor.replace('.png','');
    var color;
    var player;
    for (const [key, value] of Object.entries(pawnsColors)) {
        if(`${key}` == pawnColor){
            color = `${value.rgb}`;
        }
    }
    if(document.getElementById(property).style.backgroundColor != color){
        if(document.getElementById(property).style.backgroundColor == pawnsColors["blue"].rgb ||
            document.getElementById(property).style.backgroundColor == pawnsColors["red"].rgb ||
            document.getElementById(property).style.backgroundColor == pawnsColors["green"].rgb ||
            document.getElementById(property).style.backgroundColor == pawnsColors["yellow"].rgb){
                payRent(rent);
                var propertyBackground = document.getElementById(property).style.backgroundColor;
                for (const [key, value] of Object.entries(pawnsColors)) {
                    if(`${value.rgb}` == propertyBackground){
                        player = `${key}`;
                    }
                }
                increaseRent(rent,"~/images/pawns/" + player + ".png");
            }
    }
}

function openPopup(){
    $('#cardModal').modal('show');
}

function closePopup(){
    $('#cardModal').modal('hide');
    document.getElementById("buyProperty").hidden = true;
    document.getElementById("buyHouse").hidden = true;
    document.getElementById("proposeDeal").hidden = true;
    document.getElementById("proposeDeal").disabled = true;
    document.getElementById("proposeDealValue").style.display = "none";
    document.getElementById("acceptDeal").hidden = true;
    document.getElementById("declineDeal").hidden = true;
}


function getPropertyCost(propertyCost){
    return "Cost: " + propertyCost + "$";
} 

function getPropertyPath(propertyLand){
    return "/images/Payments/" + propertyLand + ".png";
}

function updateMoneyAfterProposal(){
    var moneyElement = _pawnSender + " money";
    var currentMoney = document.getElementById(moneyElement).innerHTML;
    var updateMoney = parseInt(currentMoney.slice(0,-1));
    updateMoney = updateMoney - _proposedValue;
    document.getElementById(moneyElement).innerHTML = (updateMoney + "$").toString();

    moneyElement = _pawnReceiver + " money";
    currentMoney = document.getElementById(moneyElement).innerHTML;
    updateMoney = parseInt(currentMoney.slice(0,-1));
    updateMoney = updateMoney + parseInt(_proposedValue);
    document.getElementById(moneyElement).innerHTML = (updateMoney + "$").toString();
}

function getPawn(pawnPath){
    var pawnColor = pawn.replace('~/images/pawns/','');
    pawnColor = pawnColor.replace('.png','');
    return pawnColor;
}

function increaseRailroadRent(pawnPath){
    var backgroundColorPawn = pawnsColors[getPawn(pawnPath)].rgb;
    var numberOfRailroadsOwned = 0;
    let railroadsPositions = [];
    var rent = 25;

    if(document.getElementById(properties["p5"].name).style.backgroundColor == backgroundColorPawn){
        numberOfRailroadsOwned++;
        rent = rent * numberOfRailroadsOwned;
        railroadsPositions.push("p5");
    }
    if(document.getElementById(properties["p15"].name).style.backgroundColor == backgroundColorPawn){
        numberOfRailroadsOwned++;
        rent = rent * numberOfRailroadsOwned;
        railroadsPositions.push("p15");
    }
    if(document.getElementById(properties["p25"].name).style.backgroundColor == backgroundColorPawn){
        numberOfRailroadsOwned++;
        rent = rent * numberOfRailroadsOwned;
        railroadsPositions.push("p25");
    }
    if(document.getElementById(properties["p35"].name).style.backgroundColor == backgroundColorPawn){
        numberOfRailroadsOwned++;
        rent = rent * numberOfRailroadsOwned;
        railroadsPositions.push("p35");
    }

    railroadsPositions.forEach(function(item){
        console.log(item);
        properties[item].rent = rent;
    });
}

function increaseAirportRent(pawnPath){
    var backgroundColorPawn = pawnsColors[getPawn(pawnPath)].rgb;
    var numberOfAirportssOwned = 0;
    let airportsPositions = [];
    var rent;

    if(document.getElementById(properties["p2"].name).style.backgroundColor == backgroundColorPawn){
        numberOfAirportssOwned++;
        airportsPositions.push("p2");
    }
    if(document.getElementById(properties["p7"].name).style.backgroundColor == backgroundColorPawn){
        numberOfAirportssOwned++;
        airportsPositions.push("p7");
    }
    if(document.getElementById(properties["p17"].name).style.backgroundColor == backgroundColorPawn){
        numberOfAirportssOwned++;
        airportsPositions.push("p17");
    }
    if(document.getElementById(properties["p28"].name).style.backgroundColor == backgroundColorPawn){
        numberOfAirportssOwned++;
        airportsPositions.push("p28");
    }
    if(document.getElementById(properties["p33"].name).style.backgroundColor == backgroundColorPawn){
        numberOfAirportssOwned++;
        airportsPositions.push("p33");
    }
    if(document.getElementById(properties["p36"].name).style.backgroundColor == backgroundColorPawn){
        numberOfAirportssOwned++;
        airportsPositions.push("p36");
    }

    if(numberOfAirportssOwned == 1){
        rent = 25;
    }else if(numberOfAirportssOwned == 2){
        rent = 50;
    }else if(numberOfAirportssOwned == 3){
        rent = 75;
    }else if(numberOfAirportssOwned == 4){
        rent = 100;
    }else if(numberOfAirportssOwned == 5){
        rent = 150;
    }else if(numberOfAirportssOwned == 6){
        rent = 200;
    }

    console.log(rent);
    airportsPositions.forEach(function(item){
        console.log(item);
        properties[item].rent = rent;
    });
}