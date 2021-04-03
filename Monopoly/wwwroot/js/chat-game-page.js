"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
    
document.getElementById("sendToGroupBtn").disabled = true;
connection.on("ReceiveGroupMessage", function (user, message) {
    console.log("IN functie");
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    console.log("funct", msg);
    document.getElementById("ulGroupMessages").appendChild(li);
    updateScroll();
});
connection.start().then(function () {
    connection.invoke('AddToGroup', "Private");
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendToGroupBtn").addEventListener("click", function (event) {
    var message = document.getElementById("txtmessage").value;
    console.log("document.",message);
    document.getElementById("txtmessage").value = '';
    document.getElementById("sendToGroupBtn").disabled = true;
    connection.invoke("SendMessageToGroup", message, "Private").catch(function (err) {
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
