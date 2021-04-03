"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

document.getElementById("sendBtn").disabled = true;
connection.on("ReceiveMessage", function (user, message, date) {
    console.log("IN functie");
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = "[" + date + "] - " + user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("ulmessages").appendChild(li);
    updateScrollMainPage();
});
connection.start().then(function () {
    // document.getElementById("sendBtn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendBtn").addEventListener("click", function (event) {
    var message = document.getElementById("txtmessage").value;
    document.getElementById("txtmessage").value = '';
    document.getElementById("sendBtn").disabled = true;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });    
    event.preventDefault();
});
var input = document.getElementById("txtmessage");

input.addEventListener("keyup", function(event) {
if (event.key  === 'Enter') {
    event.preventDefault();
    document.getElementById("sendBtn").click();
}
});

function updateScrollMainPage(){
    var element = document.getElementById("messagesList");
    element.scrollTop = element.scrollHeight;
}

